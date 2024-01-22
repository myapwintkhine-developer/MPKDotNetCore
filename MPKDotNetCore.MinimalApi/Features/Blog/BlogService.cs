using MPKDotNetCore.MinimalApi;
using MPKDotNetCore.MinimalApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace MPKDotNetCore.MinimalApi.Features.Blog;

public static class BlogService
{
    public static void AddBlogService(this IEndpointRouteBuilder app)
    {
        app.MapGet("/blog/{pageNo}/{pageSize}", async ([FromServices] AppDbContext db, [FromServices] ILogger<Program> _logger, int pageNo, int pageSize) =>
        {
            var lst = await db.Blogs
                .AsNoTracking()
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            _logger.LogInformation("Blog List => " + JsonSerializer.Serialize(lst));
            return lst;
        })
        .WithName("GetBlogs")
        .WithOpenApi();

        app.MapPost("/blog", async ([FromServices] AppDbContext db, BlogDataModel blog) =>
        {
            await db.Blogs.AddAsync(blog);
            int result = await db.SaveChangesAsync();

            string message = result > 0 ? "Saving Successful." : "Saving Failed.";
            return Results.Ok(new BlogResponseModel
            {
                Data = blog,
                IsSuccess = result > 0,
                Message = message
            });
        })
       .WithName("CreateBlog")
       .WithOpenApi();

        app.MapGet("/blog/{id}", async ([FromServices] AppDbContext db, int id) =>
        {
            var blog = await db.Blogs.Where(x => x.Blog_Id == id).FirstOrDefaultAsync();

            string message = blog == null ? "Blog not found." : "";
            var result = blog == null ? false : true;
            return Results.Ok(new BlogResponseModel
            {
                Data = blog,
                IsSuccess = result,
                Message = message
            });
        })
        .WithName("EditBlog")
        .WithOpenApi();

        app.MapPut("/blog/{id}", async ([FromServices] AppDbContext db, int id, BlogDataModel reqBlog) =>
        {
            var blog = await db.Blogs.Where(x => x.Blog_Id == id).FirstOrDefaultAsync();

            if (blog == null)
            {
                return Results.NotFound("Blog not found.");
            }

            blog.Blog_Title = reqBlog.Blog_Title;
            blog.Blog_Author= reqBlog.Blog_Author;
            blog.Blog_Content=reqBlog.Blog_Content;
            int result = await db.SaveChangesAsync();

            string message = result > 0 ? "Update Successful." : "Update Failed.";
            return Results.Ok(new BlogResponseModel
            {
                Data = blog,
                IsSuccess = result > 0,
                Message = message
            });
        })
            .WithName("PutBlog")
            .WithOpenApi();

        app.MapPatch("/blog/{id}", async ([FromServices] AppDbContext db, int id, BlogDataModel reqBlog) =>
        {
            var blog = await db.Blogs.Where(x => x.Blog_Id == id).FirstOrDefaultAsync();

            if (blog == null)
            {
                return Results.NotFound("Blog not found.");
            }

            if (!string.IsNullOrWhiteSpace(reqBlog.Blog_Title))
            {
                blog.Blog_Title = blog.Blog_Title;
            }
            if (!string.IsNullOrWhiteSpace(reqBlog.Blog_Author))
            {
                blog.Blog_Author = reqBlog.Blog_Author;
            }
            if (!string.IsNullOrWhiteSpace(reqBlog.Blog_Content))
            {
                blog.Blog_Content = reqBlog.Blog_Content;
            }
            int result = await db.SaveChangesAsync();

            string message = result > 0 ? "Update Successful." : "Update Failed.";
            return Results.Ok(new BlogResponseModel
            {
                Data = blog,
                IsSuccess = result > 0,
                Message = message
            });
        })
    .WithName("PatchBlog")
    .WithOpenApi();

        app.MapDelete("/blog/{id}", async ([FromServices] AppDbContext db, int id) =>
        {
            var blog = await db.Blogs.FindAsync(id);

            if (blog == null)
            {
                return Results.NotFound("Blog not found.");
            }

            db.Blogs.Remove(blog);
            int result = await db.SaveChangesAsync();

            string message = result > 0 ? "Deleting Successful." : "Deleting Failed.";
            return Results.Ok(new BlogResponseModel
            {
                Data = null,
                IsSuccess = result > 0,
                Message = message
            });
        })
        .WithName("DeleteBlog")
        .WithOpenApi();
    }
}