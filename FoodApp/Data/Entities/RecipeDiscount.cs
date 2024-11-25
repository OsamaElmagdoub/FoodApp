namespace FoodApp.Data.Entities
{
    public class RecipeDiscount :BaseEntity
    {
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
        public int DiscountId { get; set; }
        public Discount Discount { get; set; }

        public DateTime AppliedDate { get; set; }
    }
}
