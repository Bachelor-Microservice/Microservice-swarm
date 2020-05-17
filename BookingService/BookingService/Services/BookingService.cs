using AutoMapper;
using BookingService.DTO;
using BookingService.MassTransit.Publishers;
using BookingService.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingService.Services
{
    public interface IBookingService
    {
        public Task<List<Booking>> Get();
        public  Task<Booking> Get(string id);

        public  Task<CreateBookingDTO> Create(CreateBookingDTO s);

        public  Task<Booking> Update(string id, Booking s);

        public  Task Remove(string id);

        public  Task<List<Booking>> GetArrivalsToday();
        public  Task<List<Booking>> GetDepartueToday();
    }
    public class BookingManagerService : IBookingService
    {
        private readonly IMongoCollection<Booking> _bookingDB;
        private readonly IMapper _mapper;
        private readonly IPublishBookingCrud _publisher;

        public BookingManagerService(IConfiguration config, IMapper mapper, IPublishBookingCrud publisher)
        {
            var client = new MongoClient(config.GetConnectionString("BookingDB"));
            var database = client.GetDatabase("BookingDB");
            _bookingDB = database.GetCollection<Booking>("Bookings");
            _mapper = mapper;
            _publisher = publisher;
        }

        public async Task<List<Booking>> Get()
        {
            return await _bookingDB.Find(s => true).ToListAsync();
        }

        public async Task<Booking> Get(string id)
        {
            return await _bookingDB.Find(s => s.Id == id).FirstOrDefaultAsync();
        }

        /*
        public async Task<Booking> Create(Booking s)
        {
            //Create a supplement.
            await _bookingDB.InsertOneAsync(s);
            return s;
        }*/


        public async Task<CreateBookingDTO> Create(CreateBookingDTO dto)
        {
            var s = _mapper.Map<Booking>(dto);
            await _bookingDB.InsertOneAsync(s);
            s.BookedDays = null;
            _publisher.Created(s);
            return dto;
        }


        public async Task<Booking> Update(string id, Booking s)
        {
            await _bookingDB.ReplaceOneAsync(su => su.Id == id, s);
            var noDays = s;
            noDays.BookedDays = null;
            _publisher.Updated(noDays);
            return s;
        }


        public async Task Remove(string id)
        {
            await _bookingDB.DeleteOneAsync(su => su.Id == id);
            _publisher.Deleted(id);
        }

        public async Task<List<Booking>> GetArrivalsToday() 
        {
            var date = DateTime.Now.Date;
            
            return await _bookingDB.Find(s => s.Arrival.Date == date).ToListAsync();
        }

        public async Task<List<Booking>> GetDepartueToday()
        {
            var date = DateTime.Now.Date;
            return await _bookingDB.Find(s => s.Depature == date).ToListAsync();
        }
    }
}
