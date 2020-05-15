using ContractsV2.ItemContracts;
using ItemContracts;

namespace Shared.MassTransit.Contracts.ContractsV2
{
    public class ItemEntityUpdated : IItemEntityUpdated
    {
        public string ItemNo { get; set; }
        public string Name { get; set; }
        public string ItemCode { get; set; }
        public int Id { get; set; }
        public string Unit { get; set; }
        public double? Price { get; set; }
        public string PriceModel { get; set; }
        public bool QuickPost { get; set; }
        public double? QuickPostAmount { get; set; }
        public string PriceModelFrom { get; set; }
        public bool Vat { get; set; }
        public int? ArticleGroup { get; set; }
        public int? StatisticsQuantity { get; set; }
        public int? OverBidderQuantity { get; set; }
        public int? MinQuantity { get; set; }
        public int? MaxQuantity { get; set; }
        public string DistributionTo { get; set; }
        public double? Amount { get; set; }
        public int? CustomerId { get; set; }
        public string AccountNo { get; set; }
        public string CostCenter { get; set; }
        public string SupplierNo { get; set; }
        public bool? CashDiscount { get; set; }
        public double? Weight { get; set; }
        public string SupplierCountry { get; set; }
        public int? ProfileId { get; set; }
        public int? RelationNo { get; set; }
        public double? CostPrice { get; set; }
        public double? Stock { get; set; }
        public double? MinStock { get; set; }
        public string TicketForm { get; set; }
        public string TicketType { get; set; }
        public double? Fee { get; set; }
        public string ItemFee { get; set; }
        public string CatalogNo { get; set; }
        public int? SupplierId { get; set; }
        public int? Clips1 { get; set; }
        public int? Clips2 { get; set; }
        public int? Clips3 { get; set; }
        public double? CompuMatValue { get; set; }
        public int? OffsetFrom { get; set; }
        public int? OffsetTo { get; set; }
        public string Color { get; set; }
        public string Tags { get; set; }
        public string Switches { get; set; }
    }
}