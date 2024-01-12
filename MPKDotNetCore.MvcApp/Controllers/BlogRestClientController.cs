using Microsoft.AspNetCore.Mvc;
using MPKDotNetCore.MvcApp.Models;
using Newtonsoft.Json;
using RestSharp;
using static System.Net.Mime.MediaTypeNames;
using System.Text;

namespace MPKDotNetCore.MvcApp.Controllers
{
    public class BlogRestClientController : Controller
    {
        private readonly RestClient _restClient;

        public BlogRestClientController(RestClient restClient)
        {
            _restClient = restClient;
        }

        public async Task<IActionResult> Index()
        {
            BlogListResponseModel model = new BlogListResponseModel();
            RestRequest request = new RestRequest("api/blog", Method.Get);
            var response = await _restClient.ExecuteAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = response.Content!;
                model = JsonConvert.DeserializeObject<BlogListResponseModel>(jsonStr)!;
            }
            TempData["Controller"] = "blogrestclient";
            return View("~/Views/BlogRefit/Index.cshtml", model);
        }

        public IActionResult Create()
        {
            TempData["Controller"] = "blogrestclient";
            return View("~/Views/BlogRefit/Create.cshtml");
        }

        public async Task<IActionResult> Save(BlogDataModel blog)
        {
            RestRequest request = new RestRequest("api/blog/", Method.Post);
            request.AddBody(blog);
            RestResponse response = await _restClient.ExecuteAsync(request);
            return Redirect("/blogrestclient");
        }

        public async Task<IActionResult> Edit(int id)
        {
            BlogResponseModel model = new BlogResponseModel();
            RestRequest request = new RestRequest($"api/blog/{id}", Method.Get);
            RestResponse response = await _restClient.ExecuteAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = response.Content;
                model = JsonConvert.DeserializeObject<BlogResponseModel>(jsonStr);
            }
            TempData["Controller"] = "blogrestclient";
            return View("~/Views/BlogRefit/Edit.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, BlogDataModel blog)
        {
            RestRequest request = new RestRequest($"api/blog/{id}", Method.Put);
            request.AddBody(blog);
            RestResponse response = await _restClient.ExecuteAsync(request);
            return Redirect("/blogrestclient");
        }

        public async Task<IActionResult> Delete(int id)
        {
            RestRequest request = new RestRequest($"api/blog/{id}", Method.Delete);
            RestResponse response = await _restClient.ExecuteAsync(request);
            return Redirect("/blogrestclient");
        }
    }
}

