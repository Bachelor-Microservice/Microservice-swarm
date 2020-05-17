using AutoMapper;
using ItemContracts;
using Microsoft.AspNetCore.Routing.Constraints;
using PriceCalendarService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace PriceCalendarService.Dtos.Config
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<ItemPriceAndCurrencyResponseDTO, ItemPriceAndCurrencyResponse>();
            CreateMap<ItemPriceAndCurrencyResponse, ItemPriceAndCurrencyResponseDTO>();
            CreateMap<ItemDayDTO, ItemDay>().ReverseMap();
            CreateMap<Item, ItemDTO>().ReverseMap();
            CreateMap<Groups, GroupsDTO>().ReverseMap();
            //CreateMap<ItemEntityCreated>
        }
    }
}
