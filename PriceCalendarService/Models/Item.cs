namespace PriceCalendarService.Models
{
    public class Item
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public List<ItemDay> ItemDays {get;set;}
    }
}