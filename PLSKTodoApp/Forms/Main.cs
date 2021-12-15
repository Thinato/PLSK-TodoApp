using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PLSKTodoApp
{
    public partial class Main : Form
    {
        CheckBox cbox;
        Button btnDelete;
        private int Counter;
        public Main()
        {
            InitializeComponent();
            Counter = 0;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                AddItem(textBox1.Text);
        }
        private void AddItem(string item)
        {
            // ComboBox
            cbox = new CheckBox();
            cbox.Name = "cbox" + Counter;
            if (item.Length > 17)
                item = item.Substring(0, 17) + "...";
            cbox.Text = item;
            cbox.Location = new Point(textBox1.Location.X, textBox1.Location.Y);
            cbox.Size = new Size(textBox1.Size.Width, textBox1.Size.Height);
            cbox.Anchor = AnchorStyles.Top;
            cbox.CheckedChanged += Cbox_CheckedChanged;
            Controls.Add(cbox);

            // Delete Button
            btnDelete = new Button();
            btnDelete.Name = "btnDelete" + Counter;
            btnDelete.Text = "X";
            btnDelete.Size = new Size(25, 25);
            btnDelete.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnDelete.ForeColor = Color.Red;
            btnDelete.Location = new Point(textBox1.Location.X + textBox1.Width, textBox1.Location.Y);
            btnDelete.Anchor = AnchorStyles.Top;
            btnDelete.Click += BtnDelete_Click;
            Controls.Add(btnDelete);

            // add to counter
            Counter++;
            // correct textbox to the next location
            textBox1.Location = new Point(textBox1.Location.X, textBox1.Location.Y+30);
            textBox1.Text = "";
        }

        private void Cbox_CheckedChanged(object? sender, EventArgs e)
        {
            cbox = sender as CheckBox;
            if (cbox.Checked)
                cbox.Font = new Font("Segoe UI", 9F, FontStyle.Strikeout, GraphicsUnit.Point);
            else
                cbox.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
        }

        private void BtnDelete_Click(object? sender, EventArgs e)
        {
            btnDelete = sender as Button;
            int bId = Convert.ToInt32(btnDelete.Name.Substring(9));
            int cId;
            foreach (CheckBox c in Controls.OfType<CheckBox>())
            {
                cId = Convert.ToInt32(c.Name.Substring(4));
                if (cId == bId)
                    Controls.Remove(c);
                if (cId > bId)
                    c.Location = new Point(c.Location.X, c.Location.Y-30);
                Control d = Controls["btnDelete" + cId];
                d.Location = new Point(d.Location.X, c.Location.Y);
                
            }
                
            Controls.Remove(btnDelete);
            textBox1.Location = new Point(textBox1.Location.X, textBox1.Location.Y - 30);
        }
    }
}
