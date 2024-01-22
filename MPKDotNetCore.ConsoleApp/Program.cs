using log4net.Config;
using log4net.Core;
using log4net;
using MPKDotNetCore.ConsoleApp.AdoDotNetExamples;
using MPKDotNetCore.ConsoleApp.DapperExamples;
using MPKDotNetCore.ConsoleApp.EFCoreExamples;
using MPKDotNetCore.ConsoleApp.HTTPClientExamples;
using MPKDotNetCore.ConsoleApp.RefitExamples;
using MPKDotNetCore.ConsoleApp.RestClientExamples;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace MPKDotNetCore.ConsoleApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        
        {
            #region log4net
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
            ILog _logger = LogManager.GetLogger(typeof(LoggerManager));
            ILog log = LogManager.GetLogger(typeof(Program));
            log.Info("Application started...");
            #endregion
            AdoDotNetExample ado = new AdoDotNetExample();
            ado.Run();
            // DapperExample dapper = new DapperExample();
            // dapper.Run();
            //EFCoreExample efCore=new EFCoreExample();
            //efCore.Run();
            

            Console.WriteLine("Hello World!");

            //Console.WriteLine("waiting for api... ");
            Console.ReadKey();
            //HttpClientExample httpClientExample = new HttpClientExample();
            //await httpClientExample.Run();
            //RefitExample refitExample = new RefitExample();
            //await refitExample.Run();
            //RestClientExample restClientExample = new RestClientExample();
            //await restClientExample.Run();

        }
    }
}
