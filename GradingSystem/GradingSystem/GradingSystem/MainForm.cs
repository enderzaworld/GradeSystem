using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GradingSystem
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
 
        private void addStudent_Click(object sender, EventArgs e)
        {
            AddForm add = new AddForm();
            add.Show();
            this.Hide();
        }

        private void addTeacher_Click(object sender, EventArgs e)
        {
            AddTeacherForm teacher = new AddTeacherForm();
            teacher.Show();
            this.Hide();
        }

  

        private void addGradelvl_Click(object sender, EventArgs e)
        {
            GradingSheet sheet = new GradingSheet();
            sheet.Show();
        }



        private void addSchoolyear_Click(object sender, EventArgs e)
        {
            GradingSheet add = new GradingSheet();
            add.Show();
        }

        private void AdminMainForm_Load(object sender, EventArgs e)
        {
            this.Enabled = true;
        }

        private void AdminMainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            LoginForm login = new LoginForm();
            login.Show();
        }

        private void addTimeSchedule_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.Show();
        }
    }
}
