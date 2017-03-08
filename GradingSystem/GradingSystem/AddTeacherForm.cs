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
    public partial class AddTeacherForm : Form
    {
        public AddTeacherForm()
        {
            InitializeComponent();
        }

        private string conn;
        private MySqlConnection connect;
        private MySqlDataAdapter mySqlDataAdapter;
        private MySqlCommand cmd;

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

        private void display_teachers()
        {
            db_connection();

            mySqlDataAdapter = new MySqlDataAdapter("SELECT User.User_ID, User_Username, User_Status, Teacher_FirstName, Teacher_LastName, Teacher_Sex, Teacher_Position from user LEFT JOIN teacher ON User.User_ID = teacher.User_ID", conn);
            DataSet DS = new DataSet();
            mySqlDataAdapter.Fill(DS);
            TeacherTable.DataSource = DS.Tables[0];

            connect.Close();
        }

        private void AddTeacherForm_Load(object sender, EventArgs e)
        {
            display_teachers();
            panelAdd.Hide();
        }

        private void TeacherTable_SelectionChanged(object sender, EventArgs e)
        {

            db_connection();
            //view
            try
            {
                int rowindex = TeacherTable.CurrentCell.RowIndex;
                var getTeacherID = TeacherTable.Rows[rowindex].Cells[0].Value.ToString();
                //      var getUser = TeacherTable.Rows[rowindex].Cells[1].Value.ToString();
                //     var getPass = TeacherTable.Rows[rowindex].Cells[2].Value.ToString();
                var getStatus = TeacherTable.Rows[rowindex].Cells[3].Value.ToString();

                txtIDno.Text = getTeacherID;
                v_IDno.Text = getTeacherID;

                cmd = new MySqlCommand();
                cmd.CommandText = "SELECT * FROM user LEFT JOIN teacher ON User.User_ID = teacher.User_ID where User.User_ID='" + getTeacherID + "'";
                cmd.Connection = connect;
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    //view
                    try
                    {
                        v_FName.Text = reader.GetString(reader.GetOrdinal("Teacher_FirstName"));
                        txtFName.Text = reader.GetString(reader.GetOrdinal("Teacher_FirstName"));
                    }
                    catch (Exception ex)
                    {
                        v_FName.Text = "";
                        txtFName.Text = "";
                    }
                    try
                    {
                        v_LName.Text = reader.GetString(reader.GetOrdinal("Teacher_LastName"));
                        txtLName.Text = reader.GetString(reader.GetOrdinal("Teacher_LastName"));
                    }
                    catch (Exception ex)
                    {
                        v_LName.Text = "";
                        txtLName.Text = "";
                    }
                    try
                    {
                        v_Sex.Text = reader.GetString(reader.GetOrdinal("Teacher_Sex"));
                        cmbSex.Text = reader.GetString(reader.GetOrdinal("Teacher_Sex"));
                    }
                    catch (Exception ex)
                    {
                        v_Sex.Text = "";
                        cmbSex.Text = "";
                    }
                    try
                    {
                        v_Position.Text = reader.GetString(reader.GetOrdinal("Teacher_Position"));
                        cmbPosition.Text = reader.GetString(reader.GetOrdinal("Teacher_Position"));
                    }
                    catch (Exception ex)
                    {
                        v_Position.Text = "";
                        cmbPosition.Text = "";
                    }

                    //Add
                    try
                    {
                        txtIDno.Text = reader.GetString(reader.GetOrdinal("User_ID"));
                    }
                    catch (Exception ex)
                    {
                        txtIDno.Text = "";
                    }
                    try
                    {
                        txtUsername.Text = reader.GetString(reader.GetOrdinal("User_Username"));
                    }
                    catch (Exception ex)
                    {
                        txtUsername.Text = "";
                    }
                    try
                    {
                        getStatus = reader.GetString(reader.GetOrdinal("User_Status"));
                    }
                    catch (Exception ex)
                    {
                        getStatus = "none";
                    }

                    if (getStatus == "Active")
                    {
                        rbtnActive.Checked = true;
                    }
                    else if (getStatus == "Inactive")
                    {
                        rbtnInactive.Checked = true;
                    }
                    else
                    {
                        rbtnActive.Checked = false;
                        rbtnInactive.Checked = false;
                    }


                }
            }
            catch (Exception ex)
            {

            }
            connect.Close();
        }

        private void AddTeacherForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainForm main = new MainForm();
            main.Show();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            txtIDno.Enabled = true;
            btnSave.Text = "Save";
            panelView.Hide();
            panelAdd.Show();

            v_FName.Text = "";
            v_LName.Text = "";
            v_Sex.Text = "";
            v_Position.Text = "";
            txtIDno.Text = "";
            txtUsername.Text = "";
            rbtnActive.Checked = true;
            rbtnInactive.Checked = false;

            db_connection();

            cmd = new MySqlCommand();
            cmd.CommandText = "SELECT (`user`.`User_ID`+1) as User_ID FROM user LEFT JOIN teacher ON User.User_ID = teacher.User_ID ORDER BY `user`.`User_ID` DESC LIMIT 1";
            cmd.Connection = connect;

            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                txtIDno.Text = reader.GetString(reader.GetOrdinal("User_ID"));
            }
            connect.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text == txtCPassword.Text)
            {
                db_connection();
                cmd = new MySqlCommand();
                cmd.CommandText = "SELECT * FROM user LEFT JOIN teacher ON User.User_ID = teacher.User_ID where User.User_ID='" + txtIDno.Text + "'";
                cmd.Connection = connect;
                MySqlDataReader reader = cmd.ExecuteReader();

                String active = "Inactive";
                if (rbtnActive.Checked)
                {
                    active = "Active";
                }
                if (reader.Read())
                {
                    connect.Close();
                    db_connection();
                    if (txtPassword.Text != "")
                    {
                        cmd.CommandText = "UPDATE `user` SET `User_Username`='" + txtUsername.Text + "',`User_Password`='" + txtPassword.Text + "',`User_Status`='" + active + "' WHERE User.User_ID='" + txtIDno.Text + "'";
                    }
                    else
                    {
                        cmd.CommandText = "UPDATE `user` SET `User_Username`='" + txtUsername.Text + "',`User_Status`='" + active + "' WHERE User.User_ID='" + txtIDno.Text + "'";
                    }
                    cmd.Connection = connect;
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "UPDATE `teacher` SET `Teacher_FirstName`='" + txtFName.Text + "',`Teacher_LastName`='" + txtLName.Text + "',`Teacher_Sex`='" + cmbSex.Text + "'`Teacher_Position`='" + cmbPosition.Text + "' WHERE User.User_ID='" + txtIDno.Text + "'";
                    cmd.Connection = connect;
                    cmd.ExecuteNonQuery();
                    connect.Close();
                    if (txtPassword.Text != "")
                    {
                        DialogResult result = MessageBox.Show("Password Updated", "Updated Password", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    db_connection();
                    cmd.CommandText = "INSERT INTO `user`(`User_ID`, `User_Username`, `User_Password`, `User_Status`) " +
                        "VALUES ('" + txtIDno.Text + "','" + txtUsername.Text + "','" + txtPassword.Text + "','" + active + "')";
                    cmd.Connection = connect;
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "INSERT INTO `teacher`(`User_ID`, `Teacher_FirstName`, `Teacher_LastName`, `Teacher_Sex`, `Teacher_Position`) " +
                        "VALUES('" + txtIDno.Text + "','" + txtFName.Text + "','" + txtLName.Text + "','" + cmbSex.Text + "','" + cmbPosition.Text + "')";
                    cmd.Connection = connect;
                    cmd.ExecuteNonQuery();
                    connect.Close();
                }
            }
            else
            {
                DialogResult result = MessageBox.Show("Password Mismatch", "Check Password", MessageBoxButtons.OK);
            }
            connect.Close();
            display_teachers();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            panelView.Show();
            panelAdd.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TeacherTable_SelectionChanged(sender, e);
            txtIDno.Enabled = false;
            btnSave.Text = "Update";
            panelView.Hide();
            panelAdd.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {   //"DELETE FROM `user` WHERE User.User_ID='" + txtIDno.Text + "'"
            //"DELETE FROM `teacher` WHERE User.User_ID='" + txtIDno.Text + "'"
            TeacherTable_SelectionChanged(sender, e);
            DialogResult result = MessageBox.Show("Do you want to delete selected user?", "Confirmation", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                db_connection();
                cmd = new MySqlCommand();
                cmd.CommandText = "DELETE FROM `user` WHERE User_ID = '" + txtIDno.Text + "'";
                cmd.Connection = connect;
                cmd.ExecuteNonQuery();
                cmd.CommandText = "DELETE FROM `teacher` WHERE User_ID='" + txtIDno.Text + "'";
                cmd.Connection = connect;
                cmd.ExecuteNonQuery();
                connect.Close();
            }
            else if (result == DialogResult.No)
            {
                //...
            }
        }
    }
}