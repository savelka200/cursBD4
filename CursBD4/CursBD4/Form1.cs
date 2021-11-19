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
        SqlDataAdapter da = new SqlDataAdapter();
        DataSet ds = new DataSet();


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

        private void button7_Click(object sender, EventArgs e) // калькулятор
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

        private void button10_Click(object sender, EventArgs e) // калькулятор
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

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            sqlConnection.Close();
        }

        private void Update(object sender, EventArgs e) // обновление
        {
            comboBox15.Items.Clear();
            comboBox4.Items.Clear();
            comboBox6.Items.Clear();
            comboBox13.Items.Clear();
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            comboBox9.Items.Clear();
            comboBox7.Items.Clear();
            comboBox12.Items.Clear();
            comboBox11.Items.Clear();
            comboBox16.Items.Clear();
            SqlDataReader reader = null;
            SqlCommand crasp = new SqlCommand("SELECT * FROM [rasp]", sqlConnection);
            reader = crasp.ExecuteReader();
            while (reader.Read())
            {
                comboBox15.Items.Add(reader["id"]); // ввожу айди забегов
                comboBox4.Items.Add(reader["id"]);
                comboBox9.Items.Add(reader["id"]);
            }
            reader.Close();
            SqlCommand cbegn = new SqlCommand("SELECT * FROM [beg] WHERE [verify] = 0 ", sqlConnection); //выбрать неподтверждённых бегунов
            reader = cbegn.ExecuteReader();
            while (reader.Read())
            {
                comboBox1.Items.Add(reader["id"]); // ввожу айди неподтвердженных бегунов
                comboBox12.Items.Add(reader["id"]);
            }
            reader.Close();
            SqlCommand cbegv = new SqlCommand("SELECT * FROM [beg] WHERE [verify] = 1 ", sqlConnection); //выбрать подтверждённых бегунов
            reader = cbegv.ExecuteReader();
            while (reader.Read())
            {
                comboBox2.Items.Add(reader["id"]); // ввожу айди подтвердженных бегунов
                comboBox11.Items.Add(reader["id"]);
            }
            reader.Close();
            SqlCommand czab = new SqlCommand("SELECT * FROM [zab]", sqlConnection); //выбрать дистанции забегов
            reader = czab.ExecuteReader();
            while (reader.Read())
            {
                comboBox13.Items.Add(reader["dist"]); // ввожу дистанции
                comboBox16.Items.Add(reader["dist"]);
            }
            reader.Close();

            // обновление датагрид

            dataGridView1.DataSource = null;
            dataGridView6.DataSource = null;
            ds.Clear();
            SqlCommand com_beg = new SqlCommand("SELECT * FROM [beg]", sqlConnection);
            da.SelectCommand = com_beg;
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView6.DataSource = ds.Tables[0];

            dataGridView3.DataSource = null;
            dataGridView4.DataSource = null;
            dataGridView5.DataSource = null;
            ds.Clear();
            SqlCommand com_result = new SqlCommand("SELECT * FROM [result]", sqlConnection);
            da.SelectCommand = com_result;
            da.Fill(ds);
            dataGridView3.DataSource = ds.Tables[0];
            dataGridView4.DataSource = ds.Tables[0];
            dataGridView5.DataSource = ds.Tables[0];

            dataGridView2.DataSource = null;
            ds.Clear();
            SqlCommand com_rasp = new SqlCommand("SELECT * FROM [rasp]", sqlConnection);
            da.SelectCommand = com_rasp;
            da.Fill(ds);
            dataGridView2.DataSource = ds.Tables[0];
        }

        private void button2_Click(object sender, EventArgs e) // подтверждение
        {
            if (comboBox1.Text != "")
            {
                SqlCommand cver = new SqlCommand("UPDATE [beg] SET [verify] = 1 WHERE [id] = @id", sqlConnection); //обвновить стоблец верифай где айди равен указанному
                cver.Parameters.AddWithValue("id", comboBox1.Text); //указываю что есть айди
                cver.ExecuteNonQuery();
            }
            else MessageBox.Show("Выберите значение", "Ошибка", MessageBoxButtons.OK);
        }

        private void button3_Click(object sender, EventArgs e) // снятие подтверждения
        {
            if (comboBox2.Text != "")
            {
                SqlCommand cver = new SqlCommand("UPDATE [beg] SET [verify] = 0 WHERE [id] = @id", sqlConnection); //обвновить стоблец верифай где айди равен указанному
                cver.Parameters.AddWithValue("id", comboBox2.Text); //указываю что есть айди
                cver.ExecuteNonQuery();
            }
            else MessageBox.Show("Выберите значение", "Ошибка", MessageBoxButtons.OK);
        }

        private void button9_Click(object sender, EventArgs e)// подтверждение
        {
            if (comboBox12.Text != "")
            {
                SqlCommand cver = new SqlCommand("UPDATE [beg] SET [verify] = 1 WHERE [id] = @id", sqlConnection); //обвновить стоблец верифай где айди равен указанному
                cver.Parameters.AddWithValue("id", comboBox12.Text); //указываю что есть айди
                cver.ExecuteNonQuery();
            }
            else MessageBox.Show("Выберите значение", "Ошибка", MessageBoxButtons.OK);
        }

        private void button8_Click(object sender, EventArgs e)// снятие подтверждения
        {
            if (comboBox11.Text != "")
            {
                SqlCommand cver = new SqlCommand("UPDATE [beg] SET [verify] = 0 WHERE [id] = @id", sqlConnection); //обвновить стоблец верифай где айди равен указанному
                cver.Parameters.AddWithValue("id", comboBox11.Text); //указываю что есть айди
                cver.ExecuteNonQuery();
            }
            else MessageBox.Show("Выберите значение", "Ошибка", MessageBoxButtons.OK);
        }

        private void button13_Click(object sender, EventArgs e) // добавление забега
        {
            if (comboBox13.Text != "")
            {
                SqlCommand crasp = new SqlCommand("INSERT INTO [rasp] (dist,date) VALUES (@dist,@date)", sqlConnection); //обвновить стоблец верифай где айди равен указанному
                crasp.Parameters.AddWithValue("dist", comboBox13.Text); //указываю что есть dist
                crasp.Parameters.AddWithValue("date", dateTimePicker1.Value); //указываю что есть date
                crasp.ExecuteNonQuery();
            }
            else MessageBox.Show("Выберите значение", "Ошибка", MessageBoxButtons.OK);
        }
    }
}
