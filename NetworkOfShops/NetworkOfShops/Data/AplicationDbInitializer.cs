using Microsoft.AspNetCore.Identity;
using NetworkOfShops.Models;

namespace NetworkOfShops.Data
{
    public class AplicationDbInitializer
    {
        private readonly AplicationDbContext _context;
        private readonly UserManager<AplicationUser> _userManager;
        public AplicationDbInitializer(AplicationDbContext aplicationDbContext, UserManager<AplicationUser> userManager)
        {
            _context = aplicationDbContext;
            _userManager = userManager;
        }
        public void Seed()
        {
            if (!_context.Shops.Any()) CreateShops(_context).Wait();
            if (!_context.Orders.Any()) CreateOrders(_context);
            if (!_context.ProductsInShop.Any()) CreateProductInShop(_context);

        }
        private async Task CreateShops(AplicationDbContext aplicationDbContext)
        {
            var manager = await _userManager.FindByNameAsync("manager@test.pl");
            var staff = await _userManager.FindByNameAsync("staff@test.pl");
            var shop1 = new Shop()
            {
                Id = 1,
                Name = "Biedronka",
                Description = "sdsds",
                Town = "Warszawa",
                Street = "Wodna 2",
                Telephone = "56543433",
                Email = "b@gmail.com",
                UserId = manager.Id
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
                Email = "a@gmail.com",
                UserId = staff.Id
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
            var orderDetails1 = new OrderDetails() { Id = 1, Amount = 23,Email = "Antek@wp.pl", Surname = "Kowalski", Name = "Antek", Price = 23, OrderId = 1, ProductId = 1};
            var order2 = new Order()
            {
                Id = 2,
                ShopId = 2,
                CreationDate = DateTime.Now,
                Status = OrderStatus.Rozpoczęte
            };
            var orderDetails2 = new OrderDetails() { Id = 2, Amount = 43,Email="Antek@wp.pl",Surname="Kowalski",Name="Antek", Price = 55, OrderId = 2, ProductId = 2 };
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
                PriceInShop = 13,
                ProductImage = ""
            };
            var productInShop2 = new ProductInShop()
            {
                Id = 2,
                ShopId = 2,
                ProductId = 2,
                PriceInShop = 33,
                ProductImage = ""
            };
            aplicationDbContext.ProductsInShop.AddRange(productInShop1, productInShop2);
            aplicationDbContext.SaveChanges();
        }
    }
}
