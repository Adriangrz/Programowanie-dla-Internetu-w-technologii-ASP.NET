﻿using System;
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
        public int ShopId { get; set; }
        public decimal PriceInShop { get; set; }
    }
}