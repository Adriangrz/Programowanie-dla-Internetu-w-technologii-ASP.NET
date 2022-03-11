using System;
using System.Collections.Generic;
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
        public float Price { get; set; }
        public int ShopId { get; set; }
        public Shop? Shop { get; set; }
        public ICollection<OrderDetails>? OrderDetails { get; set; }
    }
}
