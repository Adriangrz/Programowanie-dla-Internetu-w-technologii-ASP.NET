﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkOfShops.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int ShopId { get; set; }
        public Shop Shop { get; set; }
        public ICollection<OrderDetails> OrderDetails { get; set; }
        public ICollection<ProductInShop>? ProductsInShop { get; set; }
    }
}
