using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetworkOfShops.Models;

namespace NetworkOfShops.Data.Configuration
{
    public class ShopConfiguration : IEntityTypeConfiguration<Shop>
    {
        public void Configure(EntityTypeBuilder<Shop> builder)
        {
            builder.HasOne<AplicationUser>(s => s.User)
                .WithMany(au => au.Shops)
                .HasForeignKey(s => s.UserId);
        }
    }
}
