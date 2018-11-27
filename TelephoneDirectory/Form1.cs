using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TelephoneDirectory
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private int currentIndex = 0;//поточний індекс
        private List<TelephonDirectory> list = new List<TelephonDirectory>();//список телефонів
        private WorkerFile file = new WorkerFile("database.txt");//робота з файлом
        private void CloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();//вихід з програми
        }

        private void EditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var item in видToolStripMenuItem.DropDownItems)
            {
                ((ToolStripMenuItem)item).Checked = false;//усім зняти галочку
            }
            EditToolStripMenuItem.Checked = true;//поставити голочку
            panelEdit.BringToFront();//винести панель на передній ряд
        }

        private void TableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var item in видToolStripMenuItem.DropDownItems)
            {
                ((ToolStripMenuItem)item).Checked = false;//усім зняти галочку
            }
            TableToolStripMenuItem.Checked = true;//поставити голочку
            panelTable.BringToFront();//винести панель на передній ряд
        }

        private void Task1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var item in завданняToolStripMenuItem.DropDownItems)
            {
                ((ToolStripMenuItem)item).Checked = false;
            }
            Task1ToolStripMenuItem.Checked = true;
            panelTask1.BringToFront();
        }

        private void Task2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var item in завданняToolStripMenuItem.DropDownItems)
            {
                ((ToolStripMenuItem)item).Checked = false;
            }
            Task2ToolStripMenuItem.Checked = true;
            panelTask2.BringToFront();
        }

        private void Task3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var item in завданняToolStripMenuItem.DropDownItems)
            {
                ((ToolStripMenuItem)item).Checked = false;
            }
            Task3ToolStripMenuItem.Checked = true;
            panelTask3.BringToFront();
        }

        private void Task4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var item in завданняToolStripMenuItem.DropDownItems)
            {
                ((ToolStripMenuItem)item).Checked = false;
            }
            Task4ToolStripMenuItem.Checked = true;
            var cities = list.Select(p => p.City).Distinct();
            listBoxTask4.Items.Clear();
            foreach (var city in cities)
            {
                listBoxTask4.Items.Add($"{city}:{list.Where(p => p.City == city).Count()}");
            }
            panelTask4.BringToFront();
        }

        private void AboutProgrammToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var item in довідкаToolStripMenuItem.DropDownItems)
            {
                ((ToolStripMenuItem)item).Checked = false;
            }
            AboutProgrammToolStripMenuItem.Checked = true;
            panelInfo.BringToFront();
            pictureBox.Visible = false;
            textBoxAbout.Visible = true;
        }

        private void TasksToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            foreach (var item in довідкаToolStripMenuItem.DropDownItems)
            {
                ((ToolStripMenuItem)item).Checked = false;
            }
            TasksToolStripMenuItem1.Checked = true;
            panelInfo.BringToFront();
            pictureBox.Visible = true;
            textBoxAbout.Visible = false;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (textBoxCity.TextLength == 0)//перевірка валідності поля
            {
                MessageBox.Show("Введіть місто");
                textBoxCity.Focus();
                return;
            }
            if (textBoxAddress.TextLength == 0)//перевірка валідності поля
            {
                MessageBox.Show("Введіть адресу");
                textBoxAddress.Focus();
                return;
            }
            if (textBoxFullName.TextLength == 0)//перевірка валідності поля
            {
                MessageBox.Show("Введіть ПІБ");
                textBoxFullName.Focus();
                return;
            }
            if (textBoxPhone.TextLength == 0)//перевірка валідності поля
            {
                MessageBox.Show("Введіть телефон");
                textBoxPhone.Focus();
                return;
            }
            if (list.Any(p => p.Phone == textBoxPhone.Text))//перевірка на однаковий телефон
            {
                MessageBox.Show("Такий телефон вже є в довіднику");
                textBoxPhone.Focus();
                return;
            }
            int maxId = list.Count == 0 ? 0 : list.Max(p => p.ID) + 1;//максимальне айди
            //додаємо в ліст
            list.Add(new TelephonDirectory { ID = maxId, City = textBoxCity.Text, Address = textBoxAddress.Text, FullName = textBoxFullName.Text, Phone = textBoxPhone.Text });
            file.SaveChanges(list);//зберігаємо змінення в файл
            ChangeList();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            list = file.GetPhonesDirectory();//отримаємо усі телефони з файлу
            ChangeList();
        }
        private void ChangeList()
        {
            while (currentIndex >= list.Count)//зменшення індексу
                currentIndex--;
            if (list.Count == 0)
            {
                label5.Text = "Немає данних";
                buttonNext.Enabled = false;
                buttonPrev.Enabled = false;
                buttonDelete.Enabled = false;
                buttonSave.Enabled = false;
            }
            else
            {
                label5.Text = $"{currentIndex + 1} з {list.Count}";
                buttonDelete.Enabled = true;
                buttonSave.Enabled = true;
                textBoxCity.Text = list[currentIndex].City;
                textBoxAddress.Text = list[currentIndex].Address;
                textBoxFullName.Text = list[currentIndex].FullName;
                textBoxPhone.Text = list[currentIndex].Phone;
                if (currentIndex == 0)
                {
                    buttonPrev.Enabled = false;
                }
                else
                {
                    buttonPrev.Enabled = true;
                }
                if (currentIndex + 1 == list.Count)
                {
                    buttonNext.Enabled = false;
                }
                else
                {
                    buttonNext.Enabled = true;
                }
                dataGridView1.Rows.Clear();
                foreach (var item in list)
                {
                    dataGridView1.Rows.Add();
                    dataGridView1["City", dataGridView1.Rows.Count - 2].Value = item.City;
                    dataGridView1["Address", dataGridView1.Rows.Count - 2].Value = item.Address;
                    dataGridView1["FullName", dataGridView1.Rows.Count - 2].Value = item.FullName;
                    dataGridView1["Phone", dataGridView1.Rows.Count - 2].Value = item.Phone;
                }
                comboBoxCity.Items.Clear();
                comboBoxCity.Items.AddRange(list.Select(p => p.City).Distinct().ToArray());
            }
        }

        private void buttonPrev_Click(object sender, EventArgs e)
        {
            currentIndex--;//зменшення індексу
            ChangeList();
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {

            currentIndex++;//збільшення індексу
            ChangeList();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            list.Remove(list[currentIndex]);//видалення з лісту
            file.SaveChanges(list);//збереження змінень у файл
            ChangeList();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (textBoxCity.TextLength == 0)//перевірка на валідність поля
            {
                MessageBox.Show("Введіть місто");
                textBoxCity.Focus();
                return;
            }
            if (textBoxAddress.TextLength == 0)//перевірка на валідність поля
            {
                MessageBox.Show("Введіть адресу");
                textBoxAddress.Focus();
                return;
            }
            if (textBoxFullName.TextLength == 0)//перевірка на валідність поля
            {
                MessageBox.Show("Введіть ПІБ");
                textBoxFullName.Focus();
                return;
            }
            if (textBoxPhone.TextLength == 0)//перевірка на валідність поля
            {
                MessageBox.Show("Введіть телефон");
                textBoxPhone.Focus();
                return;
            }
            if (list.Any(p => p.Phone == textBoxPhone.Text && p.ID != list[currentIndex].ID))//перевірка на однаковий телефон
            {
                MessageBox.Show("Такий телефон вже є в довіднику");
                textBoxPhone.Focus();
                return;
            }
            list[currentIndex].City = textBoxCity.Text;
            list[currentIndex].Address = textBoxAddress.Text;
            list[currentIndex].FullName = textBoxFullName.Text;
            list[currentIndex].Phone = textBoxPhone.Text;
            file.SaveChanges(list);
        }

        private void comboBoxCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBoxTask1.Items.Clear();//очищаємо усі ПІБи з ліста
            //вибираємо усі ПІБи обраного міста та сортируємо
            listBoxTask1.Items.AddRange(list.Where(p => p.City == comboBoxCity.SelectedItem.ToString()).Select(p => p.FullName).OrderBy(x => x).ToArray());
        }

        private void textBoxTask2_TextChanged(object sender, EventArgs e)
        {
            if (textBoxTask2.TextLength == 0)//перевірка на валідність поля
            {
                dataGridViewTask2.Rows.Clear();//очищаємо таблцию
                foreach (var item in list)
                {
                    dataGridViewTask2.Rows.Add();//додаємо рядок
                    dataGridViewTask2["CityTask2", dataGridViewTask2.Rows.Count - 2].Value = item.City;
                    dataGridViewTask2["AddressTask2", dataGridViewTask2.Rows.Count - 2].Value = item.Address;
                    dataGridViewTask2["FullNameTask2", dataGridViewTask2.Rows.Count - 2].Value = item.FullName;
                    dataGridViewTask2["PhoneTask2", dataGridViewTask2.Rows.Count - 2].Value = item.Phone;
                }
            }
            else
            {
                dataGridViewTask2.Rows.Clear();//очищаємо таблцию
                foreach (var item in list.Where(p => p.FullName.Contains(textBoxTask2.Text)))//знаходимо усіх у кого співпадає ПІБ
                {
                    dataGridViewTask2.Rows.Add();//додаємо рядок
                    dataGridViewTask2["CityTask2", dataGridViewTask2.Rows.Count - 2].Value = item.City;
                    dataGridViewTask2["AddressTask2", dataGridViewTask2.Rows.Count - 2].Value = item.Address;
                    dataGridViewTask2["FullNameTask2", dataGridViewTask2.Rows.Count - 2].Value = item.FullName;
                    dataGridViewTask2["PhoneTask2", dataGridViewTask2.Rows.Count - 2].Value = item.Phone;
                }
            }
        }

        private void buttonSearchTask3_Click(object sender, EventArgs e)
        {
            var item = list.FirstOrDefault(p => p.Phone == textBoxPhoneTask3.Text);//шукаємо телефон
            if (item == null)//якщо телефон не знайдено
            {
                MessageBox.Show("Телефон не знайден");
                return;
            }
            labelAddress.Text = $"Адреса:{item.Address}";//виводимо адресу
        }
    }
}
