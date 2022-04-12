using Microsoft.AspNetCore.Identity;
using NetworkOfShops.Models;

namespace NetworkOfShops.Data
{
    public class AplicationDbInitializer
    {
        private readonly AplicationDbContext _context;
        public AplicationDbInitializer(AplicationDbContext aplicationDbContext)
        {
            _context = aplicationDbContext;
        }
        public void Seed()
        {
            if (!_context.Shops.Any()) CreateShops(_context);
            if (!_context.Orders.Any()) CreateOrders(_context);
            if (!_context.ProductsInShop.Any()) CreateProductInShop(_context);

        }
        private void CreateShops(AplicationDbContext aplicationDbContext)
        {
            var shop1 = new Shop()
            {
                Id = 1,
                Name = "Biedronka",
                Description = "sdsds",
                Town = "Warszawa",
                Street = "Wodna 2",
                Telephone = "56543433",
                Email = "b@gmail.com"
            };
            var products1 = new Product() { Id = 1, Name = "mekovita", Description = "sasasc", Price = 23, ShopId = 1 };
            var shop2 = new Shop()
            {
                Id = 2,
                Name = "Aldi",
                Description = "sdsdds",
                Town = "Wrocław",
                Street = "Wodna 42",
                Telephone = "567234897",
                Email = "a@gmail.com"
            };
            var products2 = new Product() { Id = 2, Name = "mlek", Description = "sasasc", Price = 23, ShopId = 2 };
            aplicationDbContext.Shops.AddRange(shop1, shop2);
            aplicationDbContext.Products.AddRange(products1, products2);
            aplicationDbContext.SaveChanges();
        }
        private void CreateOrders(AplicationDbContext aplicationDbContext)
        {
            var order1 = new Order()
            {
                Id = 1,
                ShopId = 1,
                CreationDate = DateTime.Now,
                Status = OrderStatus.Rozpoczęte
            };
            var orderDetails1 = new OrderDetails() { Id = 1, Amount = 23, Price = 23, OrderId = 1, ProductId = 1};
            var order2 = new Order()
            {
                Id = 2,
                ShopId = 2,
                CreationDate = DateTime.Now,
                Status = OrderStatus.Rozpoczęte
            };
            var orderDetails2 = new OrderDetails() { Id = 2, Amount = 43, Price = 55, OrderId = 2, ProductId = 2 };
            aplicationDbContext.Orders.AddRange(order1, order2);
            aplicationDbContext.OrderDetails.AddRange(orderDetails1, orderDetails2);
            aplicationDbContext.SaveChanges();
        }
        private void CreateProductInShop(AplicationDbContext aplicationDbContext)
        {
            var productInShop1 = new ProductInShop()
            {
                Id = 1,
                ShopId = 1,
                ProductId = 1,
                PriceInShop = 13
            };
            var productInShop2 = new ProductInShop()
            {
                Id = 2,
                ShopId = 2,
                ProductId = 2,
                PriceInShop = 33
            };
            aplicationDbContext.ProductsInShop.AddRange(productInShop1, productInShop2);
            aplicationDbContext.SaveChanges();
        }
    }
}
