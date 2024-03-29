﻿using MPKDotNetCore.ConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MPKDotNetCore.ConsoleApp.EFCoreExamples
{
    public class EFCoreExample
    {
        public void Run()
        {
            Read();
            Create("Pride", "Jane", "Romance");
            Edit(1009);
            Update(1009, "Peace", "Mark", "Drama");
            Delete(2);
        }

        private void Read()
        {
            AppDbContext db = new AppDbContext();
            var lst=db.Blogs.OrderByDescending(x=>x.Blog_Id).ToList();   
            foreach(var item in lst)
            {
                Console.WriteLine(item.Blog_Title);
                Console.WriteLine(item.Blog_Author);
                Console.WriteLine(item.Blog_Content);
            }
        }

        private void Create(string title,string author,string content)
        {
            BlogDataModel blog = new BlogDataModel()
            {
                Blog_Title = title,
                Blog_Author = author,
                Blog_Content = content
            };

            AppDbContext db = new AppDbContext();
            db.Blogs.Add(blog);
            var result=db.SaveChanges();
            string message = result > 0 ? "Create successful" : "Create error";
            Console.WriteLine(message);
        }

        private void Edit(int id)
        {
            AppDbContext db = new AppDbContext();
            BlogDataModel item = db.Blogs.FirstOrDefault(x => x.Blog_Id == id);
            if (item == null)
            {
                Console.WriteLine("No data found");
                return;
            }

            Console.WriteLine(item.Blog_Title);
            Console.WriteLine(item.Blog_Author);
            Console.WriteLine(item.Blog_Content);
        }

        private void Update(int id,string title,string author,string content)
        {
            AppDbContext db = new AppDbContext();
            BlogDataModel item = db.Blogs.FirstOrDefault(x => x.Blog_Id == id);
            if (item == null) { 
                Console.WriteLine("no data found"); 
                return; 
            }

            item.Blog_Title = title;
            item.Blog_Author = author;
            item.Blog_Content = content;

            var result=db.SaveChanges();
            string message = result > 0 ? "Update Success" : "Update error";
            Console.WriteLine(message);

        }

        private void Delete(int id)
        {
            AppDbContext db = new AppDbContext();
            BlogDataModel item = db.Blogs.FirstOrDefault(x => x.Blog_Id == id);
            if (item == null)
            {
                Console.WriteLine("No data found");
                return;
            }

            db.Blogs.Remove(item);
            var result=db.SaveChanges();
            string message = result > 0 ? "Delete success" : "Delete error";
            Console.WriteLine(message);
        }
    }
}
