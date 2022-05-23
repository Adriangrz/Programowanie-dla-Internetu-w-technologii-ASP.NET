using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetworkOfShops.Models;

namespace NetworkOfShops.Controllers
{
    public class ShopsController : Controller
    {
         private static List<ShopViewModel> shopViewModels = new List<ShopViewModel>(){
                new ShopViewModel { Id = 1, Name = "Biedronka", Email = "b@gmail.com", Street = "Wodna 5", Town = "Warszawa", Telephone = "556778332"},
                new ShopViewModel { Id = 2, Name = "Aldi", Email = "a@gmail.com", Street = "Wodna 25", Town = "Katowice", Telephone = "856778332"},
                new ShopViewModel { Id = 3, Name = "Lidl", Email = "l@gmail.com", Street = "Wodna 35", Town = "Bielsko-Biała", Telephone = "956778332"},
                new ShopViewModel { Id = 4, Name = "Biedronka", Email = "b2@gmail.com", Street = "Wodna 45", Town = "Bielsko-Biała", Telephone = "356778332"},
                new ShopViewModel { Id = 5, Name = "Lidl", Email = "l2@gmail.com", Street = "Wodna 55", Town = "Katowice", Telephone = "256778332"},
                new ShopViewModel { Id = 6, Name = "Aldi", Email = "a2@gmail.com", Street = "Wodna 65", Town = "Cieszyn", Telephone = "776778332"},
                new ShopViewModel { Id = 7, Name = "Kosmos", Email = "k@gmail.com", Street = "Wodna 75", Town = "Cieszyn", Telephone = "886778332"},
                new ShopViewModel { Id = 8, Name = "Biedronka", Email = "b3@gmail.com", Street = "Wodna 85", Town = "Żywiec", Telephone = "353778332"}
        };
        private static ShopItemViewModel shopItemViewModel = new ShopItemViewModel { Shops = shopViewModels };
        // GET: ShopsController
        [ResponseCache(Duration = 120)]
        public ActionResult Index()
        {
            return View(shopItemViewModel);
        }

        // GET: ShopsController/Details/5
        [ResponseCache(Duration = 120, VaryByQueryKeys = new[] { "id" })]
        public ActionResult Details(int id)
        {
            return View(shopItemViewModel.Shops.FirstOrDefault(x=>x.Id == id));
        }
        
    }
}
