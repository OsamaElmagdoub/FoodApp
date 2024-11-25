namespace FoodApp.Data.Entities
{
    public class RefreshToken :BaseEntity
    { 
        public string Token { get; set; }
        public DateTime ExpiresOn { get; set; }
        public bool IsExpired => DateTime.UtcNow >= ExpiresOn;
        public DateTime CreatedOn { get; set; }
        public DateTime? RevokedOn { get; set; } 
        public bool IsActive => RevokedOn == null && !IsExpired;
        public User User { get; set; }
        public int UserId { get; set; }

    }
}
