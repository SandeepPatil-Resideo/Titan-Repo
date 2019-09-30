﻿using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Diagnostics.EventFlow;
using System.IO;

namespace TitanTemplate.titanaddressapi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var diagnostics = DiagnosticPipelineFactory.CreatePipeline(Directory.GetCurrentDirectory() + "/eventFlowConfig.json"))
            {
                CreateWebHostBuilder(args).Build().Run();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
    }
}
