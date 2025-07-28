using System;

namespace denyis
{
    public class InventoryItem
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Sku { get; set; }
        public string Category { get; set; }
        public int Quantity { get; set; }
        public int MinStock { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public string Supplier { get; set; }
        public string SupplierContact { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string StockStatus { get; set; }
        public string Notes { get; set; }
        public string ToothColor { get; set; }

        public InventoryItem()
        {
            PurchaseDate = DateTime.Now;
            StockStatus = "موجود";
        }

        // محاسبه قیمت کل
        public void CalculateTotalPrice()
        {
            TotalPrice = Quantity * UnitPrice;
        }

        // بررسی وضعیت موجودی
        public bool IsLowStock()
        {
            return Quantity <= MinStock;
        }

        // به‌روزرسانی وضعیت موجودی
        public void UpdateStockStatus()
        {
            if (Quantity == 0)
                StockStatus = "ناموجود";
            else if (IsLowStock())
                StockStatus = "کم موجود";
            else
                StockStatus = "موجود";
        }
    }
}