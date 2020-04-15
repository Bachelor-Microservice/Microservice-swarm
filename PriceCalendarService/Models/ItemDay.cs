namespace PriceCalendarService.Models
{
    public class ItemDay
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        public decimal price { get; set; }

        public string Priority { get; set; }

        public string PricePackage { get; set; }

        public CustomerType CustomerType { get; set; }
    }
}