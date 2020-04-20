using System;

namespace ItemManagerService.Entities
{
    public class ItemEntity : SimpleItem
    {
        public int? Id { get; set; }
        public string Unit { get; set; }
        public decimal? Price { get; set; }
        public string PriceModel { get; set; }
        public bool? QuickPost { get; set; }
        public decimal? QuickPostAmount { get; set; }
        public string PriceModelFrom { get; set; }
        public bool? Vat { get; set; }
        public int? ArticleGroup { get; set; }
        public int? StatisticsQuantity { get; set; }
        public int? OverBidderQuantity { get; set; }
        public int? MinQuantity { get; set; }
        public int? MaxQuantity { get; set; }
        public string DistributionTo { get; set; }
        public decimal? Amount { get; set; }
        public int? CustomerId { get; set; }
        public string AccountNo { get; set; }
        public string CostCenter { get; set; }
        public string SupplierNo { get; set; }
        public bool? CashDiscount { get; set; }
        public decimal? Weight { get; set; }
        public string SupplierCountry { get; set; }
        public int? ProfileId { get; set; }
        public int? RelationNo { get; set; }
        public decimal? CostPrice { get; set; }
        public decimal? Stock { get; set; }
        public decimal? MinStock { get; set; }
        public string TicketForm { get; set; }
        public string TicketType { get; set; }
        public decimal? Fee { get; set; }
        public string ItemFee { get; set; }
        public string CatalogNo { get; set; }
        public int? SupplierId { get; set; }
        public int? Clips1 { get; set; }
        public int? Clips2 { get; set; }
        public int? Clips3 { get; set; }
        public decimal? CompuMatValue { get; set; }
        public TimeSpan? OffsetFrom { get; set; }
        public TimeSpan? OffsetTo { get; set; }
        public string Color { get; set; }
        public string Tags { get; set; }
        public string Switches { get; set; }
    }
}