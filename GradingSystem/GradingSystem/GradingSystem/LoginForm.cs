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

    public partial class LoginForm : Form
    {
       
        public MySqlCommand cm = new MySqlCommand();
        public MySqlConnection cn = new MySqlConnection();
        
        string username, password;
        public string connectionString = "server=localhost;database=gradingsystem;uid=root;pwd=;";
        public static string FirstName = "txtFirstName.Text" ;

        public LoginForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtFirstName.Visible = false;
            txtPosition.Visible = false;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            MySqlConnection cn;
            cn = new MySqlConnection(connectionString);
            try
            {
                cn.Open();
               
            }
            catch (Exception)
            {
                MessageBox.Show("Can not open connection ! ");
            }

            try
            {
                LoginForm login = new LoginForm();
                MainForm main = new MainForm();
                username = txtUser.Text;
                password = txtPass.Text;
                
                cm.Connection = cn;
                
                string query = "select User_Username, User_Password, Teacher_FirstName, Teacher_Position from user, teacher where user.User_ID = teacher.User_ID and User_Username = '" + txtUser.Text + "' and User_Password = '" + txtPass.Text + "'";
                cm = new MySqlCommand(query, cn);
                MySqlDataReader reader = cm.ExecuteReader();
                if (reader.Read())
                {
                    txtFirstName.Text = reader.GetString(reader.GetOrdinal("Teacher_FirstName"));
                    txtPosition.Text = reader.GetString(reader.GetOrdinal("Teacher_Position"));

                }

                if (reader.HasRows)
                {
                    
MainForm admin = new MainForm();
                    admin.Show();
                        this.Hide();
                    if (txtPosition.Text == "Admin")
                    {
                    }
                    else if(txtPosition.Text == "Teacher")
                    {
                        TeacherMainForm teacher = new TeacherMainForm();
                        teacher.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Invalid Username or Password", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        reader.Close();
                    }
                    
                }
                
                   
                else
                {
                    MessageBox.Show("Invalid Username or Password", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    reader.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            cn = new MySqlConnection(connectionString);
            cn.Open();
        
        }



     

        private void LoginForm_FormClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DialogResult exit = MessageBox.Show("Are you sure you want to exit?", "Nazareth Institute of Alfonso", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (exit == DialogResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                Environment.Exit(0);
            }
        }

    
    }
}
