using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkOfShops.Models
{
    public class OrderDetails
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
    }
}
