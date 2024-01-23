using MPKDotNetCore.MinimalApi;
using MPKDotNetCore.MinimalApi.Features.Blog;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using NLog.Web;
using NLog;
using System.Reflection;

Assembly assembly = Assembly.GetExecutingAssembly();
string projectName = assembly.GetName().Name!;

#region serilog
//Log.Logger = new LoggerConfiguration()
//    .WriteTo.Console()
//    .WriteTo.File("logs/projectName.txt", rollingInterval: RollingInterval.Hour, fileSizeLimitBytes: 1024)
//    .WriteTo
//          .MSSqlServer(
//           connectionString: "Server=.;Database=TestDb;User ID=sa;Password=sasa;TrustServerCertificate=True;",
//           sinkOptions: new MSSqlServerSinkOptions
//            {
//            TableName = "LogEvents",
//            AutoCreateSqlTable = true
//            })
//         .CreateLogger();
//try
//{
//    Log.Information("Starting web application");

//    var builder = WebApplication.CreateBuilder(args);

//    builder.Host.UseSerilog(); // <-- Add this line

//    //builder.Services.AddControllersWithViews().AddJsonOptions(opt =>
//    //{
//    //    opt.JsonSerializerOptions.PropertyNameCaseInsensitive = false;
//    //    opt.JsonSerializerOptions.PropertyNamingPolicy = null;
//    //});

//    builder.Services.ConfigureHttpJsonOptions(option =>
//    {
//        option.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
//        option.SerializerOptions.PropertyNamingPolicy = null;
//    });

//    builder.Services.Configure<JsonOptions>(options =>
//    {
//        options.JsonSerializerOptions.PropertyNameCaseInsensitive = false;
//        options.JsonSerializerOptions.PropertyNamingPolicy = null;
//    });

//    //builder.Services.AddControllersWithViews().AddJsonOptions(opt =>
//    //{
//    //    opt.JsonSerializerOptions.PropertyNameCaseInsensitive = false;
//    //    opt.JsonSerializerOptions.PropertyNamingPolicy = null;
//    //});

//    builder.Services.ConfigureHttpJsonOptions(option =>
//    {
//        option.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
//        option.SerializerOptions.PropertyNamingPolicy = null;
//    });


//    // Add services to the container.
//    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//    builder.Services.AddEndpointsApiExplorer();
//    builder.Services.AddSwaggerGen();

//    builder.Services.AddDbContext<AppDbContext>(opt =>
//    {
//        opt.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"));
//    },
//    ServiceLifetime.Transient,
//    ServiceLifetime.Transient);

//    var app = builder.Build();

//    // Configure the HTTP request pipeline.
//    if (app.Environment.IsDevelopment())
//    {
//        app.UseSwagger();
//        app.UseSwaggerUI();
//    }

//    app.UseHttpsRedirection();

//    app.AddBlogService();

//    app.Run();
//}
//catch (Exception ex)
//{
//    Log.Fatal(ex, "Application terminated unexpectedly");
//}
//finally
//{
//    Log.CloseAndFlush();
//}
#endregion

#region nlog
var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");


try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Services.AddControllersWithViews();

    // NLog: Setup NLog for Dependency injection
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    builder.Services.Configure<JsonOptions>(options =>
    {
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = false;
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

    // Add services to the container.
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddDbContext<AppDbContext>(opt =>
    {
        opt.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"));
    },
    ServiceLifetime.Transient,
    ServiceLifetime.Transient);

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.AddBlogService();

    app.Run();
}
catch (Exception exception)
{
    // NLog: catch setup errors
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    NLog.LogManager.Shutdown();
}
#endregion
