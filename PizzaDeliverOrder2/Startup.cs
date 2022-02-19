using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PizzaDeliverOrder2.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PizzaDeliverOrder2.Providers;
using System.IO;

namespace PizzaDeliverOrder2
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private ILoggerFactory loggerFactory;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddSingleton(provider => Configuration);
            services.AddTransient<IPizzaDeliveryDBContext, PizzaDeliveryDbContext>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IOrderProvider, OrderProvider>();

            //var configurationForderPath = Directory.G;
            var connectionString = Configuration[$"connectionString:PizzaOrderDb"];
            services.AddDbContextPool<PizzaDeliveryDbContext>(options => options.UseSqlServer(connectionString, sqlServerOptions => { sqlServerOptions.CommandTimeout(120); }));

            services.AddMvc();
            services.AddMvcCore();
            services.AddControllers(mvcOptions => mvcOptions.EnableEndpointRouting = false);
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "PizzaDeliver APIs", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            var logConfigFullPath = env.ContentRootPath + $"Configuration\\log4net.config";
            this.loggerFactory = loggerFactory;
            this.loggerFactory.AddLog4Net(logConfigFullPath);
            var logger = loggerFactory.CreateLogger(typeof(Startup));
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json","PizzaDeliver API v1");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
