namespace FoodApp.Data.Entities
{
    public class FavouriteRecipe : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; } = null!;
    }
}
