using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lab2Library;
using System.IO;
using System.Xml.Serialization;

namespace Laba2Rabochaya_A_ne_ta_fignya_
{
    public partial class Form2 : Form
    {        
        List<Student> students;

        XmlSerializer xml = new XmlSerializer(typeof(List<Student>));

        public Form2()
        {
            InitializeComponent();           
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "XML|*.xml";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                StreamReader reader = new StreamReader(openFileDialog1.FileName);
                students = new List<Student>();
                students = (List<Student>)xml.Deserialize(reader);
                dataGridView1.DataSource = students;
                reader.Close();
                dataGridView1.EnableHeadersVisualStyles = false;
                dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.PeachPuff;
                if (MessageBox.Show("Файл открыт", "Открыть?", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    students = new List<Student>();
                    dataGridView1.DataSource = students;
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (students != null)
            {
                saveFileDialog1.Filter = "XML|*.xml";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    StreamWriter writer = new StreamWriter(saveFileDialog1.FileName);
                    xml.Serialize(writer, students);
                    writer.Flush(); writer.Close();
                    MessageBox.Show("Файл успешно сохранен", "Выполнено", MessageBoxButtons.OK);
                }
            }
            else
            {
                MessageBox.Show("У вас нечего сохранять :( ", "Абшибка", MessageBoxButtons.OK);
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (students != null)
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    StreamWriter writer = new StreamWriter(saveFileDialog1.FileName);
                    xml.Serialize(writer, students);
                    writer.Flush(); writer.Close();
                    MessageBox.Show("Файл успешно сохранен", "Выполнено", MessageBoxButtons.OK);
                }
            }
            else
            {
                MessageBox.Show("У вас нечего сохранять :( ", "Абшибка", MessageBoxButtons.OK);
            }
        }

        private void Form2_SizeChanged(object sender, EventArgs e)
        {
            dataGridView1.Size = this.Size;
            dataGridView1.RowHeadersWidth = (int)this.Width / 5;
        }

        private void ascendingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (students != null)
            {
                students = students.OrderBy(n => n.Name).ToList();
                dataGridView1.DataSource = students;
            }
        }

