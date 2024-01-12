using MPKDotNetCore.ConsoleApp.AdoDotNetExamples;
using MPKDotNetCore.ConsoleApp.DapperExamples;
using MPKDotNetCore.ConsoleApp.EFCoreExamples;
using MPKDotNetCore.ConsoleApp.HTTPClientExamples;
using MPKDotNetCore.ConsoleApp.RefitExamples;
using MPKDotNetCore.ConsoleApp.RestClientExamples;
using System;
using System.Threading.Tasks;

namespace MPKDotNetCore.ConsoleApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        
        {
            // AdoDotNetExample ado = new AdoDotNetExample();
            // ado.Run();
            // DapperExample dapper = new DapperExample();
            // dapper.Run();
            //EFCoreExample efCore=new EFCoreExample();
            //efCore.Run();

            Console.WriteLine("Hello World!");

            Console.WriteLine("waiting for api... ");
            Console.ReadKey();
            //HttpClientExample httpClientExample = new HttpClientExample();
            //await httpClientExample.Run();
            //RefitExample refitExample = new RefitExample();
            //await refitExample.Run();
            RestClientExample restClientExample = new RestClientExample();
            await restClientExample.Run();

        }
    }
}
