using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetworkOfShops.Models;

namespace NetworkOfShops.Data.Configuration
{
    public class ProductInBillConfiguration : IEntityTypeConfiguration<ProductInBill>
    {
        public void Configure(EntityTypeBuilder<ProductInBill> builder)
        {
            builder.HasOne<ProductInShop>(p => p.ProductInShop)
                .WithMany(s => s.ProductInBills)
                .HasForeignKey(p => p.ProductInShopId);

            builder.HasOne<Bill>(p => p.Bill)
                .WithMany(b => b.ProductsInBill)
                .HasForeignKey(p => p.BillId);
        }
    }
}
