using Dapper;
using Microsoft.Data.SqlClient;
using MPKDotNetCore.ConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MPKDotNetCore.ConsoleApp.DapperExamples
{
    public class DapperExample
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
            Edit(2);
            Update(2, "UpdatedTitle", "UpdatedAuthor", "UpdatedContent");
            Create("TestTitle", "TestAuthor", "TestContent");
        }

        private void Read()
        {
            string query = "select * from Blog order by BlogId desc";
            using IDbConnection db = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);

            List<BlogDataModel> lst = db.Query<BlogDataModel>(query).ToList();
            foreach (var item in lst)
            {
                Console.WriteLine(item.Blog_Id);
                Console.WriteLine(item.Blog_Title);
                Console.WriteLine(item.Blog_Author);
                Console.WriteLine(item.Blog_Content);
            }
        }

        private void Edit(int id)
        {
            string query = "select * from Blog where BlogId = @BlogId;";

            BlogDataModel blog = new BlogDataModel()
            {
                Blog_Id = id
            };
            IDbConnection db = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            BlogDataModel item = db.Query<BlogDataModel>(query, blog).FirstOrDefault();

            //if (item is null)
            if (item == null)
            {
                Console.WriteLine("No data found.");
                return;
            }

            Console.WriteLine(item.Blog_Id);
            Console.WriteLine(item.Blog_Title);
            Console.WriteLine(item.Blog_Author);
            Console.WriteLine(item.Blog_Content);
        }

    

    private void Create(string title, string author, string content)
    {
        string query = $@"INSERT INTO [dbo].[Tbl_Blog]
           ([Blog_Title]
           ,[Blog_Author]
           ,[Blog_Content])
     VALUES
           (@Blog_Title
           ,@Blog_Author
           ,@Blog_Content)";

        BlogDataModel blog = new BlogDataModel()
        {
            Blog_Author = author,
            Blog_Content = content,
            Blog_Title = title,
        };

        using IDbConnection db = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
        var result = db.Execute(query, blog);
        string message = result > 0 ? "Saving Successful." : "Saving Failed.";

        Console.WriteLine(message);
    }


    public void Delete(int id)
        {
            IDbConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            string query = @"Delete From [dbo].[Tbl_Blog] Where Blog_Id=@Blog_Id";
            int result = connection.Execute(query, new BlogDataModel
            {
                Blog_Id = id
            });

            string message = result > 0 ? "Delete successful" : "Delete error";
            Console.WriteLine(message);
        }

        private void Update(int id,string title, string author, string content)
        {
            string query = $@"Update [dbo].[Tbl_Blog] Set [Blog_Title]=@Blog_Title, [Blog_Author]=@Blog_Author, [Blog_Content]=@Blog_Content Where Blog_Id=@Blog_Id";

            BlogDataModel blog = new BlogDataModel()
            {
                Blog_Id = id,
                Blog_Author = author,
                Blog_Content = content,
                Blog_Title = title,
            };

            using IDbConnection db = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            var result = db.Execute(query, blog);
            string message = result > 0 ? "Update Successful." : "Update Failed.";

            Console.WriteLine(message);
        }

    }
}

