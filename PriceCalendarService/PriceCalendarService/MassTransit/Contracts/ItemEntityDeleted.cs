namespace Shared.MassTransit.Contracts
{
    public class ItemEntityDeleted : ItemContracts.ItemEntityDeleted
    {
        public int Id { get; set; }
        public string ItemNo { get; set; }
    }
}