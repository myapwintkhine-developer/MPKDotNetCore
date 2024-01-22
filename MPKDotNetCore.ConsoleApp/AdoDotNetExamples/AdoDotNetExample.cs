using log4net;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MPKDotNetCore.ConsoleApp.AdoDotNetExamples
{
    public class AdoDotNetExample
    {
        private static readonly ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder
        {
            DataSource = "DESKTOP-F0KQS1A", //server name (local)
            InitialCatalog = "TestDb",
            UserID = "sa", //user name
            Password = "sasa", //password
            Encrypt = true, // Enable SSL encryption
            TrustServerCertificate = true // Validate the server certificate
        };

        public void Run()
        {
            Read();
            Create("title5", "author5", "content5");
            Create("Hard Times", "Charles Dickens", "Slice of Life");
            Edit(2);
            Update(2, "title6", "author6", "content6");
            Delete(1);
        }

        private void Read()
        {
            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            string query = "Select * from Tbl_Blog";
            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            connection.Close();

            foreach (DataRow dr in dt.Rows)
            {
                Console.WriteLine($"Blog Id => {dr["Blog_Id"].ToString()}");
                Console.WriteLine($"Blog Title => {dr["Blog_Title"].ToString()}");
                Console.WriteLine($"Blog Author => {dr["Blog_Author"].ToString()}");
                Console.WriteLine($"Blog Content => {dr["Blog_Content"].ToString()}");
            }

        }

        private void Edit(int id)
        {
            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            string query = "Select * from Tbl_Blog Where Blog_Id=@Blog_Id";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Blog_Id", id);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            connection.Close();

            if (dt.Rows.Count == 0)
            {
                Console.WriteLine("No data found.");
                return;
            }

            DataRow dr = dt.Rows[0];
            Console.WriteLine($"Blog Id => {dr["Blog_Id"].ToString()}");
            Console.WriteLine($"Blog Title => {dr["Blog_Title"].ToString()}");
            Console.WriteLine($"Blog Author => {dr["Blog_Author"].ToString()}");
            Console.WriteLine($"Blog Content => {dr["Blog_Content"].ToString()}");

        }

        private void Create(string title, string author, string content)
        {
            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            string query = @"INSERT INTO [dbo].[Tbl_Blog] ([Blog_Title],[Blog_Author],[Blog_Content]) VALUES(@Blog_Title,@Blog_Author,@Blog_Content)";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Blog_Title", title);
            cmd.Parameters.AddWithValue("@Blog_Author", author);
            cmd.Parameters.AddWithValue("@Blog_Content", content);
            int result = cmd.ExecuteNonQuery();

            connection.Close();
            string message = result > 0 ? "Saving successful" : "Create error";
            Console.WriteLine(message);

        }

        private void Update(int id, string title, string author, string content)
        {
            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            string query = @"Update [dbo].[Tbl_Blog] Set [Blog_Title]=@Blog_Title, [Blog_Author]=@Blog_Author, [Blog_Content]=@Blog_Content Where Blog_Id=@Blog_Id";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Blog_Id", id);
            cmd.Parameters.AddWithValue("@Blog_Title", title);
            cmd.Parameters.AddWithValue("@Blog_Author", author);
            cmd.Parameters.AddWithValue("@Blog_Content", content);

            int result = cmd.ExecuteNonQuery();

            connection.Close();

            string message = result > 0 ? "Update successful" : "Update error";
            Console.WriteLine(message);
        }

        private void Delete(int id)
        {
            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            string query = @"Delete From [dbo].[Tbl_Blog] Where Blog_Id=@Blog_Id";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Blog_Id", id);

            int result = cmd.ExecuteNonQuery();

            connection.Close();

            string message = result > 0 ? "Delete successful" : "Delete error";
            log.Info(message);
            Console.WriteLine(message);
        }
    }
}

