using AutoMapper;
using CustomerManagerService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerManagerService.DTOs.Config
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Customer, CustomerDTO>()
                .ForMember(s => s.Bookings, c => c.MapFrom(m => m.Bookings))
                .ReverseMap();

            CreateMap<Booking, BookingDTO>().ReverseMap();

            CreateMap<Customer, CreateCustomerDTO>()
                .ForMember(s => s.Bookings, c => c.MapFrom(m => m.Bookings))    
                .ReverseMap();
            
            CreateMap<UpdateCustomerDTO, Customer>()
                .ForMember(s => s.Bookings, c => c.MapFrom(m => m.Bookings))
                .ReverseMap();
        }
    }
}
