using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using denyis; // اضافه شد برای شناسایی فرم‌ها

namespace denyis
{
    public partial class Form1 : Form
    {
        private Button btnAddEdit;
        private Button btnSearch;
        private Label lblTitle;
        private Label lblWelcome;
        private Panel panelButtons;

        public Form1()
        {
            InitializeComponent();
            this.Text = "مدیریت بیماران دندان‌پزشکی";
            this.Size = new Size(520, 400);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.DoubleBuffered = true;
            this.RightToLeft = RightToLeft.Yes;
            this.Font = new Font("B Nazanin", 12, FontStyle.Regular);
            this.BackColor = Color.White;
            this.Paint += Form1_Paint; // برای گرادینت
            InitUI();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            // گرادینت آبی به بنفش
            var rect = this.ClientRectangle;
            using (var brush = new System.Drawing.Drawing2D.LinearGradientBrush(rect, Color.FromArgb(80,180,255), Color.FromArgb(160,120,255), 45F))
            {
                e.Graphics.FillRectangle(brush, rect);
            }
        }

        private void InitUI()
        {
            // عنوان
            lblTitle = new Label();
            lblTitle.Text = "سیستم مدیریت بیماران دندان‌پزشکی";
            lblTitle.Font = new Font("B Titr", 22, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.AutoSize = false;
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            lblTitle.Dock = DockStyle.Top;
            lblTitle.Height = 80;
            lblTitle.BackColor = Color.Transparent;
            this.Controls.Add(lblTitle);

            // جمله خوش‌آمدگویی
            lblWelcome = new Label();
            lblWelcome.Text = "به نرم‌افزار مدیریت بیماران خوش آمدید!";
            lblWelcome.Font = new Font("B Nazanin", 14, FontStyle.Regular);
            lblWelcome.ForeColor = Color.FromArgb(240,240,255);
            lblWelcome.AutoSize = false;
            lblWelcome.TextAlign = ContentAlignment.MiddleCenter;
            lblWelcome.Dock = DockStyle.Top;
            lblWelcome.Height = 40;
            lblWelcome.BackColor = Color.Transparent;
            this.Controls.Add(lblWelcome);
            lblWelcome.BringToFront();

            // پنل دکمه‌ها
            panelButtons = new Panel();
            panelButtons.Size = new Size(320, 160);
            panelButtons.Location = new Point((this.ClientSize.Width - panelButtons.Width) / 2, 140);
            panelButtons.BackColor = Color.FromArgb(80, 80, 120, 40); // نیمه شفاف
            panelButtons.Anchor = AnchorStyles.None;
            panelButtons.BorderStyle = BorderStyle.None;
            this.Controls.Add(panelButtons);

            // دکمه افزودن/ویرایش
            btnAddEdit = new Button();
            btnAddEdit.Text = "افزودن / ویرایش بیمار";
            btnAddEdit.Font = new Font("B Nazanin", 16, FontStyle.Bold);
            btnAddEdit.Size = new Size(260, 55);
            btnAddEdit.Location = new Point(30, 20);
            btnAddEdit.BackColor = Color.FromArgb(80, 180, 255);
            btnAddEdit.ForeColor = Color.White;
            btnAddEdit.FlatStyle = FlatStyle.Flat;
            btnAddEdit.FlatAppearance.BorderSize = 0;
            btnAddEdit.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btnAddEdit.Width, btnAddEdit.Height, 25, 25));
            btnAddEdit.Cursor = Cursors.Hand;
            btnAddEdit.MouseEnter += (s, e) => btnAddEdit.BackColor = Color.FromArgb(60, 140, 220);
            btnAddEdit.MouseLeave += (s, e) => btnAddEdit.BackColor = Color.FromArgb(80, 180, 255);
            btnAddEdit.Paint += ButtonShadow_Paint;
            btnAddEdit.Click += BtnAddEdit_Click;
            panelButtons.Controls.Add(btnAddEdit);

            // دکمه جستجو
            btnSearch = new Button();
            btnSearch.Text = "جستجوی بیمار";
            btnSearch.Font = new Font("B Nazanin", 16, FontStyle.Bold);
            btnSearch.Size = new Size(260, 55);
            btnSearch.Location = new Point(30, 85);
            btnSearch.BackColor = Color.FromArgb(120, 220, 120);
            btnSearch.ForeColor = Color.White;
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.FlatAppearance.BorderSize = 0;
            btnSearch.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btnSearch.Width, btnSearch.Height, 25, 25));
            btnSearch.Cursor = Cursors.Hand;
            btnSearch.MouseEnter += (s, e) => btnSearch.BackColor = Color.FromArgb(80, 180, 80);
            btnSearch.MouseLeave += (s, e) => btnSearch.BackColor = Color.FromArgb(120, 220, 120);
            btnSearch.Paint += ButtonShadow_Paint;
            btnSearch.Click += BtnSearch_Click;
            panelButtons.Controls.Add(btnSearch);
        }

        // افکت سایه برای دکمه‌ها
        private void ButtonShadow_Paint(object sender, PaintEventArgs e)
        {
            Button btn = sender as Button;
            Rectangle rect = new Rectangle(2, btn.Height - 8, btn.Width - 4, 8);
            using (SolidBrush shadowBrush = new SolidBrush(Color.FromArgb(60, 0, 0, 0)))
            {
                e.Graphics.FillEllipse(shadowBrush, rect);
            }
        }

        // گوشه گرد
        [System.Runtime.InteropServices.DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        private void BtnAddEdit_Click(object sender, EventArgs e)
        {
            PatientForm pf = new PatientForm();
            pf.ShowDialog();
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            SearchForm sf = new SearchForm();
            sf.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
