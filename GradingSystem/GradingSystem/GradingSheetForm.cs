﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Threading;

namespace GradingSystem
{
    public partial class GradingSheetForm : Form
    {
        String Grade = "%";//or 1 to 10
        String Subject = "Math";//`subject`_total for total
        /*Math
          English
          Science
          Filipino
          S.S.
          M.A.P.E.H
          T.L.E*/
        String teacher = "";
        String section = "%";
        String quarter = "1";//1, 2, 3, 4

        float WrittenWorkPercent = 30;
        float PerformancePercent = 50;
        float QuarterlyPercent = 20;

        public GradingSheetForm(String Grade, String quarter, String Subject, String teacher, String section)
        {
            this.Grade = Grade;
            this.quarter = quarter;
            this.Subject = Subject;
            this.teacher = teacher;
            this.section = section;
            InitializeComponent();
            lblGradelvl.Text = "Grade " + Grade;
            lbl_grade.Text = section;
            lblSubject.Text = Subject;
            quarterCmBx.SelectedIndex = Int32.Parse(quarter) - 1;
            lblGradelvl.Text = "TeacherName: " + teacher;
            switch (Subject)
            {
                default:break;
                case "English":
                case "Filipino":
                case "S.S.":
                    WrittenWorkPercent = 30;
                    PerformancePercent = 50;
                    QuarterlyPercent = 20;
                    break;
                case "Math":
                case "Science":
                    WrittenWorkPercent = 40;
                    PerformancePercent = 40;
                    QuarterlyPercent = 20;
                    break;
                case "M.A.P.E.H":
                case "T.L.E":
                    WrittenWorkPercent = 20;
                    PerformancePercent = 60;
                    QuarterlyPercent = 20;
                    break;
            }
            lbl_written_work.Text = "WRITTEN WORK(" + WrittenWorkPercent + " %)";
            lbl_performance_task.Text = "PERFORMANCE TASK (" + PerformancePercent + "%)";
            lbl_quarter.Text = "QUARTERLY\nASSESSMENT\n        (" + QuarterlyPercent + " %)";
        }

        private void GradingSheet_Load(object sender, EventArgs e)
        {
            MySqlDataReader wT = GetWTotal();
            if (wT.Read())
            {
                txtbx_w1.Text = wT.GetString(0);
                txtbx_w2.Text = wT.GetString(1);
                txtbx_w3.Text = wT.GetString(2);
                txtbx_w4.Text = wT.GetString(3);
                txtbx_w5.Text = wT.GetString(4);
                txtbx_w6.Text = wT.GetString(5);
                txtbx_w7.Text = wT.GetString(6);
                txtbx_w8.Text = wT.GetString(7);
                txtbx_wT.Text = wT.GetString(8);
            }
            MySqlDataReader pT = GetPTotal();
            if (pT.Read())
            {
                txtbx_p1.Text = pT.GetString(0);
                txtbx_p2.Text = pT.GetString(1);
                txtbx_p3.Text = pT.GetString(2);
                txtbx_p4.Text = pT.GetString(3);
                txtbx_p5.Text = pT.GetString(4);
                txtbx_p6.Text = pT.GetString(5);
                txtbx_p7.Text = pT.GetString(6);
                txtbx_p8.Text = pT.GetString(7);
                txtbx_pT.Text = pT.GetString(8);
            }
            MySqlDataReader qT = GetQTotal();
            if (qT.Read())
            {
                txtbx_qT.Text = qT.GetString(0);
            }
            loadMale();
            loadFemale();
            //manageCompute(sender,e);
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
        }

        /*private void resizeRow(DataGridView v, int num)
        {
            if (v.Rows.Count > 0)
            {
                DataGridViewRow row = v.Rows[0];
                //dgvBName.Height = row.Height * dgvBName.Rows.Count;

                TableLayoutRowStyleCollection styles = tableLayoutPanel6.RowStyles;
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
                TableLayoutRowStyleCollection styles = tableLayoutPanel6.RowStyles;
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
        }*/

