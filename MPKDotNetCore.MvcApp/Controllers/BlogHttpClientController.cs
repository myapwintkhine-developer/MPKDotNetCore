using Microsoft.AspNetCore.Mvc;
using MPKDotNetCore.MvcApp.Models;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;
using System.Text;

namespace MPKDotNetCore.MvcApp.Controllers
{
    public class BlogHttpClientController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public BlogHttpClientController(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            //_httpClient.BaseAddress = new Uri(_configuration.GetSection("RestApiUrl").Value!);
        }

        public async Task<IActionResult> Index()
        {
            BlogListResponseModel model = new BlogListResponseModel();
            HttpResponseMessage response = await _httpClient.GetAsync("api/blog");
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
                model = JsonConvert.DeserializeObject<BlogListResponseModel>(jsonStr)!;
            }
            TempData["Controller"] = "bloghttpclient";
            return View("~/Views/BlogRefit/Index.cshtml", model);
        }

        public IActionResult Create()
        {
            TempData["Controller"] = "bloghttpclient";
            return View("~/Views/BlogRefit/Create.cshtml");
        }

        public async Task<IActionResult> Save(BlogDataModel blog)
        {
            string blogJson = JsonConvert.SerializeObject(blog);
            HttpContent httpContent = new StringContent(blogJson, Encoding.UTF8, Application.Json);
            var response = await _httpClient.PostAsync($"https://localhost:7298/api/blog", httpContent);
            return Redirect("/bloghttpclient");
        }

        public async Task<IActionResult> Edit(int id)
        {
            BlogResponseModel model = new BlogResponseModel();
            var response = await _httpClient.GetAsync($"https://localhost:7298/api/blog/{id}");
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
                model = JsonConvert.DeserializeObject<BlogResponseModel>(jsonStr);
            }
            TempData["Controller"] = "bloghttpclient";
            return View("~/Views/BlogRefit/Edit.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, BlogDataModel blog)
        {
            string blogJson = JsonConvert.SerializeObject(blog);
            HttpContent httpContent = new StringContent(blogJson, Encoding.UTF8, Application.Json);
            HttpResponseMessage response = await _httpClient.PutAsync($"https://localhost:7298/api/blog/{id}", httpContent);
            return Redirect("/bloghttpclient");
        }

        public async Task<IActionResult> Delete(int id)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"https://localhost:7298/api/blog/{id}");
            return Redirect("/bloghttpclient");
        }

    }
}
