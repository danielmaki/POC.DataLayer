using Microsoft.Extensions.Hosting;

using POC.DataLayer.Data.Hosting;

namespace POC.DataLayer.Data
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddDataServices(hostContext.Configuration);
                });
    }
}
