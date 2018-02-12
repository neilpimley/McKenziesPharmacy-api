using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Pharmacy.Repositories;
using Pharmacy.Services;
using Pharmacy.Services.Interfaces;


namespace Pharmacy.IntegrationTests
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<PharmacyContext>(
                optionsBuilder => optionsBuilder.UseInMemoryDatabase("InMemoryDb"));

            services.AddMvc();

            services.AddScoped<ICustomersService,
                CustomersService>();
        }

        public void Configure(IApplicationBuilder app,
            IHostingEnvironment env,
            ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                var repository = app.ApplicationServices.GetService<ICustomersService>();
                InitializeDatabaseAsync(repository).Wait();
                app.UseStaticFiles();
                app.UseSwagger(SwaggerHelper.ConfigureSwagger);
                app.UseSwaggerUI(SwaggerHelper.ConfigureSwaggerUI);
            }

            app.UseStaticFiles();

            app.UseMvc();
        }
    }
}
