using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseUrls("http://localhost:5001")
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
