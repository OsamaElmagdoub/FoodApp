namespace FoodApp.Data.Entities
{
    public class Invoice :BaseEntity
    {
        public Order Order { get; set; }
        public int OrderId { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
