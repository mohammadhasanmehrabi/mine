using System;

namespace denyis
{
    public class Tooth
    {
        public int Id { get; set; }                         // آیدی اصلی
        public int PatientId { get; set; }                  // آیدی بیمار
        public string ToothName { get; set; }               // نام دندان (مثلاً "فک بالا - سانترال چپ")
        public string ToothType { get; set; }               // نوع دندان (مولار، پرمولر، کانین، اینسیزور)
        public decimal UnitPrice { get; set; }              // قیمت واحد درمان
        public decimal TotalPrice { get; set; }             // قیمت کل درمان
        public string ToothSize { get; set; }               // اندازه دندان
        public string ToothColor { get; set; }              // رنگ دندان
        public DateTime CreatedAt { get; set; }             // زمان ثبت
    }
}