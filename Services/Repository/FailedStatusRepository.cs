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
    public class FailedStatusRepository : GenericRepository<FailedStatus>, IFailedStatusRepository
    {
        public FailedStatusRepository(ErrorQueueContext context) : base(context)
        {
        }
    }
}
