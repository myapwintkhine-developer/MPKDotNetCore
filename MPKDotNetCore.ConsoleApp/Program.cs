using MPKDotNetCore.ConsoleApp.AdoDotNetExamples;
using MPKDotNetCore.ConsoleApp.DapperExamples;
using System;

namespace MPKDotNetCore.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AdoDotNetExample ado = new AdoDotNetExample();
            ado.Run();
            DapperExample dapper = new DapperExample();
            dapper.Run();
        }
    }
}
