using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace BookingService.Models
{
    public class BookedDay
    {
        [BsonElement("PriceForDay")]
        public double PriceForDay { get; set; }

        [BsonElement("Date")]
        public DateTime Date { get; set; }

        [BsonElement("DiscountDescription")]
        public string DiscountDescription { get; set; }

        [BsonElement("ItemDayId")]
        public int ItemDayId { get; set; }
    }
}
