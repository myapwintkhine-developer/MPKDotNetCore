using System;
using System.Collections.Generic;
using System.Text;

namespace MPKDotNetCore.ConsoleApp.Models
{
    public class BlogResponseModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public BlogDataModel Data { get; set; }

    }
}

