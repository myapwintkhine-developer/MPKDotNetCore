using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MPKDotNetCore.ConsoleApp.AdoDotNetExamples
{
    public class AdoDotNetExample
    {
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

            string query = "Select * from Blog";
            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            connection.Close();

            foreach (DataRow dr in dt.Rows)
            {
                Console.WriteLine($"Blog Id => {dr["BlogId"].ToString()}");
                Console.WriteLine($"Blog Title => {dr["BlogTitle"].ToString()}");
                Console.WriteLine($"Blog Author => {dr["BlogAuthor"].ToString()}");
                Console.WriteLine($"Blog Content => {dr["BlogContent"].ToString()}");
            }

        }

        private void Edit(int id)
        {
            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            string query = "Select * from Blog Where BlogId=@BlogId";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
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
            Console.WriteLine($"Blog Id => {dr["BlogId"].ToString()}");
            Console.WriteLine($"Blog Title => {dr["BlogTitle"].ToString()}");
            Console.WriteLine($"Blog Author => {dr["BlogAuthor"].ToString()}");
            Console.WriteLine($"Blog Content => {dr["BlogContent"].ToString()}");

        }

        private void Create(string title, string author, string content)
        {
            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            string query = @"INSERT INTO [dbo].[Blog] ([BlogTitle],[BlogAuthor],[BlogContent]) VALUES(@BlogTitle,@BlogAuthor,@BlogContent)";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogTitle", title);
            cmd.Parameters.AddWithValue("@BlogAuthor", author);
            cmd.Parameters.AddWithValue("@BlogContent", content);
            int result = cmd.ExecuteNonQuery();

            connection.Close();
            string message = result > 0 ? "Saving successful" : "Create error";
            Console.WriteLine(message);

        }

        private void Update(int id, string title, string author, string content)
        {
            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            string query = @"Update [dbo].[Blog] Set [BlogTitle]=@BlogTitle, [BlogAuthor]=@BlogAuthor, [BlogContent]=@BlogContent Where BlogId=@BlogId";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            cmd.Parameters.AddWithValue("@BlogTitle", title);
            cmd.Parameters.AddWithValue("@BlogAuthor", author);
            cmd.Parameters.AddWithValue("@BlogContent", content);

            int result = cmd.ExecuteNonQuery();

            connection.Close();

            string message = result > 0 ? "Update successful" : "Update error";
            Console.WriteLine(message);
        }

        private void Delete(int id)
        {
            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            string query = @"Delete From [dbo].[Blog] Where BlogId=@BlogId";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);

            int result = cmd.ExecuteNonQuery();

            connection.Close();

            string message = result > 0 ? "Delete successful" : "Delete error";
            Console.WriteLine(message);
        }
    }
}

