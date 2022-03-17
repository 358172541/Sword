using Autofac;
using Autofac.Extensions.DependencyInjection;
using Core;
using EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Api
{
    public class Program
    {
        public static string Audience = "https://api.sword.com";
        public static string Issuer = "https://api.sword.com";
        public static string Secret = "Wjagjbmui5ZBCC0nV6HMdTsEYjznXJGqhOVIRbH50P8=";
        public static void Main(string[] args) => CreateHostBuilder(args).Build().Run();
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(budr => budr.UseStartup<Startup>())
                .UseSerilog((cntx, logr) => logr.ReadFrom.Configuration(cntx.Configuration))
                .UseServiceProviderFactory(cntx => new AutofacServiceProviderFactory(budr =>
                {
                    budr.RegisterType<AppDbContext>()
                        .As<ITransaction>()
                        .InstancePerLifetimeScope();
                    budr.RegisterAssemblyTypes(typeof(AppDbContext).Assembly)
                        .AsClosedTypesOf(typeof(IRepository<>))
                        .InstancePerLifetimeScope();
                }));
    }
}
