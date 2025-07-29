using System;

namespace denyis
{
    public class Cheque
    {
        public int ChequeId { get; set; }                     // کلید اصلی
        public int PatientId { get; set; }                    // آیدی بیمار
        public int PaymentId { get; set; }                    // آیدی پرداخت (کلید خارجی)
        public string ChequeNumber { get; set; }              // شماره چک
        public decimal ChequeAmount { get; set; }             // مبلغ چک
        public DateTime ChequeDate { get; set; }              // تاریخ چک
        public bool IsDefault { get; set; }                   // چک پیش‌فرض
        public DateTime CreatedAt { get; set; }               // تاریخ ایجاد
    }
} 