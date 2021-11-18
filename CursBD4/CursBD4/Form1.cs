using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CursBD4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection sqlConnection = new SqlConnection();
        void Update()
        {
            
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "databaseDataSet.beg". При необходимости она может быть перемещена или удалена.
            this.begTableAdapter.Fill(this.databaseDataSet.beg);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "databaseDataSet.blag". При необходимости она может быть перемещена или удалена.
            this.blagTableAdapter.Fill(this.databaseDataSet.blag);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "databaseDataSet.rasp". При необходимости она может быть перемещена или удалена.
            this.raspTableAdapter.Fill(this.databaseDataSet.rasp);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "databaseDataSet.result". При необходимости она может быть перемещена или удалена.
            this.resultTableAdapter.Fill(this.databaseDataSet.result);
            string connstr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\projects\vs\CursBD4\CursBD4\CursBD4\Database.mdf;Integrated Security=True";
            sqlConnection = new SqlConnection(connstr);
            sqlConnection.Open();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            label17.Text = "Результат:";
            double heigth, weight, res;
            try
            {
                heigth = Convert.ToDouble(textBox3.Text) / 100;
                weight = Convert.ToDouble(textBox4.Text);

                res = weight / (heigth * heigth);
                res = Math.Round(res, 1);
                label17.Text = "Результат:" + res.ToString();
            }
            catch
            {
                MessageBox.Show("Введите корректные значения", "Ошибка");
            }
            
        }

        private void button10_Click(object sender, EventArgs e)
        {
            double heigth, weight, age, res;
            try
            {
                heigth = Convert.ToDouble(textBox6.Text);
                weight = Convert.ToDouble(textBox5.Text);
                age = Convert.ToDouble(textBox7.Text);
                res = 0;
                if (comboBox10.SelectedIndex == 0)
                {
                    res = 66.47 + (13.75 * weight) + (5 * heigth) - (6.74 * age);
                }
                else if (comboBox10.SelectedIndex == 1)
                {
                    res = 655.1 + 9.6 * weight + 1.85 * heigth - 4.68 * age;
                }
                else MessageBox.Show("Выберите пол", "Ошибка");
                res = Math.Round(res, 1);
                label22.Text = "Результат:" + res.ToString() + " ккал";
            }
            catch
            {
                MessageBox.Show("Введите корректные значения", "Ошибка");
            }
        }
    }
}
