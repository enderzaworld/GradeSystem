using MySql.Data.MySqlClient;
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
            if (Program.position=="") {
                Program.position = "";
            }
            InitializeComponent();
            if (Program.position == "Admin")
            {
                panelAdmin.Show();
                panelTeacher.Hide();
            }
            else
            {
                panelAdmin.Hide();
                panelTeacher.Show();
            }
            ini();
        }

        public MainForm(String Position)
        {
            if (Program.position == "")
            {
                Program.position = Position;
            }
            InitializeComponent();
            if (Program.position == "Admin")
            {
                panelAdmin.Show();
                panelTeacher.Hide();
            }
            else
            {
                panelAdmin.Hide();
                panelTeacher.Show();
            }
            ini();
        }

        private void ini()
        {
            dataGridView_AClass.DataSource = GetAdvisoryClass();
            dataGridView_SClass.DataSource = GetSubjectClass();
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
            GradingSheetForm sheet = new GradingSheetForm("8","1","Math","","Section 1");
            sheet.Show();
        }



        private void addSchoolyear_Click(object sender, EventArgs e)
        {
            GradingSheetForm add = new GradingSheetForm("8", "1", "Math", "", "Section 1");
            add.Show();
        }

        private void AdminMainForm_Load(object sender, EventArgs e)
        {
            this.Enabled = true;
        }

        private void AdminMainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.position = "";
            Program.user_id = "";
            LoginForm login = new LoginForm();
            login.Show();
        }

        private void addTimeSchedule_Click(object sender, EventArgs e)
        {
            ClassSchedForm form = new ClassSchedForm();
            form.Show();
        }



        private void btnFacultyLoad_Click(object sender, EventArgs e)
        {
            TeacherLoadingForm teach = new TeacherLoadingForm();
            teach.Show();
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.position = "";
            Program.user_id = "";
            Close();
        }

        DataTable GetAdvisoryClass()
        {
            DataTable dt = new DataTable();
            MySqlConnection con;
            con = new MySqlConnection(Program.connectionString);
            con.Open();
            String sql;/*
            if (Program.position=="Admin")
            {
                sql = "SELECT `7_subject` as `Subject` ,`7_GradeLevel` as `Advisory Class`, CONCAT(`Teacher_FirstName`,' ', `Teacher_LastName`) as `Teacher Name`  FROM `teacher_schedule` LEFT JOIN `teacher` ON `teacher`.`User_ID` = `teacher_schedule`.`User_ID`  WHERE `advisory_class` = `7_GradeLevel` " +
                      "UNION " +
                      "SELECT `8_subject`,`8_GradeLevel`, CONCAT(`Teacher_FirstName`,' ', `Teacher_LastName`)  FROM `teacher_schedule` LEFT JOIN `teacher` ON `teacher`.`User_ID` = `teacher_schedule`.`User_ID` WHERE `advisory_class` = `8_GradeLevel` " +
                      "UNION " +
                      "SELECT `9_subject`,`9_GradeLevel`, CONCAT(`Teacher_FirstName`,' ', `Teacher_LastName`)  FROM `teacher_schedule` LEFT JOIN `teacher` ON `teacher`.`User_ID` = `teacher_schedule`.`User_ID` WHERE `advisory_class` = `9_GradeLevel` " +
                      "UNION " +
                      "SELECT `10_subject`,`10_GradeLevel`, CONCAT(`Teacher_FirstName`,' ', `Teacher_LastName`)  FROM `teacher_schedule` LEFT JOIN `teacher` ON `teacher`.`User_ID` = `teacher_schedule`.`User_ID` WHERE `advisory_class` = `10_GradeLevel` " +
                      "UNION " +
                      "SELECT `12_subject`,`12_GradeLevel`, CONCAT(`Teacher_FirstName`,' ', `Teacher_LastName`)  FROM `teacher_schedule` LEFT JOIN `teacher` ON `teacher`.`User_ID` = `teacher_schedule`.`User_ID` WHERE `advisory_class` = `12_GradeLevel` " +
                      "UNION " +
                      "SELECT `1_subject`,`1_GradeLevel`, CONCAT(`Teacher_FirstName`,' ', `Teacher_LastName`)  FROM `teacher_schedule` LEFT JOIN `teacher` ON `teacher`.`User_ID` = `teacher_schedule`.`User_ID` WHERE `advisory_class` = `1_GradeLevel` " +
                      "UNION " +
                      "SELECT `2_subject`,`2_GradeLevel`, CONCAT(`Teacher_FirstName`,' ', `Teacher_LastName`)  FROM `teacher_schedule` LEFT JOIN `teacher` ON `teacher`.`User_ID` = `teacher_schedule`.`User_ID` WHERE `advisory_class` = `2_GradeLevel` ";
            } else*/
            {
                sql = "SELECT `7_subject` as `Subject` ,`7_GradeLevel` as `Advisory Class`, CONCAT(`Teacher_FirstName`,' ', `Teacher_LastName`) as `Teacher Name`, `7_Section` as `Section` FROM `teacher_schedule` LEFT JOIN `teacher` ON `teacher`.`User_ID` = `teacher_schedule`.`User_ID` WHERE `advisory_class` = `7_GradeLevel` AND `teacher`.`User_ID` = " + Program.user_id + " " +
                      "UNION " +
                      "SELECT `8_subject`,`8_GradeLevel`, CONCAT(`Teacher_FirstName`,' ', `Teacher_LastName`), `8_Section`  FROM `teacher_schedule` LEFT JOIN `teacher` ON `teacher`.`User_ID` = `teacher_schedule`.`User_ID` WHERE `advisory_class` = `8_GradeLevel` AND `teacher`.`User_ID` = " + Program.user_id + " " +
                      "UNION " +
                      "SELECT `9_subject`,`9_GradeLevel`, CONCAT(`Teacher_FirstName`,' ', `Teacher_LastName`), `9_Section`  FROM `teacher_schedule` LEFT JOIN `teacher` ON `teacher`.`User_ID` = `teacher_schedule`.`User_ID` WHERE `advisory_class` = `9_GradeLevel` AND `teacher`.`User_ID` = " + Program.user_id + " " +
                      "UNION " +
                      "SELECT `10_subject`,`10_GradeLevel`, CONCAT(`Teacher_FirstName`,' ', `Teacher_LastName`), `10_Section`  FROM `teacher_schedule` LEFT JOIN `teacher` ON `teacher`.`User_ID` = `teacher_schedule`.`User_ID` WHERE `advisory_class` = `10_GradeLevel` AND `teacher`.`User_ID` = " + Program.user_id + " " +
                      "UNION " +
                      "SELECT `12_subject`,`12_GradeLevel`, CONCAT(`Teacher_FirstName`,' ', `Teacher_LastName`), `12_Section`  FROM `teacher_schedule` LEFT JOIN `teacher` ON `teacher`.`User_ID` = `teacher_schedule`.`User_ID` WHERE `advisory_class` = `12_GradeLevel` AND `teacher`.`User_ID` = " + Program.user_id + " " +
                      "UNION " +
                      "SELECT `1_subject`,`1_GradeLevel`, CONCAT(`Teacher_FirstName`,' ', `Teacher_LastName`), `1_Section`  FROM `teacher_schedule` LEFT JOIN `teacher` ON `teacher`.`User_ID` = `teacher_schedule`.`User_ID` WHERE `advisory_class` = `1_GradeLevel` AND `teacher`.`User_ID` = " + Program.user_id +" "+
                      "UNION " +
                      "SELECT `2_subject`,`2_GradeLevel`, CONCAT(`Teacher_FirstName`,' ', `Teacher_LastName`), `2_Section`  FROM `teacher_schedule` LEFT JOIN `teacher` ON `teacher`.`User_ID` = `teacher_schedule`.`User_ID` WHERE `advisory_class` = `2_GradeLevel` AND `teacher`.`User_ID` = " + Program.user_id;
            }
            using (MySqlCommand cmd = new MySqlCommand(sql, con))
            {

                MySqlDataAdapter adpt = new MySqlDataAdapter(cmd);
                adpt.Fill(dt);
            }


            return dt;
        }

        DataTable GetSubjectClass()
        {
            DataTable dt = new DataTable();
            MySqlConnection con;
            con = new MySqlConnection(Program.connectionString);
            con.Open();
            String sql;/*
            if (Program.position == "Admin")
            {
                sql = "SELECT `7_subject` as `subject` ,`7_GradeLevel` as `Grade Level`, CONCAT(`Teacher_FirstName`,' ', `Teacher_LastName`) as `Teacher Name`  FROM `teacher_schedule` LEFT JOIN `teacher` ON `teacher`.`User_ID` = `teacher_schedule`.`User_ID` " +
                      "UNION ALL "+
                      "SELECT `8_subject`,`8_GradeLevel`, CONCAT(`Teacher_FirstName`,' ', `Teacher_LastName`)  FROM `teacher_schedule` LEFT JOIN `teacher` ON `teacher`.`User_ID` = `teacher_schedule`.`User_ID` " +
                      "UNION ALL " +
                      "SELECT `9_subject`,`9_GradeLevel`, CONCAT(`Teacher_FirstName`,' ', `Teacher_LastName`)  FROM `teacher_schedule` LEFT JOIN `teacher` ON `teacher`.`User_ID` = `teacher_schedule`.`User_ID` " +
                      "UNION ALL " +
                      "SELECT `10_subject`,`10_GradeLevel`, CONCAT(`Teacher_FirstName`,' ', `Teacher_LastName`)  FROM `teacher_schedule` LEFT JOIN `teacher` ON `teacher`.`User_ID` = `teacher_schedule`.`User_ID` " +
                      "UNION ALL " +
                      "SELECT `12_subject`,`12_GradeLevel`, CONCAT(`Teacher_FirstName`,' ', `Teacher_LastName`)  FROM `teacher_schedule` LEFT JOIN `teacher` ON `teacher`.`User_ID` = `teacher_schedule`.`User_ID` " +
                      "UNION ALL " +
                      "SELECT `1_subject`,`1_GradeLevel`, CONCAT(`Teacher_FirstName`,' ', `Teacher_LastName`)  FROM `teacher_schedule` LEFT JOIN `teacher` ON `teacher`.`User_ID` = `teacher_schedule`.`User_ID` " +
                      "UNION ALL " +
                      "SELECT `2_subject`,`2_GradeLevel`, CONCAT(`Teacher_FirstName`,' ', `Teacher_LastName`)  FROM `teacher_schedule` LEFT JOIN `teacher` ON `teacher`.`User_ID` = `teacher_schedule`.`User_ID` ";
            }
            else*/
            {
                sql = "SELECT `7_subject` as `subject` ,`7_GradeLevel` as `Grade Level`, CONCAT(`Teacher_FirstName`,' ', `Teacher_LastName`) as `Teacher Name`, `7_Section` as `Section`  FROM `teacher_schedule` LEFT JOIN `teacher` ON `teacher`.`User_ID` = `teacher_schedule`.`User_ID` WHERE `teacher`.`User_ID` = " + Program.user_id + " "+
                      "UNION ALL "+
                      "SELECT `8_subject`,`8_GradeLevel`, CONCAT(`Teacher_FirstName`,' ', `Teacher_LastName`), `8_Section`  FROM `teacher_schedule` LEFT JOIN `teacher` ON `teacher`.`User_ID` = `teacher_schedule`.`User_ID` WHERE `teacher`.`User_ID` = " + Program.user_id + " " +
                      "UNION ALL " +
                      "SELECT `9_subject`,`9_GradeLevel`, CONCAT(`Teacher_FirstName`,' ', `Teacher_LastName`), `9_Section`  FROM `teacher_schedule` LEFT JOIN `teacher` ON `teacher`.`User_ID` = `teacher_schedule`.`User_ID` WHERE `teacher`.`User_ID` = " + Program.user_id + " " +
                      "UNION ALL " +
                      "SELECT `10_subject`,`10_GradeLevel`, CONCAT(`Teacher_FirstName`,' ', `Teacher_LastName`), `10_Section`  FROM `teacher_schedule` LEFT JOIN `teacher` ON `teacher`.`User_ID` = `teacher_schedule`.`User_ID` WHERE `teacher`.`User_ID` = " + Program.user_id + " " +
                      "UNION ALL " +
                      "SELECT `12_subject`,`12_GradeLevel`, CONCAT(`Teacher_FirstName`,' ', `Teacher_LastName`), `12_Section`  FROM `teacher_schedule` LEFT JOIN `teacher` ON `teacher`.`User_ID` = `teacher_schedule`.`User_ID` WHERE `teacher`.`User_ID` = " + Program.user_id + " " +
                      "UNION ALL " +
                      "SELECT `1_subject`,`1_GradeLevel`, CONCAT(`Teacher_FirstName`,' ', `Teacher_LastName`), `1_Section`  FROM `teacher_schedule` LEFT JOIN `teacher` ON `teacher`.`User_ID` = `teacher_schedule`.`User_ID` WHERE `teacher`.`User_ID` = " + Program.user_id + " " +
                      "UNION ALL " +
                      "SELECT `2_subject`,`2_GradeLevel`, CONCAT(`Teacher_FirstName`,' ', `Teacher_LastName`), `2_Section`  FROM `teacher_schedule` LEFT JOIN `teacher` ON `teacher`.`User_ID` = `teacher_schedule`.`User_ID` WHERE `teacher`.`User_ID` = " + Program.user_id + " ";
            }
            using (MySqlCommand cmd = new MySqlCommand(sql, con))
            {

                MySqlDataAdapter adpt = new MySqlDataAdapter(cmd);
                adpt.Fill(dt);
            }


            return dt;
        }
        

        private void dataGridView_AClass_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void t_btnStudent_Click(object sender, EventArgs e)
        {
            AddForm add = new AddForm();
            add.Show();
            this.Hide();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ini();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string gradeLevel = dataGridView_AClass.SelectedRows[0].Cells[1].Value.ToString().Substring(6);
            string subject = dataGridView_AClass.SelectedRows[0].Cells[0].Value.ToString();
            string teacher = dataGridView_AClass.SelectedRows[0].Cells[2].Value.ToString();
            string section = dataGridView_AClass.SelectedRows[0].Cells[3].Value.ToString();
            GradingSheetForm gsf = new GradingSheetForm(gradeLevel, "1", subject, teacher, section);
            gsf.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string gradeLevel = dataGridView_SClass.SelectedRows[0].Cells[1].Value.ToString().Substring(6);
            string subject = dataGridView_SClass.SelectedRows[0].Cells[0].Value.ToString();
            string teacher = dataGridView_SClass.SelectedRows[0].Cells[2].Value.ToString();
            string section = dataGridView_AClass.SelectedRows[0].Cells[3].Value.ToString();
            GradingSheetForm gsf = new GradingSheetForm(gradeLevel, "1", subject, teacher, section);
            gsf.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string gradeLevel = dataGridView_AClass.SelectedRows[0].Cells[1].Value.ToString().Substring(6);
            string section = dataGridView_AClass.SelectedRows[0].Cells[3].Value.ToString();
            GradesForm gsf = new GradesForm(gradeLevel, section);
            gsf.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string gradeLevel = dataGridView_SClass.SelectedRows[0].Cells[1].Value.ToString().Substring(6);
            string section = dataGridView_AClass.SelectedRows[0].Cells[3].Value.ToString();
            GradesForm gsf = new GradesForm(gradeLevel, section);
            gsf.Show();
        }
    }
}
