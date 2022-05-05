using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NetworkOfShops.Models;

namespace NetworkOfShops.Data
{
    public class AplicationDbContext : IdentityDbContext<AplicationUser>
    {
        public AplicationDbContext()
        {
        }

        public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Shop> Shops { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductInShop> ProductsInShop { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }


        public DbSet<NetworkOfShops.Models.Bill> Bill { get; set; }


        public DbSet<NetworkOfShops.Models.ProductInBill> ProductInBill { get; set; }
    }
}
