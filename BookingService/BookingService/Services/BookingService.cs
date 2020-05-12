using AutoMapper;
using BookingService.DTO;
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
    }
    public class BookingManagerService : IBookingService
    {
        private readonly IMongoCollection<Booking> _bookingDB;
        private readonly IMapper _mapper;
        
        public BookingManagerService(IConfiguration config, IMapper mapper)
        {
            var client = new MongoClient(config.GetConnectionString("BookingDB"));
            var database = client.GetDatabase("BookingDB");
            _bookingDB = database.GetCollection<Booking>("Bookings");
            _mapper = mapper;
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
            return dto;
        }


        public async Task<Booking> Update(string id, Booking s)
        {
            await _bookingDB.ReplaceOneAsync(su => su.Id == id, s);
            return s;
        }


        public async Task Remove(string id)
        {
            await _bookingDB.DeleteOneAsync(su => su.Id == id);
        }
    }
}