        private void resizeRow(DataGridView v, int num, DataGridView v1, int num1)
        {
            Height = 24;
            if (v.Rows.Count > 0)
            {
                DataGridViewRow row = v.Rows[0];
                //dgvBName.Height = row.Height * dgvBName.Rows.Count;

                TableLayoutRowStyleCollection styles = tableLayoutPanel6.RowStyles;
                int x = 0;
                foreach (RowStyle style in styles)
                {
                    if (x == num)
                    {
                        style.SizeType = SizeType.Absolute;
                        style.Height = row.Height * (v.Rows.Count + 1);
                        Height+= row.Height * (v.Rows.Count + 1);
                    }
                    x++;
                }
            }
            else
            {
                TableLayoutRowStyleCollection styles = tableLayoutPanel6.RowStyles;
                int x = 0;
                foreach (RowStyle style in styles)
                {
                    if (x == num)
                    {
                        style.SizeType = SizeType.Absolute;
                        style.Height = 40 * (v.Rows.Count + 1);
                        Height += 40 * (v.Rows.Count + 1);
                    }
                    x++;
                }
            }
            if (v1.Rows.Count > 0)
            {
                DataGridViewRow row = v1.Rows[0];
                //dgvBName.Height = row.Height * dgvBName.Rows.Count;

                TableLayoutRowStyleCollection styles = tableLayoutPanel6.RowStyles;
                int x = 0;
                foreach (RowStyle style in styles)
                {
                    if (x == num1)
                    {
                        style.SizeType = SizeType.Absolute;
                        style.Height = row.Height * (v1.Rows.Count + 1);
                        Height += row.Height * (v1.Rows.Count + 1);
                    }
                    x++;
                }
            }
            else
            {
                TableLayoutRowStyleCollection styles = tableLayoutPanel6.RowStyles;
                int x = 0;
                foreach (RowStyle style in styles)
                {
                    if (x == num1)
                    {
                        style.SizeType = SizeType.Absolute;
                        style.Height = 40 * (v1.Rows.Count + 1);
                        Height += 40 * (v1.Rows.Count + 1);
                    }
                    x++;
                }
            }
            tableLayoutPanel6.Height = Height;
        }

        private void loadMale()
        {
            dgvBName.DataSource = GetSNameMale();
            dgvBWW.DataSource = GetWScoreMale();
            dgvBPT.DataSource = GetPScoreMale();
            dgvBQA.DataSource = GetQScoreMale();
            resizeDGV(dgvBName,100);
            resizeDGV(dgvBWW);
            resizeDGV(dgvBPT);
            resizeDGV(dgvBQA);
        }

        private void loadFemale()
        {
            dgvGName.DataSource = GetSNameFemale();
            dgvGWW.DataSource = GetWScoreFemale();
            dgvGPT.DataSource = GetPScoreFemale();
            dgvGQA.DataSource = GetQScoreFemale();
            resizeDGV(dgvGName, 100);
            resizeRow(dgvBName, 0, dgvGName, 2);
            resizeDGV(dgvGWW);
            resizeDGV(dgvGPT);
            resizeDGV(dgvGQA);
        }

        private void checkData(DataGridView refDGV, DataGridView checkDGV,int len=9)
        {
            float[,] refe = getFloat2dArray(refDGV);
            float[,] data = getFloat2dArray(checkDGV);
            if (data.GetLength(0) == 0 && refe.GetLength(0) > 0)
            {
                data = new float[refe.GetLength(0), len];
                setDataToGridView(checkDGV, data);
            }else
            if (data.GetLength(0) != refe.GetLength(0))
            {
                float[,] tempData = data.Clone() as float[,];
                data = new float[refe.GetLength(0), len];
                for (int i = 0; i < tempData.GetLength(0); i++)
                    for (int x = 0; x < tempData.GetLength(1); x++)
                        try { data[i, x] = tempData[i, x]; } catch { }
                setDataToGridView(checkDGV, data);
            }
        }

