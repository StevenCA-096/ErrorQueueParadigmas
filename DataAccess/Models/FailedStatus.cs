using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class FailedStatus
    {
        public string Id { get; set; }
        public int NumberOfDuplicates { get; set; }
        public string Status { get; set; }

        public FailedStatus()
        {
            Status = "FailedRepeatedly";
        }
    }
}
