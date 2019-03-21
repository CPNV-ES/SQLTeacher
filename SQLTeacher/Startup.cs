using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SQLTeacher.Models;

namespace SQLTeacher
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<SQLTeacherContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                // Routes for exercises
                routes.MapRoute(
                    name: "exercises",
                    template: "exercises",
                    defaults: new { controller = "Exercises", action = "Index" });
                routes.MapRoute(
                    name: "exercises-create",
                    template: "exercises/create",
                    defaults: new { controller = "Exercises", action = "Create" });
                routes.MapRoute(
                    name: "exercises-edit",
                    template: "exercises/edit/{id}",
                    defaults: new { controller = "Exercises", action = "Edit"});
                routes.MapRoute(
                    name: "exercises-delete",
                    template: "exercises/delete/{id}",
                    defaults: new { controller = "Exercises", action = "Delete" });
                routes.MapRoute(
                    name: "exercises-detail",
                    template: "exercises/details/{id}",
                    defaults: new { controller = "Exercises", action = "Details" });
                routes.MapRoute(
                    name: "exercises-activate",
                    template: "exercises/activate/{id}",
                    defaults: new { controller = "Exercises", action = "Activate" });

                // Routes for queries
                routes.MapRoute(
                    name: "queries",
                    template: "queries",
                    defaults: new { controller = "Queries", action = "Index" });
                routes.MapRoute(
                    name: "queries-create",
                    template: "queries/create",
                    defaults: new { controller = "Queries", action = "Create" });
                routes.MapRoute(
                    name: "queries-edit",
                    template: "queries/edit/{id}",
                    defaults: new { controller = "Queries", action = "Edit" });
                routes.MapRoute(
                    name: "queries-delete",
                    template: "queries/delete/{id}",
                    defaults: new { controller = "Queries", action = "Delete" });
                routes.MapRoute(
                    name: "queries-detail",
                    template: "queries/details/{id}",
                    defaults: new { controller = "Queries", action = "Details" });

                // Routes for home
                routes.MapRoute(
                    name: "home",
                    template: "/",
                    defaults: new { controller = "Home", action = "Index" });

                // Default
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
