using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace ConsulWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseUrls($"http://*:8010")
                .UseStartup<Startup>();
    }
}
