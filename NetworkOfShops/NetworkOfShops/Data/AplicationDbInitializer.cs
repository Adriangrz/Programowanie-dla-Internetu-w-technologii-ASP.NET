using Microsoft.EntityFrameworkCore;
using NetworkOfShops.Models;

namespace NetworkOfShops.Data
{
    public static class AplicationDbInitializer
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            CreateShops(modelBuilder);
        }

        private static void CreateShops(ModelBuilder modelBuilder)
        {
            var shop1 = new Shop()
            {
                Id = 1,
                Name = "Biedronka",
                Description = "sdsdds",
                Town = "Warszawa",
                Street = "Wodna 2",
                Telephone = "56543433",
                Email = "b@gmail.com",
                Products = new List<Product>()
            };
            var products1 = new List<Product>()
            {
                new Product() { Id = 1, Name = "mekovita", Description = "sasasc", Price = 23, Shop = shop1}
            };
            shop1.Products = products1;
            var shop2 = new Shop()
            {
                Id = 2,
                Name = "Aldi",
                Description = "sdsdds",
                Town = "Wrocław",
                Street = "Wodna 42",
                Telephone = "567234897",
                Email = "a@gmail.com",
                Products = new List<Product>()
            };
            var products2 = new List<Product>()
            {
                new Product() { Id = 2, Name = "mlek", Description = "sasasc", Price = 23, Shop = shop2}
            };
            shop2.Products = products2;
            modelBuilder.Entity<Shop>().HasData(
                shop1,
                shop2
            );
            modelBuilder.Entity<Product>().HasData(
                products1,
                products2
            );
        }
    }
}
