using System;
using System.Collections.Generic;

namespace MPKDotNetCore.DbFirstCommandApp.Models;

public partial class Blog
{
    public int BlogId { get; set; }

    public string BlogTitle { get; set; } = null!;

    public string BlogAuthor { get; set; } = null!;

    public string? BlogContent { get; set; }
}
