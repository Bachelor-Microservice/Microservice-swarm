using AutoMapper;
using CustomerManagerService.DTOs;
using CustomerManagerService.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace CustomerManagerService.Services
{
    public class CustomerService
    {
        private readonly IMongoCollection<Customer> _customerContext;
        private readonly IMapper _mapper;

        public CustomerService(IConfiguration config, IMapper mapper)
        {
            // Connects to MongoDB.
            var client = new MongoClient(config.GetConnectionString("CustomerDB"));
            // Gets the supplementDB.
            var database = client.GetDatabase("CustomerDB");
            //Fetches the supplement collection.
            _customerContext = database.GetCollection<Customer>("Customers");

            _mapper = mapper;
        }

        public async Task<List<Customer>> Get()
        {
            return await _customerContext.Find(s => true).ToListAsync();
        }

        public async Task<Customer> Get(string id)
        {
            return await _customerContext.Find(s => s.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Customer> Create(Customer s)
        {
            await _customerContext.InsertOneAsync(s);
            return s;
        }

        public async Task<CreateCustomerDTO> Create(CreateCustomerDTO c)
        {
            var model = _mapper.Map<Customer>(c);
            await _customerContext.InsertOneAsync(model);
            return c;
        }

        public async Task<Customer> Update(string id, Customer s)
        {
            await _customerContext.ReplaceOneAsync(su => su.Id == id, s);
            return s;
        }


        public async Task Remove(string id)
        {
            await _customerContext.DeleteOneAsync(su => su.Id == id);
        }

        public string GenerateRandomLowerCaseNormalizedString(int length)
        {
            var chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[length];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            return new String(stringChars);
        }
    }
}
