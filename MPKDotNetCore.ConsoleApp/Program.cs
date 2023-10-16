﻿using MPKDotNetCore.ConsoleApp.AdoDotNetExamples;
using MPKDotNetCore.ConsoleApp.DapperExamples;
using MPKDotNetCore.ConsoleApp.EFCoreExamples;
using System;

namespace MPKDotNetCore.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
           // AdoDotNetExample ado = new AdoDotNetExample();
           // ado.Run();
           // DapperExample dapper = new DapperExample();
           // dapper.Run();
           EFCoreExample efCore=new EFCoreExample();
           efCore.Run();
        }
    }
}
