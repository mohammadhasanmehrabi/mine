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

        // فیلدهای جدید برای ویژگی‌های اضافی
        public string BaseFractureTop { get; set; }         // نوع سیم فک بالا
        public string BaseFractureBottom { get; set; }      // نوع سیم فک پایین
        public bool SoftLayerTop { get; set; }              // Soft Layer فک بالا
        public bool SoftLayerBottom { get; set; }           // Soft Layer فک پایین
        public bool HardRedLayerTop { get; set; }           // Hard Red Layer فک بالا
        public bool HardRedLayerBottom { get; set; }        // Hard Red Layer فک پایین
        public bool HardClearLayerTop { get; set; }         // Hard Clear Layer فک بالا
        public bool HardClearLayerBottom { get; set; }      // Hard Clear Layer فک پایین
        public bool Saksion { get; set; }                   // ساکسیون
        public decimal PriceBaseFracture { get; set; }      // قیمت Base Fracture
        public decimal PriceSoftLayer { get; set; }         // قیمت Soft Layer
        public decimal PriceHardRedLayer { get; set; }      // قیمت Hard Red Layer
        public decimal PriceHardClearLayer { get; set; }    // قیمت Hard Clear Layer
        public decimal PriceSaksion { get; set; }           // قیمت ساکسیون
    }
}