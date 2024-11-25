using FoodApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodApp.Data.Configurations
{
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(o => o.status).HasConversion
                (
                    status => status.ToString(),
                    status => (OrderStatus)Enum.Parse(typeof(OrderStatus), status)
               );

            builder.OwnsOne(order => order.ShppingAddress,
                         shippingAddress => shippingAddress.WithOwner());

            builder.Property(o => o.TotalPrice)
                .HasColumnType("decimal(12,2)");
        }
    }
}
