using Serilog.Sinks.MSSqlServer;
using Serilog;
using log4net.Config;
using log4net.Core;
using log4net;
using System.Reflection;
using Microsoft.VisualBasic;
using System.Diagnostics;

Console.WriteLine("Hello, World!");

#region serilog
//Log.Logger = new LoggerConfiguration()
//         .MinimumLevel.Debug()
//         .WriteTo.Console()
//         .WriteTo.File("logs/myapp.txt", rollingInterval: RollingInterval.Hour, fileSizeLimitBytes: 1024)
//         .WriteTo
//            .MSSqlServer(
//                connectionString: "Server=.;Database=TestDb;User ID=sa;Password=sasa;TrustServerCertificate=True;",
//                sinkOptions: new MSSqlServerSinkOptions
//                {
//                    TableName = "LogEvents",
//                    AutoCreateSqlTable = true
//                })
//         .CreateLogger();

//Log.Information("Hello, world!");

//int a = 10, b = 0;
//try
//{
//    Log.Debug("Dividing {A} by {B}", a, b);
//    Console.WriteLine(a / b);
//}
//catch (Exception ex)
//{
//    Log.Error(ex, "Something went wrong");
//}
//finally
//{
//    await Log.CloseAndFlushAsync();
//}
#endregion

#region log4net
var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
ILog _logger = LogManager.GetLogger(typeof(LoggerManager));

_logger.Info("Testing information log");
_logger.Debug("Testing Debug log");
_logger.Fatal("Testing Fatal log");
#endregion
