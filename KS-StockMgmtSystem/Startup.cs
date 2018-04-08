using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KS_StockMgmtSystem.Model.Entities;
using KS_StockMgmtSystem.Model.IRepositories;
using KS_StockMgmtSystem.Model.Repositories;
using KS_StockMgmtSystem.Service;
using KS_StockMgmtSystem.Service.IService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace KS_StockMgmtSystem
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
            //連線字串
            var sqlConnectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<SystemDBContext>(options => options.UseSqlServer(sqlConnectionString));


            // 加入Asp.Net Identity
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            })
                .AddEntityFrameworkStores<SystemDBContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IVersionDataRepository, VersionDataRepository>();
            services.AddScoped<IVersionDataService, VersionDataService>();

            services.AddScoped<IStockDataRepository, StockDataRepository>();
            services.AddScoped<IStockDataService, StockDataService>();


            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Stock Management System API", Version = "v1" });
            });

            services.AddMvc();

            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Stock Management System API V1");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Stock}/{action=List}/{id?}");
            });
        }
    }
}
