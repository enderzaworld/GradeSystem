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
            resizeDGV(dgvBName,100);

            dgvBWW.DataSource = GetWScoreMale();
            checkData(dgvBName, dgvBWW);
            resizeDGV(dgvBWW);

            dgvBPT.DataSource = GetPScoreMale();
            checkData(dgvBName, dgvBPT);
            resizeDGV(dgvBPT);

            dgvBQA.DataSource = GetQScoreMale();
            checkData(dgvBName, dgvBQA,1);
            resizeDGV(dgvBQA);
        }

        private void loadFemale()
        {
            dgvGName.DataSource = GetSNameFemale();
            resizeDGV(dgvGName, 100);
            resizeRow(dgvBName, 0, dgvGName, 2);

            dgvGWW.DataSource = GetWScoreFemale();
            checkData(dgvGName, dgvGWW);
            resizeDGV(dgvGWW);

            dgvGPT.DataSource = GetPScoreFemale();
            checkData(dgvGName, dgvGPT);
            resizeDGV(dgvGPT);

            dgvGQA.DataSource = GetQScoreFemale();
            checkData(dgvGName, dgvGQA, 1);
            resizeDGV(dgvGQA);
        }

        private void checkData(DataGridView refDGV, DataGridView checkDGV,int len=9)
        {
            float[,] refe = Program.getFloat2dArray(refDGV);
            float[,] data = Program.getFloat2dArray(checkDGV);
            if (data.GetLength(0) == 0 && refe.GetLength(0) > 0)
            {
                data = new float[refe.GetLength(0), len];
                setDataToGridView(ref checkDGV, data);
            }else
            if (data.GetLength(0) != refe.GetLength(0))
            {
                float[,] tempData = data.Clone() as float[,];
                data = new float[refe.GetLength(0), len];
                for (int i = 0; i < tempData.GetLength(0); i++)
                    for (int x = 0; x < tempData.GetLength(1); x++)
                        data[i, x] = tempData[i,x];
                setDataToGridView(ref checkDGV, data);
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
            GradingSheet_Load(sender,e);
        }

        private void manageCompute(object sender, EventArgs e) {
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
            float[,] arrBWW = Program.getFloat2dArray(dgvBWW);
            float[,] arrBWWPS = new float[arrBWW.GetLength(0), 1];//Program.getFloat2dArray(dgvBWWPS);
            float[,] arrBWWWS = new float[arrBWW.GetLength(0), 1];//Program.getFloat2dArray(dgvBWWWS);
            computeThis(ref arrBWW, ref arrBWWPS, ref arrBWWWS, txtbx_wT.Text, WrittenWorkPercent);
            setDataToGridView(ref dgvBWW, arrBWW);
            setDataToGridView(ref dgvBWWPS, arrBWWPS);
            setDataToGridView(ref dgvBWWWS, arrBWWWS);
            
            float[,] arrBPT = Program.getFloat2dArray(dgvBPT);
            float[,] arrBPTPS = new float[arrBPT.GetLength(0), 1];//Program.getFloat2dArray(dgvBPTPS);
            float[,] arrBPTWS = new float[arrBPT.GetLength(0), 1];//Program.getFloat2dArray(dgvBPTWS);
            computeThis(ref arrBPT, ref arrBPTPS, ref arrBPTWS, txtbx_pT.Text, PerformancePercent);
            setDataToGridView(ref dgvBPT, arrBPT);
            setDataToGridView(ref dgvBPTPS, arrBPTPS);
            setDataToGridView(ref dgvBPTWS, arrBPTWS);

            float[,] arrBQA = Program.getFloat2dArray(dgvBQA);
            float[,] arrBQAPS = new float[arrBQA.GetLength(0), 1];//Program.getFloat2dArray(dgvBQAPS);
            float[,] arrBQAWS = new float[arrBQA.GetLength(0), 1];//Program.getFloat2dArray(dgvBQAWS);
            computeThis(ref arrBQA, ref arrBQAPS, ref arrBQAWS, txtbx_qT.Text, QuarterlyPercent);
            setDataToGridView(ref dgvBQA, arrBQA);
            setDataToGridView(ref dgvBQAPS, arrBQAPS);
            setDataToGridView(ref dgvBQAWS, arrBQAWS);

            float[,] arrBIG = new float[arrBQA.GetLength(0), 1];//Program.getFloat2dArray(dgvBIG);
            float[,] arrBQG = new float[arrBQA.GetLength(0), 1];//Program.getFloat2dArray(dgvBQG);
            computeFinalGrade(arrBWWWS, arrBPTWS, arrBQAWS, ref arrBIG, ref arrBQG);
            setDataToGridView(ref dgvBIG, arrBIG);
            setDataToGridView(ref dgvBQG, arrBQG,500,500);
            //girls computation
            float[,] arrGWW = Program.getFloat2dArray(dgvGWW);
            float[,] arrGWWPS = new float[arrGWW.GetLength(0), 1];//Program.getFloat2dArray(dgvGWWPS);
            float[,] arrGWWWS = new float[arrGWW.GetLength(0), 1];//Program.getFloat2dArray(dgvGWWWS);
            computeThis(ref arrGWW, ref arrGWWPS, ref arrGWWWS, txtbx_wT.Text, WrittenWorkPercent);
            setDataToGridView(ref dgvGWW, arrGWW);
            setDataToGridView(ref dgvGWWPS, arrGWWPS);
            setDataToGridView(ref dgvGWWWS, arrGWWWS);

            float[,] arrGPT = Program.getFloat2dArray(dgvGPT);
            float[,] arrGPTPS = new float[arrGPT.GetLength(0), 1];//Program.getFloat2dArray(dgvGPTPS);
            float[,] arrGPTWS = new float[arrGPT.GetLength(0), 1];//Program.getFloat2dArray(dgvGPTWS);
            computeThis(ref arrGPT, ref arrGPTPS, ref arrGPTWS, txtbx_pT.Text, PerformancePercent);
            setDataToGridView(ref dgvGPT, arrGPT);
            setDataToGridView(ref dgvGPTPS, arrGPTPS);
            setDataToGridView(ref dgvGPTWS, arrGPTWS);

            float[,] arrGQA = Program.getFloat2dArray(dgvGQA);
            float[,] arrGQAPS = new float[arrGQA.GetLength(0), 1];//Program.getFloat2dArray(dgvGQAPS);
            float[,] arrGQAWS = new float[arrGQA.GetLength(0), 1];//Program.getFloat2dArray(dgvGQAWS);
            computeThis(ref arrGQA, ref arrGQAPS, ref arrGQAWS, txtbx_qT.Text, QuarterlyPercent);
            setDataToGridView(ref dgvGQA, arrGQA);
            setDataToGridView(ref dgvGQAPS, arrGQAPS);
            setDataToGridView(ref dgvGQAWS, arrGQAWS);

            float[,] arrGIG = new float[arrGQA.GetLength(0), 1];//Program.getFloat2dArray(dgvGIG);
            float[,] arrGQG = new float[arrGQA.GetLength(0), 1];//Program.getFloat2dArray(dgvGQG);
            computeFinalGrade(arrGWWWS, arrGPTWS, arrGQAWS, ref arrGIG, ref arrGQG);
            setDataToGridView(ref dgvGIG, arrGIG);
            setDataToGridView(ref dgvGQG, arrGQG, 500, 500);
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

        public void setDataToGridView(ref DataGridView v, float[,] data,int defaultSize = 50,int lastSize = 100)
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

        private void manageCompute(object sender, DataGridViewCellEventArgs e)
        {
            manageCompute(sender, e);
        }

        private void btn_save_Click(object sender, EventArgs e)
        {

        }
    }
}
