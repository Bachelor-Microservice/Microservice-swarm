using AutoMapper;
using BookingService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingService.DTO
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Booking, CreateBookingDTO>()
                .ForMember(s => s.BookedDays, c => c.MapFrom(m => m.BookedDays))
                .ReverseMap();
            CreateMap<BookedDay, BookedDayDTO>().ReverseMap();
        }
    }
}