        MySqlDataReader GetWTotal()
        {
            return Program.GetReaderFromQuery("SELECT `WWS1`, `WWS2`, `WWS3`, `WWS4`, `WWS5`, `WWS6`, `WWS7`, `WWS8`, (`WWS1` + `WWS2` + `WWS3` + `WWS4` + `WWS5` + `WWS6` + `WWS7` + `WWS8`) as `TOTAL` " +
            "FROM `student_profile` " +
            "LEFT JOIN `student_ww` ON `student_ww`.`student_ID` = `student_profile`.`student_ID` " +
            "WHERE `student_Level` like 'Grade " + Grade + "' " +
            "AND `student_Section` = '" + section + "' " +
            "AND (`subject` like '" + Subject + "_total' OR `subject` = NULL ) " +
            "AND (`quarter_ID` = '" + quarter + "' OR `quarter_ID` = NULL ) ");
        }
        MySqlDataReader GetPTotal()
        {
            return Program.GetReaderFromQuery("SELECT `PTS1`, `PTS2`, `PTS3`, `PTS4`, `PTS5`, `PTS6`, `PTS7`, `PTS8`, (`PTS1` + `PTS2` + `PTS3` + `PTS4` + `PTS5` + `PTS6` + `PTS7` + `PTS8`) as `TOTAL`" +
            "FROM `student_profile` " +
            "LEFT JOIN `student_perf` ON `student_perf`.`student_ID` = `student_profile`.`student_ID`" +
            "WHERE `student_Level` like 'Grade " + Grade + "' " +
            "AND `student_Section` = '" + section + "' " +
            "AND (`subject` like '" + Subject + "_total' OR `subject` = NULL ) " +
            "AND (`quarter_ID` = '" + quarter + "' OR `quarter_ID` = NULL ) ");
        }
        MySqlDataReader GetQTotal()
        {
            return Program.GetReaderFromQuery("SELECT `quarterly_score` " +
            "FROM `student_profile` " +
            "LEFT JOIN `student_qa` ON `student_qa`.`student_ID` = `student_profile`.`student_ID`" +
            "WHERE `student_Level` like 'Grade " + Grade + "' " +
            "AND `student_Section` = '" + section + "' " +
            "AND (`subject` like '" + Subject + "_total' OR `subject` = NULL ) " +
            "AND (`quarter_ID` = '" + quarter + "' OR `quarter_ID` = NULL ) ");
        }
        DataTable GetSNameMale()
        {
            return Program.GetDataFromQuery("SELECT `Student_LastName`,`Student_FirstName`, `Student_MI` " +
                "FROM `student_profile` " +
                "WHERE `Student_Sex` like 'Male' " +
                "AND `student_Level` like 'Grade " + Grade + "' AND `student_Section` = '" + section + "' ");
        }
        DataTable GetWScoreMale()
        {
            return Program.GetDataFromQuery("SELECT `WWS1`, `WWS2`, `WWS3`, `WWS4`, `WWS5`, `WWS6`, `WWS7`, `WWS8`, (`WWS1` + `WWS2` + `WWS3` + `WWS4` + `WWS5` + `WWS6` + `WWS7` + `WWS8`) as `TOTAL` " +
            "FROM `student_profile` " +
            "LEFT JOIN `student_ww` ON `student_ww`.`student_ID` = `student_profile`.`student_ID` " +
            "WHERE `Student_Sex` like 'Male' " +
            "AND `student_Level` like 'Grade " + Grade + "' "+
            "AND `student_Section` = '" + section + "' "+
            "AND (`subject` like '" + Subject + "' OR `subject` = NULL ) " +
            "AND (`quarter_ID` = '" + quarter + "' OR `quarter_ID` = NULL ) " );
        }
        DataTable GetPScoreMale()
        {
            return Program.GetDataFromQuery("SELECT `PTS1`, `PTS2`, `PTS3`, `PTS4`, `PTS5`, `PTS6`, `PTS7`, `PTS8`, (`PTS1` + `PTS2` + `PTS3` + `PTS4` + `PTS5` + `PTS6` + `PTS7` + `PTS8`) as `TOTAL`" +
            "FROM `student_profile` " +
            "LEFT JOIN `student_perf` ON `student_perf`.`student_ID` = `student_profile`.`student_ID`" +
            "WHERE `Student_Sex` like 'Male' " +
            "AND `student_Level` like 'Grade " + Grade + "' "+
            "AND `student_Section` = '" + section + "' "+
            "AND (`subject` like '" + Subject + "' OR `subject` = NULL ) " +
            "AND (`quarter_ID` = '" + quarter + "' OR `quarter_ID` = NULL ) " );
        }
        DataTable GetQScoreMale()
        {
            return Program.GetDataFromQuery("SELECT `quarterly_score` " +
            "FROM `student_profile` " +
            "LEFT JOIN `student_qa` ON `student_qa`.`student_ID` = `student_profile`.`student_ID`" +
            "WHERE `Student_Sex` like 'Male' " +
            "AND `student_Level` like 'Grade " + Grade + "' " +
            "AND `student_Section` = '" + section + "' " +
            "AND (`subject` like '" + Subject + "' OR `subject` = NULL ) " +
            "AND (`quarter_ID` = '" + quarter + "' OR `quarter_ID` = NULL ) ");
        }
        DataTable GetSNameFemale()
        {
            return Program.GetDataFromQuery("SELECT `Student_LastName`,`Student_FirstName`, `Student_MI` "+
                "FROM `student_profile` "+
                "WHERE `Student_Sex` like 'Female' " +
                "AND `student_Level` like 'Grade "+Grade+"' AND `student_Section` = '"+ section + "' ");
        }
        DataTable GetWScoreFemale()
        {
            return Program.GetDataFromQuery("SELECT `WWS1`, `WWS2`, `WWS3`, `WWS4`, `WWS5`, `WWS6`, `WWS7`, `WWS8`, (`WWS1` + `WWS2` + `WWS3` + `WWS4` + `WWS5` + `WWS6` + `WWS7` + `WWS8`) as `TOTAL`" +
            "FROM `student_profile` " +
            "LEFT JOIN `student_ww` ON `student_ww`.`student_ID` = `student_profile`.`student_ID`" +
            "WHERE `Student_Sex` like 'Female' " +
            "AND `student_Level` like 'Grade " + Grade + "' " +
            "AND `student_Section` = '" + section + "' " +
            "AND (`subject` like '" + Subject + "' OR `subject` = NULL ) " +
            "AND (`quarter_ID` = '" + quarter + "' OR `quarter_ID` = NULL ) ");
        }
        DataTable GetPScoreFemale()
        {
            return Program.GetDataFromQuery("SELECT `PTS1`, `PTS2`, `PTS3`, `PTS4`, `PTS5`, `PTS6`, `PTS7`, `PTS8`, (`PTS1` + `PTS2` + `PTS3` + `PTS4` + `PTS5` + `PTS6` + `PTS7` + `PTS8`) as `TOTAL`" +
            "FROM `student_profile` " +
            "LEFT JOIN `student_perf` ON `student_perf`.`student_ID` = `student_profile`.`student_ID`" +
            "WHERE `Student_Sex` like 'Female' " +
            "AND `student_Level` like 'Grade " + Grade + "' " +
            "AND `student_Section` = '" + section + "' " +
            "AND (`subject` like '" + Subject + "' OR `subject` = NULL ) " +
            "AND (`quarter_ID` = '" + quarter + "' OR `quarter_ID` = NULL ) ");
        }
        DataTable GetQScoreFemale()
        {
            return Program.GetDataFromQuery("SELECT `quarterly_score` " +
            "FROM `student_profile` " +
            "LEFT JOIN `student_qa` ON `student_qa`.`student_ID` = `student_profile`.`student_ID`" +
            "WHERE `Student_Sex` like 'Female' " +
            "AND `student_Level` like 'Grade " + Grade + "' " +
            "AND `student_Section` = '" + section + "' " +
            "AND (`subject` like '" + Subject + "' OR `subject` = NULL ) " +
            "AND (`quarter_ID` = '" + quarter + "' OR `quarter_ID` = NULL ) " );
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FinalRatingForm frf = new FinalRatingForm(Grade, quarter, Subject, teacher);
            frf.Show();
        }

