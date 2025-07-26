using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Globalization;

namespace denyis
{
    public partial class PatientForm : Form
    {
        private HashSet<string> selectedTeeth = new HashSet<string>();
        List<ToothButton> toothButtons = new List<ToothButton>();
        private string toothImagePath = "";
        private string doctorSignPath = "";
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
            }

            // نمایش لیست دندان‌های انتخاب‌شده بر اساس Tag (نه Text)
            txtSelectedTeeth.Text = string.Join(" / ", selectedTeeth.Select(t => t.Trim()));
            
            // محاسبه قیمت کل
            CalculateTotalPrice();
        }
        private void SelectAllTeeth(bool selectAll)
        {
            selectedTeeth.Clear();

            foreach (Button btn in panelTeeth.Controls.OfType<Button>())
            {
                string tag = btn.Tag?.ToString();
                if (string.IsNullOrEmpty(tag)) continue;

                if (selectAll)
                {
                    selectedTeeth.Add(tag);
                    btn.BackColor = Color.DarkBlue;
                    btn.ForeColor = Color.White;
                }
                else
                {
                    btn.BackColor = SystemColors.Control;
                    btn.ForeColor = Color.Black;
                }
            }

            // به‌روزرسانی تکست‌باکس
            txtSelectedTeeth.Text = string.Join(" / ", selectedTeeth.Select(t => t.Trim()));
            
            // محاسبه قیمت کل
            CalculateTotalPrice();
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
            if (string.IsNullOrEmpty(txtTotalAmount.Text) ||
                cmbPaymentType.SelectedIndex == -1)
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
            txtTotalAmount.Clear();
            cmbPaymentType.SelectedIndex = -1;
            txtCheckNumber.Clear();
            dateTimePicker1dtpCheckDate.Value = DateTime.Now;
            txtPaymentNotes.Clear();

            // پاک کردن دندان‌های انتخاب شده
            txtSelectedTeeth.Clear();
            selectedTeeth.Clear();
            chkSelectAllTeeth.Checked = false;

            // ریست کردن ComboBox های دندان
            cmbTeethSize.SelectedIndex = 1; // متوسط
            cmbTeethColor.SelectedIndex = 0; // A1

            // پاک کردن انتخاب دندان‌ها
            foreach (Button btn in panelTeeth.Controls.OfType<Button>())
            {
                btn.BackColor = SystemColors.Control;
                btn.ForeColor = Color.Black;
            }

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
            // تنظیم ComboBox اندازه دندان
            cmbTeethSize.Items.Clear();
            cmbTeethSize.Items.AddRange(new object[] { "کوچک", "متوسط", "بزرگ" });
            cmbTeethSize.SelectedIndex = 1; // پیش‌فرض: متوسط

            // تنظیم ComboBox رنگ دندان
            cmbTeethColor.Items.Clear();
            cmbTeethColor.Items.AddRange(new object[] { 
                "A1", "A2", "A3", "A3.5", "A4", 
                "B1", "B2", "B3", "B4", 
                "C1", "C2", "C3", "C4", 
                "D2", "D3", "D4" 
            });
            cmbTeethColor.SelectedIndex = 0; // پیش‌فرض: A1
        }

        private void CalculateTotalPrice()
        {
            try
            {
                decimal unitPrice = 0;
                if (decimal.TryParse(txtunit_price.Text, out unitPrice))
                {
                    int selectedTeethCount = selectedTeeth.Count;
                    decimal totalPrice = unitPrice * selectedTeethCount;
                    txttotal_price.Text = totalPrice.ToString("0.00");
                }
                else
                {
                    txttotal_price.Text = "0.00";
                }
            }
            catch
            {
                txttotal_price.Text = "0.00";
            }
        }

        private void chkSelectAllTeeth_CheckedChanged(object sender, EventArgs e)
        {
            txtSelectedTeeth.Clear();

            foreach (var tooth in toothButtons)
            {
                if (chkSelectAllTeeth.Checked)
                {
                    tooth.SelectTooth();
                    txtSelectedTeeth.AppendText(tooth.ToothName + Environment.NewLine);
                }
                else
                {
                    tooth.DeselectTooth();
                }
            }
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
                    decimal totalAmount = 0;
                    if (decimal.TryParse(txtTotalAmount.Text, out totalAmount))
                    {
                        string selectedPaymentType = cmbPaymentType.SelectedItem?.ToString() ?? "نقد";
                        var payment = new Payment
                        {
                            PatientId = patientId,
                            PaymentType = selectedPaymentType,
                            Amount = totalAmount,
                            PaidAt = DateTime.Now,
                            ChequeNumber = txtCheckNumber.Text.Trim(),
                            ChequeDate = dateTimePicker1dtpCheckDate.Value,
                            Notes = txtPaymentNotes.Text.Trim()
                        };
                        mysqlManager.AddPayment(payment);
                    }

                    // 5. ذخیره دندان‌های انتخاب شده (teeth) - همه در یک رکورد
                    if (selectedTeeth.Count > 0)
                    {
                        decimal unitPrice = 0;
                        decimal.TryParse(txtunit_price.Text, out unitPrice);
                        
                        var tooth = new Tooth
                        {
                            PatientId = patientId,
                            ToothName = string.Join(" / ", selectedTeeth),
                            ToothType = "انتخابی",
                            UnitPrice = unitPrice,
                            TotalPrice = unitPrice * selectedTeeth.Count,
                            ToothSize = cmbTeethSize.SelectedItem?.ToString() ?? "متوسط",
                            ToothColor = cmbTeethColor.SelectedItem?.ToString() ?? "A1",
                            CreatedAt = DateTime.Now
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
    }
}
