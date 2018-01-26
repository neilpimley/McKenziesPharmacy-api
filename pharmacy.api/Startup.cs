using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pharmacy.Models;
using Pharmacy.Repositories;
using Pharmacy.Repositories.Interfaces;
using Pharmacy.Services;
using Pharmacy.Services.Interfaces;
using Swashbuckle.AspNetCore.Swagger;

namespace Pharmacy
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // _testSecret = Configuration["PharmacySecret"];

            services.AddDbContext<PharmacyContext>(options => options.UseSqlServer(Configuration["ConnectionString:Entities"]));

            // Add application services.
            services.AddTransient<ICustomersService, CustomersService>();
            services.AddTransient<IDrugsService, DrugsService>();
            services.AddTransient<IFavouritesService, FavouritesService>();
            services.AddTransient<IOrdersService, OrdersService>();
            services.AddTransient<IRegisterService, RegisterService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IReminderService, ReminderService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v2", new Info { Title = "McKenzies Pharmacy API", Version = "v2" });
            });

            services.Configure<ServiceSettings>(Configuration.GetSection("ServiceSettings"));


            // TODO: find a workaround to having this regferenced here so I can decouple application
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            string domain = $"https://{Configuration["Auth0:Domain"]}/";
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.Authority = domain;
                options.Audience = Configuration["Auth0:ApiIdentifier"];
            });

            services.AddMvc();
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Shows UseCors with CorsPolicyBuilder.
            var allowClient = Configuration["ClientDomain"];
            app.UseCors(builder => builder
                .WithOrigins(allowClient)
                .AllowAnyMethod()
                .WithHeaders("authorization", "accept", "content-type", "origin"));;

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v2/swagger.json", "McKenzies Pharmacy API V2");
            });

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
