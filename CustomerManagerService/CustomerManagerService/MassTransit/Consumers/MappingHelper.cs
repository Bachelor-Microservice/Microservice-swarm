using BookingService.MassTransit.Contracts;
using ContractsV2.BookingContracts;
using CustomerManagerService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerManagerService.MassTransit.Consumers
{
    public class MappingHelper
    {
        public static Customer Map(IBookingCreated cons)
        {
            var cust = new Customer
            {
                SupplementName = cons.CustomerName,
                Type = "N/A",
                RegistrationDate = cons.Arrival,
                Email = cons.Email,
                Address = "N/A",
                Bookings = new List<Booking>()
            };

            var booking = new Booking
            {
                Arrival = cons.Arrival,
                Depature = cons.Depature,
                ItemName = cons.ItemName,
                ItemNo = cons.ItemNo,
                Price = cons.Currency + ": " + cons.Price.ToString()
            };

            cust.Bookings.Add(booking);

            return cust;
        }

        public static Booking MapFromExisting(IBookingCreated cons)
        {
            return new Booking
            {
                Arrival = cons.Arrival,
                Depature = cons.Depature,
                ItemName = cons.ItemName,
                ItemNo = cons.ItemNo,
                Price = cons.Currency + ": " + cons.Price.ToString()
            };
        }

        public static Booking Map(IBookingUpdated cons)
        {
            var booking = new Booking
            {
                Arrival = cons.Arrival,
                Depature = cons.Depature,
                ItemName = cons.ItemName,
                ItemNo = cons.ItemNo,
                Price = cons.Currency + ": " + cons.Price.ToString()
            };

            return booking;
        }
    }
}
