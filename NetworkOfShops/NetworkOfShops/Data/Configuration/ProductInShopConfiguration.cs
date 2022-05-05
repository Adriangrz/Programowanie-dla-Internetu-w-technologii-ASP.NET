using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetworkOfShops.Models;

namespace NetworkOfShops.Data.Configuration
{
    public class ProductInShopConfiguration : IEntityTypeConfiguration<ProductInShop>
    {
        public void Configure(EntityTypeBuilder<ProductInShop> builder)
        {
            builder.HasOne<Product>(ps => ps.Product)
                .WithMany(p => p.ProductsInShop)
                .HasForeignKey(ps => ps.ProductId);

            builder.HasOne<Shop>(ps => ps.Shop)
                .WithMany(s => s.ProductsInShop)
                .HasForeignKey(ps => ps.ShopId);
        }
    }
}
