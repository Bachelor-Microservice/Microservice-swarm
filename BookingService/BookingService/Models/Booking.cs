using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingService.Models
{
    public class Booking
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Arrival")]
        public DateTime Arrival { get; set; }

        [BsonElement("Depature")]
        public DateTime Depature { get; set; }

        [BsonElement("Price")]
        public double Price { get; set; }

        [BsonElement("Customerid")]
        public string Customerid { get; set; }

        [BsonElement("CustomerName")]
        public string CustomerName { get; set; }

        [BsonElement("Email")]
        public string Email { get; set; }

        [BsonElement("ItemDescription")]
        public string ItemDescription { get; set; }

        [BsonElement("ItemName")]
        public string ItemName { get; set; }

        [BsonElement("ItemNo")]
        public string ItemNo { get; set; }

        [BsonElement("BookedDays")]
        public List<BookedDay> BookedDays { get; set; }

        [BsonElement("Currency")]
        public string Currency { get; set; }

    }
}
