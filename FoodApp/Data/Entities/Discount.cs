namespace FoodApp.Data.Entities
{
    public class Discount :BaseEntity
    {
        public decimal DiscountPercent { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive => /*DateTime.UtcNow >= StartDate &&*/ DateTime.UtcNow <= EndDate;
        public ICollection<RecipeDiscount> RecipeDiscounts { get; set; } = new List<RecipeDiscount>();

    }
}
