using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MPKDotNetCore.RestApi.Models;
using System.Reflection.Metadata;

namespace MPKDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetBlogs()
        {
            AppDbContext db = new AppDbContext();
            var list=db.Blogs.ToList();
            BlogListResponseModel model = new BlogListResponseModel
            {
                IsSuccess = true,
                Message = "Success",
                Data = list

            };
            return Ok(model);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            AppDbContext db = new AppDbContext();
            BlogResponseModel model = new BlogResponseModel();
            var item = db.Blogs.FirstOrDefault(x => x.BlogId == id);
            if(item is null)
            {
                model.IsSuccess = false;
                model.Message = "No data found";
                return NotFound(model);
            }

            model.IsSuccess = true;
            model.Message = "Success";
            model.Data = item;
            return Ok(model);
        }

        [HttpPost]
        public IActionResult CreateBlogs([FromBody] BlogDataModel blog)
        {
            AppDbContext db = new AppDbContext();
            db.Blogs.Add(blog);
            var result=db.SaveChanges();
            string message = result > 0 ? "Save success" : "Save error";
            BlogResponseModel model = new BlogResponseModel();
            model.IsSuccess = result > 0;
            model.Message = message;
            return Ok(model);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlogs(int id, [FromBody] BlogDataModel blog)
        {
            BlogResponseModel model=new BlogResponseModel();
            AppDbContext db = new AppDbContext();
            var item = db.Blogs.FirstOrDefault(x => x.BlogId == id);
            if(item is null)
            {
                model.IsSuccess = false;
                model.Message = "No data found";
                return NotFound(model);
            }

            item.BlogTitle = blog.BlogTitle;
            item.BlogAuthor = blog.BlogAuthor;
            item.BlogContent = blog.BlogContent;

            var result=db.SaveChanges();
            string message = result > 0 ? "Update success" : "Update fail";

            model = new BlogResponseModel()
            {
                IsSuccess = result > 0,
                Message = message,
                Data=item
            };
            return Ok(model);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlogs(int id, [FromBody] BlogDataModel blog)
        {
            BlogResponseModel model = new BlogResponseModel();
            AppDbContext db = new AppDbContext();
            var item = db.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                model.IsSuccess = false;
                model.Message = "No data found";
                return NotFound(model);
            }

            item.BlogTitle = blog.BlogTitle;
            item.BlogAuthor = blog.BlogAuthor;
            item.BlogContent = blog.BlogContent;

            var result = db.SaveChanges();
            string message = result > 0 ? "Update success" : "Update fail";

            model = new BlogResponseModel()
            {
                IsSuccess = result > 0,
                Message = message,
                Data=item
            };
            return Ok(model);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlogs(int id)
        {
            BlogResponseModel model = new BlogResponseModel();
            AppDbContext db = new AppDbContext();
            var item = db.Blogs.FirstOrDefault(x => x.BlogId == id);

            if (item is null)
            {
                model.IsSuccess = false;
                model.Message = "No data found";
                return NotFound(model);
            }

            db.Blogs.Remove(item);
            var result= db.SaveChanges();
            string message = result > 0 ? "Delete success" : "Delete error";
            model = new BlogResponseModel()
            {
                IsSuccess = result > 0,
                Message = message,
            };
            return Ok(model);
        }
    }
}
