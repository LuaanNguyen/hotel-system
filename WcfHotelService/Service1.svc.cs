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

        // allows member to add a hotel rating
        public bool AddHotelRating(string username, string hotelID, float rating, string comment)
        {
            try
            {
                string membersPath = HostingEnvironment.MapPath("~/App_Data/Members.xml");

                // (additional error checking)
                if (!System.IO.File.Exists(membersPath))
                {
                    throw new Exception("Members.xml not found");
                }

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

        // returns all the hotels that have been booked by an agent (staff)
        public List<HotelListing> BrowseHotels()
        {
            // availableHotels will hold all the hotels that have agent-booked rooms
            List<HotelListing> availableHotels = new List<HotelListing>();
            
            
            try
            {
                // additional error checking (this shouldn't ever happen, though)
                string hotelsPath = HostingEnvironment.MapPath("~/App_Data/Hotels.xml");

                if (!System.IO.File.Exists(hotelsPath))
                {
                    throw new Exception("Hotels.xml not found");
                }

                // get all the hotels
                XDocument hotelsDoc = XDocument.Load(hotelsPath);

                var hotels = hotelsDoc.Descendants("Hotel");

                // search for hotels that actually have booked rooms available
                foreach (var hotel in hotels)
                {
                    string bookedRoomsText = hotel.Element("BookedRooms")?.Value ?? "0";
                    int bookedRooms = int.Parse(bookedRoomsText);

                    // Only include hotels with available rooms (BookedRooms > 0)
                    if (bookedRooms > 0)
                    {

                        // (first parse the price)
                         string priceText = hotel.Element("Price")?.Value ?? "0.00";
                         float price = float.Parse(priceText);

                        // get the address separately, since it's a more complex type
                        var addressElement = hotel.Element("Address");

                        // format the HotelListing, and add it in
                        HotelListing listing = new HotelListing
                        {
                            HotelID = hotel.Attribute("ID")?.Value,
                            Name = hotel.Element("Name")?.Value,
                            BookedRooms = bookedRooms,
                            Price = price,
                            NearestAirport = addressElement?.Attribute("NearestAirport")?.Value,
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

        // method for creating a new member account
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

        // returns all hotels stored in Hotels.xml
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

        // Allows the agent to book hotel rooms
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

        // Allows user (both staff and member) to change their password. 0 userType = staff; 1 userType = Member
        public bool ChangePassword(string username, string newPassword, int userType)
        {
            try
            {
                string xmlPath;

                // Depending on user type, we'll have to adjust the xmlPath
                // staff user type
                if (userType == 0)
                {
                    xmlPath = HostingEnvironment.MapPath("~/App_Data/Staff.xml");
                }
                // member user type
                else
                {
                    xmlPath = HostingEnvironment.MapPath("~/App_Data/Members.xml");
                }

                XDocument doc = XDocument.Load(xmlPath);

                // Now find the user with the matching username
                XElement user = null;
                if(userType == 0)
                {
                    // get matching staff
                    var allStaff = doc.Descendants("StaffMember");

                    //iterate through all members
                    foreach (var s in allStaff)
                    {
                        var tempUsername = s.Element("Username");

                        // if you find a member with the same username, that's a problem. Return false
                        // (every username needs to be unique)
                        if (tempUsername != null && tempUsername.Value == username)
                        {
                            user = s;
                            break;
                        }
                    }
                }
                else
                {
                    // get matching member
                    var allMembers = doc.Descendants("Member");

                    //iterate through all members
                    foreach (var m in allMembers)
                    {
                        var tempUsername = m.Element("Username");

                        // if you find a member with the same username, that's a problem. Return false
                        // (every username needs to be unique)
                        if (tempUsername != null && tempUsername.Value == username)
                        {
                            user = m;
                            break;
                        }
                    }

                }

                // see if user has been assigned
                if(user == null)
                {
                    return false;
                }

                // update password
                XElement passwordElement = user.Element("Password");
                if (passwordElement != null) {
                    passwordElement.Value = newPassword; 
                }
                // (this shouldn't happen, as every user has to enter password, but just in case...)
                else
                {
                    user.Add(new XElement("Password", newPassword));
                }

                // save changes back to the xmldoc
                doc.Save(xmlPath);
                return true;
            }
            catch
            {
                return false;
            }


        }

        // given a username, return all the hotel rooms that the user has booked
        public List<HotelBooking> GetBookedHotels(string username)
        {
            List<HotelBooking> bookedHotels = new List<HotelBooking>();

            try
            {
                string membersPath = HostingEnvironment.MapPath("~/App_Data/Members.xml");
                string hotelsPath = HostingEnvironment.MapPath("~/App_Data/Hotels.xml");

                XDocument membersDoc = XDocument.Load(membersPath);
                XDocument hotelsDoc = XDocument.Load(hotelsPath);

                // get matching member
                XElement member = null;
                var allMembers = membersDoc.Descendants("Member");

                //iterate through all members
                foreach (var m in allMembers)
                {
                    var tempUsername = m.Element("Username");

                    // if you find a member with the same username, that's a problem. Return false
                    // (every username needs to be unique)
                    if (tempUsername != null && tempUsername.Value == username)
                    {
                        member = m;
                        break;
                    }
                }

                if (member == null)
                {
                    return bookedHotels;
                }

                // Get all booked hotels for this member
                var bookedHotelElements = member.Descendants("BookedHotel");

                foreach (var hotel in bookedHotelElements)
                {
                    int hotelId = int.Parse(hotel.Element("HotelID")?.Value ?? "0");

                    // Look up hotel name from Hotels.xml using HotelID
                    string hotelName = hotelsDoc.Descendants("Hotel")
                            .FirstOrDefault(h => h.Attribute("ID")?.Value == hotelId.ToString())
                            ?.Element("Name")?.Value ?? "Error: Unknown Hotel Name";

                    HotelBooking bookedHotel = new HotelBooking()
                    {
                        HotelID = hotelId,
                        HotelName = hotelName,  // Now includes the name
                        Price = float.Parse(hotel.Element("Price")?.Value ?? "0"),
                        Start_Date = hotel.Element("StartDate")?.Value ?? "",
                        End_Date = hotel.Element("EndDate")?.Value ?? ""
                    };

                    bookedHotels.Add(bookedHotel);
                }

                return bookedHotels;
            }
            catch 
            {
                return bookedHotels;
            }
        }

        // allows member to book a hotel
        public int BookHotel(string username, int hotelId, string startDate, string endDate)
        {
            try
            {
                string membersPath = HostingEnvironment.MapPath("~/App_Data/Members.xml");
                string hotelsPath = HostingEnvironment.MapPath("~/App_Data/Hotels.xml");

                // additional error checking
                if (!System.IO.File.Exists(hotelsPath))
                {
                    throw new Exception("Hotels.xml not found");
                }

                if (!System.IO.File.Exists(membersPath))
                {
                    throw new Exception("Members.xml not found");
                }

                XDocument membersDoc = XDocument.Load(membersPath);
                XDocument hotelsDoc = XDocument.Load(hotelsPath);

                // Find the member with matching username first
                XElement member = membersDoc.Descendants("Member")
                                            .FirstOrDefault(m => m.Element("Username")?.Value == username);

                if (member == null)
                {
                    return 3; // error 3 represents user not found
                }

                // Find the hotel
                XElement hotel = hotelsDoc.Descendants("Hotel")
                                          .FirstOrDefault(h => h.Attribute("ID")?.Value == hotelId.ToString());

                if (hotel == null)
                {
                    return 4; // error four represents hotel not found
                }

                // Get hotel price and available rooms
                float hotelPrice = float.Parse(hotel.Element("Price")?.Value ?? "0");
                int bookedRooms = int.Parse(hotel.Element("BookedRooms")?.Value ?? "0");

                // Check if rooms are available (this shouldn't occur, based on how front-end/BrowseHotels is designed, though)
                // (it's just additional error checking)
                if (bookedRooms <= 0)
                {
                    return 2; // Error 2 represents no rooms available
                }

                // Get member's balance
                float memberBalance = float.Parse(member.Element("Balance")?.Value ?? "0");

                // Check if member has sufficient balance
                if (memberBalance < hotelPrice)
                {
                    return 1; // Error 1 represents insufficient balance
                }

                // no errors encountered, so proceed

                // Deduct price from member's balance
                member.Element("Balance").Value = (memberBalance - hotelPrice).ToString("F2");

                // Add booking to member's BookedHotels
                XElement bookedHotels = member.Element("BookedHotels");
                if (bookedHotels == null)
                {
                    // Create BookedHotels element if it doesn't exist
                    bookedHotels = new XElement("BookedHotels");
                    member.Add(bookedHotels);
                }

                // format the new BookedHotel elemenet,a nd add it in
                XElement newBooking = new XElement("BookedHotel",
                    new XElement("HotelID", hotelId),
                    new XElement("Price", hotelPrice.ToString("F2")),
                    new XElement("StartDate", startDate),
                    new XElement("EndDate", endDate)
                );

                bookedHotels.Add(newBooking);

                // Decrease available rooms (BookedRooms) by 1 in Hotels.xml
                hotel.Element("BookedRooms").Value = (bookedRooms - 1).ToString();

                // Save changes to both documents
                membersDoc.Save(membersPath);
                hotelsDoc.Save(hotelsPath);

                return 0; // Success
            }
            catch (Exception ex)
            {
                return -1; // this will just represent uncaught errors; will generate basic error message
            }
        }

        public float getBalance(string username)
        {
            try
            {
                string membersPath = HostingEnvironment.MapPath("~/App_Data/Members.xml");

                // (additional error checking)
                if (!System.IO.File.Exists(membersPath))
                {
                    throw new Exception("Members.xml not found");
                }

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

                // if no match after searching, I'll throw an exception
                if (member == null)
                {
                    throw new Exception($"Member with username '{username}' doesn't exist!");
                }

                // otherwise, get the balance, convert it to a float, and return it
                string balanceStr = member.Element("Balance").Value;
                float balance = float.Parse(balanceStr);

                return balance;
            }
            catch
            {
                return 0;
            }
        }

        public bool addBalance(string username, float balance)
        {
            try
            {
                string membersPath = HostingEnvironment.MapPath("~/App_Data/Members.xml");

                // (additional error checking)
                if (!System.IO.File.Exists(membersPath))
                {
                    throw new Exception("Members.xml not found");
                }

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

                if (member == null)
                {
                    return false;
                }

                // Get current balance
                float currentBalance = float.Parse(member.Element("Balance")?.Value ?? "0");

                // Add funds
                float newBalance = currentBalance + balance;

                // Update balance
                member.Element("Balance").Value = newBalance.ToString("F2");

                // Save changes
                members.Save(membersPath);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }

            
}
