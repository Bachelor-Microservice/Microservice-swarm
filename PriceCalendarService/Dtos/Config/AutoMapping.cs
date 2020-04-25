using AutoMapper;
using Microsoft.AspNetCore.Routing.Constraints;
using PriceCalendarService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PriceCalendarService.Dtos.Config
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            /*CreateMap<ItemPriceAndCurrencyResponseDTO, ItemPriceAndCurrencyResponse>();
            CreateMap<ItemPriceAndCurrencyResponse, ItemPriceAndCurrencyResponseDTO>();
            */
            CreateMap<ItemDayDTO, ItemDay>();
            CreateMap<ItemDay, ItemDayDTO>();

            CreateMap<ItemDTO, Item>()
                .ForMember(i => i.ItemDay, o => o.MapFrom(src => src.ItemDays)).ReverseMap();

            CreateMap<GroupsDTO, Groups>()
                .ForMember(g => g.Item, o => o.MapFrom(src => src.Items)).ReverseMap();

            CreateMap<ItemPriceAndCurrencyResponseDTO, ItemPriceAndCurrencyResponse>()
                .ForMember(d => d.Groups, o => o.MapFrom(src => src.Groups));
        }
    }
}
