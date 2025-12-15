using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Sinks.Email;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace BrainstormSessions
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                //.WriteTo.Email(
                //    new EmailSinkOptions
                //    {
                //        From = "app@brainstorm.local",
                //        To = new List<string> { "admin@brainstorm.local" },
                //        Host = "localhost",
                //        Port = 25,
                //        Subject = new Serilog.Formatting.Display.MessageTemplateTextFormatter("Brainstorm Error Alert", null),
                //        Credentials = new NetworkCredential("username", "password")
                //    },
                //    restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Error
                //)
                .CreateLogger();

            try
            {
                Log.Information("Starting application");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Application failed to start");
            } 
            finally
            {
                Log.CloseAndFlush();
            }

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
