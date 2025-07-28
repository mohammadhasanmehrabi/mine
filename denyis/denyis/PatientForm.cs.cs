using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace denyis
{
    public partial class PatientForm : Form
    {
        private HashSet<string> selectedTeeth = new HashSet<string>();
        List<ToothButton> toothButtons = new List<ToothButton>();
        private string toothImagePath = "";
        private string doctorSignPath = "";
        private List<Cheque> cheques = new List<Cheque>();
        private MySqlManager mysqlManager;
        private bool inventoryDecreased = false; // برای جلوگیری از کم کردن مکرر موجودی
        // در بالای کلاس این متغیرها را اضافه کنید:
        private DateTime selectedPersianDate = DateTime.Now;

        // متد برای نمایش تقویم شمسی
        private void ShowPersianCalendar()
        {
            using (var form = new Form())
            {
                form.Text = "انتخاب تاریخ شمسی";
                form.Size = new Size(300, 250);
                form.StartPosition = FormStartPosition.CenterParent;
                form.FormBorderStyle = FormBorderStyle.FixedDialog;
                form.MaximizeBox = false;
                form.MinimizeBox = false;

                var monthCalendar = new MonthCalendar();
                monthCalendar.Location = new Point(10, 10);
                monthCalendar.Size = new Size(260, 180);

                // تبدیل تاریخ میلادی به شمسی برای نمایش
                PersianCalendar pc = new PersianCalendar();
                var persianDate = pc.ToDateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0, 0);
                monthCalendar.SetDate(persianDate);

                var btnOK = new Button();
                btnOK.Text = "تأیید";
                btnOK.Location = new Point(100, 200);
                btnOK.DialogResult = DialogResult.OK;

                form.Controls.Add(monthCalendar);
                form.Controls.Add(btnOK);
                form.AcceptButton = btnOK;

                if (form.ShowDialog() == DialogResult.OK)
                {
                    selectedPersianDate = monthCalendar.SelectionStart;
                    lblDateValue.Text = ConvertToPersianDate(selectedPersianDate);
                }
            }
        }
        private DateTime ShowPersianCalendarForDateTimePicker(DateTime currentValue)
        {
            using (var calendarForm = new Form())
            {
                calendarForm.Text = "انتخاب تاریخ شمسی";
                calendarForm.Size = new Size(350, 400);
                calendarForm.StartPosition = FormStartPosition.CenterParent;
                calendarForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                calendarForm.MaximizeBox = false;
                calendarForm.MinimizeBox = false;

                // تبدیل تاریخ میلادی به شمسی
                PersianCalendar pc = new PersianCalendar();
                int persianYear = pc.GetYear(currentValue);
                int persianMonth = pc.GetMonth(currentValue);
                int persianDay = pc.GetDayOfMonth(currentValue);

                // ایجاد کنترل‌های تقویم شمسی
                var lblYear = new Label { Text = $"سال: {persianYear}", Location = new Point(10, 10), Width = 100 };
                var lblMonth = new Label { Text = $"ماه: {persianMonth}", Location = new Point(120, 10), Width = 100 };
                
                var btnPrevMonth = new Button { Text = "ماه قبل", Location = new Point(10, 40), Width = 80 };
                var btnNextMonth = new Button { Text = "ماه بعد", Location = new Point(100, 40), Width = 80 };
                var btnToday = new Button { Text = "امروز", Location = new Point(190, 40), Width = 80 };

                // روزهای هفته
                string[] weekDays = { "ش", "ی", "د", "س", "چ", "پ", "ج" };
                for (int i = 0; i < 7; i++)
                {
                    var lblDay = new Label { Text = weekDays[i], Location = new Point(10 + i * 45, 80), Width = 40, TextAlign = ContentAlignment.MiddleCenter };
                    calendarForm.Controls.Add(lblDay);
                }

                // نمایش روزهای ماه
                var dayButtons = new List<Button>();
                int currentYear = persianYear;
                int currentMonth = persianMonth;

                void UpdateCalendar()
                {
                    // پاک کردن دکمه‌های قبلی
                    foreach (var btn in dayButtons)
                    {
                        calendarForm.Controls.Remove(btn);
                    }
                    dayButtons.Clear();

                    // محاسبه روزهای ماه
                    int daysInMonth = pc.GetDaysInMonth(currentYear, currentMonth);
                    DateTime firstDayOfMonth = pc.ToDateTime(currentYear, currentMonth, 1, 0, 0, 0, 0);
                    int firstDayOfWeek = (int)firstDayOfMonth.DayOfWeek;

                    // تنظیم برچسب‌ها
                    lblYear.Text = $"سال: {currentYear}";
                    lblMonth.Text = $"ماه: {currentMonth}";

                    int dayCounter = 1;
                    for (int week = 0; week < 6; week++)
                    {
                        for (int dayOfWeek = 0; dayOfWeek < 7; dayOfWeek++)
                        {
                            var btn = new Button
                            {
                                Width = 40,
                                Height = 30,
                                Location = new Point(10 + dayOfWeek * 45, 110 + week * 35),
                                TextAlign = ContentAlignment.MiddleCenter
                            };

                            if ((week == 0 && dayOfWeek < firstDayOfWeek) || dayCounter > daysInMonth)
                            {
                                btn.Text = "";
                                btn.Enabled = false;
                            }
                            else
                            {
                                btn.Text = dayCounter.ToString();
                                int currentDay = dayCounter;
                                btn.Click += (s, e) =>
                                {
                                    selectedPersianDate = pc.ToDateTime(currentYear, currentMonth, currentDay, 0, 0, 0, 0);
                                    calendarForm.Close();
                                };
                                dayCounter++;
                            }

                            dayButtons.Add(btn);
                            calendarForm.Controls.Add(btn);
                        }
                    }
                }

                btnPrevMonth.Click += (s, e) =>
                {
                    if (currentMonth == 1)
                    {
                        currentMonth = 12;
                        currentYear--;
                    }
                    else
                    {
                        currentMonth--;
                    }
                    UpdateCalendar();
                };

                btnNextMonth.Click += (s, e) =>
                {
                    if (currentMonth == 12)
                    {
                        currentMonth = 1;
                        currentYear++;
                    }
                    else
                    {
                        currentMonth++;
                    }
                    UpdateCalendar();
                };

                btnToday.Click += (s, e) =>
                {
                    selectedPersianDate = DateTime.Now;
                    calendarForm.Close();
                };

                calendarForm.Controls.Add(lblYear);
                calendarForm.Controls.Add(lblMonth);
                calendarForm.Controls.Add(btnPrevMonth);
                calendarForm.Controls.Add(btnNextMonth);
                calendarForm.Controls.Add(btnToday);

                UpdateCalendar();
                calendarForm.ShowDialog();

                return selectedPersianDate;
            }
        }
        public PatientForm()
        {
            InitializeComponent();
            mysqlManager = new MySqlManager();
            SetupTeethComboBoxes();
            SetupChequeDataGridView();
            SetupAdditionalTeethFeatures();
        }
        private void ToothButton_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            if (btn == null) return;

            string toothId = btn.Tag?.ToString();  // یکتا و قابل ذخیره در دیتابیس

            if (selectedTeeth.Contains(toothId))
            {
                selectedTeeth.Remove(toothId);
                btn.BackColor = SystemColors.Control;
                btn.ForeColor = Color.Black;
            }
            else
            {
                selectedTeeth.Add(toothId);
                btn.BackColor = Color.DarkBlue;
                btn.ForeColor = Color.White;
                
                // اگر تعداد دندان‌ها به 28 رسید، موجودی کم کن
                if (selectedTeeth.Count == 28)
                {
                    DecreaseInventoryForCompleteSet();
                }
            }

            // نمایش لیست دندان‌های انتخاب‌شده
            txtSelectedTeeth.Text = string.Join(" / ", selectedTeeth.Select(t => t.Trim()));
            
            // محاسبه قیمت کل
            CalculateTotalPrice();
        }
        private void SelectAllTeeth(bool selectAll)
        {
            selectedTeeth.Clear();
            if (selectAll)
            {
                // اضافه کردن فقط 28 دندان (بدون دندان‌های عقل)
                int selectedCount = 0;
                foreach (Button btn in panelTeeth.Controls.OfType<Button>())
                {
                    // دندان‌های عقل معمولاً در موقعیت‌های خاصی هستند
                    // ما فقط 28 دندان اول را انتخاب می‌کنیم
                    if (selectedCount < 28)
                    {
                        btn.BackColor = Color.LightBlue;
                        btn.ForeColor = Color.Black;
                        selectedTeeth.Add(btn.Tag?.ToString() ?? btn.Text);
                        selectedCount++;
                    }
                }
                
                // کم کردن موجودی از انبار اگر تمام 28 دندان انتخاب شدند
                DecreaseInventoryForCompleteSet();
            }
            else
            {
                // پاک کردن انتخاب تمام دندان‌ها
                foreach (Button btn in panelTeeth.Controls.OfType<Button>())
                {
                    btn.BackColor = SystemColors.Control;
                    btn.ForeColor = Color.Black;
                }
            }
            UpdateSelectedTeethText();
        }
        private bool ValidateForm()
        {
            // گروه اطلاعات بیمار
            if (string.IsNullOrEmpty(txtFirstName.Text) ||
                string.IsNullOrEmpty(txtLastName.Text) ||
                string.IsNullOrEmpty(txtPhone.Text))
            {
                MessageBox.Show("لطفاً اطلاعات بیمار را کامل کنید", "خطا",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // گروه وضعیت پرونده
            if (string.IsNullOrEmpty(txtVisitReason.Text) ||
                cmbTreatmentStatus.SelectedIndex == -1)
            {
                MessageBox.Show("لطفاً وضعیت پرونده را کامل کنید", "خطا",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // گروه اطلاعات ویزیت و درمان
            if (dtpVisitDate.Value == null ||
                dtpFvisitDate.Value == null)
            {
                MessageBox.Show("لطفاً تاریخ‌های ویزیت را مشخص کنید", "خطا",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // گروه اطلاعات پرداخت
            if (cmbPaymentType.SelectedIndex == -1)
            {
                MessageBox.Show("لطفاً اطلاعات پرداخت را کامل کنید", "خطا",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // بررسی انتخاب حداقل یک دندان
            if (selectedTeeth.Count == 0)
            {
                MessageBox.Show("لطفاً حداقل یک دندان انتخاب کنید", "خطا",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }
        private void ClearForm()
        {
            // پاک کردن اطلاعات بیمار
            txtFirstName.Clear();
            txtLastName.Clear();
            txtPhone.Clear();

            // پاک کردن وضعیت پرونده
            txtVisitReason.Clear();
            cmbTreatmentStatus.SelectedIndex = -1;
            txtDoctorNote.Clear();

            // پاک کردن اطلاعات ویزیت
            dtpVisitDate.Value = DateTime.Now;
            dtpCentricRecord.Value = DateTime.Now;
            dtpToothTest.Value = DateTime.Now;
            dtpFinalTest.Value = DateTime.Now;
            dtpDeliveryDate.Value = DateTime.Now;
            dtpFvisitDate.Value = DateTime.Now;
            txtVisitNotes.Clear();

            // پاک کردن اطلاعات پرداخت
            cmbPaymentType.SelectedIndex = -1;
            txtPaymentNotes.Clear();
            cheques.Clear();
            RefreshChequeDataGridView();

            // پاک کردن دندان‌های انتخاب شده
            txtSelectedTeeth.Clear();
            selectedTeeth.Clear();
            chkSelectAllTeeth.Checked = false;
            inventoryDecreased = false; // ریست کردن علامت کم کردن موجودی

            // ریست کردن ComboBox های دندان
            cmbTeethSize.SelectedIndex = 1; // متوسط
            cmbTeethColor.SelectedIndex = 0; // A1

            // پاک کردن انتخاب دندان‌ها
            foreach (Button btn in panelTeeth.Controls.OfType<Button>())
            {
                btn.BackColor = SystemColors.Control;
                btn.ForeColor = Color.Black;
            }

            // پاک کردن ویژگی‌های اضافی
            comboBaseFractureTop.SelectedIndex = 0;
            comboBaseFractureBottom.SelectedIndex = 0;
            checkSoftLayerTop.Checked = false;
            checkSoftLayerBottom.Checked = false;
            checkHardRedTop.Checked = false;
            checkHardRedBottom.Checked = false;
            checkHardClearTop.Checked = false;
            checkHardClearBottom.Checked = false;
            // پاک کردن chkSkeshen
            var chkSkeshen = Controls.Find("chkSkeshen", true).FirstOrDefault() as CheckBox;
            if (chkSkeshen != null)
            {
                chkSkeshen.Checked = false;
            }

            // پاک کردن قیمت‌های اضافی
            txtPriceBaseFracture.Clear();
            txtPriceSoftLayer.Clear();
            txtPriceHardRedLayer.Clear();
            txtPriceHardClearLayer.Clear();
            saksionprice.Clear();

            // پاک کردن تصاویر
            picToothImage.Image = null;
            picDoctorSign.Image = null;
            
            // پاک کردن قیمت‌ها
            txtunit_price.Clear();
            txttotal_price.Clear();
            
            // محاسبه مجدد قیمت کل
            CalculateTotalPrice();
        }
        private string ConvertToPersianDate(DateTime date)
        {
            PersianCalendar pc = new PersianCalendar();
            return $"{pc.GetYear(date)}/{pc.GetMonth(date):00}/{pc.GetDayOfMonth(date):00}";
        }

        private DateTime ConvertFromPersianDate(string persianDate)
        {
            PersianCalendar pc = new PersianCalendar();
            string[] parts = persianDate.Split('/');
            int year = int.Parse(parts[0]);
            int month = int.Parse(parts[1]);
            int day = int.Parse(parts[2]);
            return pc.ToDateTime(year, month, day, 0, 0, 0, 0);
        }
        private DateTime ConvertToPersianDateTime(DateTime gregorianDate)
        {
            PersianCalendar pc = new PersianCalendar();
            return pc.ToDateTime(gregorianDate.Year, gregorianDate.Month, gregorianDate.Day,
                                gregorianDate.Hour, gregorianDate.Minute, gregorianDate.Second, 0);
        }

        private DateTime ConvertFromPersianDateTime(DateTime gregorianDate)
        {
            // این متد تاریخ میلادی رو به میلادی برمی‌گردونه (بدون تغییر)
            // چون DateTimePicker ها میلادی هستند
            return gregorianDate;
        }

        private DateTime ConvertPersianToGregorian(string persianDate)
        {
            try
            {
                PersianCalendar pc = new PersianCalendar();
                string[] parts = persianDate.Split('/');
                if (parts.Length == 3)
                {
                    int year = int.Parse(parts[0]);
                    int month = int.Parse(parts[1]);
                    int day = int.Parse(parts[2]);
                    return pc.ToDateTime(year, month, day, 0, 0, 0, 0);
                }
            }
            catch
            {
                // در صورت خطا، تاریخ امروز برگردان
            }
            return DateTime.Now;
        }

        private string GetEnglishStatus(string persianStatus)
        {
            switch (persianStatus)
            {
                case "در حال درمان": return "در حال درمان";
                case "تمام شده": return "تمام شده";
                case "نیاز به پیگیری": return "نیاز به پیگیری ";
                default: return "در حال درمان";
            }
        }

        private void SetPersianCalendar(DateTimePicker dtp)
        {
            dtp.Format = DateTimePickerFormat.Custom;
            dtp.CustomFormat = "yyyy/MM/dd";
            dtp.Value = ConvertToPersianDateTime(DateTime.Now);
        }

        private void SetupTeethComboBoxes()
        {
            // تنظیم ComboBox رنگ دندان
            cmbTeethColor.Items.Clear();
            cmbTeethColor.Items.AddRange(new object[] { 
                "A1", "A2", "A3", "A3.5", "A4", 
                "B1", "B2", "B3", "B4", 
                "C1", "C2", "C3", "C4", 
                "D2", "D3", "D4" 
            });
            cmbTeethColor.SelectedIndex = 0; // پیش‌فرض: A1

            // تنظیم ComboBox نوع دندان از انبار
            LoadToothTypesFromInventory();
        }

        private void LoadToothTypesFromInventory()
        {
            try
            {
                cmbToothType.Items.Clear();
                cmbToothType.Items.Add("انتخاب کنید"); // گزینه پیش‌فرض
                
                // بارگذاری محصولات دندان از انبار
                var dentalItems = mysqlManager.GetDentalItemsFromInventory();
                foreach (var item in dentalItems)
                {
                    cmbToothType.Items.Add(item.ProductName);
                }
                
                cmbToothType.SelectedIndex = 0; // پیش‌فرض: انتخاب کنید
                
                // اضافه کردن event handler
                cmbToothType.SelectedIndexChanged += CmbToothType_SelectedIndexChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطا در بارگذاری انواع دندان: {ex.Message}", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CmbToothType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbToothType.SelectedIndex > 0) // اگر گزینه "انتخاب کنید" انتخاب نشده
                {
                    string selectedToothType = cmbToothType.SelectedItem.ToString();
                    
                    // دریافت اطلاعات دندان از انبار
                    var dentalItems = mysqlManager.GetDentalItemsFromInventory();
                    var selectedItem = dentalItems.FirstOrDefault(item => item.ProductName == selectedToothType);
                    
                    if (selectedItem != null)
                    {
                        // تنظیم رنگ دندان
                        cmbTeethColor.Text = selectedItem.ToothColor;
                        
                        // تنظیم قیمت واحد
                        txtunit_price.Text = selectedItem.UnitPrice.ToString("N0");
                        
                        // نمایش پیام
                        MessageBox.Show($"رنگ دندان: {selectedItem.ToothColor}\nقیمت: {selectedItem.UnitPrice:N0} تومان", 
                            "اطلاعات دندان", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطا در بارگذاری اطلاعات دندان: {ex.Message}", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // متد جدید برای کم کردن موجودی از انبار
        private void DecreaseInventoryForCompleteSet()
        {
            try
            {
                if (cmbToothType.SelectedIndex > 0 && selectedTeeth.Count == 28 && !inventoryDecreased) // اگر نوع دندان انتخاب شده و تمام 28 دندان انتخاب شده و قبلاً کم نشده
                {
                    string selectedToothType = cmbToothType.SelectedItem.ToString();
                    
                    // دریافت اطلاعات دندان از انبار
                    var dentalItems = mysqlManager.GetDentalItemsFromInventory();
                    var selectedItem = dentalItems.FirstOrDefault(item => item.ProductName == selectedToothType);
                    
                    if (selectedItem != null && selectedItem.Quantity > 0)
                    {
                        // کم کردن 1 عدد از موجودی
                        selectedItem.Quantity -= 1;
                        
                        // به‌روزرسانی در دیتابیس
                        mysqlManager.UpdateInventoryItem(selectedItem);
                        
                        // علامت‌گذاری که موجودی کم شده
                        inventoryDecreased = true;
                        
                        MessageBox.Show($"1 عدد از موجودی '{selectedToothType}' کم شد.\nموجودی جدید: {selectedItem.Quantity} عدد", 
                            "کم کردن موجودی", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (selectedItem != null && selectedItem.Quantity <= 0)
                    {
                        MessageBox.Show($"موجودی '{selectedToothType}' تمام شده است!", 
                            "هشدار موجودی", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (selectedItem == null)
                    {
                        MessageBox.Show($"نوع دندان '{selectedToothType}' در انبار یافت نشد!", 
                            "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطا در کم کردن موجودی: {ex.Message}", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateSelectedTeethText()
        {
            // نمایش لیست دندان‌های انتخاب‌شده
            txtSelectedTeeth.Text = string.Join(" / ", selectedTeeth.Select(t => t.Trim()));
            
            // محاسبه قیمت کل
            CalculateTotalPrice();
        }

        private void SetupChequeDataGridView()
        {
            try
            {
                dgvCheques.AutoGenerateColumns = false;
                dgvCheques.Columns.Clear();
                dgvCheques.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvCheques.AllowUserToAddRows = false;
                dgvCheques.AllowUserToDeleteRows = false;
                dgvCheques.ReadOnly = true;

                dgvCheques.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "Number",
                    HeaderText = "شماره چک",
                    Width = 120
                });

                dgvCheques.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "Amount",
                    HeaderText = "مبلغ",
                    Width = 100,
                    DefaultCellStyle = new DataGridViewCellStyle { Format = "N0" }
                });

                dgvCheques.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "Date",
                    HeaderText = "تاریخ چک",
                    Width = 120,
                    DefaultCellStyle = new DataGridViewCellStyle { Format = "yyyy/MM/dd" }
                });

                dgvCheques.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "Description",
                    HeaderText = "توضیحات",
                    Width = 150
                });

                // اضافه کردن یک چک پیش‌فرض
                cheques.Clear();
                cheques.Add(new Cheque
                {
                    Number = "1",
                    Amount = 0,
                    Date = DateTime.Now,
                    Description = "چک پیش‌فرض"
                });
                RefreshChequeDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطا در تنظیم جدول چک‌ها: {ex.Message}", "خطا", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CalculateTotalPrice()
        {
            try
            {
                decimal totalPrice = 0;

                // محاسبه قیمت دندان‌های انتخاب شده
                decimal unitPrice = 0;
                if (decimal.TryParse(txtunit_price.Text, out unitPrice))
                {
                    int selectedTeethCount = selectedTeeth.Count;
                    totalPrice += unitPrice * selectedTeethCount;
                }

                // محاسبه قیمت Base Fracture
                decimal baseFracturePrice = 0;
                if (decimal.TryParse(txtPriceBaseFracture.Text, out baseFracturePrice))
                {
                    // اگر فک بالا انتخاب شده
                    if (comboBaseFractureTop.SelectedIndex > 0)
                    {
                        totalPrice += baseFracturePrice;
                    }
                    // اگر فک پایین انتخاب شده
                    if (comboBaseFractureBottom.SelectedIndex > 0)
                    {
                        totalPrice += baseFracturePrice;
                    }
                }

                // محاسبه قیمت Soft Layer
                decimal softLayerPrice = 0;
                if (decimal.TryParse(txtPriceSoftLayer.Text, out softLayerPrice))
                {
                    // اگر فک بالا انتخاب شده
                    if (chkSoftTop.Checked)
                    {
                        totalPrice += softLayerPrice;
                    }
                    // اگر فک پایین انتخاب شده
                    if (chksoftdown.Checked)
                    {
                        totalPrice += softLayerPrice;
                    }
                }

                // محاسبه قیمت Hard Red Layer
                decimal hardRedPrice = 0;
                if (decimal.TryParse(txtPriceHardRedLayer.Text, out hardRedPrice))
                {
                    // اگر فک بالا انتخاب شده
                    if (chkHardRedTop.Checked)
                    {
                        totalPrice += hardRedPrice;
                    }
                    // اگر فک پایین انتخاب شده
                    if (chkHardRedDown.Checked)
                    {
                        totalPrice += hardRedPrice;
                    }
                }

                // محاسبه قیمت Hard Clear Layer
                decimal hardClearPrice = 0;
                if (decimal.TryParse(txtPriceHardClearLayer.Text, out hardClearPrice))
                {
                    // اگر فک بالا انتخاب شده
                    if (chkHardClearTop.Checked)
                    {
                        totalPrice += hardClearPrice;
                    }
                    // اگر فک پایین انتخاب شده
                    if (chkHardClearDown.Checked)
                    {
                        totalPrice += hardClearPrice;
                    }
                }

                // محاسبه قیمت Saksion
                decimal saksionPrice = 0;
                if (decimal.TryParse(saksionprice.Text, out saksionPrice))
                {
                    if (chkSakshen.Checked)
                    {
                        totalPrice += saksionPrice;
                    }
                }

                txttotal_price.Text = totalPrice.ToString("0.00");
            }
            catch
            {
                txttotal_price.Text = "0.00";
            }
        }

        private void chkSelectAllTeeth_CheckedChanged(object sender, EventArgs e)
        {
            SelectAllTeeth(chkSelectAllTeeth.Checked);
        }
        private void panelHeader_Paint(object sender, PaintEventArgs e)
        {
            // در صورت نیاز می‌تونی طراحی دلخواه اینجا بنویسی
        }
        private void cmbTreatmentStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            // در صورت نیاز، منطق لازم رو بعداً اضافه کن
        }
        private void lblCreatedAt_Click(object sender, EventArgs e)
        {
            // اینجا می‌تونی کدی بنویسی یا خالی بذاری
        }

        private void lblLastName_Click(object sender, EventArgs e)
        {
            // اینجا می‌تونی کدی بنویسی یا خالی بذاری
        }

        private void PatientForm_Load(object sender, EventArgs e)
        {
            // تنظیم تاریخ‌های پیش‌فرض به شمسی
            lblDateValue.Text = ConvertToPersianDate(DateTime.Now);

            // تنظیم DateTimePicker ها به تقویم ایرانی
            // حذف شده - DateTimePicker ها میلادی باقی می‌مانند

            // حذف شده - DateTimePicker ها میلادی باقی می‌مانند

            // تنظیم ComboBox های دندان
            SetupTeethComboBoxes();
            
            // محاسبه اولیه قیمت کل
            CalculateTotalPrice();
        }

        // حذف شده - DateTimePicker ها میلادی باقی می‌مانند

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void chkSelectAllTeeth_CheckedChanged_1(object sender, EventArgs e)
        {
            SelectAllTeeth(chkSelectAllTeeth.Checked);
        }

        private void txtunit_price_TextChanged(object sender, EventArgs e)
        {
            CalculateTotalPrice();
        }

        private void btnAddCheque_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtChequeNumber.Text) || string.IsNullOrEmpty(txtChequeAmount.Text))
                {
                    MessageBox.Show("لطفاً شماره چک و مبلغ را وارد کنید", "هشدار",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!decimal.TryParse(txtChequeAmount.Text, out decimal amount))
                {
                    MessageBox.Show("لطفاً مبلغ معتبر وارد کنید", "هشدار",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var cheque = new Cheque
                {
                    Number = txtChequeNumber.Text.Trim(),
                    Amount = amount,
                    Date = dtpChequeDate.Value,
                    Description = txtPaymentNotes.Text.Trim()
                };

                cheques.Add(cheque);
                RefreshChequeDataGridView();

                // پاک کردن فیلدها
                txtChequeNumber.Clear();
                txtChequeAmount.Clear();
                txtPaymentNotes.Clear();
                dtpChequeDate.Value = DateTime.Now;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطا در اضافه کردن چک: {ex.Message}", "خطا",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRemoveCheque_Click(object sender, EventArgs e)
        {
            try
            {
                // بررسی انتخاب ردیف
                if (dgvCheques.SelectedRows.Count == 0)
                {
                    MessageBox.Show("لطفاً یک چک را انتخاب کنید", "هشدار", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // حذف چک انتخاب شده
                int selectedIndex = dgvCheques.SelectedRows[0].Index;
                if (selectedIndex >= 0 && selectedIndex < cheques.Count)
                {
                    cheques.RemoveAt(selectedIndex);
                    RefreshChequeDataGridView();
                    MessageBox.Show("چک با موفقیت حذف شد", "موفقیت", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"خطا در حذف چک - Index: {selectedIndex}, Count: {cheques.Count}", "خطا", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطا در حذف چک: {ex.Message}", "خطا", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AdditionalFeature_CheckedChanged(object sender, EventArgs e)
        {
            // Debug: نمایش اطلاعات checkbox
            if (sender is CheckBox checkBox)
            {
                Console.WriteLine($"CheckBox {checkBox.Name} changed to {checkBox.Checked}");
            }
            CalculateTotalPrice();
        }

        private void ComboBaseFracture_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalculateTotalPrice();
        }

        private void PriceTextBox_TextChanged(object sender, EventArgs e)
        {
            CalculateTotalPrice();
        }



        private void RefreshChequeDataGridView()
        {
            try
            {
                dgvCheques.DataSource = null;
                dgvCheques.DataSource = cheques;
                
                // تنظیم نمایش تاریخ به فارسی
                if (dgvCheques.Columns["Date"] != null)
                {
                    dgvCheques.Columns["Date"].DefaultCellStyle.Format = "yyyy/MM/dd";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطا در به‌روزرسانی جدول چک‌ها: {ex.Message}", "خطا", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupAdditionalTeethFeatures()
        {
            // اضافه کردن event handlers برای checkbox ها
            chkSoftTop.CheckedChanged += AdditionalFeature_CheckedChanged;
            chksoftdown.CheckedChanged += AdditionalFeature_CheckedChanged;
            chkHardRedTop.CheckedChanged += AdditionalFeature_CheckedChanged;
            chkHardRedDown.CheckedChanged += AdditionalFeature_CheckedChanged;
            chkHardClearTop.CheckedChanged += AdditionalFeature_CheckedChanged;
            chkHardClearDown.CheckedChanged += AdditionalFeature_CheckedChanged;
            chkSakshen.CheckedChanged += AdditionalFeature_CheckedChanged;

            // اضافه کردن event handlers برای combo box ها
            comboBaseFractureTop.SelectedIndexChanged += ComboBaseFracture_SelectedIndexChanged;
            comboBaseFractureBottom.SelectedIndexChanged += ComboBaseFracture_SelectedIndexChanged;

            // اضافه کردن event handlers برای text box های قیمت
            txtPriceBaseFracture.TextChanged += PriceTextBox_TextChanged;
            txtPriceSoftLayer.TextChanged += PriceTextBox_TextChanged;
            txtPriceHardRedLayer.TextChanged += PriceTextBox_TextChanged;
            txtPriceHardClearLayer.TextChanged += PriceTextBox_TextChanged;
            saksionprice.TextChanged += PriceTextBox_TextChanged;
        }

        private void btnClearForm_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // اعتبارسنجی فرم
                if (!ValidateForm())
                    return;

                var mysqlManager = new MySqlManager();
                int patientId = 0;

                // 1. ذخیره اطلاعات بیمار
                var patient = new Patient
                {
                    FirstName = txtFirstName.Text.Trim(),
                    LastName = txtLastName.Text.Trim(),
                    Phone = txtPhone.Text.Trim(),
                    CreatedAt = DateTime.Now
                };
                patientId = mysqlManager.AddPatient(patient);

                if (patientId > 0)
                {
                    // 2. ذخیره وضعیت پرونده (cases)
                    string selectedStatus = cmbTreatmentStatus.SelectedItem?.ToString() ?? "در حال درمان";
                    var caseInfo = new Case
                    {
                        PatientId = patientId,
                        Status = GetEnglishStatus(selectedStatus),
                        Description = txtDoctorNote.Text.Trim(),
                        VisitReason = txtVisitReason.Text.Trim(),
                        LastUpdate = DateTime.Now
                    };
                    mysqlManager.AddCase(caseInfo);

                    // 3. ذخیره اطلاعات ویزیت (visits)
                    var visit = new Visit
                    {
                        PatientId = patientId,
                        DateVisit = dtpVisitDate.Value,
                        DateRecord = dtpCentricRecord.Value,
                        DateTestTeeth = dtpToothTest.Value,
                        DateTestGeneral = dtpFinalTest.Value,
                        DateDelivery = dtpDeliveryDate.Value,
                        Notes = txtVisitNotes.Text.Trim()
                    };
                    mysqlManager.AddVisit(visit);

                    // 4. ذخیره اطلاعات پرداخت (payments)
                    // ذخیره چک‌ها
                    string selectedPaymentType = cmbPaymentType.SelectedItem?.ToString() ?? "نقد";
                    
                    if (cheques.Count > 0)
                    {
                        // ذخیره هر چک به صورت جداگانه
                        foreach (var cheque in cheques)
                        {
                            var payment = new Payment
                            {
                                PatientId = patientId,
                                PaymentType = "چک",
                                Amount = cheque.Amount,
                                PaidAt = DateTime.Now,
                                ChequeNumber = cheque.Number,
                                ChequeDate = cheque.Date,
                                Notes = cheque.Description
                            };
                            mysqlManager.AddPayment(payment);
                        }
                    }
                    else
                    {
                        // اگر چکی نباشد، یک رکورد پرداخت نقدی ذخیره کن
                        var payment = new Payment
                        {
                            PatientId = patientId,
                            PaymentType = selectedPaymentType,
                            Amount = 0,
                            PaidAt = DateTime.Now,
                            Notes = txtPaymentNotes.Text.Trim()
                        };
                        mysqlManager.AddPayment(payment);
                    }

                    // 5. ذخیره دندان‌های انتخاب شده (teeth) - همه در یک رکورد
                    if (selectedTeeth.Count > 0)
                    {
                        decimal unitPrice = 0;
                        decimal.TryParse(txtunit_price.Text, out unitPrice);
                        
                        // محاسبه قیمت کل شامل ویژگی‌های اضافی
                        decimal totalPrice = unitPrice * selectedTeeth.Count;
                        
                        // اضافه کردن قیمت Base Fracture
                        decimal baseFracturePrice = 0;
                        if (decimal.TryParse(txtPriceBaseFracture.Text, out baseFracturePrice))
                        {
                            if (comboBaseFractureTop.SelectedIndex > 0) totalPrice += baseFracturePrice;
                            if (comboBaseFractureBottom.SelectedIndex > 0) totalPrice += baseFracturePrice;
                        }
                        
                        // اضافه کردن قیمت Soft Layer
                        decimal softLayerPrice = 0;
                        if (decimal.TryParse(txtPriceSoftLayer.Text, out softLayerPrice))
                        {
                            if (chkSoftTop.Checked) totalPrice += softLayerPrice;
                            if (chksoftdown.Checked) totalPrice += softLayerPrice;
                        }
                        
                        // اضافه کردن قیمت Hard Red Layer
                        decimal hardRedPrice = 0;
                        if (decimal.TryParse(txtPriceHardRedLayer.Text, out hardRedPrice))
                        {
                            if (chkHardRedTop.Checked) totalPrice += hardRedPrice;
                            if (chkHardRedDown.Checked) totalPrice += hardRedPrice;
                        }
                        
                        // اضافه کردن قیمت Hard Clear Layer
                        decimal hardClearPrice = 0;
                        if (decimal.TryParse(txtPriceHardClearLayer.Text, out hardClearPrice))
                        {
                            if (chkHardClearTop.Checked) totalPrice += hardClearPrice;
                            if (chkHardClearDown.Checked) totalPrice += hardClearPrice;
                        }
                        
                        // اضافه کردن قیمت Saksion
                        decimal saksionPrice = 0;
                        if (decimal.TryParse(saksionprice.Text, out saksionPrice))
                        {
                            if (chkSakshen.Checked)
                            {
                                totalPrice += saksionPrice;
                            }
                        }
                        
                        var tooth = new Tooth
                        {
                            PatientId = patientId,
                            ToothName = string.Join(" / ", selectedTeeth),
                            ToothType = cmbToothType.SelectedItem?.ToString() ?? "انتخابی",
                            UnitPrice = unitPrice,
                            TotalPrice = totalPrice,
                            ToothSize = cmbTeethSize.SelectedItem?.ToString() ?? "متوسط",
                            ToothColor = cmbTeethColor.SelectedItem?.ToString() ?? "A1",
                            CreatedAt = DateTime.Now,
                            BaseFractureTop = comboBaseFractureTop.SelectedIndex > 0 ? comboBaseFractureTop.SelectedItem?.ToString() : "",
                            BaseFractureBottom = comboBaseFractureBottom.SelectedIndex > 0 ? comboBaseFractureBottom.SelectedItem?.ToString() : "",
                            SoftLayerTop = chkSoftTop.Checked,
                            SoftLayerBottom = chksoftdown.Checked,
                            HardRedLayerTop = chkHardRedTop.Checked,
                            HardRedLayerBottom = chkHardRedDown.Checked,
                            HardClearLayerTop = chkHardClearTop.Checked,
                            HardClearLayerBottom = chkHardClearDown.Checked,
                            Saksion = GetSaksionCheckBoxValue(),
                            PriceBaseFracture = baseFracturePrice,
                            PriceSoftLayer = softLayerPrice,
                            PriceHardRedLayer = hardRedPrice,
                            PriceHardClearLayer = hardClearPrice,
                            PriceSaksion = saksionPrice
                        };
                        mysqlManager.AddTooth(tooth);
                    }
                    // 6. ذخیره تصاویر (images) - اگر آپلود شده باشند
                    if (!string.IsNullOrEmpty(toothImagePath))
                    {
                        var toothImage = new Image
                        {
                            PatientId = patientId,
                            Description = "عکس دندان",
                            ImageData = File.ReadAllBytes(toothImagePath),
                            CreatedAt = DateTime.Now
                        };
                        mysqlManager.AddImage(toothImage);
                    }

                    if (!string.IsNullOrEmpty(doctorSignPath))
                    {
                        var signImage = new Image
                        {
                            PatientId = patientId,
                            Description = "امضای بیمار",
                            ImageData = File.ReadAllBytes(doctorSignPath),
                            CreatedAt = DateTime.Now
                        };
                        mysqlManager.AddImage(signImage);
                    }
                    MessageBox.Show("اطلاعات بیمار با موفقیت ذخیره شد", "موفقیت",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // پاک کردن فرم
                    ClearForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطا در ذخیره اطلاعات: {ex.Message}", "خطا",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPickDate_Click(object sender, EventArgs e)
        {
            ShowPersianCalendar();
        }

        private void btnUploadToothImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "تصاویر|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                openFileDialog.Title = "انتخاب عکس دندان";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        picToothImage.Image = System.Drawing.Image.FromFile(openFileDialog.FileName);                        // ذخیره مسیر فایل برای استفاده بعدی
                        toothImagePath = openFileDialog.FileName;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"خطا در بارگذاری تصویر: {ex.Message}", "خطا",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnUploadDoctorSign_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "تصاویر|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                openFileDialog.Title = "انتخاب امضای بیمار";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        picDoctorSign.Image = System.Drawing.Image.FromFile(openFileDialog.FileName);                        // ذخیره مسیر فایل برای استفاده بعدی
                        doctorSignPath = openFileDialog.FileName;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"خطا در بارگذاری تصویر: {ex.Message}", "خطا",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private bool GetSaksionCheckBoxValue()
        {
            try
            {
                return chkSakshen.Checked;
            }
            catch
            {
                return false;
            }
        }
    }
}
