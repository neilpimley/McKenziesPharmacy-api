using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using Pharmacy.Models;
using Pharmacy.Models.Pocos;
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
            services.AddDbContext<PharmacyContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:Entities"]));
            
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

            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CustomerPoco, Customer>();
                cfg.CreateMap<OrderPoco, Order>();
                cfg.CreateMap<DrugPoco, Drug>();
                cfg.CreateMap<ReminderPoco, Reminder>();

                cfg.CreateMap<Customer, CustomerPoco>();
                cfg.CreateMap<Order, OrderPoco>();
                cfg.CreateMap<Drug, DrugPoco>();
                cfg.CreateMap<Reminder, ReminderPoco>();
            });

            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            services.AddTransient<IUnitOfWork, UnitOfWork>();

            string domain = $"https://{Configuration["ServerSettings:Auth0Domain"]}/";
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.Authority = domain;
                options.Audience = Configuration["ServerSettings:Auth0ApiIdentifier"];
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
