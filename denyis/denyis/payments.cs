using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace denyis
{
    public class Payment
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string PaymentType { get; set; } // 'cash', 'card', 'cheque'
        public decimal Amount { get; set; }
        public DateTime PaidAt { get; set; }
        public int ChequeCount { get; set; }
        public string ChequeNumber { get; set; }
        public DateTime ChequeDate { get; set; }
        public string Notes { get; set; }
    }
}
