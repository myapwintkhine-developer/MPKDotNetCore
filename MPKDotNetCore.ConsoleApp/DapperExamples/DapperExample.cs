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
            Password = "sasa" //password
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
                Console.WriteLine(item.BlogId);
                Console.WriteLine(item.BlogTitle);
                Console.WriteLine(item.BlogAuthor);
                Console.WriteLine(item.BlogContent);
            }
        }

        private void Edit(int id)
        {
            string query = "select * from Blog where BlogId = @BlogId;";

            BlogDataModel blog = new BlogDataModel()
            {
                BlogId = id
            };
            IDbConnection db = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            BlogDataModel item = db.Query<BlogDataModel>(query, blog).FirstOrDefault();

            //if (item is null)
            if (item == null)
            {
                Console.WriteLine("No data found.");
                return;
            }

            Console.WriteLine(item.BlogId);
            Console.WriteLine(item.BlogTitle);
            Console.WriteLine(item.BlogAuthor);
            Console.WriteLine(item.BlogContent);
        }

    

    private void Create(string title, string author, string content)
    {
        string query = $@"INSERT INTO [dbo].[Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           (@BlogTitle
           ,@BlogAuthor
           ,@BlogContent)";

        BlogDataModel blog = new BlogDataModel()
        {
            BlogAuthor = author,
            BlogContent = content,
            BlogTitle = title,
        };

        using IDbConnection db = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
        var result = db.Execute(query, blog);
        string message = result > 0 ? "Saving Successful." : "Saving Failed.";

        Console.WriteLine(message);
    }


    public void Delete(int id)
        {
            IDbConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            string query = @"Delete From [dbo].[Blog] Where BlogId=@BlogId";
            int result = connection.Execute(query, new BlogDataModel
            {
                BlogId = id
            });

            string message = result > 0 ? "Delete successful" : "Delete error";
            Console.WriteLine(message);
        }

        private void Update(int id,string title, string author, string content)
        {
            string query = $@"Update [dbo].[Blog] Set [BlogTitle]=@BlogTitle, [BlogAuthor]=@BlogAuthor, [BlogContent]=@BlogContent Where BlogId=@BlogId";

            BlogDataModel blog = new BlogDataModel()
            {
                BlogId = id,
                BlogAuthor = author,
                BlogContent = content,
                BlogTitle = title,
            };

            using IDbConnection db = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            var result = db.Execute(query, blog);
            string message = result > 0 ? "Update Successful." : "Update Failed.";

            Console.WriteLine(message);
        }

    }
}

