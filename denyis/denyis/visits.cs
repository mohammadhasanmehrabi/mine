using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace denyis
{
    public class Visit
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public DateTime DateVisit { get; set; }
        public DateTime DateRecord { get; set; }
        public DateTime DateTestTeeth { get; set; }
        public DateTime DateTestGeneral { get; set; }
        public DateTime DateDelivery { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Notes { get; set; }
    }
}
