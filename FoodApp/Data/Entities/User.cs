namespace FoodApp.Data.Entities
{
    public class User : BaseEntity
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public bool IsEmailVerified { get; set; } = false;
        public string PhoneNumber { get; set; }
        public string Country { get; set; }
        public string? VerificationOTP { get; set; }
        public DateTime? VerificationOTPExpiration { get; set; }
        public string? PasswordResetOTP { get; set; }
        public DateTime? PasswordResetOTPExpiration { get; set; }
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
        public ICollection<RefreshToken>? RefreshTokens { get; set; } = new List<RefreshToken>();
        public ICollection<FavouriteRecipe> FavouriteRecipes { get; set; } = new List<FavouriteRecipe>();

    }
}
