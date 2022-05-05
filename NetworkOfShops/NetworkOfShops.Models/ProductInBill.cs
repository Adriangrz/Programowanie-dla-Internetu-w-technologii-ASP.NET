using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkOfShops.Models
{
    public class ProductInBill
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public decimal? Price { get; set; }
        public int ProductInShopId { get; set; }
        public ProductInShop? ProductInShop { get; set; }
        public int BillId { get; set; }
        public Bill? Bill { get; set; }
    }
}
