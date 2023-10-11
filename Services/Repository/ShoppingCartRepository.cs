using DataAccess.Context;
using DataAccess.Models;
using Services.Generic;
using Services.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository
{
    public class ShoppingCartRepository : GenericRepository<ShoppingCart>, IShoppingCartRepository
    {
        public ShoppingCartRepository(ErrorQueueContext context) : base(context)
        {
        }

    }
}
