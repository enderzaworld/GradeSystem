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
    public partial class AddForm : Form
    {
        public AddForm()
        {
            InitializeComponent();
        }

        private string conn;
        private MySqlConnection connect;
        private MySqlDataAdapter mySqlDataAdapter;
        private MySqlCommand cmd = new MySqlCommand();
        private MySqlCommand cmd1 = new MySqlCommand();
        private MySqlCommand cmd2 = new MySqlCommand();
        private MySqlCommand cmd3 = new MySqlCommand();
        private string wwID;
        private string wID;

        private void db_connection()
        {
            try
            {
                conn = "Server=localhost;Database=gradingsystem;Uid=root;Pwd=;";
                connect = new MySqlConnection(conn);
                connect.Open();
            }
            catch (MySqlException e)
            {
                MessageBox.Show("Database not connected!");
                throw e;
            }
        }

        private void display_students()
        {
            db_connection();

            mySqlDataAdapter = new MySqlDataAdapter("SELECT * FROM student_profile", conn);
            DataSet DS = new DataSet();
            mySqlDataAdapter.Fill(DS);
            StudentTable.DataSource = DS.Tables[0];

            connect.Close();
            
        }

        private void StudentTable_SelectionChanged(object sender, EventArgs e)
        {
            db_connection();

            int rowindex = StudentTable.CurrentCell.RowIndex;
            var getStudentID = StudentTable.Rows[rowindex].Cells[0].Value.ToString();
         //   var getName = StudentTable.Rows[rowindex].Cells[1].Value.ToString();
          //  var getSex = StudentTable.Rows[rowindex].Cells[2].Value.ToString();
           // var getLevel = StudentTable.Rows[rowindex].Cells[3].Value.ToString();

            txtIDno.Text = getStudentID;
            //v_lblAcctID.Text = getStudentID;

            cmd = new MySqlCommand();
            cmd.CommandText = "SELECT * FROM student_profile WHERE Student_ID ='" + getStudentID + "'";
            cmd.Connection = connect;
            MySqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                
                txtFName.Text = reader.GetString(reader.GetOrdinal("Student_FirstName"));
                txtMI.Text = reader.GetString(reader.GetOrdinal("Student_MI"));
                txtLName.Text = reader.GetString(reader.GetOrdinal("Student_LastName"));
                cmbSex.Text = reader.GetString(reader.GetOrdinal("Student_Sex"));
                cmbGradelvl.Text = reader.GetString(reader.GetOrdinal("Student_Level"));
                comboBox1.Text = reader.GetString(reader.GetOrdinal("student_Section"));

            }
            else
            {
                MessageBox.Show("No records found.");
            }

            connect.Close();
        }

        private void Add_Load(object sender, EventArgs e)
        {
            display_students();
            btnhide();
            
            label9.Visible = true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            btndisable();
            panel1.Enabled = true;
            btnUpdate.Visible = false;
            btnSave.Visible = true;
            btnCancel.Visible = true;
            Clear();
            db_connection();

            string lastID_ = "SELECT Student_ID FROM student_profile ORDER BY Student_ID DESC LIMIT 1";
            cmd1 = new MySqlCommand(lastID_, connect);
            MySqlDataReader reader1 = cmd1.ExecuteReader();
            int y1 = 0;

            if (reader1.Read())
            {
                string getID = reader1.GetString(reader1.GetOrdinal("Student_ID"));
                Int32.TryParse(getID, out y1); //convert count to int as y

                txtIDno.Text = (y1 + 1).ToString();
            }
            reader1.Close();

          
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
            btnhide();
            btnenable();
            panel1.Enabled = false;
            
        }

        

        private void btnEdit_Click(object sender, EventArgs e)
        {
            btndisable();
            db_connection();
            panel1.Enabled = true;
            btnUpdate.Visible = true;
            btnCancel.Visible = true;
            int rowindex = StudentTable.CurrentCell.RowIndex;
            var getStudentID = StudentTable.Rows[rowindex].Cells[0].Value.ToString();
            var getName = StudentTable.Rows[rowindex].Cells[1].Value.ToString();
            var getSex = StudentTable.Rows[rowindex].Cells[2].Value.ToString();
            var getLevel = StudentTable.Rows[rowindex].Cells[3].Value.ToString();

            txtIDno.Text = getStudentID;
            //v_lblAcctID.Text = getStudentID;

            cmd = new MySqlCommand();
            cmd.CommandText = "SELECT * FROM student_profile WHERE Student_ID ='" + getStudentID + "'";
            cmd.Connection = connect;
            MySqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                txtFName.Text = reader.GetString(reader.GetOrdinal("Student_FirstName"));
                txtMI.Text = reader.GetString(reader.GetOrdinal("Student_MI"));
                txtLName.Text = reader.GetString(reader.GetOrdinal("Student_LastName"));
                cmbSex.Text = reader.GetString(reader.GetOrdinal("Student_Sex"));
                cmbGradelvl.Text = reader.GetString(reader.GetOrdinal("Student_Level"));
                comboBox1.Text = reader.GetString(reader.GetOrdinal("Student_Section"));
            }
            reader.Close();


            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            db_connection();
            label9.Visible = false;
            if (txtFName.Text == "" || txtLName.Text == "" || txtMI.Text == "" || cmbSex.Text == null || cmbGradelvl.Text == null)
            {
                MessageBox.Show("Please complete the form.");
            }
            else
            {
                
                string lastwwID_ = "SELECT Written_ID FROM student_ww ORDER BY Written_ID DESC LIMIT 1";
                cmd2 = new MySqlCommand(lastwwID_, connect);
                MySqlDataReader reader2 = cmd2.ExecuteReader();
                int y2 = 0;
                if (reader2.Read())
                {
                    string getwwID = reader2.GetString(reader2.GetOrdinal("Written_ID"));
                    Int32.TryParse(getwwID, out y2); //convert count to int as y
                    wwID = (y2 + 1).ToString();
                }
                reader2.Close();
                

                
                string lastwID_ = "SELECT Work_ID FROM written_work ORDER BY Work_ID DESC LIMIT 1";
                cmd3 = new MySqlCommand(lastwID_, connect);
                MySqlDataReader reader3 = cmd3.ExecuteReader();
                int y3 = 0;
                if (reader3.Read())
                {
                    string getwID = reader3.GetString(reader3.GetOrdinal("Work_ID"));
                    Int32.TryParse(getwID, out y3); //convert count to int as y
                    wID = (y3 + 1).ToString();
                }
                reader3.Close();
               

                DialogResult dr = MessageBox.Show("Do you really want to add this student?", "ADD", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);


                if (dr == DialogResult.Yes)
                {
                    try
                    {
                       
                        string query1 = "INSERT INTO student_profile(Student_ID,Student_FirstName,Student_MI,Student_LastName,Student_Sex,Student_Level,Student_Section) VALUES('" + txtIDno.Text + "','" + txtFName.Text + "','" + txtMI.Text + "','" + txtLName.Text + "','" + cmbSex.Text + "','" + cmbGradelvl.Text + "','" + comboBox1.Text + "');";
                        cmd = new MySqlCommand(query1, connect);
                        string query2 = "INSERT INTO student_ww(Written_ID, Student_ID) VALUES('" + wwID + "','" + txtIDno.Text + "');";
                        cmd1 = new MySqlCommand(query2, connect);
                        string query3 = "INSERT INTO written_work(Work_ID, Written_ID) VALUES('" + wID + "','" + wwID + "');";
                        cmd2 = new MySqlCommand(query3, connect);

                        if (cmd.ExecuteNonQuery() == 1 && cmd1.ExecuteNonQuery() == 1 && cmd2.ExecuteNonQuery() == 1)
                        {
                            MessageBox.Show("student successfully added.");


                        }
                        else
                        {
                            MessageBox.Show("student not added.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    display_students();
                    panel1.Enabled = false;
                    btnSave.Visible = false;
                    
                    btnCancel.Visible = false;
                    btnenable();



                }
                
                }
            connect.Close();
            
        }


        private void Clear()
        {
            txtFName.Text = "";
            txtMI.Text = "";
            txtLName.Text = "";
            cmbGradelvl.Text = "";
            cmbSex.Text = "";
        }

        

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you really want to update this student?", "UPDATE", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            string id = txtIDno.Text;

            if (dr == DialogResult.Yes)
            {
                string query1 = "UPDATE student_profile SET Student_FirstName='" + txtFName.Text + "', Student_MI='" + txtMI.Text + "', Student_LastName='" + txtLName.Text + "', Student_Sex='" + cmbSex.Text + "', Student_Level='" + cmbGradelvl.Text + "', Student_Section='" + comboBox1.Text + "' WHERE Student_ID ='" + id + "'";
                cmd = new MySqlCommand(query1, connect);

                if (cmd.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("student successfully updated.");



                }
                else
                {
                    MessageBox.Show("student not updated.");
                }


                display_students();
                panel1.Enabled = false;
                btnhide();
                btnenable();

            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            db_connection();
            int rowindex = StudentTable.CurrentCell.RowIndex;
            var getStudentID = StudentTable.Rows[rowindex].Cells[0].Value.ToString();
            var getName = StudentTable.Rows[rowindex].Cells[1].Value.ToString();
            var getSex = StudentTable.Rows[rowindex].Cells[2].Value.ToString();
            var getLevel = StudentTable.Rows[rowindex].Cells[3].Value.ToString();

            txtIDno.Text = getStudentID;
            //v_lblAcctID.Text = getStudentID;

            cmd = new MySqlCommand();
            cmd.CommandText = "SELECT * FROM student_profile WHERE Student_ID ='" + getStudentID + "'";
            cmd.Connection = connect;
            MySqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {

                txtFName.Text = reader.GetString(reader.GetOrdinal("Student_FirstName"));
                txtMI.Text = reader.GetString(reader.GetOrdinal("Student_MI"));
                txtLName.Text = reader.GetString(reader.GetOrdinal("Student_LastName"));
                cmbSex.Text = reader.GetString(reader.GetOrdinal("Student_Sex"));
                cmbGradelvl.Text = reader.GetString(reader.GetOrdinal("Student_Level"));
                comboBox1.Text = reader.GetString(reader.GetOrdinal("Student_Section"));


            }
            reader.Close();
             
            DialogResult dr = MessageBox.Show("Do you really want to delete this student?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            string id = txtIDno.Text;

            if (dr == DialogResult.Yes)
            {
                string query1 = "Delete  FROM student_profile WHERE Student_ID ='" + id + "'";
                cmd = new MySqlCommand(query1, connect);

                if (cmd.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("student successfully deleted.");

 display_students();

                }
                else
                {
                    MessageBox.Show("Student not deleted.");
                }
               
            }
            connect.Close();

            }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();    
        }

        private void AddForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainForm admin = new MainForm();
            admin.Show();
        }

        private void btnhide()
        {
            btnSave.Hide();
            btnUpdate.Hide();
            btnCancel.Hide();
        }

        private void btnenable()
        {
            btnAdd.Enabled = true;
            btnDelete.Enabled = true;
            btnEdit.Enabled = true;
            btnClose.Enabled = true;
        }

        private void btndisable()
        {
            btnAdd.Enabled = false;
            btnDelete.Enabled = false;
            btnEdit.Enabled = false;
            btnClose.Enabled = false;
        }
    }
}
