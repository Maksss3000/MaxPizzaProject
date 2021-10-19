using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MaxPizzaProject.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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

            app.UseRouting();


            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });

            SeedData.Seed(ctx);
        }
    }
}
