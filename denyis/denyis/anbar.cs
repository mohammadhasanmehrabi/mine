using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace denyis
{
    public partial class anbar : Form
    {
        private MySqlManager mysqlManager;
        private InventoryItem currentItem;
        private bool isEditMode = false;

        public anbar()
        {
            InitializeComponent();
            mysqlManager = new MySqlManager();
            InitializeForm();
        }

        private void InitializeForm()
        {
            // تنظیم ComboBox ها
            cmbCategory.Items.AddRange(new string[] { "دندان", "دندان‌پزشکی", "ابزار پزشکی", "مواد مصرفی", "تجهیزات", "سایر" });
            cmbStockStatus.Items.AddRange(new string[] { "موجود", "کم موجود", "ناموجود" });
            
            // تنظیم ComboBox رنگ دندان
            cmbTeethColor.Items.AddRange(new object[] { 
                "A1", "A2", "A3", "A3.5", "A4", 
                "B1", "B2", "B3", "B4", 
                "C1", "C2", "C3", "C4", 
                "D2", "D3", "D4" 
            });
            cmbTeethColor.SelectedIndex = 0; // پیش‌فرض: A1

            // تنظیم DataGridView
            dgvInventory.AutoGenerateColumns = false;
            dgvInventory.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            
            // تنظیم ستون‌های DataGridView
            colId.DataPropertyName = "Id";
            colProductName.DataPropertyName = "ProductName";
            colSku.DataPropertyName = "Sku";
            colCategory.DataPropertyName = "Category";
            colQuantity.DataPropertyName = "Quantity";
            colMinStock.DataPropertyName = "MinStock";
            colUnitPrice.DataPropertyName = "UnitPrice";
            colSupplier.DataPropertyName = "Supplier";
            colPurchaseDate.DataPropertyName = "PurchaseDate";
            colStockStatus.DataPropertyName = "StockStatus";
            colNotes.DataPropertyName = "Notes";

            // بارگذاری اولیه
            LoadInventoryData();
            UpdateStatistics();
            ClearForm();

            // اضافه کردن Event Handlers
            btnSave.Click += BtnSave_Click;
            btnClear.Click += BtnClear_Click;
            btnDelete.Click += BtnDelete_Click;
            btnEdite.Click += BtnEdite_Click;
            btnSearch.Click += BtnSearch_Click;
            btnImport.Click += BtnImport_Click;
            btnExport.Click += BtnExport_Click;
            dgvInventory.CellClick += DgvInventory_CellClick;
            txtSearch.TextChanged += TxtSearch_TextChanged;

            // Event handlers برای محاسبه خودکار قیمت کل
            txtQuantity.TextChanged += CalculateTotalPrice;
            txtUnitPrice.TextChanged += CalculateTotalPrice;
        }

        private void LoadInventoryData()
        {
            try
            {
                var items = mysqlManager.GetAllInventoryItems();
                dgvInventory.DataSource = items;
                UpdateStatistics();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطا در بارگذاری داده‌ها: {ex.Message}", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateStatistics()
        {
            try
            {
                var stats = mysqlManager.GetInventoryStatistics();
                
                // Debug: نمایش مقادیر
                Console.WriteLine($"TotalProducts: {stats["TotalProducts"]}");
                Console.WriteLine($"TotalValue: {stats["TotalValue"]}");
                Console.WriteLine($"DentalItems: {stats["DentalItems"]}");
                Console.WriteLine($"LowStockItems: {stats["LowStockItems"]}");
                
                lblTotalProducts.Text = $"تعداد کل محصولات = {stats["TotalProducts"]}";
                lblTotalValue.Text = $"ارزش کل انبار = {stats["TotalValue"]:N0} تومان";
                lblDentalItems.Text = $"تعداد محصولات دندان‌پزشکی = {stats["DentalItems"]}";
                lblLowStockItems.Text = $"محصولات کم‌موجود = {stats["LowStockItems"]}";

                // بررسی هشدار موجودی کم
                CheckLowStockWarning();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطا در به‌روزرسانی آمار: {ex.Message}", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CheckLowStockWarning()
        {
            try
            {
                // تغییر کوئری برای نمایش محصولات که موجودی آن‌ها <= (حداقل + 2) است
                var lowStockItems = mysqlManager.GetLowStockItems();
                var warningItems = lowStockItems.Where(item => item.Quantity <= (item.MinStock + 2) && item.Quantity > 0).ToList();
                
                if (warningItems.Count > 0)
                {
                    var message = "⚠️ هشدار موجودی کم:\n\n";
                    foreach (var item in warningItems.Take(5)) // فقط 5 مورد اول
                    {
                        message += $"• {item.ProductName}: {item.Quantity} عدد (حداقل: {item.MinStock}, هشدار از: {item.MinStock + 2})\n";
                    }
                    if (warningItems.Count > 5)
                        message += $"\nو {warningItems.Count - 5} محصول دیگر...";
             
                    MessageBox.Show(message, "هشدار موجودی کم", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception)
            {
                 //خطا را نادیده می‌گیریم تا برنامه متوقف نشود
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateForm())
                    return;

                var item = new InventoryItem
                {
                    ProductName = txtProductName.Text.Trim(),
                    Sku = txtSku.Text.Trim(),
                    Category = cmbCategory.Text,
                    Quantity = int.Parse(txtQuantity.Text),
                    MinStock = int.Parse(txtMinStock.Text),
                    UnitPrice = decimal.Parse(txtUnitPrice.Text),
                    Supplier = txtSupplier.Text.Trim(),
                    SupplierContact = txtSupplierContact.Text.Trim(),
                    PurchaseDate = dateTimePicker1.Value,
                    ToothColor = cmbTeethColor.SelectedItem?.ToString() ?? "A1",
                    StockStatus = "موجود", // مقدار پیش‌فرض
                    Notes = txtNotes.Text.Trim()
                };

                item.CalculateTotalPrice();
                item.UpdateStockStatus();

                // بررسی هشدار موجودی کم قبل از ذخیره
                if (item.Quantity <= (item.MinStock + 2) && item.Quantity > 0)
                {
                    var result = MessageBox.Show(
                        $"⚠️ هشدار: محصول '{item.ProductName}' نزدیک به اتمام موجودی می‌باشد!\n\n" +
                        $"موجودی فعلی: {item.Quantity} عدد\n" +
                        $"حداقل موجودی: {item.MinStock} عدد\n" +
                        $"هشدار از: {item.MinStock + 2} عدد\n\n" +
                        "آیا می‌خواهید ادامه دهید؟",
                        "هشدار موجودی کم",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                    if (result == DialogResult.No)
                    {
                        return;
                    }
                }

                if (isEditMode && currentItem != null)
                {
                    item.Id = currentItem.Id;
                    mysqlManager.UpdateInventoryItem(item);
                    MessageBox.Show("محصول با موفقیت ویرایش شد.", "موفقیت", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    mysqlManager.AddInventoryItem(item);
                    MessageBox.Show("محصول با موفقیت اضافه شد.", "موفقیت", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                LoadInventoryData();
                ClearForm();
                isEditMode = false;
                currentItem = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطا در ذخیره‌سازی: {ex.Message}", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(txtProductName.Text))
            {
                MessageBox.Show("لطفاً نام محصول را وارد کنید.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtProductName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtSku.Text))
            {
                MessageBox.Show("لطفاً کد محصول را وارد کنید.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSku.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(cmbCategory.Text))
            {
                MessageBox.Show("لطفاً دسته‌بندی را انتخاب کنید.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbCategory.Focus();
                return false;
            }

            if (!int.TryParse(txtQuantity.Text, out int quantity) || quantity < 0)
            {
                MessageBox.Show("لطفاً تعداد معتبر وارد کنید.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtQuantity.Focus();
                return false;
            }

            if (!int.TryParse(txtMinStock.Text, out int minStock) || minStock < 0)
            {
                MessageBox.Show("لطفاً حداقل موجودی معتبر وارد کنید.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMinStock.Focus();
                return false;
            }

            if (!decimal.TryParse(txtUnitPrice.Text, out decimal unitPrice) || unitPrice < 0)
            {
                MessageBox.Show("لطفاً قیمت واحد معتبر وارد کنید.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUnitPrice.Focus();
                return false;
            }

            return true;
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            txtProductName.Clear();
            txtSku.Clear();
            cmbCategory.SelectedIndex = -1;
            cmbTeethColor.SelectedIndex = 0; // پیش‌فرض: A1
            txtQuantity.Clear();
            txtMinStock.Clear();
            txtUnitPrice.Clear();
            txtTotalPrice.Clear();
            txtSupplier.Clear();
            txtSupplierContact.Clear();
            dateTimePicker1.Value = DateTime.Now;
            cmbStockStatus.SelectedIndex = -1;
            txtNotes.Clear();

            isEditMode = false;
            currentItem = null;
            btnSave.Text = "💾ذخیره کرد ";
            btnDelete.Enabled = false;
            btnEdite.Enabled = false;
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (currentItem == null)
            {
                MessageBox.Show("لطفاً ابتدا یک محصول را انتخاب کنید.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show($"آیا از حذف محصول '{currentItem.ProductName}' اطمینان دارید؟", 
                "تأیید حذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    mysqlManager.DeleteInventoryItem(currentItem.Id);
                    MessageBox.Show("محصول با موفقیت حذف شد.", "موفقیت", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadInventoryData();
                    ClearForm();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"خطا در حذف محصول: {ex.Message}", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnEdite_Click(object sender, EventArgs e)
        {
            if (currentItem == null)
            {
                MessageBox.Show("لطفاً ابتدا یک محصول را انتخاب کنید.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            LoadItemToForm(currentItem);
            isEditMode = true;
            btnSave.Text = "✏️ ویرایش";
        }

        private void LoadItemToForm(InventoryItem item)
        {
            txtProductName.Text = item.ProductName;
            txtSku.Text = item.Sku;
            cmbCategory.Text = item.Category;
            cmbTeethColor.Text = item.ToothColor ?? "A1";
            txtQuantity.Text = item.Quantity.ToString();
            txtMinStock.Text = item.MinStock.ToString();
            txtUnitPrice.Text = item.UnitPrice.ToString();
            txtTotalPrice.Text = item.TotalPrice.ToString();
            txtSupplier.Text = item.Supplier;
            txtSupplierContact.Text = item.SupplierContact;
            dateTimePicker1.Value = item.PurchaseDate;
            cmbStockStatus.Text = item.StockStatus;
            txtNotes.Text = item.Notes;

            btnDelete.Enabled = true;
            btnEdite.Enabled = true;
        }

        private void DgvInventory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvInventory.Rows[e.RowIndex];
                var item = row.DataBoundItem as InventoryItem;
                if (item != null)
                {
                    currentItem = item;
                    LoadItemToForm(item);
                }
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            SearchInventory();
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                LoadInventoryData();
            }
        }

        private void SearchInventory()
        {
            try
            {
                var keyword = txtSearch.Text.Trim();
                if (string.IsNullOrWhiteSpace(keyword))
                {
                    LoadInventoryData();
                    return;
                }

                var items = mysqlManager.SearchInventoryItems(keyword);
                dgvInventory.DataSource = items;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطا در جستجو: {ex.Message}", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CalculateTotalPrice(object sender, EventArgs e)
        {
            try
            {
                if (int.TryParse(txtQuantity.Text, out int quantity) && 
                    decimal.TryParse(txtUnitPrice.Text, out decimal unitPrice))
                {
                    decimal totalPrice = quantity * unitPrice;
                    txtTotalPrice.Text = totalPrice.ToString("N0");
                }
                else
                {
                    txtTotalPrice.Clear();
                }
            }
            catch
            {
                txtTotalPrice.Clear();
            }
        }

        private void BtnImport_Click(object sender, EventArgs e)
        {
            MessageBox.Show("قابلیت ورودی اکسل در نسخه بعدی اضافه خواهد شد.", "اطلاع‌رسانی", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            MessageBox.Show("قابلیت خروجی اکسل در نسخه بعدی اضافه خواهد شد.", "اطلاع‌رسانی", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
