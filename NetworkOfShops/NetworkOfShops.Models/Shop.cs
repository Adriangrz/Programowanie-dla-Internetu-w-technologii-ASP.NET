using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkOfShops.Models
{
    public class Shop
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Town { get; set; }
        public string Street { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string UserId { get; set; }
        public AplicationUser? User { get; set; }
        public ICollection<Product>? Products { get; set; }
        public ICollection<Order>? Orders { get; set; }
        public ICollection<Bill>? Bills { get; set; }
        public ICollection<ProductInShop>? ProductsInShop { get; set; }

    }
}
