using FoodApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ProjectManagementSystem.Data.Context
{
    public class ApplicationDBContext :DbContext 
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
            :base(options) 
        {
           // ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Role> roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Discount> discounts { get; set; }
        public DbSet<FavouriteRecipe> favouriteRecipes { get; set; }
        public DbSet<OrderItem> orderItems { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<Invoice> invoices { get; set; }


    }
}
