using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkOfShops.Models
{
    public class Order
    {
        public int Id { get; set; }
        public Shop Shop { get; set; }
        public DateTime CreatedDate { get; set; }
        public OrderStatus Status { get; set; }
        public ICollection<OrderDetails> Details { get; set; }
    }

    public enum OrderStatus
    {
        Rozpoczęte,
        ZłożoneDoRealizacji,
        WRealizacji,
        Zakonczone
    }
}
