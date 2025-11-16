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

    // (for my own debugging purposes)
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class Service1 : IService1
    {
        // Given username, fetch all the hotels they have previously rated
        public List<RatedHotel> GetRatedHotels(string username)
        {
            // first initialize the list
            List<RatedHotel> ratedHotels = new List<RatedHotel>();

            try
            {
                // we need both the Members.xml and Hotels.xml
                string membersPath = HostingEnvironment.MapPath("~/App_Data/Members.xml");
                string hotelsPath = HostingEnvironment.MapPath("~/App_Data/Hotels.xml");

                // load both xml files up
                XDocument members = XDocument.Load(membersPath);
                XDocument hotels = XDocument.Load(hotelsPath);

                // Find the member with passed in user name
                var allMembers = members.Descendants("Member");
                XElement member = null;

                // iterate through all members...
                foreach (var m in allMembers)
                {
                    // fetch that member's username
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

                    // iterate through all hotels
                    foreach (var h in allHotels)
                    {
                        // pull the ID from the attribute, and check if it's the same ID we're looking for
                        var tempHotelID = h.Attribute("ID");

                        if (tempHotelID.Value != null && tempHotelID.Value == hotelID)
                        {
                            currHotel = h;
                            break;
                        }
                    }

                    // if currHotel != null (so the member actually rated an existing hotel)
                    if (currHotel != null)
                    {
                        // format the address first
                        var addressElement = currHotel.Element("Address");

                        // get the address and create the Address object
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
                        
                        // ... with the address object created, you can create the rest RatedHotel object
                        ratedHotels.Add(new RatedHotel
                        {
                            HotelID = currHotel.Attribute("ID").Value,
                            HotelName = currHotel.Element("Name").Value,
                            Rating = float.Parse(rating.Element("Rating")?.Value ?? "0.0"), //if rating is not null, get value, otherwise, set 0.0
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

        // add new rating for a hotel
        public bool AddHotelRating(string username, string hotelID, float rating, string comment)
        {
            try
            {
                // first, we're going to load up the XML document (rating info stored in Members.xml)
                string membersPath = HostingEnvironment.MapPath("~/App_Data/Members.xml");
                XDocument members = XDocument.Load(membersPath);

                // get matching member
                var allMembers = members.Descendants("Member");
                XElement member = null;

                // iterate through all members
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

                // if no match after searching, I'll throw an exception (three invalid possiblities, so throw exception instead of returning false)
                if (member == null)
                {
                    throw new Exception($"Member with username '{username}' doesn't exist!");
                }

                //check if already rated (I disallow double ratings)
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

                // check if the hotelID is valid
                if(!int.TryParse(hotelID, out int hotelIDint))
                {
                    throw new Exception("Hotel ID must be an integer!");
                }
                if(hotelIDint < 1 || hotelIDint > 10)
                {
                    throw new Exception("Only Hotel ID's 1 through 10 exist.");
                }

                // find/create the container
                var ratedHotelsElement = member.Element("RatedHotels");

                // create container
                if (ratedHotelsElement == null)
                {
                    ratedHotelsElement = new XElement("RatedHotels");
                    member.Add(ratedHotelsElement);
                }

                // create new rating
                XElement newRating = new XElement("RatedHotel",
                    new XElement("HotelID", hotelID),
                    new XElement("Rating", rating.ToString("F1")),
                    new XElement("Comment", comment)
                );
                // add it to the list 
                ratedHotelsElement.Add(newRating);

                // write back to document and return true
                members.Save(membersPath);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // login functionality for staff
        public bool LoginStaff(string username, string password)
        {
            // first validate the inputs
            if(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
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
                throw new Exception (ex.Message);
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
                throw new Exception (ex.Message);
            }
        }
    }
}
