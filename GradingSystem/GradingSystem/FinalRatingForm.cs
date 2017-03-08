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
    public partial class FinalRatingForm : Form
    {
        String Grade = "%";//or 1 to 10
        String quarter = "1";//1, 2, 3, 4
        String Subject = "Math";//`subject`_total for total
        String teacher = "";
        /*Math
          English
          Science
          Filipino
          S.S.
          M.A.P.E.H
          T.L.E*/
        public FinalRatingForm(String Grade, String quarter, String Subject, String teacher)
        {
            this.Grade = Grade;
            this.quarter = quarter;
            this.Subject = Subject;
            this.teacher = teacher;
            InitializeComponent();
            lbl_grade_lvl.Text = "Grade " + Grade;
            lbl_subject.Text = Subject;
            lblGradelvl.Text = "TeacherName: " + teacher;
            load();
        }

        private void load() {
            loadBoys();
            loadGirls();
            resizeDGV(dgvBSN, 100, 100);
            resizeDGVPercent(dgvBG, 20, 20);
            resizeDGV(dgvGSN, 100, 100);
            resizeDGVPercent(dgvGG, 20, 20);
            resizeRow(dgvBSN, 0);
            resizeRow(dgvGSN, 2);
        }

        private void loadBoys()
        {
            
            dgvBSN.DataSource = Program.GetDataFromQuery("SELECT `student_FirstName`, `student_MI`, `student_LastName` FROM `student_profile` " +
            "LEFT JOIN `student_finalgrade` ON `student_finalgrade`.`student_ID` = `student_profile`.`student_ID` " +
            "WHERE `student_profile`.`student_Level` = 'Grade " + Grade + "' " +
            "AND `student_sex` = 'Male' " +
            "AND `subject` = '" + Subject + "'");
            dgvBG.DataSource = Program.GetDataFromQuery("SELECT `1st_Grading`, `2nd_Grading`, `3rd_Grading`, `4th_Grading`, ((`1st_Grading`+`2nd_Grading`+`3rd_Grading`+`4th_Grading`)/4) as `FINAL` FROM `student_profile` "+
            "LEFT JOIN `student_finalgrade` ON `student_profile`.`student_ID` = `student_finalgrade`.`student_ID` "+
            "WHERE `student_Level` = 'Grade " + Grade + "' " +
            "AND `student_Sex` = 'Male' " +
            "AND `subject` = '" + Subject + "'");
        }

        private void loadGirls()
        {
            dgvGSN.DataSource = Program.GetDataFromQuery("SELECT `student_FirstName`, `student_MI`, `student_LastName` FROM `student_profile` " +
            "LEFT JOIN `student_finalgrade` ON `student_finalgrade`.`student_ID` = `student_profile`.`student_ID` " +
            "WHERE `student_profile`.`student_Level` = 'Grade " + Grade + "' " +
            "AND `student_sex` = 'Female' " +
            "AND `subject` = '" + Subject + "'");
            dgvGG.DataSource = Program.GetDataFromQuery("SELECT `1st_Grading`, `2nd_Grading`, `3rd_Grading`, `4th_Grading`, ((`1st_Grading`+`2nd_Grading`+`3rd_Grading`+`4th_Grading`)/4) as `FINAL` FROM `student_profile` " +
            "LEFT JOIN `student_finalgrade` ON `student_profile`.`student_ID` = `student_finalgrade`.`student_ID` " +
            "WHERE `student_Level` = 'Grade " + Grade + "' " +
            "AND `student_Sex` = 'Female' " +
            "AND `subject` = '" + Subject + "'");
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

                TableLayoutRowStyleCollection styles = tableLayoutPanel7.RowStyles;
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
                TableLayoutRowStyleCollection styles = tableLayoutPanel7.RowStyles;
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

                TableLayoutRowStyleCollection styles = tableLayoutPanel7.RowStyles;
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
                TableLayoutRowStyleCollection styles = tableLayoutPanel7.RowStyles;
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
