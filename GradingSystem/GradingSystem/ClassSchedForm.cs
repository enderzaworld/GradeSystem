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
    public partial class ClassSchedForm : Form
    {
        String sql = "SELECT CONCAT(`Teacher_FirstName`,' ',`Teacher_LastName`) as `Name`, `advisory_class` FROM `teacher` LEFT JOIN `teacher_schedule` ON `teacher`.`User_ID` = `teacher_schedule`.`User_ID`";
        String s7sql = "SELECT CONCAT(`Teacher_FirstName`,' ',`Teacher_LastName`) as `Name`, `7_subject` FROM `teacher` LEFT JOIN `teacher_schedule` ON `teacher`.`User_ID` = `teacher_schedule`.`User_ID` "+
                        "WHERE `7_GradeLevel` like ";
        String s8sql = "SELECT CONCAT(`Teacher_FirstName`,' ',`Teacher_LastName`) as `Name`, `8_subject` FROM `teacher` LEFT JOIN `teacher_schedule` ON `teacher`.`User_ID` = `teacher_schedule`.`User_ID` " +
                        "WHERE `8_GradeLevel` like ";
        String s9sql = "SELECT CONCAT(`Teacher_FirstName`,' ',`Teacher_LastName`) as `Name`, `9_subject` FROM `teacher` LEFT JOIN `teacher_schedule` ON `teacher`.`User_ID` = `teacher_schedule`.`User_ID` " +
                        "WHERE `9_GradeLevel` like ";
        String s10sql = "SELECT CONCAT(`Teacher_FirstName`,' ',`Teacher_LastName`) as `Name`, `10_subject` FROM `teacher` LEFT JOIN `teacher_schedule` ON `teacher`.`User_ID` = `teacher_schedule`.`User_ID` " +
                        "WHERE `10_GradeLevel` like ";
        String s12sql = "SELECT CONCAT(`Teacher_FirstName`,' ',`Teacher_LastName`) as `Name`, `12_subject` FROM `teacher` LEFT JOIN `teacher_schedule` ON `teacher`.`User_ID` = `teacher_schedule`.`User_ID` " +
                        "WHERE `12_GradeLevel` like ";
        String s1sql = "SELECT CONCAT(`Teacher_FirstName`,' ',`Teacher_LastName`) as `Name`, `1_subject` FROM `teacher` LEFT JOIN `teacher_schedule` ON `teacher`.`User_ID` = `teacher_schedule`.`User_ID` " +
                        "WHERE `1_GradeLevel` like ";
        String s2sql = "SELECT CONCAT(`Teacher_FirstName`,' ',`Teacher_LastName`) as `Name`, `2_subject` FROM `teacher` LEFT JOIN `teacher_schedule` ON `teacher`.`User_ID` = `teacher_schedule`.`User_ID` " +
                        "WHERE `2_GradeLevel` like ";

        public ClassSchedForm()
        {
            InitializeComponent();
            dataGridView1.DataSource = Program.GetDataFromQuery(sql);
            dataGridView1_CellClick(null,null);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.DataSource = Program.GetDataFromQuery(sql + " WHERE `advisory_class` like '%" + textBox1.Text
                    + "%' OR  CONCAT(`Teacher_FirstName`,' ',`Teacher_LastName`) like '%" + textBox1.Text + "%'");
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            write();
        }

        private void write()
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                String name = row.Cells[0].Value.ToString();
                String gradeLevel = row.Cells[1].Value.ToString();
                tamadAdviser.Text = name;
                tamadGradeLevel.Text = gradeLevel;
                MySqlDataReader reader = Program.GetReaderFromQuery(s7sql + "'" + gradeLevel + "'");
                if (reader.Read())
                {
                    s7.Text = reader.GetString(reader.GetOrdinal("7_subject"));
                    t7.Text = reader.GetString(reader.GetOrdinal("name"));
                }
                else
                {
                    s7.Text = "";
                    t7.Text = "";
                }
                reader = Program.GetReaderFromQuery(s8sql + "'" + gradeLevel + "'");
                if (reader.Read())
                {
                    s8.Text = reader.GetString(reader.GetOrdinal("8_subject"));
                    t8.Text = reader.GetString(reader.GetOrdinal("name"));
                }
                else
                {
                    s8.Text = "";
                    t8.Text = "";
                }
                reader = Program.GetReaderFromQuery(s9sql + "'" + gradeLevel + "'");
                if (reader.Read())
                {
                    s9.Text = reader.GetString(reader.GetOrdinal("9_subject"));
                    t9.Text = reader.GetString(reader.GetOrdinal("name"));
                }
                else
                {
                    s9.Text = "";
                    t9.Text = "";
                }
                reader = Program.GetReaderFromQuery(s10sql + "'" + gradeLevel + "'");
                if (reader.Read())
                {
                    s10.Text = reader.GetString(reader.GetOrdinal("10_subject"));
                    t10.Text = reader.GetString(reader.GetOrdinal("name"));
                }
                else
                {
                    s10.Text = "";
                    t10.Text = "";
                }
                reader = Program.GetReaderFromQuery(s12sql + "'" + gradeLevel + "'");
                if (reader.Read())
                {
                    s12.Text = reader.GetString(reader.GetOrdinal("12_subject"));
                    t12.Text = reader.GetString(reader.GetOrdinal("name"));
                }
                else
                {
                    s12.Text = "";
                    t12.Text = "";
                }
                reader = Program.GetReaderFromQuery(s1sql + "'" + gradeLevel + "'");
                if (reader.Read())
                {
                    s1.Text = reader.GetString(reader.GetOrdinal("1_subject"));
                    t1.Text = reader.GetString(reader.GetOrdinal("name"));
                }
                else
                {
                    s1.Text = "";
                    t1.Text = "";
                }
                reader = Program.GetReaderFromQuery(s2sql + "'" + gradeLevel + "'");
                if (reader.Read())
                {
                    s2.Text = reader.GetString(reader.GetOrdinal("2_subject"));
                    t2.Text = reader.GetString(reader.GetOrdinal("name"));
                }
                else
                {
                    s2.Text = "";
                    t2.Text = "";
                }
            }
        }

        private void ClassSchedForm_Load(object sender, EventArgs e)
        {
            write();
        }
    }
}
