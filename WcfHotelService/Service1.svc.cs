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

    }
}
