using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Hosting;
using System.Xml.Linq;

namespace WcfHotelService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class Service1 : IService1
    {
        // Given username, fetch all the hotels they have previously rated
        public List<RatedHotel> GetRatedHotels(string username)
        {
            List<RatedHotel> ratedHotels = new List<RatedHotel>();

            try
            {
                string membersPath = HostingEnvironment.MapPath("~/App_Data/Members.xml");
                string hotelsPath = HostingEnvironment.MapPath("~/App_Data/Hotels.xml");

                XDocument members = XDocument.Load(membersPath);
                XDocument hotels = XDocument.Load(hotelsPath);


                // Find the member with passed in user name

                var allMembers = members.Descendants("Member");
                XElement member = null;

                foreach (var m in allMembers)
                {
                    var tempUsername = m.Element("Username");

                    // found member with matching username
                    if (tempUsername != null && tempUsername.Value == username)
                    {
                        member = m;
                        break;
                    }
                }

                // if no match after searching, I'll set a special value in List
                if (member == null)
                {
                    // set the id to -1, and add it in list to indicate invalid username
                    RatedHotel invalid = new RatedHotel();
                    invalid.HotelID = "-1";
                    ratedHotels.Add(invalid);
                    return ratedHotels;
                }


                // otherwise, assuming that we found the member...
                var ratings = member.Descendants("RatedHotel");

                // now go through all the ratings (user can rate multiple hotels)
                foreach (var rating in ratings)
                {
                    // null protection with ?, if not null, get the value
                    string hotelID = rating.Element("HotelID")?.Value;

                    // now find the matching hotel
                    var allHotels = hotels.Descendants("Hotel");
                    XElement currHotel = null;

                    foreach (var h in allHotels)
                    {
                        var tempHotelID = h.Attribute("ID");

                        if (tempHotelID.Value != null && tempHotelID.Value == hotelID)
                        {
                            currHotel = h;
                            break;
                        }
                    }

                    if (currHotel != null)
                    {
                        // format the address first
                        var addressElement = currHotel.Element("Address");

                        Address currHotelAddress = null;
                        if (addressElement != null)
                        {
                            currHotelAddress = new Address
                            {
                                Number = addressElement.Element("Number").Value,
                                Street = addressElement.Element("Street").Value,
                                City = addressElement.Element("City").Value,
                                State = addressElement.Element("State").Value,
                                Zip = addressElement.Element("Zip").Value
                            };
                        }
                        
                        
                        ratedHotels.Add(new RatedHotel
                        {
                            HotelID = currHotel.Attribute("ID").Value,
                            HotelName = currHotel.Element("Name").Value,
                            Rating = float.Parse(rating.Element("Rating")?.Value ?? "0.0"),
                            Comment = rating.Element("Comment")?.Value ?? "No comment",
                            HotelAddress = currHotelAddress
                        });
                    }


                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return ratedHotels ;
        }

        public bool AddHotelRating(string username, string hotelID, float rating, string comment)
        {
            try
            {
                string membersPath = HostingEnvironment.MapPath("~/App_Data/Members.xml");
                XDocument members = XDocument.Load(membersPath);

                // get matching member
                var allMembers = members.Descendants("Member");
                XElement member = null;

                foreach (var m in allMembers)
                {
                    var tempUsername = m.Element("Username");

                    // found member with matching username
                    if (tempUsername != null && tempUsername.Value == username)
                    {
                        member = m;
                        break;
                    }
                }

                // if no match after searching, I'll throw an exception (two invalid possiblities, so throw exception)
                if (member == null)
                {
                    throw new Exception($"Member with username '{username}' doesn't exist!");
                }

                //check if already rated
                var existingRating = member.Descendants("RatedHotel").FirstOrDefault(r => r.Element("HotelID")?.Value == hotelID);
                if (existingRating != null)
                {
                    throw new Exception($"You have already rated hotel with ID {hotelID}");
                }

                // check if rating given is valid
                if (rating < 0.0f || rating > 5.0f)
                {
                    throw new Exception("Rating must be between 0 and 5");
                }

                // find/create the container
                var ratedHotelsElement = member.Element("RatedHotels");

                // create container
                if (ratedHotelsElement == null)
                {
                    ratedHotelsElement = new XElement("RatedHotels");
                    member.Add(ratedHotelsElement);
                }

                // new rating
                XElement newRating = new XElement("RatedHotel",
                    new XElement("HotelID", hotelID),
                    new XElement("Rating", rating.ToString("F1")),
                    new XElement("Comment", comment)
                );
                ratedHotelsElement.Add(newRating);
                members.Save(membersPath);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<HotelListing> BrowseHotels()
        {
            List<HotelListing> availableHotels = new List<HotelListing>();

            try
            {
                string hotelsPath = HostingEnvironment.MapPath("~/App_Data/Hotels.xml");

                if (!System.IO.File.Exists(hotelsPath))
                {
                    throw new Exception("Hotels.xml not found");
                }

                XDocument hotelsDoc = XDocument.Load(hotelsPath);

                var hotels = hotelsDoc.Descendants("Hotel");

                foreach (var hotel in hotels)
                {
                    string bookedRoomsText = hotel.Element("BookedRooms")?.Value ?? "0";
                    int bookedRooms = int.Parse(bookedRoomsText);

                    // Only include hotels with available rooms (BookedRooms > 0)
                    if (bookedRooms > 0)
                    {
                         string priceText = hotel.Element("Price")?.Value ?? "0.00";
                         float price = float.Parse(priceText);

                        var addressElement = hotel.Element("Address");

                        HotelListing listing = new HotelListing
                        {
                            HotelID = hotel.Element("HotelID")?.Value,
                            Name = hotel.Element("Name")?.Value,
                            BookedRooms = bookedRooms,
                            Price = price,
                            NearestAirport = hotel.Element("NearestAirport")?.Value,
                            HotelAddress = new Address
                            {
                                Number = addressElement?.Element("Number")?.Value,
                                Street = addressElement?.Element("Street")?.Value,
                                City = addressElement?.Element("City")?.Value,
                                State = addressElement?.Element("State")?.Value,
                                Zip = addressElement?.Element("Zip")?.Value
                            }
                        };

                        availableHotels.Add(listing);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error loading hotels: " + ex.Message);
            }

            return availableHotels;
        }
          
         
        // login functionality for staff
        public bool LoginStaff(string username, string password)
        {
            // first validate the inputs
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return false;
            }

            try
            {
                // load up the staff.xmldocument
                string staffPath = HostingEnvironment.MapPath("~/App_Data/Staff.xml");
                XDocument staff = XDocument.Load(staffPath);

                // get matching staff member
                var allStaff = staff.Descendants("StaffMember");

                // iterate through all staff
                foreach (var m in allStaff)
                {
                    var tempUsername = m.Element("Username");
                    var tempPassword = m.Element("Password");

                    // found member with matching username & [password
                    if (tempUsername != null && tempUsername.Value == username && tempPassword != null && tempPassword.Value == password)
                    {
                        return true;
                    }
                }
                // if I didn't find a matching staff, return false
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // login functionality for member
        public bool LoginMember(string username, string password)
        {
            // validate inputs
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return false;
            }

            try
            {
                // load up Members.xml
                string membersPath = HostingEnvironment.MapPath("~/App_Data/Members.xml");
                XDocument members = XDocument.Load(membersPath);

                // get matching member
                var allMembers = members.Descendants("Member");

                //iterate through all members
                foreach (var m in allMembers)
                {
                    var tempUsername = m.Element("Username");
                    var tempPassword = m.Element("Password");

                    // found member with matching username
                    if (tempUsername != null && tempUsername.Value == username && tempPassword != null && tempPassword.Value == password)
                    {
                        return true;
                    }
                }

                // if the member wasn't found, return false
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool RegisterMember(string username, string password, float balance)
        {
            try
            {
                // load up Members.xml
                string membersPath = HostingEnvironment.MapPath("~/App_Data/Members.xml");
                XDocument members = XDocument.Load(membersPath);

                // get matching member
                var allMembers = members.Descendants("Member");

                //iterate through all members
                foreach (var m in allMembers)
                {
                    var tempUsername = m.Element("Username");

                    // if you find a member with the same username, that's a problem. Return false
                    // (every username needs to be unique)
                    if (tempUsername != null && tempUsername.Value == username)
                    {
                        return false;
                    }
                }

                // Create new member with empty RatedHotels and BookedHotels
                XElement newMember = new XElement("Member",
                    new XElement("Username", username),
                    new XElement("Password", password),
                    new XElement("Balance", balance.ToString("F2")),
                    new XElement("RatedHotels"),
                    new XElement("BookedHotels")
                );

                // Add to document the newly formatted member
                members.Root.Add(newMember);

                // Save the new member
                members.Save(membersPath);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<HotelListing> GetAllHotels()
        {
            List<HotelListing> hotelsList = new List<HotelListing>();

            string hotelsPath = HostingEnvironment.MapPath("~/App_Data/Hotels.xml");
            XDocument doc = XDocument.Load(hotelsPath);

            foreach (var h in doc.Descendants("Hotel"))
            {
                var addressElement = h.Element("Address");

                hotelsList.Add(new HotelListing
                {
                    HotelID = h.Attribute("ID")?.Value,
                    Name = h.Element("Name")?.Value,
                    BookedRooms = int.Parse(h.Element("BookedRooms")?.Value ?? "0"),
                    Price = float.Parse(h.Element("Price")?.Value ?? "0"),
                    NearestAirport = addressElement?.Attribute("NearestAirport")?.Value,
                    HotelAddress = new Address
                    {
                        Number = addressElement.Element("Number").Value,
                        Street = addressElement.Element("Street").Value,
                        City = addressElement.Element("City").Value,
                        State = addressElement.Element("State").Value,
                        Zip = addressElement.Element("Zip").Value
                    }
                });
            }

            return hotelsList;
        }

        public bool BookHotelRooms(string hotelID, int roomsToBook, float discountPercent)
        {
            string hotelsPath = HostingEnvironment.MapPath("~/App_Data/Hotels.xml");
            XDocument doc = XDocument.Load(hotelsPath);

            var hotel = doc.Descendants("Hotel")
                           .FirstOrDefault(h => h.Attribute("ID")?.Value == hotelID);

            if (hotel == null) return false;

            // Update booked rooms
            int current = int.Parse(hotel.Element("BookedRooms").Value);
            hotel.Element("BookedRooms").Value = (current + roomsToBook).ToString();

            // Apply discount
            float oldPrice = float.Parse(hotel.Element("Price").Value);
            float newPrice = oldPrice * (1 - (discountPercent / 100f));
            hotel.Element("Price").Value = newPrice.ToString("F2");

            doc.Save(hotelsPath);
            return true;
        }


    }

}
