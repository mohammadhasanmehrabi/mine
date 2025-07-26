using System;
using System.Drawing;
using System.Windows.Forms;

namespace denyis
{
    public class ToothButton : Button
    {
        public string ToothName { get; private set; }
        public bool IsSelected { get; private set; }

        public ToothButton(string toothName)
        {
            this.ToothName = toothName;
            this.Tag = toothName;
            this.Text = toothName;
            this.Width = 80;
            this.Height = 40;
            this.BackColor = SystemColors.Control;
            this.ForeColor = Color.Black;
            this.Font = new Font("B Nazanin", 9);
            this.FlatStyle = FlatStyle.Flat;
            this.Margin = new Padding(3);

            // رویداد کلیک داخلی
            this.Click += (s, e) =>
            {
                if (IsSelected)
                    DeselectTooth();
                else
                    SelectTooth();
            };
        }

        public void SelectTooth()
        {
            IsSelected = true;
            this.BackColor = Color.DarkBlue;
            this.ForeColor = Color.White;
        }

        public void DeselectTooth()
        {
            IsSelected = false;
            this.BackColor = SystemColors.Control;
            this.ForeColor = Color.Black;
        }
    }
}
