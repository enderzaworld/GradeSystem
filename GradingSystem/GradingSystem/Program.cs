using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GradingSystem
{
    static class Program
    {
        static public String position = "";
        static public String user_id = "";
        static public string connectionString = "server=localhost;database=gradingsystem;uid=root;pwd=;";
        static public MySqlCommand cm = new MySqlCommand();
        static public MySqlConnection cn = new MySqlConnection();
        static private string conn;
        static private MySqlConnection connect;
        static private MySqlDataAdapter mySqlDataAdapter;
        static private MySqlCommand cmd;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm());
        }

        static public void db_connection()
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

        static public DataTable GetDataFromQuery(String sql)
        {
            DataTable dt = new DataTable();
            MySqlConnection con;
            con = new MySqlConnection(Program.connectionString);
            con.Open();
            
            using (MySqlCommand cmd = new MySqlCommand(sql, con))
            {

                MySqlDataAdapter adpt = new MySqlDataAdapter(cmd);
                try
                {
                    adpt.Fill(dt);
                }
                catch (Exception e){
                    MessageBox.Show(e.Message);
                }
            }


            return dt;
        }

        static public MySqlDataReader GetReaderFromQuery(String sql)
        {
            db_connection();
            cmd = new MySqlCommand();
            cmd.CommandText = sql;
            cmd.Connection = connect;
            MySqlDataReader reader = cmd.ExecuteReader();
            return reader;
        }

        static public float safeParse(String str)
        {
            try
            {
                return float.Parse(str);
            }
            catch
            {
                return 0;
            }
        }

        static public float sum(float[] myArray)
        {
            float sum = 0;
            foreach (float value in myArray)
            {
                sum += value;/*
                Console.WriteLine(value);
                Console.ReadLine();*/
            }
            return sum;
        }

        static public float transmutate(float grade)
        {
            if (grade >= 0.00 && grade <= 3.99) return 60;
            if (grade >= 4.00 && grade <= 7.99) return 61;
            if (grade >= 8.00 && grade <= 11.99) return 62;
            if (grade >= 12.00 && grade <= 15.99) return 63;
            if (grade >= 16.00 && grade <= 19.99) return 64;
            if (grade >= 20.00 && grade <= 23.99) return 65;
            if (grade >= 24.00 && grade <= 27.99) return 66;
            if (grade >= 28.00 && grade <= 31.99) return 67;
            if (grade >= 32.00 && grade <= 35.99) return 68;
            if (grade >= 36.00 && grade <= 39.99) return 69;
            if (grade >= 40.00 && grade <= 43.99) return 70;
            if (grade >= 44.00 && grade <= 47.99) return 71;
            if (grade >= 48.00 && grade <= 51.99) return 72;
            if (grade >= 52.00 && grade <= 55.99) return 73;
            if (grade >= 56.00 && grade <= 59.99) return 74;
            if (grade >= 60.00 && grade <= 61.99) return 75;
            if (grade >= 61.60 && grade <= 63.19) return 76;
            if (grade >= 63.20 && grade <= 64.79) return 77;
            if (grade >= 64.80 && grade <= 66.39) return 78;
            if (grade >= 66.40 && grade <= 67.99) return 79;
            if (grade >= 68.00 && grade <= 69.59) return 80;
            if (grade >= 69.60 && grade <= 71.19) return 81;
            if (grade >= 71.20 && grade <= 72.79) return 82;
            if (grade >= 72.80 && grade <= 74.39) return 83;
            if (grade >= 74.40 && grade <= 75.99) return 84;
            if (grade >= 76.00 && grade <= 77.59) return 85;
            if (grade >= 77.60 && grade <= 79.19) return 86;
            if (grade >= 79.20 && grade <= 80.79) return 87;
            if (grade >= 80.80 && grade <= 82.39) return 88;
            if (grade >= 82.40 && grade <= 83.99) return 89;
            if (grade >= 84.00 && grade <= 85.59) return 90;
            if (grade >= 85.60 && grade <= 87.19) return 91;
            if (grade >= 87.20 && grade <= 88.79) return 92;
            if (grade >= 88.80 && grade <= 90.39) return 93;
            if (grade >= 90.40 && grade <= 91.99) return 94;
            if (grade >= 92.00 && grade <= 93.59) return 95;
            if (grade >= 93.60 && grade <= 95.19) return 96;
            if (grade >= 95.20 && grade <= 96.79) return 97;
            if (grade >= 96.80 && grade <= 98.39) return 98;
            if (grade >= 98.40 && grade <= 99.99) return 99;
            if (grade >= 100.00 && grade <= 100.00) return 100;
            return 0;
        }

        static public float[,] getFloat2dArray(DataGridView v)
        {
            float[,] arr2d = new float[v.Rows.Count, v.Columns.Count];

            for (int x = 0; x < arr2d.GetLength(0); x++)
                for (int i = 0; i < arr2d.GetLength(1); i++)
                    arr2d[x, i] = safeParse(v.Rows[x].Cells[i].Value.ToString()); 
            return arr2d;
        }

        static public void setDataToGridView(DataGridView v, float[,] data)
        {
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
        }
    }
}
