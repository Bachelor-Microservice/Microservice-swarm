using ContractsV2.ItemContracts;

namespace Shared.MassTransit.Contracts
{
    public class ItemEntityDeleted : IItemEntityDeleted
    {
        public int Id { get; set; }
        public string ItemNo { get; set; }
    }
}