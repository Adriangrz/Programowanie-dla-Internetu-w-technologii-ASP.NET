using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetworkOfShops.Models;

namespace NetworkOfShops.Data.Configuration
{
    public class BillConfiguration : IEntityTypeConfiguration<Bill>
    {
        public void Configure(EntityTypeBuilder<Bill> builder)
        {
            builder.HasOne<Shop>(b => b.Shop)
                .WithMany(s => s.Bills)
                .HasForeignKey(b => b.ShopId);
        }
    }
}
