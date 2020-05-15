using AutoMapper;
using Shared.MassTransit.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared.MassTransit.Contracts.ContractsV2;

namespace ItemManagerService.Models.Mapping
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Items, ItemEntityCreatedContact>().ReverseMap();
            CreateMap<Items, ItemEntityDeletedContract>().ReverseMap();
            CreateMap<Items, ItemEntityUpdatedContract>().ReverseMap();
            CreateMap<Items, ItemEntityUpdated>().ReverseMap();
            CreateMap<Items, ItemEntityDeleted>().ReverseMap();
            CreateMap<Items, ItemEntityCreated>().ReverseMap();
        }
    }
}
