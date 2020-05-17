using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.ItemContracts
{
    public interface IItemEntityCreated
    {
        string ItemNo { get; set; }
        string Name { get; set; }
        string ItemCode { get; set; }
        int Id { get; set; }
        string Unit { get; set; }
        double? Price { get; set; }
        string PriceModel { get; set; }
        bool QuickPost { get; set; }
        double? QuickPostAmount { get; set; }
        string PriceModelFrom { get; set; }
        bool Vat { get; set; }
        int? ArticleGroup { get; set; }
        int? StatisticsQuantity { get; set; }
        int? OverBidderQuantity { get; set; }
        int? MinQuantity { get; set; }
        int? MaxQuantity { get; set; }
        string DistributionTo { get; set; }
        double? Amount { get; set; }
        int? CustomerId { get; set; }
        string AccountNo { get; set; }
        string CostCenter { get; set; }
        string SupplierNo { get; set; }
        bool? CashDiscount { get; set; }
        double? Weight { get; set; }
        string SupplierCountry { get; set; }
        int? ProfileId { get; set; }
        int? RelationNo { get; set; }
        double? CostPrice { get; set; }
        double? Stock { get; set; }
        double? MinStock { get; set; }
        string TicketForm { get; set; }
        string TicketType { get; set; }
        double? Fee { get; set; }
        string ItemFee { get; set; }
        string CatalogNo { get; set; }
        int? SupplierId { get; set; }
        int? Clips1 { get; set; }
        int? Clips2 { get; set; }
        int? Clips3 { get; set; }
        double? CompuMatValue { get; set; }
        int? OffsetFrom { get; set; }
        int? OffsetTo { get; set; }
        string Color { get; set; }
        string Tags { get; set; }
        string Switches { get; set; }
    }
}
