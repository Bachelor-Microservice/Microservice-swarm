using AutoMapper;
using ContractsV2.ItemContracts;
using Shared.MassTransit.Contracts.ContractsV2;
using ItemManagerService.Models;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItemManagerService.MassTransit.Publishers
{
    public interface IPublishItemCrud
    {
        public void Deleted(int Id, string ItemNo);
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

        public async void Deleted(int Id, string ItemNo)
        {
            var contract = new ItemEntityDeleted { Id = Id, ItemNo = ItemNo};
            //await _bus.Publish<ItemEntityDeleted>(contract);
            await _publishEndpoint.Publish<IItemEntityDeleted>(contract);
        }

        public async void Updated(Items item)
        {
            var contract = _mapper.Map<ItemEntityUpdated> (item);
            await _publishEndpoint.Publish<IItemEntityUpdated>(contract);
        }

        public async void Created(Items item)
        {
            var contract = _mapper.Map<ItemEntityCreated>(item);
            await _publishEndpoint.Publish<IItemEntityCreated>(contract);
        }
    }
}
