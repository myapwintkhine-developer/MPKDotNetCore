using System;
using System.Collections.Generic;
using System.Text;

namespace MPKDotNetCore.ConsoleApp.Models
{
    public class BlogListResponseModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<BlogDataModel> Data { get; set; }
    }
}
