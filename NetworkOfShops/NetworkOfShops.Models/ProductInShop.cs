using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkOfShops.Models
{
    public class ProductInShop
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public int ShopId { get; set; }
        public Shop? Shop { get; set; }
        public decimal PriceInShop { get; set; }
        public string ProductImage { get; set; }
        public ICollection<ProductInBill>? ProductInBills { get; set; }
    }
}
