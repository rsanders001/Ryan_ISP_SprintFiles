using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TravelApp.Models;
using Microsoft.AspNetCore.Identity;

namespace TravelApp
{
    public class Startup
    {

        // this exposes the connection string in appsettings.json
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }



        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //add identity
            services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer(Configuration["Data:Travel:ConnectionString"]));
            //override password policy
            services.AddIdentity<AppUser, IdentityRole>(opts =>
            {
                opts.User.RequireUniqueEmail = true;
                opts.Password.RequiredLength = 6;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireDigit = false;
                opts.Password.RequiredUniqueChars = 1;
            }).AddEntityFrameworkStores<AppIdentityDbContext>().AddDefaultTokenProviders();

            services.AddDbContext<StudentTravelDocsContext>(options => options.UseSqlServer(Configuration["Data:Travel:ConnectionString"]));
            services.AddTransient<ITripRepository, EFTripRepository>();
            services.AddTransient<IPersonRepository, EFPersonRepository>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //custom route
            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //        name: "pagination",
            //        template: "Page{page}",
            //        defaults: new { Controller = "Home", action = "Index" });
            //    routes.MapRoute(
            //        name: "default",
            //        template: "{controller=Home}/{action=Index}/{id?}");
            //});

            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();
            app.UseStaticFiles();
        }
    }
}
