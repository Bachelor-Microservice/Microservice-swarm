using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerManagerService.Models
{
    public class Customer
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        public string SupplementName { get; set; }

        [BsonElement("Type")]
        public string Type { get; set; }

        [BsonElement("RegistrationDate")]
        public DateTime RegistrationDate { get; set; }

        [BsonElement("Email")]
        public string Email { get; set; }

        [BsonElement("Address")]
        public string Address {get; set;}

        [BsonElement("TelephonePrimary")]
        public string TelephonePrimary {get; set;}

        [BsonElement("MobilePhone")]
        public string MobilePhone {get; set;}

        [BsonElement("Bookings")]
        public List<Booking> Bookings { get; set; }
    }
}
