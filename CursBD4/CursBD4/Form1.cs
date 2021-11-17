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
            string connstr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\projects\vs\CursBD4\CursBD4\CursBD4\Database.mdf;Integrated Security=True";
            sqlConnection = new SqlConnection(connstr);
        }
    }
}
