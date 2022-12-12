using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RedOctober.DAL.EntityFramework;
using RedOctober.DAL.Repositories.Abstractions;
using RedOctober.DAL.Repositories.Implementations;
using RedOctober.Forms;

namespace RedOctober
{
    public static class Registrar
    {
        public static IHostBuilder AddServices(this IHostBuilder services)
        {
            return services
                .InstallServices()
                .InstallRepositories();
        }

        private static IHostBuilder InstallServices(this IHostBuilder serviceCollection)
        {
            serviceCollection.ConfigureServices((context, services) => {
                services.AddTransient<FormMain>();
                var MyConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                var connection = MyConfig.GetValue<string>("ConnectionStrings:DefaultConnection");
                services.AddDbContext<DataContext>(options => options.UseSqlServer(connection));
            });
            return serviceCollection;
        }

        private static IHostBuilder InstallRepositories(this IHostBuilder serviceCollection)
        {
            serviceCollection.ConfigureServices((context, services) => {
                services.AddTransient<IDepartmentsReposytory, DepartmentsReposytory>();
                services.AddTransient<IEmployeesReposytory, EmployeesReposytory>();
                services.AddTransient<IPositionsReposytory, PositionsReposytory>();
            });
            return serviceCollection;
        }
    }
}
