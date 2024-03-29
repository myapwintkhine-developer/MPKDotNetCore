﻿using MPKDotNetCore.ConsoleApp.Models;
using Newtonsoft.Json;
using Refit;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MPKDotNetCore.ConsoleApp.RefitExamples
{
    public class RefitExample
    {
        private readonly IBlogApi blogApi;

        public RefitExample()
        {
            blogApi = RestService.For<IBlogApi>("https://localhost:7098");
        }

        public async Task Run()
        {
            await Read();
            await Create("title", "author", "content");
            await Edit(1);
            await Update(1, "title test", "author test", "content test");
            await Delete(1);
            await Patch(2, "title2", "author2", "content2");
        }

        private async Task Read()
        {
            BlogListResponseModel model = await blogApi.GetBlogs();
            Console.WriteLine(JsonConvert.SerializeObject(model));
            Console.WriteLine(JsonConvert.SerializeObject(model, Formatting.Indented));
        }

        private async Task Create(string title, string author, string content)
        {
            BlogResponseModel model = await blogApi.CreateBlog(new BlogDataModel
            {
                Blog_Title = title,
                Blog_Author = author,
                Blog_Content = content,
            });
            Console.WriteLine(JsonConvert.SerializeObject(model, Formatting.Indented));
        }

        private async Task Edit(int id)
        {
            BlogResponseModel model = await blogApi.GetBlog(id);

            Console.WriteLine(JsonConvert.SerializeObject(model, Formatting.Indented));
        }

        private async Task Update(int id, string title, string author, string content)
        {
            BlogResponseModel model = await blogApi.UpdateBlog(id, new BlogDataModel()
            {
                Blog_Title = title,
                Blog_Author = author,
                Blog_Content = content,
            });

            Console.WriteLine(JsonConvert.SerializeObject(model, Formatting.Indented));
        }

        private async Task Delete(int id)
        {
            BlogResponseModel model = await blogApi.DeleteBlog(id);

            Console.WriteLine(JsonConvert.SerializeObject(model, Formatting.Indented));
        }

        private async Task Patch(int id, string title, string author, string content)
        {
            BlogResponseModel model = await blogApi.PatchBlog(id, new BlogDataModel()
            {
                Blog_Title = title,
                Blog_Author = author,
                Blog_Content = content,
            });

            Console.WriteLine(JsonConvert.SerializeObject(model, Formatting.Indented));
        }
    }

    public interface IBlogApi
    {
        [Get("/api/blog")]
        Task<BlogListResponseModel> GetBlogs();

        [Get("/api/blog/{pageNo}/{pageSize}")]
        Task<BlogListResponseModel> GetBlogs(int pageNo, int pageSize = 10);

        [Get("/api/blog/{id}")]
        Task<BlogResponseModel> GetBlog(int id);

        [Post("/api/blog")]
        Task<BlogResponseModel> CreateBlog(BlogDataModel blog);

        [Put("/api/blog/{id}")]
        Task<BlogResponseModel> UpdateBlog(int id, BlogDataModel blog);

        [Patch("/api/blog/{id}")]
        Task<BlogResponseModel> PatchBlog(int id, BlogDataModel blog);

        [Delete("/api/blog/{id}")]
        Task<BlogResponseModel> DeleteBlog(int id);
    }
}
