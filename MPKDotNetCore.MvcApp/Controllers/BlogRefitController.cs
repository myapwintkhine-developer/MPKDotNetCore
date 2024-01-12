using Microsoft.AspNetCore.Mvc;
using MPKDotNetCore.MvcApp.Interfaces;
using MPKDotNetCore.MvcApp.Models;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;
using System.Text;

namespace MPKDotNetCore.MvcApp.Controllers
{
    public class BlogRefitController : Controller
    {
        private readonly IBlogApi _blogApi;

        public BlogRefitController(IBlogApi blogApi)
        {
            _blogApi = blogApi;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _blogApi.GetBlogs();
            TempData["Controller"] = "blogrefit";
            return View(model);
        }

        public IActionResult Create()
        {
            TempData["Controller"] = "blogrefit";
            return View();
        }

        public async Task<IActionResult> Save(BlogDataModel blog)
        {
            BlogResponseModel model = await _blogApi.CreateBlog(blog);
            return Redirect("/blogrefit");
        }

        public async Task<IActionResult> Edit(int id)
        {
            BlogResponseModel model = await _blogApi.GetBlog(id);
            TempData["Controller"] = "blogrefit";
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, BlogDataModel blog)
        {
            BlogResponseModel model = await _blogApi.UpdateBlog(id, blog);
            return Redirect("/blogrefit");
        }

        public async Task<IActionResult> Delete(int id)
        {
            BlogResponseModel model = await _blogApi.DeleteBlog(id);
            return Redirect("/blogrefit");
        }
    }


}