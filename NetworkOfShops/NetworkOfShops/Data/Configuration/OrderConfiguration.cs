using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetworkOfShops.Models;

namespace NetworkOfShops.Data.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasOne<Shop>(o => o.Shop)
                .WithMany(s => s.Orders)
                .HasForeignKey(o => o.ShopId);
        }
    }
}