        private void quarterCmBx_SelectedIndexChanged(object sender, EventArgs e)
        {
            quarter = (quarterCmBx.SelectedIndex + 1).ToString();
            GradingSheet_Load(sender,e);
        }

        private void manageCompute(object sender, EventArgs e) {
            if (!dis) {
                float[] nums = new float[8];
                nums[0] = Program.safeParse(txtbx_w1.Text);
                nums[1] = Program.safeParse(txtbx_w2.Text);
                nums[2] = Program.safeParse(txtbx_w3.Text);
                nums[3] = Program.safeParse(txtbx_w4.Text);
                nums[4] = Program.safeParse(txtbx_w5.Text);
                nums[5] = Program.safeParse(txtbx_w6.Text);
                nums[6] = Program.safeParse(txtbx_w7.Text);
                nums[7] = Program.safeParse(txtbx_w8.Text);
                txtbx_wT.Text = Program.sum(nums).ToString();

                nums[0] = Program.safeParse(txtbx_p1.Text);
                nums[1] = Program.safeParse(txtbx_p2.Text);
                nums[2] = Program.safeParse(txtbx_p3.Text);
                nums[3] = Program.safeParse(txtbx_p4.Text);
                nums[4] = Program.safeParse(txtbx_p5.Text);
                nums[5] = Program.safeParse(txtbx_p6.Text);
                nums[6] = Program.safeParse(txtbx_p7.Text);
                nums[7] = Program.safeParse(txtbx_p8.Text);
                txtbx_pT.Text = Program.sum(nums).ToString();
                //boys computation
                float[,] arrBWW = getFloat2dArray(dgvBWW);
                float[,] arrBWWPS = new float[arrBWW.GetLength(0), 1];//getFloat2dArray(dgvBWWPS);
                float[,] arrBWWWS = new float[arrBWW.GetLength(0), 1];//getFloat2dArray(dgvBWWWS);
                computeThis(ref arrBWW, ref arrBWWPS, ref arrBWWWS, txtbx_wT.Text, WrittenWorkPercent);
                setDataToGridView(dgvBWW, arrBWW);
                setDataToGridView(dgvBWWPS, arrBWWPS);
                setDataToGridView(dgvBWWWS, arrBWWWS);

                float[,] arrBPT = getFloat2dArray(dgvBPT);
                float[,] arrBPTPS = new float[arrBPT.GetLength(0), 1];//getFloat2dArray(dgvBPTPS);
                float[,] arrBPTWS = new float[arrBPT.GetLength(0), 1];//getFloat2dArray(dgvBPTWS);
                computeThis(ref arrBPT, ref arrBPTPS, ref arrBPTWS, txtbx_pT.Text, PerformancePercent);
                setDataToGridView(dgvBPT, arrBPT);
                setDataToGridView(dgvBPTPS, arrBPTPS);
                setDataToGridView(dgvBPTWS, arrBPTWS);

                float[,] arrBQA = getFloat2dArray(dgvBQA);
                float[,] arrBQAPS = new float[arrBQA.GetLength(0), 1];//getFloat2dArray(dgvBQAPS);
                float[,] arrBQAWS = new float[arrBQA.GetLength(0), 1];//getFloat2dArray(dgvBQAWS);
                computeThis(ref arrBQA, ref arrBQAPS, ref arrBQAWS, txtbx_qT.Text, QuarterlyPercent);
                setDataToGridView(dgvBQA, arrBQA);
                setDataToGridView(dgvBQAPS, arrBQAPS);
                setDataToGridView(dgvBQAWS, arrBQAWS);

                float[,] arrBIG = new float[arrBQA.GetLength(0), 1];//getFloat2dArray(dgvBIG);
                float[,] arrBQG = new float[arrBQA.GetLength(0), 1];//getFloat2dArray(dgvBQG);
                computeFinalGrade(arrBWWWS, arrBPTWS, arrBQAWS, ref arrBIG, ref arrBQG);
                setDataToGridView(dgvBIG, arrBIG);
                setDataToGridView(dgvBQG, arrBQG, 500, 500);
                //girls computation
                float[,] arrGWW = getFloat2dArray(dgvGWW);
                float[,] arrGWWPS = new float[arrGWW.GetLength(0), 1];//getFloat2dArray(dgvGWWPS);
                float[,] arrGWWWS = new float[arrGWW.GetLength(0), 1];//getFloat2dArray(dgvGWWWS);
                computeThis(ref arrGWW, ref arrGWWPS, ref arrGWWWS, txtbx_wT.Text, WrittenWorkPercent);
                setDataToGridView(dgvGWW, arrGWW);
                setDataToGridView(dgvGWWPS, arrGWWPS);
                setDataToGridView(dgvGWWWS, arrGWWWS);

                float[,] arrGPT = getFloat2dArray(dgvGPT);
                float[,] arrGPTPS = new float[arrGPT.GetLength(0), 1];//getFloat2dArray(dgvGPTPS);
                float[,] arrGPTWS = new float[arrGPT.GetLength(0), 1];//getFloat2dArray(dgvGPTWS);
                computeThis(ref arrGPT, ref arrGPTPS, ref arrGPTWS, txtbx_pT.Text, PerformancePercent);
                setDataToGridView(dgvGPT, arrGPT);
                setDataToGridView(dgvGPTPS, arrGPTPS);
                setDataToGridView(dgvGPTWS, arrGPTWS);

                float[,] arrGQA = getFloat2dArray(dgvGQA);
                float[,] arrGQAPS = new float[arrGQA.GetLength(0), 1];//getFloat2dArray(dgvGQAPS);
                float[,] arrGQAWS = new float[arrGQA.GetLength(0), 1];//getFloat2dArray(dgvGQAWS);
                computeThis(ref arrGQA, ref arrGQAPS, ref arrGQAWS, txtbx_qT.Text, QuarterlyPercent);
                setDataToGridView(dgvGQA, arrGQA);
                setDataToGridView(dgvGQAPS, arrGQAPS);
                setDataToGridView(dgvGQAWS, arrGQAWS);

                float[,] arrGIG = new float[arrGQA.GetLength(0), 1];//getFloat2dArray(dgvGIG);
                float[,] arrGQG = new float[arrGQA.GetLength(0), 1];//getFloat2dArray(dgvGQG);
                computeFinalGrade(arrGWWWS, arrGPTWS, arrGQAWS, ref arrGIG, ref arrGQG);
                setDataToGridView(dgvGIG, arrGIG);
                setDataToGridView(dgvGQG, arrGQG, 500, 500);
            }
        }

