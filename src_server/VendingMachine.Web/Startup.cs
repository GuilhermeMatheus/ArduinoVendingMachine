using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using VendingMachine.Core.Services;
using VendingMachine.EF;
using VendingMachine.EF.Repository;

namespace VendingMachine.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvc()
                .AddControllersAsServices();

            services
                .AddSingleton(
                    new LoggerFactory()
                    .AddConsole(LogLevel.Trace)
                    .AddDebug()
                )
                .AddLogging();

            var builder = new ContainerBuilder();

            builder.Register(
                c =>
                {
                    var optionsBuilder = new DbContextOptionsBuilder<VendingMachineDbContext>();
                    optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Database=VendingMachine;trusted_connection=true;Integrated Security=True");
                    var dbContext = new VendingMachineDbContext(optionsBuilder.Options);
                    dbContext.Database.EnsureCreated();
                    return dbContext;
                })
                .As<VendingMachineDbContext>()
                .As<DbContext>()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(ClientCardRepository).Assembly)
                   .Where(t => t.Name.EndsWith("Repository"))
                   .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(typeof(SaleService).Assembly)
                   .Where(t => t.Name.EndsWith("Service"))
                   .AsImplementedInterfaces();

            builder.Populate(services);
            
            return new AutofacServiceProvider(builder.Build());
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var seeder = new SampleDataSeeder();
            seeder.Initialize(app.ApplicationServices).Wait();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
