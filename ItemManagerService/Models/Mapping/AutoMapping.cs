using AutoMapper;
using ItemContracts;
using Shared.MassTransit.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItemManagerService.Models.Mapping
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Items, ItemEntityCreatedContact>().ReverseMap();
            CreateMap<Items, ItemEntityDeletedContract>().ReverseMap();
            CreateMap<Items, ItemEntityUpdatedContract>().ReverseMap();
        }
    }
}