        private void computeFinalGrade(float[,]ws1, float[,] ws2, float[,] ws3, ref float[,] ini, ref float[,] qg)
        {
            int i=0, x;
            for (x = 0; x < ws1.GetLength(0); x++)
            {
                ini[x, i] = ws1[x, i] + ws2[x, i] + ws3[x, i];
                qg[x,i] = Program.transmutate(ini[x, i]);
            }
        }

        public void computeThis(ref float[,]arr,ref float[,] arr1,ref float[,] arr2,String fromTxtbx,float percent)
        {

            int i, x;
            float sum = 0;
            for (x = 0; x < arr.GetLength(0); x++)
            {
                for (i = 0; i < arr.GetLength(1); i++)
                {
                    if (i == 0)
                    {
                        sum = 0;
                    }
                    if (arr.GetLength(1) == 1)
                    {
                        sum += arr[x, i];
                    }
                    if (i + 1 >= arr.GetLength(1))
                    {
                        arr[x, i] = sum;
                        arr1[x, 0] = arr[x, i] / Program.safeParse(fromTxtbx) * 100;
                        arr2[x, 0] = arr1[x, 0] * percent / 100;
                    }
                    else
                    {
                        sum += arr[x, i];
                    }
                }
            }

        }

        public void setDataToGridView(DataGridView v, float[,] data,int defaultSize = 50,int lastSize = 100)
        {
            v.DataSource = new float[data.GetLength(0), data.GetLength(1)];
            v.DataSource = null;
            var rowCount = data.GetLength(0);
            var rowLength = data.GetLength(1);
            v.ColumnCount = rowLength;
            for (int rowIndex = 0; rowIndex < rowCount; ++rowIndex)
            {
                var row = new DataGridViewRow();

                for (int columnIndex = 0; columnIndex < rowLength; ++columnIndex)
                {
                    row.Cells.Add(new DataGridViewTextBoxCell()
                    {
                        Value = data[rowIndex, columnIndex]
                    });
                }
                v.Rows.Add(row);
            }
            resizeDGV(v,defaultSize,lastSize);
        }

