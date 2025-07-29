using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace denyis
{
    public partial class SearchForm : Form
    {
        private MySqlManager mysqlManager;
        private HashSet<string> selectedTeeth = new HashSet<string>();
        private int currentPatientId = 0;

        public SearchForm()
        {
            InitializeComponent();
            mysqlManager = new MySqlManager();
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void panelHeader_Paint(object sender, PaintEventArgs e)
        {

        }

        private void SearchForm_Load(object sender, EventArgs e)
        {
            LoadAllPatients();
            SetupDataGridView();
            SetupTeethComboBoxes();
            SetupToothButtons();
        }

        private void SetupToothButtons()
        {
            // اضافه کردن event handler برای تمام دکمه‌های دندان
            foreach (Control control in panelTeeth.Controls)
            {
                if (control is Button btn && !string.IsNullOrEmpty(btn.Tag?.ToString()))
                {
                    btn.Click += ToothButton_Click;
                }
            }
        }

        private void LoadAllPatients()
        {
            try
            {
                var patients = mysqlManager.GetAllPatients();
                dgvPatients.DataSource = null;
                dgvPatients.DataSource = patients;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطا در بارگذاری بیماران: {ex.Message}", "خطا",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupDataGridView()
        {
            dgvPatients.AutoGenerateColumns = false;
            dgvPatients.Columns.Clear();

            dgvPatients.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Id",
                HeaderText = "شناسه",
                Width = 80
            });

            dgvPatients.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "FirstName",
                HeaderText = "نام",
                Width = 120
            });

            dgvPatients.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "LastName",
                HeaderText = "نام خانوادگی",
                Width = 150
            });

            dgvPatients.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Phone",
                HeaderText = "شماره تماس",
                Width = 120
            });

            dgvPatients.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "CreatedAt",
                HeaderText = "تاریخ ثبت",
                Width = 120
            });
        }

        private void SetupTeethComboBoxes()
        {
            // تنظیم ComboBox اندازه دندان
            cmbTeethSize.Items.Clear();
            cmbTeethSize.Items.AddRange(new object[] { "کوچک", "متوسط", "بزرگ" });

            // تنظیم ComboBox رنگ دندان
            cmbTeethColor.Items.Clear();
            cmbTeethColor.Items.AddRange(new object[] {
                "A1", "A2", "A3", "A3.5", "A4",
                "B1", "B2", "B3", "B4",
                "C1", "C2", "C3", "C4",
                "D2", "D3", "D4"
            });

            // تنظیم ComboBox نوع دندان
            LoadToothTypesFromInventory();
        }

        private void LoadToothTypesFromInventory()
        {
            try
            {
                cmbToothType.Items.Clear();
                cmbToothType.Items.Add("انتخاب کنید");
                var dentalItems = mysqlManager.GetDentalItemsFromInventory();
                foreach (var item in dentalItems)
                {
                    cmbToothType.Items.Add(item.ProductName);
                }
                cmbToothType.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطا در بارگذاری انواع دندان: {ex.Message}", "خطا",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadChequesToDataGridView(List<Cheque> cheques)
        {
            try
            {
                if (dgvCheques != null)
                {
                    dgvCheques.AutoGenerateColumns = false;
                    dgvCheques.Columns.Clear();
                    
                    dgvCheques.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        DataPropertyName = "ChequeNumber",
                        HeaderText = "شماره چک",
                        Width = 120
                    });

                    dgvCheques.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        DataPropertyName = "ChequeAmount",
                        HeaderText = "مبلغ",
                        Width = 100,
                        DefaultCellStyle = new DataGridViewCellStyle { Format = "N0" }
                    });

                    dgvCheques.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        DataPropertyName = "ChequeDate",
                        HeaderText = "تاریخ چک",
                        Width = 120,
                        DefaultCellStyle = new DataGridViewCellStyle { Format = "yyyy/MM/dd" }
                    });

                    dgvCheques.DataSource = cheques;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطا در بارگذاری چک‌ها: {ex.Message}", "خطا",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvPatients_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvPatients.Rows[e.RowIndex];
                if (row.DataBoundItem is Patient patient)
                {
                    currentPatientId = patient.Id;
                    LoadPatientDetails(currentPatientId);
                }
            }
        }

        private void LoadPatientDetails(int patientId)
        {
            try
            {
                // 1. بارگذاری اطلاعات بیمار
                var patient = mysqlManager.GetPatientById(patientId);
                if (patient != null)
                {
                    txtFirstName.Text = patient.FirstName;
                    txtLastName.Text = patient.LastName;
                    txtPhone.Text = patient.Phone;
                    lblDateValue.Text = patient.CreatedAt.ToString("yyyy/MM/dd");
                }

                // 2. بارگذاری اطلاعات وضعیت پرونده (cases)
                var cases = mysqlManager.GetCasesByPatientId(patientId);
                if (cases.Count > 0)
                {
                    var caseInfo = cases[0];
                    txtVisitReason.Text = caseInfo.VisitReason ?? "";
                    cmbTreatmentStatus.Text = caseInfo.Status ?? "";
                    txtDoctorNote.Text = caseInfo.Description ?? "";
                    
                    // دیباگ - نمایش اطلاعات در کنسول
                    Console.WriteLine($"VisitReason: '{caseInfo.VisitReason}'");
                    Console.WriteLine($"Status: '{caseInfo.Status}'");
                    Console.WriteLine($"Description: '{caseInfo.Description}'");
                }
                else
                {
                    // اگر هیچ رکوردی در جدول cases نباشه
                    txtVisitReason.Text = "";
                    cmbTreatmentStatus.Text = "";
                    txtDoctorNote.Text = "";
                    Console.WriteLine("هیچ رکوردی در جدول cases یافت نشد");
                }

                // 3. بارگذاری اطلاعات ویزیت (visits)
                var visits = mysqlManager.GetVisitsByPatientId(patientId);
                if (visits.Count > 0)
                {
                    var visit = visits[0];
                    dtpVisitDate.Value = visit.DateVisit;
                    dtpFvisitDate.Value = visit.DateVisit;
                    dtpToothTest.Value = visit.DateTestTeeth;
                    dtpFinalTest.Value = visit.DateTestGeneral;
                    dtpDeliveryDate.Value = visit.DateDelivery;
                    dtpCentricRecord.Value = visit.DateRecord;
                    txtVisitNotes.Text = visit.Notes;
                }

                // 4. بارگذاری اطلاعات پرداخت (payments)
                var payments = mysqlManager.GetPaymentsByPatientId(patientId);
                if (payments.Count > 0)
                {
                    var payment = payments[0];
                    cmbPaymentType.Text = payment.PaymentMethod ?? "";
                    txtPaymentNotes.Text = payment.Notes ?? "";
                    
                    // بارگذاری چک‌ها
                    var cheques = mysqlManager.GetChequesByPatientId(patientId);
                    LoadChequesToDataGridView(cheques);
                    
                    // نمایش تعداد چک‌ها و مبلغ کل
                    if (txtTottalCheque != null)
                    {
                        txtTottalCheque.Text = payment.ChequeCount.ToString();
                    }
                    if (totalchekmony != null)
                    {
                        totalchekmony.Text = payment.TotalAmount.ToString("N0");
                    }
                }

                // 5. بارگذاری اطلاعات دندان‌ها (teeth)
                var teeth = mysqlManager.GetTeethByPatientId(patientId);
                if (teeth.Count > 0)
                {
                    var tooth = teeth[0];
                    txtunit_price.Text = tooth.UnitPrice.ToString();
                    txttotal_price.Text = tooth.TotalPrice.ToString();
                    cmbTeethSize.Text = tooth.ToothSize ?? "";
                    cmbTeethColor.Text = tooth.ToothColor ?? "";
                    cmbToothType.Text = tooth.ToothType ?? "";

                    // بارگذاری ویژگی‌های اضافی دندان
                    LoadAdditionalFeatures(tooth);

                    // نمایش دندان‌های انتخاب شده
                    if (!string.IsNullOrEmpty(tooth.ToothName))
                    {
                        string[] toothNames = tooth.ToothName.Split(new string[] { " / " }, StringSplitOptions.RemoveEmptyEntries);
                        selectedTeeth.Clear();
                        foreach (string toothName in toothNames)
                        {
                            selectedTeeth.Add(toothName.Trim());
                        }
                        DisplaySelectedTeeth();
                    }
                }

                // 6. بارگذاری تصاویر (images)
                var images = mysqlManager.GetImagesByPatientId(patientId);
                foreach (var image in images)
                {
                    if (image.Description == "عکس دندان")
                    {
                        if (image.ImageData != null && image.ImageData.Length > 0)
                        {
                            using (var ms = new MemoryStream(image.ImageData))
                            {
                                picToothImage.Image = System.Drawing.Image.FromStream(ms);
                            }
                        }
                    }
                    else if (image.Description == "امضای بیمار")
                    {
                        if (image.ImageData != null && image.ImageData.Length > 0)
                        {
                            using (var ms = new MemoryStream(image.ImageData))
                            {
                                picDoctorSign.Image = System.Drawing.Image.FromStream(ms);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطا در بارگذاری جزئیات بیمار: {ex.Message}", "خطا",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplaySelectedTeeth()
        {
            // پاک کردن رنگ همه دکمه‌ها
            foreach (Control control in panelTeeth.Controls)
            {
                if (control is Button btn)
                {
                    btn.BackColor = Color.White;
                    btn.ForeColor = Color.LightSeaGreen;
                }
            }

            // رنگ کردن دندان‌های انتخاب شده
            foreach (string toothName in selectedTeeth)
            {
                foreach (Control control in panelTeeth.Controls)
                {
                    if (control is Button btn && btn.Tag?.ToString() == toothName)
                    {
                        btn.BackColor = Color.DarkBlue;
                        btn.ForeColor = Color.White;
                        break;
                    }
                }
            }

            // نمایش در TextBox
            txtSelectedTeeth.Text = string.Join(" / ", selectedTeeth);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(searchTerm))
            {
                LoadAllPatients();
                return;
            }

            try
            {
                var patients = mysqlManager.GetAllPatients();
                var filteredPatients = patients.Where(p =>
                    p.FirstName.ToLower().Contains(searchTerm.ToLower()) ||
                    p.LastName.ToLower().Contains(searchTerm.ToLower()) ||
                    p.Phone.Contains(searchTerm)
                ).ToList();

                dgvPatients.DataSource = filteredPatients;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطا در جستجو: {ex.Message}", "خطا",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClearSearch_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            LoadAllPatients();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (currentPatientId == 0)
            {
                MessageBox.Show("لطفاً ابتدا یک بیمار انتخاب کنید", "هشدار",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 1. به‌روزرسانی اطلاعات بیمار
                var patient = new Patient
                {
                    Id = currentPatientId,
                    FirstName = txtFirstName.Text.Trim(),
                    LastName = txtLastName.Text.Trim(),
                    Phone = txtPhone.Text.Trim()
                };
                mysqlManager.UpdatePatient(patient);

                // 2. به‌روزرسانی وضعیت پرونده (cases)
                var cases = mysqlManager.GetCasesByPatientId(currentPatientId);
                if (cases.Count > 0)
                {
                    var caseInfo = cases[0];
                    caseInfo.VisitReason = txtVisitReason.Text.Trim();
                    caseInfo.Status = cmbTreatmentStatus.Text;
                    caseInfo.Description = txtDoctorNote.Text.Trim();
                    mysqlManager.UpdateCase(caseInfo);
                }

                // 3. به‌روزرسانی اطلاعات ویزیت (visits)
                var visits = mysqlManager.GetVisitsByPatientId(currentPatientId);
                if (visits.Count > 0)
                {
                    var visit = visits[0];
                    visit.DateVisit = dtpVisitDate.Value;
                    visit.DateTestTeeth = dtpToothTest.Value;
                    visit.DateTestGeneral = dtpFinalTest.Value;
                    visit.DateDelivery = dtpDeliveryDate.Value;
                    visit.DateRecord = dtpCentricRecord.Value;
                    visit.Notes = txtVisitNotes.Text.Trim();
                    mysqlManager.UpdateVisit(visit);
                }

                // 4. به‌روزرسانی اطلاعات پرداخت (payments)
                var payments = mysqlManager.GetPaymentsByPatientId(currentPatientId);
                if (payments.Count > 0)
                {
                    var payment = payments[0];
                    payment.PaymentMethod = cmbPaymentType.Text;
                    payment.Notes = txtPaymentNotes.Text.Trim();
                    mysqlManager.UpdatePayment(payment);
                }

                // 5. به‌روزرسانی اطلاعات دندان‌ها (teeth)
                var teeth = mysqlManager.GetTeethByPatientId(currentPatientId);
                if (teeth.Count > 0)
                {
                    var tooth = teeth[0];
                    tooth.ToothName = string.Join(" / ", selectedTeeth);
                    tooth.UnitPrice = decimal.Parse(txtunit_price.Text);
                    tooth.TotalPrice = decimal.Parse(txttotal_price.Text);
                    tooth.ToothSize = cmbTeethSize.Text;
                    tooth.ToothColor = cmbTeethColor.Text;
                    
                    // ذخیره ویژگی‌های اضافی دندان
                    SaveAdditionalFeatures(tooth);
                    
                    mysqlManager.UpdateTooth(tooth);
                }

                MessageBox.Show("اطلاعات بیمار با موفقیت به‌روزرسانی شد", "موفقیت",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                // بارگذاری مجدد لیست بیماران
                LoadAllPatients();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطا در به‌روزرسانی اطلاعات: {ex.Message}", "خطا",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (currentPatientId == 0)
            {
                MessageBox.Show("لطفاً ابتدا یک بیمار انتخاب کنید", "هشدار",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show("آیا از حذف این بیمار اطمینان دارید؟", "تأیید حذف",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    // حذف بیمار (به دلیل CASCADE، تمام رکوردهای مرتبط نیز حذف می‌شوند)
                    mysqlManager.DeletePatient(currentPatientId);

                    MessageBox.Show("بیمار با موفقیت حذف شد", "موفقیت",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // پاک کردن فرم
                    ClearForm();

                    // بارگذاری مجدد لیست بیماران
                    LoadAllPatients();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"خطا در حذف بیمار: {ex.Message}", "خطا",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ClearForm()
        {
            currentPatientId = 0;
            selectedTeeth.Clear();

            // پاک کردن تمام فیلدها
            txtFirstName.Clear();
            txtLastName.Clear();
            txtPhone.Clear();
            lblDateValue.Text = "----/--/--";

            txtVisitReason.Clear();
            cmbTreatmentStatus.SelectedIndex = -1;
            txtDoctorNote.Clear();

            dtpVisitDate.Value = DateTime.Now;
            dtpFvisitDate.Value = DateTime.Now;
            dtpToothTest.Value = DateTime.Now;
            dtpFinalTest.Value = DateTime.Now;
            dtpDeliveryDate.Value = DateTime.Now;
            dtpCentricRecord.Value = DateTime.Now;
            txtVisitNotes.Clear();

            cmbPaymentType.SelectedIndex = -1;
            txtPaymentNotes.Clear();
            
            // پاک کردن چک‌ها
            if (dgvCheques != null)
            {
                dgvCheques.DataSource = null;
            }
            if (txtTottalCheque != null)
            {
                txtTottalCheque.Clear();
            }
            if (totalchekmony != null)
            {
                totalchekmony.Clear();
            }

            // پاک کردن ویژگی‌های اضافی دندان
            ClearAdditionalFeatures();

            txtunit_price.Clear();
            txttotal_price.Clear();
            cmbTeethSize.SelectedIndex = -1;
            cmbTeethColor.SelectedIndex = -1;
            cmbToothType.SelectedIndex = -1;
            txtSelectedTeeth.Clear();

            picToothImage.Image = null;
            picDoctorSign.Image = null;

            // پاک کردن رنگ دکمه‌های دندان
            foreach (Control control in panelTeeth.Controls)
            {
                if (control is Button btn)
                {
                    btn.BackColor = Color.White;
                    btn.ForeColor = Color.LightSeaGreen;
                }
            }
        }

        private void btnDellet_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        // Event handlers برای دکمه‌های دندان
        private void ToothButton_Click(object sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                string toothName = btn.Tag?.ToString();
                if (!string.IsNullOrEmpty(toothName))
                {
                    if (selectedTeeth.Contains(toothName))
                    {
                        selectedTeeth.Remove(toothName);
                        btn.BackColor = Color.White;
                        btn.ForeColor = Color.LightSeaGreen;
                    }
                    else
                    {
                        selectedTeeth.Add(toothName);
                        btn.BackColor = Color.DarkBlue;
                        btn.ForeColor = Color.White;
                    }

                    txtSelectedTeeth.Text = string.Join(" / ", selectedTeeth);
                    CalculateTotalPrice();
                }
            }
        }

        private void chkSelectAllTeeth_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSelectAllTeeth.Checked)
            {
                SelectAllTeeth();
            }
            else
            {
                DeselectAllTeeth();
            }
        }

        private void SelectAllTeeth()
        {
            selectedTeeth.Clear();
            foreach (Control control in panelTeeth.Controls)
            {
                if (control is Button btn && !string.IsNullOrEmpty(btn.Tag?.ToString()))
                {
                    selectedTeeth.Add(btn.Tag.ToString());
                    btn.BackColor = Color.DarkBlue;
                    btn.ForeColor = Color.White;
                }
            }
            txtSelectedTeeth.Text = string.Join(" / ", selectedTeeth);
            CalculateTotalPrice();
        }

        private void DeselectAllTeeth()
        {
            selectedTeeth.Clear();
            foreach (Control control in panelTeeth.Controls)
            {
                if (control is Button btn)
                {
                    btn.BackColor = Color.White;
                    btn.ForeColor = Color.LightSeaGreen;
                }
            }
            txtSelectedTeeth.Text = "";
            CalculateTotalPrice();
        }

        private void CalculateTotalPrice()
        {
            try
            {
                if (decimal.TryParse(txtunit_price.Text, out decimal unitPrice))
                {
                    decimal totalPrice = unitPrice * selectedTeeth.Count;
                    txttotal_price.Text = totalPrice.ToString();
                }
            }
            catch
            {
                txttotal_price.Text = "0";
            }
        }

        private void txtunit_price_TextChanged(object sender, EventArgs e)
        {
            CalculateTotalPrice();
        }

        private void LoadAdditionalFeatures(Tooth tooth)
        {
            try
            {
                // بارگذاری ویژگی‌های اضافی دندان با استفاده از Controls.Find
                if (Controls.Find("comboBaseFractureTop", true).FirstOrDefault() is ComboBox comboBaseFractureTop)
                    comboBaseFractureTop.Text = tooth.BaseFractureTop ?? "";
                if (Controls.Find("comboBaseFractureBottom", true).FirstOrDefault() is ComboBox comboBaseFractureBottom)
                    comboBaseFractureBottom.Text = tooth.BaseFractureBottom ?? "";
                
                // CheckBox ها
                if (Controls.Find("checkBox1", true).FirstOrDefault() is CheckBox chkSaksion)
                    chkSaksion.Checked = tooth.Saksion;
                if (Controls.Find("checkBox3", true).FirstOrDefault() is CheckBox chkSoftLayerTop)
                    chkSoftLayerTop.Checked = tooth.SoftLayerTop;
                if (Controls.Find("checkBox4", true).FirstOrDefault() is CheckBox chkSoftLayerBottom)
                    chkSoftLayerBottom.Checked = tooth.SoftLayerBottom;
                if (Controls.Find("checkBox5", true).FirstOrDefault() is CheckBox chkHardRedTop)
                    chkHardRedTop.Checked = tooth.HardRedLayerTop;
                if (Controls.Find("checkBox6", true).FirstOrDefault() is CheckBox chkHardRedBottom)
                    chkHardRedBottom.Checked = tooth.HardRedLayerBottom;
                if (Controls.Find("checkBox7", true).FirstOrDefault() is CheckBox chkHardClearTop)
                    chkHardClearTop.Checked = tooth.HardClearLayerTop;
                if (Controls.Find("checkBox8", true).FirstOrDefault() is CheckBox chkHardClearBottom)
                    chkHardClearBottom.Checked = tooth.HardClearLayerBottom;

                // قیمت‌ها
                if (Controls.Find("txtPriceSoftLayer", true).FirstOrDefault() is TextBox txtSoftLayer)
                    txtSoftLayer.Text = tooth.PriceSoftLayer.ToString();
                if (Controls.Find("txtPriceHardRedLayer", true).FirstOrDefault() is TextBox txtHardRed)
                    txtHardRed.Text = tooth.PriceHardRedLayer.ToString();
                if (Controls.Find("txtPriceHardClearLayer", true).FirstOrDefault() is TextBox txtHardClear)
                    txtHardClear.Text = tooth.PriceHardClearLayer.ToString();
                if (Controls.Find("txtPriceBaseFracture", true).FirstOrDefault() is TextBox txtBaseFracture)
                    txtBaseFracture.Text = tooth.PriceBaseFracture.ToString();
                if (Controls.Find("saksionprice", true).FirstOrDefault() is TextBox txtSaksion)
                    txtSaksion.Text = tooth.PriceSaksion.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطا در بارگذاری ویژگی‌های اضافی: {ex.Message}", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveAdditionalFeatures(Tooth tooth)
        {
            try
            {
                // ذخیره ویژگی‌های اضافی دندان
                if (Controls.Find("comboBaseFractureTop", true).FirstOrDefault() is ComboBox comboBaseFractureTop)
                    tooth.BaseFractureTop = comboBaseFractureTop.Text;
                if (Controls.Find("comboBaseFractureBottom", true).FirstOrDefault() is ComboBox comboBaseFractureBottom)
                    tooth.BaseFractureBottom = comboBaseFractureBottom.Text;
                
                // CheckBox ها
                if (Controls.Find("checkBox1", true).FirstOrDefault() is CheckBox chkSaksion)
                    tooth.Saksion = chkSaksion.Checked;
                if (Controls.Find("checkBox3", true).FirstOrDefault() is CheckBox chkSoftLayerTop)
                    tooth.SoftLayerTop = chkSoftLayerTop.Checked;
                if (Controls.Find("checkBox4", true).FirstOrDefault() is CheckBox chkSoftLayerBottom)
                    tooth.SoftLayerBottom = chkSoftLayerBottom.Checked;
                if (Controls.Find("checkBox5", true).FirstOrDefault() is CheckBox chkHardRedTop)
                    tooth.HardRedLayerTop = chkHardRedTop.Checked;
                if (Controls.Find("checkBox6", true).FirstOrDefault() is CheckBox chkHardRedBottom)
                    tooth.HardRedLayerBottom = chkHardRedBottom.Checked;
                if (Controls.Find("checkBox7", true).FirstOrDefault() is CheckBox chkHardClearTop)
                    tooth.HardClearLayerTop = chkHardClearTop.Checked;
                if (Controls.Find("checkBox8", true).FirstOrDefault() is CheckBox chkHardClearBottom)
                    tooth.HardClearLayerBottom = chkHardClearBottom.Checked;

                // قیمت‌ها
                if (Controls.Find("txtPriceSoftLayer", true).FirstOrDefault() is TextBox txtSoftLayer)
                    tooth.PriceSoftLayer = decimal.Parse(txtSoftLayer.Text);
                if (Controls.Find("txtPriceHardRedLayer", true).FirstOrDefault() is TextBox txtHardRed)
                    tooth.PriceHardRedLayer = decimal.Parse(txtHardRed.Text);
                if (Controls.Find("txtPriceHardClearLayer", true).FirstOrDefault() is TextBox txtHardClear)
                    tooth.PriceHardClearLayer = decimal.Parse(txtHardClear.Text);
                if (Controls.Find("txtPriceBaseFracture", true).FirstOrDefault() is TextBox txtBaseFracture)
                    tooth.PriceBaseFracture = decimal.Parse(txtBaseFracture.Text);
                if (Controls.Find("saksionprice", true).FirstOrDefault() is TextBox txtSaksion)
                    tooth.PriceSaksion = decimal.Parse(txtSaksion.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطا در ذخیره ویژگی‌های اضافی: {ex.Message}", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearAdditionalFeatures()
        {
            try
            {
                // پاک کردن ComboBox ها
                if (Controls.Find("comboBaseFractureTop", true).FirstOrDefault() is ComboBox comboBaseFractureTop)
                    comboBaseFractureTop.SelectedIndex = -1;
                if (Controls.Find("comboBaseFractureBottom", true).FirstOrDefault() is ComboBox comboBaseFractureBottom)
                    comboBaseFractureBottom.SelectedIndex = -1;
                
                // پاک کردن CheckBox ها
                if (Controls.Find("checkBox1", true).FirstOrDefault() is CheckBox chkSaksion)
                    chkSaksion.Checked = false;
                if (Controls.Find("checkBox3", true).FirstOrDefault() is CheckBox chkSoftLayerTop)
                    chkSoftLayerTop.Checked = false;
                if (Controls.Find("checkBox4", true).FirstOrDefault() is CheckBox chkSoftLayerBottom)
                    chkSoftLayerBottom.Checked = false;
                if (Controls.Find("checkBox5", true).FirstOrDefault() is CheckBox chkHardRedTop)
                    chkHardRedTop.Checked = false;
                if (Controls.Find("checkBox6", true).FirstOrDefault() is CheckBox chkHardRedBottom)
                    chkHardRedBottom.Checked = false;
                if (Controls.Find("checkBox7", true).FirstOrDefault() is CheckBox chkHardClearTop)
                    chkHardClearTop.Checked = false;
                if (Controls.Find("checkBox8", true).FirstOrDefault() is CheckBox chkHardClearBottom)
                    chkHardClearBottom.Checked = false;

                // پاک کردن قیمت‌ها
                if (Controls.Find("txtPriceSoftLayer", true).FirstOrDefault() is TextBox txtSoftLayer)
                    txtSoftLayer.Clear();
                if (Controls.Find("txtPriceHardRedLayer", true).FirstOrDefault() is TextBox txtHardRed)
                    txtHardRed.Clear();
                if (Controls.Find("txtPriceHardClearLayer", true).FirstOrDefault() is TextBox txtHardClear)
                    txtHardClear.Clear();
                if (Controls.Find("txtPriceBaseFracture", true).FirstOrDefault() is TextBox txtBaseFracture)
                    txtBaseFracture.Clear();
                if (Controls.Find("saksionprice", true).FirstOrDefault() is TextBox txtSaksion)
                    txtSaksion.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطا در پاک کردن ویژگی‌های اضافی: {ex.Message}", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
