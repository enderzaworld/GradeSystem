using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace GradingSystem
{
    public partial class GradingSheet : Form
    {
        public GradingSheet()
        {
            InitializeComponent();
        }

        public string connectionString = "server=localhost;database=gradingsystem;uid=root;pwd=;";

 private void GradingSheet_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = GetSNameMale();
            dataGridView1.Columns[0].Width = 100;
            dataGridView1.Columns[1].Width = 100;
            dataGridView1.Columns[2].Width = 100;

            dataGridView2.DataSource = GetPScore();
            dataGridView2.Columns[0].Width = 50;
            dataGridView2.Columns[1].Width = 50;
            dataGridView2.Columns[2].Width = 50;
        }

        DataTable GetSNameMale()
        {
            DataTable dt = new DataTable();
            MySqlConnection con;
            con = new MySqlConnection(connectionString);
            con.Open();
            using (MySqlCommand cmd = new MySqlCommand("select Student_LastName,Student_FirstName, Student_MI from student_profile where Student_Sex = 'Male'", con))
            {

                MySqlDataAdapter adpt = new MySqlDataAdapter(cmd);
                adpt.Fill(dt);
            }


            return dt;
        }

        DataTable GetPScore()
        {

            DataTable dt = new DataTable();
            MySqlConnection con;
            con = new MySqlConnection(connectionString);
            con.Open();
            using (MySqlCommand cmd = new MySqlCommand("select WWS1, WWS2, WWS3 from student_ww ", con))
            {

                MySqlDataAdapter adpt = new MySqlDataAdapter(cmd);
                adpt.Fill(dt);
            }


            return dt;

        }

       
    }
}
