using DataAccess.Relations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class ShoppingCart
    {
            public int TotalAmount { get; set; }
            public int SubTotal { get; set; }

            public int Total { get; set; }

            public List<Cart_Products> Cart_Products { get; set; }
        
    }
}
