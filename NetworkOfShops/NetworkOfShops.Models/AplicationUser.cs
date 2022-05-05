using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkOfShops.Models
{
    public class AplicationUser : IdentityUser
    {
        public ICollection<Shop> Shops { get; set; }
        public ICollection<Bill> Bills { get; set; }
    }
}
