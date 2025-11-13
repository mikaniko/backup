using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OurLibraryManagementSystem
{
public class RoundedTextBox : UserControl
    {
        private TextBox textBox;

        public RoundedTextBox()
        {
            this.BackColor = Color.Transparent;
            this.Size = new Size(250, 35);

            textBox = new TextBox();
            textBox.BorderStyle = BorderStyle.None;
            textBox.Font = new Font("Segoe UI", 10);
            textBox.ForeColor = Color.Black;
            textBox.BackColor = Color.LightGray;
            textBox.Location = new Point(10, 8);
            textBox.Width = this.Width - 20;

            this.Controls.Add(textBox);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            using (GraphicsPath path = new GraphicsPath())
            {
                int radius = 15;
                Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
                path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
                path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90);
                path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90);
                path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90);
                path.CloseFigure();

                using (SolidBrush brush = new SolidBrush(Color.LightGray))
                {
                    g.FillPath(brush, path);
                }
            }
        }

        public string TextValue
        {
            get => textBox.Text;
            set => textBox.Text = value;
        }
        public bool UsePasswordChar
        {
            get => textBox.UseSystemPasswordChar;
            set => textBox.UseSystemPasswordChar = value;
        }
    }

}

