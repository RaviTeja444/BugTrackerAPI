using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BugTrackerAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

        //private static void RunMassTransitPublisherWithRabbit()
        //{
        //    string rabbitMqAddress = "rabbitmq://localhost:5672/accounting";
        //    string rabbitMqQueue = "mycompany.domains.queues";
        //    Uri rabbitMqRootUri = new Uri(rabbitMqAddress);

        //    IBusControl rabbitBusControl = Bus.Factory.CreateUsingRabbitMq(rabbit =>
        //    {
        //        rabbit.Host(rabbitMqRootUri, settings =>
        //        {
        //            settings.Password("accountant");
        //            settings.Username("accountant");
        //        });
        //    });
        //}
        }
}
