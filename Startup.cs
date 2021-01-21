using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using animated_spoon.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using animated_spoon.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace animated_spoon
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
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddTransient<IProductRepository, EFProductRepository>();
            services.AddDbContext<ProductDbContext>(options => options.UseNpgsql(Configuration.GetConnectionString("ProductContext")));
            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ProductDbContext>().AddDefaultTokenProviders();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                   name: "products",
                   pattern: "products/{category?}",
                   defaults: new { controller = "Product", action = "List" }
                );

                endpoints.MapControllerRoute(
                   name: "products",
                   pattern: "products/{action=Index}",
                   defaults: new { controller = "Product", action = "Index" }
                );

                endpoints.MapControllerRoute(
                   name: "admin",
                   pattern: "admin/products/{action=Index}",
                   defaults: new { controller = "Admin", action = "Index" }
                );

                endpoints.MapControllerRoute(
                    name: "admin",
                    pattern: "admin/product/{id}/{action=Edit}",
                    defaults: new { controller = "Admin", action = "Edit", id = 1 }
                );

                endpoints.MapControllerRoute(
                    name: "account",
                    pattern: "account/{action=Login}",
                    defaults: new { controller = "Account", action = "Login" }
                );
            });

            SeedData.EnsurePopulated(app);
        }
    }
}
