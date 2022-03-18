using System.ComponentModel.DataAnnotations;

namespace NetworkOfShops.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [Range(1, 1000.99)]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        public int ShopId { get; set; }
        public ShopViewModel Shop { get; set; }
    }
}
