using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RedOctober.Forms;

namespace RedOctober
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            var builder = Host.CreateDefaultBuilder();
            builder.AddServices();
            var host = builder.Build();
            ServiceProvider = host.Services;

            Application.Run(ServiceProvider.GetRequiredService<FormMain>());
        }

        public static IServiceProvider? ServiceProvider { get; private set; }
    }
}