        private void descendToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (students != null)
            {
                students = students.OrderByDescending(n => n.Name).ToList();
                dataGridView1.DataSource = students;
            }
        }

        private void dateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (students != null)
            {
                students = students.OrderBy(n => n.Day).ToList();
                dataGridView1.DataSource = students;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Student found = students.Find(n => n.Name.Contains(textBox1.Text));
            if (found != null)
            {
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    dataGridView1.Rows[i].Selected = false;
                    for (int j = 0; j < dataGridView1.ColumnCount; j++)
                        if (dataGridView1.Rows[i].Cells[j].Value != null)
                            if (dataGridView1.Rows[i].Cells[j].Value.ToString().Contains(textBox1.Text))
                            {
                                dataGridView1.Rows[i].Selected = true;
                                 
                                break;
                            }
                }
                MessageBox.Show("Найдены следующий(ие) студенты:", "Student", MessageBoxButtons.OK);
            }
            else
                MessageBox.Show("Такого студента нет", "Student", MessageBoxButtons.OK);
        }

        private void ascendToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (students != null)
            {
                students = students.OrderByDescending(n => n.Grade).ToList();
                dataGridView1.DataSource = students;
            }
        }

        private void descendToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (students != null)
            {
                students = students.OrderBy(n => n.Grade).ToList();
                dataGridView1.DataSource = students;
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            students.Add(new Student());
            BindingSource source = new BindingSource();
            source.DataSource = students;
            dataGridView1.DataSource = source; 

        }

        private void Form2_KeyPress(object sender, KeyPressEventArgs e)
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

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell != null)
            {
                students.RemoveAt(dataGridView1.CurrentCell.RowIndex);
                BindingSource source = new BindingSource();
                source.DataSource = students;
                dataGridView1.DataSource = source;
            }
        }

        bool direction = true;
        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            switch (e.ColumnIndex)
            {
                case 1:
                    if (direction)
                    {
                        students = students.OrderBy(n => n.Name).ToList();
                    }
                    else students = students.OrderByDescending(n => n.Name).ToList();
                    dataGridView1.DataSource = students;
                    direction = !direction;
                break;

                default: break;
            }
        }

        private void dataGridView1_ColumnHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            switch (e.ColumnIndex)
            {
                case 0:
                    if (direction)
                    {
                        students = students.OrderBy(n => n.Name).ToList();
                    }
                    else students = students.OrderByDescending(n => n.Name).ToList();
                    dataGridView1.DataSource = students;
                    direction = !direction;
                    break;
                case 1:
                    if (direction)
                    {
                        students = students.OrderBy(n => n.Number).ToList();
                    }
                    else students = students.OrderByDescending(n => n.Number).ToList();
                    dataGridView1.DataSource = students;
                    direction = !direction;
                    break;
                case 2:
                    if (direction)
                    {
                        students = students.OrderBy(n => n.Day).ToList();
                    }
                    else students = students.OrderByDescending(n => n.Day).ToList();
                    dataGridView1.DataSource = students;
                    direction = !direction;
                    break;
                case 3:
                    if (direction)
                    {
                        students = students.OrderBy(n => n.Institute).ToList();
                    }
                    else students = students.OrderByDescending(n => n.Institute).ToList();
                    dataGridView1.DataSource = students;
                    direction = !direction;
                    break;
                case 4:
                    if (direction)
                    {
                        students = students.OrderBy(n => n.Group).ToList();
                    }
                    else students = students.OrderByDescending(n => n.Group).ToList();
                    dataGridView1.DataSource = students;
                    direction = !direction;
                    break;
                case 5:
                    if (direction)
                    {
                        students = students.OrderBy(n => n.Course).ToList();
                    }
                    else students = students.OrderByDescending(n => n.Course).ToList();
                    dataGridView1.DataSource = students;
                    direction = !direction;
                    break;
                case 6:
                    if (direction)
                    {
                        students = students.OrderBy(n => n.Grade).ToList();
                    }
                    else students = students.OrderByDescending(n => n.Grade).ToList();
                    dataGridView1.DataSource = students;
                    direction = !direction;
                    break;
                default: break;
            }
        }

        private void lehaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = ("BMP|*.bmp");
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Student found = students.Find(n => n.compare(new Bitmap(openFileDialog1.FileName)) == true);
                if (found != null)
                {
                    MessageBox.Show($"Найден студент с такой подписью.\n {found.Name}");
                    return;
                }
                MessageBox.Show($"Не найден студент с такой подписью.", "LUL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void addImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Bitmap|*.bmp";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                int Number = dataGridView1.CurrentCell.RowIndex;
                students[Number].AddSign(new Bitmap(openFileDialog1.FileName));
                MessageBox.Show($"Цифровая подпись добавлена студенту:\n{students[Number].Name}", "Добавление цифровой подписи", MessageBoxButtons.OK);
            }
        }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            int res;
            if (e.ColumnIndex == 1 | e.ColumnIndex == 5)
            {
                if (e.FormattedValue.ToString() == string.Empty)
                    return;
                else
                if (!int.TryParse(e.FormattedValue.ToString(), out res) || e.FormattedValue.ToString().Length > 10)
                {
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 1;
                    MessageBox.Show("Введи числовое значение");
                    e.Cancel = true;
                    return;
                }
            }
            else
            if (e.ColumnIndex == 6)
            {
                double re;
                if (e.FormattedValue.ToString() == string.Empty)
                    return;
                else
                if (!double.TryParse(e.FormattedValue.ToString(), out re) || e.FormattedValue.ToString().Length > 10)
                {
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 1;
                    MessageBox.Show("Введи числовое значение");
                    e.Cancel = true;
                    return;
                }
            }
            
        }

        byte IsFirstClick = 0;

        private void button2_Click(object sender, EventArgs e)
        {
            IsFirstClick++;         
            
            Random r = new Random();
            int w = r.Next(this.ClientSize.Width - button2.Size.Width);
            int h = r.Next(this.ClientSize.Height - button2.Size.Height);
            
            this.button2.Location = new System.Drawing.Point(w, h);

            if (IsFirstClick == 10)
            {
                button2.Hide();
                MessageBox.Show("Привет, ты зря потратил своё время ^_^ Умница", "Mda...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                

            }
        }
        
    }
}
