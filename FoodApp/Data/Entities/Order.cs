namespace FoodApp.Data.Entities
{
    public class Order : BaseEntity
    {
        public OrderStatus status { get; set; } = OrderStatus.Pending;
        public decimal TotalPrice { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public Address ShppingAddress { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }


    }
}
