namespace FoodApp.Data.Entities
{
    public class OrderItem :BaseEntity
    {
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string RecipeName { get; set; }
        public int RecipeId { get; set; }
        public Order Order { get; set;} = null!;
        public int OrderId { get; set; }

    }
}