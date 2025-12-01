using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Xml;

namespace WcfHotelService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        List<RatedHotel> GetRatedHotels(string username);

        [OperationContract]
        bool AddHotelRating(string username, string hotelID, float rating, string comment);

        [OperationContract]
        List<HotelListing> BrowseHotels();

        [OperationContract]
        bool LoginStaff(string username, string password);

        [OperationContract]
        bool LoginMember(string username, string password);

        [OperationContract]
        bool RegisterMember(string username, string password, float balance);

        [OperationContract]
        List<HotelListing> GetAllHotels();

        [OperationContract]
        bool BookHotelRooms(string hotelID, int roomsToBook, float discountPercent);

        [OperationContract]
        bool ChangePassword(string username, string newPassword, int userType);

        [OperationContract]
        List<HotelBooking> GetBookedHotels(string username);

        [OperationContract]
        float getBalance(string username);

        [OperationContract]
        bool addBalance(string username, float balance);


        // (more potential errors with booking hotels, have it return int for more descriptive error handling/reporting)
        [OperationContract]
        int BookHotel(string username, int hotelId, string startDate, string endDate);

        // TODO: Add your service operations here
    }


    // Custom data structure allowing me to keep track of rated hotel info
    [DataContract]
    public class RatedHotel
    {
        [DataMember]
        public string HotelID { get; set; }

        [DataMember]
        public string HotelName { get; set; }

        [DataMember]
        public float Rating { get; set; }

        [DataMember]
        public string Comment { get; set; }

        [DataMember]
        public Address HotelAddress { get; set; }
    }

    // Address is complex type, so just make a separate data contract
    [DataContract]
    public class Address
    {
        [DataMember]
        public string Number {  get; set; }

        [DataMember]
        public string Street { get; set; }

        [DataMember]
        public string City { get; set; }

        [DataMember]
        public string State { get; set; }

        [DataMember]
        public string Zip { get; set; }
    }


    // Custom data structure holding all hotel information
    [DataContract]
    public class Hotel
    {
        [DataMember]
        public string HotelID { get; set; }

        [DataMember]
        public string HotelName { get; set; }

        [DataMember]
        public float Rating { get; set; }


        [DataMember]
        public List<string> PhoneNo { get; set; }

        [DataMember]
        public string NearestAirport { get; set; }

        [DataMember]
        public Address HotelAddress { get; set; }
    }

    // Custom data structure holding hotel listing information (for members to browse)
    [DataContract]
    public class HotelListing
    {
        [DataMember]
        public string HotelID { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public Address HotelAddress { get; set; }

        [DataMember]
        public int BookedRooms { get; set; }

        [DataMember]
        public float Price { get; set; }

        [DataMember]
        public string NearestAirport { get; set; }
    }

    // Custom data structure to display the hotel rooms a member has booked
    [DataContract]
    public class HotelBooking
    {
        [DataMember]
        public int HotelID { get; set; }

        [DataMember]
        public string HotelName { get; set; }

        [DataMember]
        public float Price { get; set; }

        [DataMember]
        public string Start_Date { get; set; }

        [DataMember]
        public string End_Date { get; set; }
    }
}
