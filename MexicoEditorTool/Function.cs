using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Npgsql;

namespace MexicoEditorTool
{
    class Functions
    {
        public static NpgsqlConnection con;
        public static void Connection()
        {
            con = new NpgsqlConnection("Server=172.16.0.79;Port=5432;User Id=user_dev;Password=45-C8-2A-38-43-CA;Database=automexico_dev;CommandTimeout=320");
            con.Open();
        }
        public static void Disconnect()
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
                con.Dispose();
                con = null;
            }
        }
        public static DataTable GetDataToTable(string sql)
        {
            //NpgsqlCommand cmd = new NpgsqlCommand();
            //cmd.Connection = Functions.con;
            //cmd.CommandText = sql;
            //NpgsqlDataReader dataReader = cmd.ExecuteReader();
            //DataTable tbl = new DataTable();
            //tbl.Load(dataReader);
            //return tbl;
            NpgsqlDataAdapter dap = new NpgsqlDataAdapter(sql, con); //Định nghĩa đối tượng thuộc lớp SqlDataAdapter
            DataTable tbl = new DataTable();//Khai báo đối tượng table thuộc lớp DataTable
            dap.Fill(tbl); //Đổ kết quả từ câu lệnh sql vào table
            return tbl;
        }
        public static void RunSqlDel(string sql)
        {
            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = Functions.con;
            cmd.CommandText = sql;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
            cmd.Dispose();
            cmd = null;
        }
        public static bool CheckKey(string sql)
        {
            NpgsqlDataAdapter dap = new NpgsqlDataAdapter(sql, con);
            DataTable table = new DataTable();
            dap.Fill(table);
            if (table.Rows.Count > 0)
                return true;
            else return false;
        }
        public static bool CheckKeys(string sql, string str)
        {
            NpgsqlDataAdapter dap = new NpgsqlDataAdapter(sql, con);
            DataTable table = new DataTable();
            NpgsqlDataAdapter adp = new NpgsqlDataAdapter(str, con);
            dap.Fill(table);
            adp.Fill(table);
            if (table.Rows.Count > 0)
                return true;
            else return false;

        }
        public static string GetFieldValues(string sql)
        {
            string ma = "";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
            NpgsqlDataReader reader;
            reader = cmd.ExecuteReader();
            while (reader.Read())
                ma = reader.GetValue(0).ToString();
            reader.Close();
            return ma;
        }
        public static void FillCombo(string sql, ComboBox cbo, string ma, string ten)
        {
            NpgsqlDataAdapter dap = new NpgsqlDataAdapter(sql, con);
            DataTable table = new DataTable();
            dap.Fill(table);
            cbo.DataSource = table;
            cbo.ValueMember = ma; //Trường giá trị
            cbo.DisplayMember = ten; //Trường hiển thị
        }
        public static void FillCombo1(string sql, ComboBox cbo, string ma)
        {
            NpgsqlDataAdapter dap = new NpgsqlDataAdapter(sql, con);
            DataTable table = new DataTable();
            dap.Fill(table);
            cbo.DataSource = table;
            cbo.ValueMember = ma; //Trường giá trị

        }


        public static void RunSQL(string sql)
        {
            NpgsqlCommand cmd;
            cmd = new NpgsqlCommand();
            cmd.Connection = con;
            cmd.CommandText = sql;
            try
            {
                cmd.ExecuteNonQuery(); //Thực hiện câu lệnh SQL
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            cmd.Dispose();
            cmd = null;
        }
        public static string ConvertDateTime(string date)
        {
            string[] elements = date.Split('/');
            string dt = string.Format("{0}/{1}/{2}", elements[0], elements[1], elements[2]);
            return dt;
        }
        public static string ConvertTimeTo24(string hour)
        {
            string h = "";
            switch (hour)
            {
                case "1":
                    h = "13";
                    break;
                case "2":
                    h = "14";
                    break;
                case "3":
                    h = "15";
                    break;
                case "4":
                    h = "16";
                    break;
                case "5":
                    h = "17";
                    break;
                case "6":
                    h = "18";
                    break;
                case "7":
                    h = "19";
                    break;
                case "8":
                    h = "20";
                    break;
                case "9":
                    h = "21";
                    break;
                case "10":
                    h = "22";
                    break;
                case "11":
                    h = "23";
                    break;
                case "12":
                    h = "0";
                    break;
            }
            return h;
        }
    }
}
