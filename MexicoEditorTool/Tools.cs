using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Npgsql;
using MexicoEditorTool.Models;
namespace MexicoEditorTool
{
    public partial class Tools : Form
    {
        NpgsqlConnection connection = new NpgsqlConnection("Server=172.16.0.79;Port=5432;User Id=user_dev;Password=45-C8-2A-38-43-CA;Database=automexico_dev;CommandTimeout=320");
        public Tools()
        {
            InitializeComponent();
            LoadForm();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<Articles> articlesList = new List<Articles>();
            connection.Open();
            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "select id, optimizedcontent from articles";
            NpgsqlDataReader dataReader = command.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(dataReader);
            foreach (DataRow item in dataTable.Rows)
            {
                Articles article = new Articles(item);
                articlesList.Add(article);
            }
            dataReader.Close();
            command.Dispose();

            foreach (var item in articlesList)
            {
                if (!string.IsNullOrEmpty(item.OptimizedContent) && item.OptimizedContent.Contains("<img") && !item.OptimizedContent.Contains("lazy-load"))
                {
                    // Hoàn nguyên lại optimized Content ban đầu.
                    string oldLinkOptimizedContent = item.OptimizedContent.Replace("<img class=\"lazy-load\"", "<img")
                        .Replace("data-src=\"https://img", "src=\"https://img");
                    // Add lazy load.
                    string newLinkOptimizedContent = oldLinkOptimizedContent.Replace("<img", "<img class=\"lazy-load\"")
                        .Replace("src=\"https://img", "data-src=\"https://img");
                    UpdateContent(newLinkOptimizedContent , item.Id);
                }
            }
            connection.Close();
            LoadForm();
            MessageBox.Show("Cập nhật thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void UpdateContent(string content, int articleId) {
            string updateContent = "\'"+ content +"\'";
            string query = "UPDATE articles SET optimizedcontent = "+ updateContent + " WHERE id = "+ articleId +"";
            NpgsqlCommand cmd; 
            cmd = new NpgsqlCommand();
            cmd.Connection = connection; 
            cmd.CommandText = query; 
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
        private void LoadForm()
        {
            connection.Open();
            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "select id, title, optimizedcontent from articles";
            NpgsqlDataReader dataReader = command.ExecuteReader();
            if (dataReader.HasRows)
            {
                DataTable dataTable = new DataTable();
                dataTable.Load(dataReader);
                dataGridView1.DataSource = dataTable;
            }
            command.Dispose();
            connection.Close();
        }
    }
}
