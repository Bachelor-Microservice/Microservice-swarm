using AutoMapper;
using ItemContracts;
using ItemManagerService.Models;
using MassTransit;
using Shared.MassTransit.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItemManagerService.MassTransit.Publishers
{
    public interface IPublishItemCrud
    {
        public void Deleted(int Id);
        public void Updated(Items item);
        public void Created(Items item);

    }
    public class PublishItemCrud : IPublishItemCrud
    {
        private readonly IMapper _mapper;
        private readonly IBus _bus;
        private readonly IPublishEndpoint _publishEndpoint;
        
        public PublishItemCrud(IMapper mapper, IBus bus, IPublishEndpoint publishEndpoint)
        {
            _mapper = mapper;
            _bus = bus;
            _publishEndpoint = publishEndpoint;
        }

        public async void Deleted(int Id)
        {
            var contract = new ItemEntityDeletedContract { Id = Id };
            //await _bus.Publish<ItemEntityDeleted>(contract);
            await _publishEndpoint.Publish<ItemEntityDeleted>(contract);
        }

        public async void Updated(Items item)
        {
            var contract = _mapper.Map<ItemEntityUpdatedContract>(item);
            await _publishEndpoint.Publish<ItemEntityUpdated>(contract);
        }

        public async void Created(Items item)
        {
            var contract = _mapper.Map<ItemEntityCreatedContact>(item);
            await _publishEndpoint.Publish<ItemEntityCreated>(contract);
        }
    }
}
