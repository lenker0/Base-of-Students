using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace Laba2Rabochaya_A_ne_ta_fignya_
{
    public partial class Form1 : Form
    {
        static string login = "admin";
        static string password = "admin";
        public Form1()
        {
            InitializeComponent();
            this.BackColor = System.Drawing.Color.Khaki;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == login && textBox2.Text == password)
            {
                Hide();
                Form2 form2 = new Form2();
                form2.ShowDialog();
                this.Close();
            }
            else MessageBox.Show("Неправильный login или password");
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                button1_Click(sender, e);
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                button1_Click(sender, e);
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                button1_Click(sender, e);
            }
        }

    }
}
