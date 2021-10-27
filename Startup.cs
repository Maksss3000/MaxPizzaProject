using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MaxPizzaProject.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MaxPizzaProject
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddRazorPages();


            //Adding services for Sessions.
            services.AddDistributedMemoryCache();
            services.AddSession();
            //When we using class Cart ,the programm will
            //create SessionCart.GetCart(IserviceProvider sp)
            //It returns SessionCart cart = session?.GetJson<SessionCart>("Cart") ??
            //new SessionCart()
            services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


            services.AddServerSideBlazor();

            //Adding Connection to My Database.(MaxPizzaStore db).
            string connString = "ConnectionStrings:PizzeriaStoreConnection";
            services.AddDbContext<PizzeriaDbContext>(opts => opts.UseSqlServer(Configuration[connString]));

            services.AddScoped<IToppingRepository, EFToppingRepository>();
            services.AddScoped<IPizzaRepository, EFPizzaRepository>();
            services.AddScoped<ISizeRepository, EFSizeRepository>();

            /*By WebOptimizer we can optimize our .css code.
            In link we will use main.css file , but in fact this file
            are  optimized style.scss file.
            */
            services.AddWebOptimizer(pipeline =>
            {
                pipeline.AddScssBundle("css/main.css", "sass/style.scss");
               

            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
                              PizzeriaDbContext ctx)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseWebOptimizer();

            app.UseStaticFiles();

            app.UseSession();

            app.UseRouting();


            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("specificPizza", "Pizza/{pizzaId:long}",
                   new { Controller = "Home", action = "SpecificPizza" });

                endpoints.MapControllerRoute("category", "Category/{pizzaCatId}",
                   new { Controller = "Home", action = "AllPizzas" , pizzaCatId = 0 });

                endpoints.MapControllerRoute("cart", "Cart/",
                  new { Controller = "Home", action = "SeeCart" });

               

                endpoints.MapDefaultControllerRoute();
                endpoints.MapBlazorHub();
            });

            SeedData.Seed(ctx);
        }
    }
}
