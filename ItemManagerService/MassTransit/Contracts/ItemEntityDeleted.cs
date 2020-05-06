using ItemContracts;

namespace Shared.MassTransit.Contracts
{
    public class ItemEntityDeletedContract : ItemEntityDeleted
    {
        public int Id { get; set; }
    }
}