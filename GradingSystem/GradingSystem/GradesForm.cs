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
    public partial class GradesForm : Form
    {
        String Grade = "%";//or 1 to 10
        String Section = "%";//or 1 to 2

        public GradesForm(String Grade, String section)
        {
            this.Grade = Grade;
            this.Section = section;
            InitializeComponent();
            label16.Text = "Grade " + Grade+" "+section;
            load();
        }

        private void load()
        {
            loadBoys();
            loadGirls();
            resizeDGV(dgvBSN, 100, 100);
            resizeDGVPercent(dgvBSG, 10, 10);
            resizeDGV(dgvGSN, 100, 100);
            resizeDGVPercent(dgvGSG, 10, 10);
            resizeRow(dgvBSN, 0);
            resizeRow(dgvGSN, 2);
        }

        private void loadBoys()
        {

            dgvBSN.DataSource = Program.GetDataFromQuery("SELECT `student_FirstName`,`student_MI`,`student_LastName` " +
"FROM `student_profile` " +
"LEFT JOIN `student_all_subject_grade` ON `student_profile`.`student_ID` = `student_all_subject_grade`.`student_ID` " +
"WHERE `student_Level` = 'Grade " + Grade + "' AND `student_Section` = '"+Section+"' "+
"AND `student_Sex` = 'Male'");
            dgvBSG.DataSource = Program.GetDataFromQuery("SELECT `FILIPINO`, `ENGLISH`, `MATH`, `SCIENCE`, `AP`, `VALUES`, `MAPEH`, `TLE`, ((`FILIPINO`+`ENGLISH`+`MATH`+`SCIENCE`+`AP`+`VALUES`+`MAPEH`+`TLE` )/8), FIND_IN_SET( ((`FILIPINO`+`ENGLISH`+`MATH`+`SCIENCE`+`AP`+`VALUES`+`MAPEH`+`TLE` )/8), ( " +
"SELECT GROUP_CONCAT(((`FILIPINO`+`ENGLISH`+`MATH`+`SCIENCE`+`AP`+`VALUES`+`MAPEH`+`TLE` ) / 8) " +
"ORDER BY ((`FILIPINO`+`ENGLISH`+`MATH`+`SCIENCE`+`AP`+`VALUES`+`MAPEH`+`TLE` )/ 8) DESC )  " +
"FROM `student_profile` " +
"LEFT JOIN `student_all_subject_grade` ON `student_all_subject_grade`.`student_ID` = `student_profile`.`student_ID` WHERE `student_profile`.`student_Level` = 'Grade 8' AND `student_Section` = '" + Section + "' )) AS rank " +
"FROM `student_profile` " +
"LEFT JOIN `student_all_subject_grade` ON `student_all_subject_grade`.`student_ID` = `student_profile`.`student_ID` " +
"WHERE `student_profile`.`student_Level` = 'Grade 8' AND `student_sex` = 'male' AND `student_Section` = '" + Section + "' ");
        }

        private void loadGirls()
        {
            dgvGSN.DataSource = Program.GetDataFromQuery("SELECT `student_FirstName`,`student_MI`,`student_LastName` " +
"FROM `student_profile` " +
"LEFT JOIN `student_all_subject_grade` ON `student_profile`.`student_ID` = `student_all_subject_grade`.`student_ID` " +
"WHERE `student_Level` = 'Grade " + Grade + "' AND `student_Section` = '" + Section + "' " +
"AND `student_Sex` = 'Female'");
            dgvGSG.DataSource = Program.GetDataFromQuery("SELECT `FILIPINO`, `ENGLISH`, `MATH`, `SCIENCE`, `AP`, `VALUES`, `MAPEH`, `TLE`, ((`FILIPINO`+`ENGLISH`+`MATH`+`SCIENCE`+`AP`+`VALUES`+`MAPEH`+`TLE` )/8), FIND_IN_SET( ((`FILIPINO`+`ENGLISH`+`MATH`+`SCIENCE`+`AP`+`VALUES`+`MAPEH`+`TLE` )/8), ( " +
"SELECT GROUP_CONCAT(((`FILIPINO`+`ENGLISH`+`MATH`+`SCIENCE`+`AP`+`VALUES`+`MAPEH`+`TLE` ) / 8) " +
"ORDER BY ((`FILIPINO`+`ENGLISH`+`MATH`+`SCIENCE`+`AP`+`VALUES`+`MAPEH`+`TLE` )/ 8) DESC )  " +
"FROM `student_profile` " +
"LEFT JOIN `student_all_subject_grade` ON `student_all_subject_grade`.`student_ID` = `student_profile`.`student_ID` WHERE `student_profile`.`student_Level` = 'Grade 8' AND `student_Section` = '" + Section + "' )) AS rank " +
"FROM `student_profile` " +
"LEFT JOIN `student_all_subject_grade` ON `student_all_subject_grade`.`student_ID` = `student_profile`.`student_ID` " +
"WHERE `student_profile`.`student_Level` = 'Grade 8' AND `student_sex` = 'Female' AND `student_Section` = '" + Section + "' ");
        }

        private void resizeDGV(DataGridView v, int defSize = 50, int lastSize = 100)
        {
            int i;
            for (i = 0; i < v.ColumnCount; i++)
            {
                v.Columns[i].Width = defSize;
                if (i + 1 == v.ColumnCount)
                {
                    v.Columns[i].Width = lastSize;
                }
            }
            v.ClearSelection();
        }

        private void resizeDGVPercent(DataGridView v, int defSize = 50, int lastSize = 100)
        {
            int i;
            for (i = 0; i < v.ColumnCount; i++)
            {
                v.Columns[i].FillWeight = defSize;
                v.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                if (i + 1 == v.ColumnCount)
                {
                    v.Columns[i].FillWeight = lastSize;
                    v.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            v.ClearSelection();
        }

        private void resizeRow(DataGridView v, int num)
        {
            if (v.Rows.Count > 0)
            {
                DataGridViewRow row = v.Rows[0];
                //dgvBName.Height = row.Height * dgvBName.Rows.Count;

                TableLayoutRowStyleCollection styles = tableLayoutPanel2.RowStyles;
                int x = 0;
                foreach (RowStyle style in styles)
                {
                    if (x == num)
                    {
                        style.SizeType = SizeType.Absolute;
                        style.Height = row.Height * (v.Rows.Count + 1);
                    }
                    x++;
                }
            }
            else
            {
                TableLayoutRowStyleCollection styles = tableLayoutPanel2.RowStyles;
                int x = 0;
                foreach (RowStyle style in styles)
                {
                    if (x == num)
                    {
                        style.SizeType = SizeType.Absolute;
                        style.Height = 40 * (v.Rows.Count + 1);
                    }
                    x++;
                }
            }
        }

        private void resizeRowPercent(DataGridView v, int num)
        {
            if (v.Rows.Count > 0)
            {
                DataGridViewRow row = v.Rows[0];
                //dgvBName.Height = row.Height * dgvBName.Rows.Count;

                TableLayoutRowStyleCollection styles = tableLayoutPanel2.RowStyles;
                int x = 0;
                foreach (RowStyle style in styles)
                {
                    if (x == num)
                    {
                        style.SizeType = SizeType.Percent;
                        style.Height = row.Height * (v.Rows.Count + 1);
                    }
                    x++;
                }
            }
            else
            {
                TableLayoutRowStyleCollection styles = tableLayoutPanel2.RowStyles;
                int x = 0;
                foreach (RowStyle style in styles)
                {
                    if (x == num)
                    {
                        style.SizeType = SizeType.AutoSize;
                        style.Height = 40 * (v.Rows.Count + 1);
                    }
                    x++;
                }
            }
        }
    }
}