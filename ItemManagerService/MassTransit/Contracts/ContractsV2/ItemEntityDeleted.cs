using ContractsV2.ItemContracts;
using ItemContracts;

namespace Shared.MassTransit.Contracts.ContractsV2
{
    public class ItemEntityDeleted : IItemEntityDeleted
    {
        public int Id { get; set; }
        public string ItemNo { get; set; }
    }
}