        private void dgvGPT_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            manageCompute(sender, e);
        }

        static public float[,] getFloat2dArray(DataGridView v)
        {
            float[,] arr2d = new float[v.Rows.Count, v.Columns.Count];

            for (int x = 0; x < arr2d.GetLength(0); x++)
                for (int i = 0; i < arr2d.GetLength(1); i++)
                    arr2d[x, i] = Program.safeParse(v.Rows[x].Cells[i].Value.ToString()); 
            return arr2d;
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            float[] arrWWT = new float[8];
            arrWWT[0] = Program.safeParse(txtbx_w1.Text);
            arrWWT[1] = Program.safeParse(txtbx_w2.Text);
            arrWWT[2] = Program.safeParse(txtbx_w3.Text);
            arrWWT[3] = Program.safeParse(txtbx_w4.Text);
            arrWWT[4] = Program.safeParse(txtbx_w5.Text);
            arrWWT[5] = Program.safeParse(txtbx_w6.Text);
            arrWWT[6] = Program.safeParse(txtbx_w7.Text);
            arrWWT[7] = Program.safeParse(txtbx_w8.Text);

            float[] arrPTT = new float[8];
            arrPTT[0] = Program.safeParse(txtbx_p1.Text);
            arrPTT[1] = Program.safeParse(txtbx_p2.Text);
            arrPTT[2] = Program.safeParse(txtbx_p3.Text);
            arrPTT[3] = Program.safeParse(txtbx_p4.Text);
            arrPTT[4] = Program.safeParse(txtbx_p5.Text);
            arrPTT[5] = Program.safeParse(txtbx_p6.Text);
            arrPTT[6] = Program.safeParse(txtbx_p7.Text);
            arrPTT[7] = Program.safeParse(txtbx_p8.Text);

            float arrQAT = Program.safeParse(txtbx_qT.Text);

            //boys data
            float[,] arrBWW = getFloat2dArray(dgvBWW);
            float[,] arrBPT = getFloat2dArray(dgvBPT);
            float[,] arrBQA = getFloat2dArray(dgvBQA);
            //girls data
            float[,] arrGWW = getFloat2dArray(dgvGWW);
            float[,] arrGPT = getFloat2dArray(dgvGPT);
            float[,] arrGQA = getFloat2dArray(dgvGQA);

            //boys in
            dataGridView1.DataSource = Program.GetDataFromQuery("SELECT `student_ID` FROM `student_profile` WHERE `Student_Sex` like 'Male' AND `student_Level` like 'Grade 8' AND `student_Section` = 'Section 1' ");

            float[,] arr = getFloat2dArray(dataGridView1);
            String toDelete = "DELETE FROM `student_ww` WHERE `student_ID` IN (";
            for (int r=0; r<arr.GetLength(0) ;r++)
            {
                if (r + 1 >= arr.GetLength(0)){ toDelete += arr[r,0]+""; }
                else { toDelete += arr[r, 0]+","; }
            }
            toDelete+= ") AND (`subject` = '" + Subject + "' OR `subject` = '" + Subject + "_total') AND `quarter_ID` = '" + quarter + "'";
            if(arr.GetLength(0)>0) Program.doNonQuery(toDelete);
            String sql;
            for (int r = 0; r < arr.GetLength(0); r++){
                sql = "INSERT INTO `student_ww`(`student_ID`, `WWS1`, `WWS2`, `WWS3`, `WWS4`, `WWS5`, `WWS6`, `WWS7`, `WWS8`, `subject`, `quarter_ID`) "+
"VALUES('"+arr[r,0]+"', '"+ arrBWW[r,0]+ "', '" + arrBWW[r, 1] + "', '" + arrBWW[r, 2] + "', '" + arrBWW[r, 3] + "', '" + arrBWW[r, 4] + "', '" + arrBWW[r, 5] + "', '" + arrBWW[r, 6] + "', '" + arrBWW[r, 7] + "', '" + Subject+"', '"+quarter+"')";
                Program.doNonQuery(sql);
            }
            sql = "INSERT INTO `student_ww`(`student_ID`, `WWS1`, `WWS2`, `WWS3`, `WWS4`, `WWS5`, `WWS6`, `WWS7`, `WWS8`, `subject`, `quarter_ID`) " +
"VALUES('" + arr[0, 0] + "', '" + arrWWT[0] + "', '" + arrWWT[1] + "', '" + arrWWT[2] + "', '" + arrWWT[3] + "', '" + arrWWT[4] + "', '" + arrWWT[5] + "', '" + arrWWT[6] + "', '" + arrWWT[7] + "', '" + Subject + "', '" + quarter + "')";
            Program.doNonQuery(sql);
            //----------------------------------------------------
            toDelete = "DELETE FROM `student_perf` WHERE `student_ID` IN (";
            for (int r = 0; r < arr.GetLength(0); r++)
            {
                if (r + 1 >= arr.GetLength(0)) { toDelete += arr[r, 0] + ""; }
                else { toDelete += arr[r, 0] + ","; }
            }
            toDelete += ") AND (`subject` = '" + Subject + "' OR `subject` = '" + Subject + "_total') AND `quarter_ID` = '" + quarter + "'";
            if (arr.GetLength(0) > 0) Program.doNonQuery(toDelete);
            for (int r = 0; r < arr.GetLength(0); r++)
            {
                sql = "INSERT INTO `student_perf`(`student_ID`, `PTS1`, `PTS2`, `PTS3`, `PTS4`, `PTS5`, `PTS6`, `PTS7`, `PTS8`, `subject`, `quarter_ID`) " +
"VALUES('" + arr[r, 0] + "', '" + arrBWW[r, 0] + "', '" + arrBWW[r, 1] + "', '" + arrBWW[r, 2] + "', '" + arrBWW[r, 3] + "', '" + arrBWW[r, 4] + "', '" + arrBWW[r, 5] + "', '" + arrBWW[r, 6] + "', '" + arrBWW[r, 7] + "', '" + Subject + "', '" + quarter + "')";
                Program.doNonQuery(sql);
            }
            sql = "INSERT INTO `student_perf`(`student_ID`, `PTS1`, `PTS2`, `PTS3`, `PTS4`, `PTS5`, `PTS6`, `PTS7`, `PTS8`, `subject`, `quarter_ID`) " +
"VALUES('" + arr[0, 0] + "', '" + arrWWT[0] + "', '" + arrWWT[1] + "', '" + arrWWT[2] + "', '" + arrWWT[3] + "', '" + arrWWT[4] + "', '" + arrWWT[5] + "', '" + arrWWT[6] + "', '" + arrWWT[7] + "', '" + Subject + "', '" + quarter + "')";
            Program.doNonQuery(sql);//-----------------------------------------
            //----------------------------------------------------
            toDelete = "DELETE FROM `student_qa` WHERE `student_ID` IN (";
            for (int r = 0; r < arr.GetLength(0); r++)
            {
                if (r + 1 >= arr.GetLength(0)) { toDelete += arr[r, 0] + ""; }
                else { toDelete += arr[r, 0] + ","; }
            }
            toDelete += ") AND (`subject` = '" + Subject + "' OR `subject` = '" + Subject + "_total') AND `quarter_ID` = '" + quarter + "'";
            if (arr.GetLength(0) > 0) Program.doNonQuery(toDelete);
            for (int r = 0; r < arr.GetLength(0); r++)
            {
                sql = "INSERT INTO `student_qa`(`student_ID`, `quarterly_score`,`subject`, `quarter_ID`) " +
"VALUES('" + arr[r, 0] + "', '" + arrBQA[r, 0]+"','" + Subject + "', '" + quarter + "')";
                Program.doNonQuery(sql);
            }
            sql = "INSERT INTO `student_qa`(`student_ID`, `quarterly_score`,`subject`, `quarter_ID`) " +
"VALUES('" + arr[0, 0] + "', '" + arrQAT + "','" + Subject + "', '" + quarter + "')";
            Program.doNonQuery(sql);//-----------------------------------------

            //girls in
            dataGridView1.DataSource = Program.GetDataFromQuery("SELECT `student_ID` FROM `student_profile` WHERE `Student_Sex` like 'Male' AND `student_Level` like 'Grade 8' AND `student_Section` = 'Section 1' ");
            arr = getFloat2dArray(dataGridView1);
            toDelete = "DELETE FROM `student_ww` WHERE `student_ID` IN (";
            for (int r = 0; r < arr.GetLength(0); r++)
            {
                if (r + 1 >= arr.GetLength(0)) { toDelete += arr[r, 0] + ""; }
                else { toDelete += arr[r, 0] + ","; }
            }
            toDelete += ") AND (`subject` = '" + Subject + "' OR `subject` = '" + Subject + "_total') AND `quarter_ID` = '" + quarter + "'";
            if (arr.GetLength(0) > 0) Program.doNonQuery(toDelete);
            for (int r = 0; r < arr.GetLength(0); r++)
            {
                sql = "INSERT INTO `student_ww`(`student_ID`, `WWS1`, `WWS2`, `WWS3`, `WWS4`, `WWS5`, `WWS6`, `WWS7`, `WWS8`, `subject`, `quarter_ID`) " +
"VALUES('" + arr[r, 0] + "', '" + arrBWW[r, 0] + "', '" + arrBWW[r, 1] + "', '" + arrBWW[r, 2] + "', '" + arrBWW[r, 3] + "', '" + arrBWW[r, 4] + "', '" + arrBWW[r, 5] + "', '" + arrBWW[r, 6] + "', '" + arrBWW[r, 7] + "', '" + Subject + "', '" + quarter + "')";
                Program.doNonQuery(sql);
            }
            sql = "INSERT INTO `student_ww`(`student_ID`, `WWS1`, `WWS2`, `WWS3`, `WWS4`, `WWS5`, `WWS6`, `WWS7`, `WWS8`, `subject`, `quarter_ID`) " +
"VALUES('" + arr[0, 0] + "', '" + arrWWT[0] + "', '" + arrWWT[1] + "', '" + arrWWT[2] + "', '" + arrWWT[3] + "', '" + arrWWT[4] + "', '" + arrWWT[5] + "', '" + arrWWT[6] + "', '" + arrWWT[7] + "', '" + Subject + "', '" + quarter + "')";
            Program.doNonQuery(sql);
            //----------------------------------------------------
            toDelete = "DELETE FROM `student_perf` WHERE `student_ID` IN (";
            for (int r = 0; r < arr.GetLength(0); r++)
            {
                if (r + 1 >= arr.GetLength(0)) { toDelete += arr[r, 0] + ""; }
                else { toDelete += arr[r, 0] + ","; }
            }
            toDelete += ") AND (`subject` = '" + Subject + "' OR `subject` = '" + Subject + "_total') AND `quarter_ID` = '" + quarter + "'";
            if (arr.GetLength(0) > 0) Program.doNonQuery(toDelete);
            for (int r = 0; r < arr.GetLength(0); r++)
            {
                sql = "INSERT INTO `student_perf`(`student_ID`, `PTS1`, `PTS2`, `PTS3`, `PTS4`, `PTS5`, `PTS6`, `PTS7`, `PTS8`, `subject`, `quarter_ID`) " +
"VALUES('" + arr[r, 0] + "', '" + arrBWW[r, 0] + "', '" + arrBWW[r, 1] + "', '" + arrBWW[r, 2] + "', '" + arrBWW[r, 3] + "', '" + arrBWW[r, 4] + "', '" + arrBWW[r, 5] + "', '" + arrBWW[r, 6] + "', '" + arrBWW[r, 7] + "', '" + Subject + "', '" + quarter + "')";
                Program.doNonQuery(sql);
            }
            sql = "INSERT INTO `student_perf`(`student_ID`, `PTS1`, `PTS2`, `PTS3`, `PTS4`, `PTS5`, `PTS6`, `PTS7`, `PTS8`, `subject`, `quarter_ID`) " +
"VALUES('" + arr[0, 0] + "', '" + arrWWT[0] + "', '" + arrWWT[1] + "', '" + arrWWT[2] + "', '" + arrWWT[3] + "', '" + arrWWT[4] + "', '" + arrWWT[5] + "', '" + arrWWT[6] + "', '" + arrWWT[7] + "', '" + Subject + "', '" + quarter + "')";
            Program.doNonQuery(sql);//-----------------------------------------
            //----------------------------------------------------
            toDelete = "DELETE FROM `student_qa` WHERE `student_ID` IN (";
            for (int r = 0; r < arr.GetLength(0); r++)
            {
                if (r + 1 >= arr.GetLength(0)) { toDelete += arr[r, 0] + ""; }
                else { toDelete += arr[r, 0] + ","; }
            }
            toDelete += ") AND (`subject` = '" + Subject + "' OR `subject` = '" + Subject + "_total') AND `quarter_ID` = '" + quarter + "'";
            if (arr.GetLength(0) > 0) Program.doNonQuery(toDelete);
            for (int r = 0; r < arr.GetLength(0); r++)
            {
                sql = "INSERT INTO `student_qa`(`student_ID`, `quarterly_score`,`subject`, `quarter_ID`) " +
"VALUES('" + arr[r, 0] + "', '" + arrBQA[r, 0] + "','" + Subject + "', '" + quarter + "')";
                Program.doNonQuery(sql);
            }
            sql = "INSERT INTO `student_qa`(`student_ID`, `quarterly_score`,`subject`, `quarter_ID`) " +
"VALUES('" + arr[0, 0] + "', '" + arrQAT + "','" + Subject + "', '" + quarter + "')";
            Program.doNonQuery(sql);//-----------------------------------------

            MessageBox.Show("Saved I suppose this is from line 714 at GradingSheetForm.cs and I Just lost most of my life here");
        }

        private Boolean dis = true;
        private Boolean fdis = true;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!fdis) {
                dis = false;
                timer1.Enabled = false;
                checkData(dgvBName, dgvBWW, 9);
                resizeDGV(dgvBWW);
                
                checkData(dgvBName, dgvBPT, 9);
                resizeDGV(dgvBPT);
                
                checkData(dgvBName, dgvBQA, 1);
                resizeDGV(dgvBQA);

                checkData(dgvGName, dgvGWW, 9);
                resizeDGV(dgvGWW);
                
                checkData(dgvGName, dgvGPT, 9);
                resizeDGV(dgvGPT);
                
                checkData(dgvGName, dgvGQA, 1);
                resizeDGV(dgvGQA);
            }
            else
            {
                fdis = false;
            }
        }

        private void dgvBWW_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!dis) {
            manageCompute(sender, e);
            dis = true;
            fdis = true;
            timer1.Enabled = true; }
        }
    }
}
