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
            /*MySqlConnection cn;
            cn = new MySqlConnection(connectionString);
            try
            {
                cn.Open();
               
            }
            catch (Exception)
            {
                MessageBox.Show("Can not open connection ! ");
            }*/
            string query="";
            try
            {
                username = txtUser.Text;
                password = txtPass.Text;
                //LoginForm login = new LoginForm();
                //MainForm main = new MainForm();
                
                query = "SELECT user.User_ID,`User_Username`, `User_Password`, `Teacher_FirstName`, `Teacher_Position` "+
                    "FROM `user` LEFT JOIN `teacher` ON `user`.`User_ID` = `teacher`.`User_ID` "+
                    "WHERE `User_Username` like '" + txtUser.Text + "' AND `User_Password` like '" + txtPass.Text + "'";
                MySqlDataReader reader = Program.GetReaderFromQuery(query);
                if (reader.Read())
                {
                    txtFirstName.Text = reader.GetString(reader.GetOrdinal("Teacher_FirstName"));
                    txtPosition.Text = reader.GetString(reader.GetOrdinal("Teacher_Position"));
                    
                    Program.user_id = reader.GetString(reader.GetOrdinal("User_ID"));
                    Program.position = txtPosition.Text;
                    MainForm admin = new MainForm();
                    admin.Show();
                    this.Hide();
                    if (txtPosition.Text == "Admin")
                    {
                    }
                    else if(txtPosition.Text == "Teacher")
                    {
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
                MessageBox.Show(ex.Message+"\n"+ username+"\nrawr"+password, "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
