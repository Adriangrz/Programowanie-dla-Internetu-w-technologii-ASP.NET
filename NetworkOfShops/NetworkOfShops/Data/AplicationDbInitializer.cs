using NetworkOfShops.Models;

namespace NetworkOfShops.Data
{
    public class AplicationDbInitializer
    {
        private readonly AplicationDbContext _context;
        public AplicationDbInitializer(AplicationDbContext aplicationDbContext)
        {
            this._context = aplicationDbContext;
        }
        public void Seed()
        {
            if (!_context.Shops.Any()) CreateShops(_context);

        }
        private void CreateShops(AplicationDbContext aplicationDbContext)
        {
            var shop1 = new Shop()
            {
                Id = 1,
                Name = "Biedronka",
                Description = "sdsdds",
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
    }
}
