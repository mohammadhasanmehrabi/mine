using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace denyis
{
    public class Payment
    {
        public int PaymentId { get; set; }                    // کلید اصلی
        public int PatientId { get; set; }                    // آیدی بیمار
        public decimal TotalAmount { get; set; }              // مبلغ کل
        public string PaymentMethod { get; set; }             // روش پرداخت (نقد، کارت، چک، معاوضه)
        public string Notes { get; set; }                     // توضیحات
        public int ChequeCount { get; set; }                  // تعداد چک‌ها
        public DateTime CreatedAt { get; set; }               // تاریخ ایجاد
    }
}
