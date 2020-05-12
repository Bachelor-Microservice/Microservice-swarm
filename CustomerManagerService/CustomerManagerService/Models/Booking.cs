using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace CustomerManagerService.Models
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

        [BsonElement("ItemName")]
        public string ItemName { get; set; }

        [BsonElement("ItemNo")]
        public string ItemNo { get; set; }
    }
}