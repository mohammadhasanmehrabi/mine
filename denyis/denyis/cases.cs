using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace denyis
{
    public class Case
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string Status { get; set; } // 'active', 'completed', 'cancelled'
        public string Description { get; set; }
        public string VisitReason { get; set; } // علت مراجعه
        public DateTime LastUpdate { get; set; }
    }
}
