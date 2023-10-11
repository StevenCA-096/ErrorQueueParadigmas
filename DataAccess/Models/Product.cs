using DataAccess.Relations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Product:BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public int Price { get; set; }

        public List<Cart_Products> Cart_Products { get; set; }
    }
}
