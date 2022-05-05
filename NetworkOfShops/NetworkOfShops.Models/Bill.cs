using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkOfShops.Models
{
    public class Bill
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public decimal? ToPay { get; set; }
        public int ShopId { get; set; }
        public Shop? Shop { get; set; }
        public ICollection<ProductInBill>? ProductsInBill { get; set; }
    }
}
