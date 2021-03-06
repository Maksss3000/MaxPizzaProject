using Advanced.Models;
using MaxPizzaProject.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
            services.AddScoped<IDrinkRepository, EFDrinkRepository>();
            services.AddScoped<ICategoryRepository, EFCategoryRepository>();
            services.AddScoped<ISnackRepository, EFSnackRepository>();

            //Admin Services.
            services.AddScoped<IAdminRepository, EFAdminRepository>();


            services.AddScoped<ValidationClass>();
            /*By WebOptimizer we can optimize our .css code.
            In link we will use [name].css file , but in fact this file
            are  optimized [name].scss file.
            */
            services.AddWebOptimizer(pipeline =>
            {
                pipeline.AddScssBundle("css/pizzas.css", "sass/pizzas.scss");
               
            });

            //Identity Service.
            services.AddDbContext<IdentityContext>
                (opts => opts.UseSqlServer
                                    (Configuration["ConnectionStrings:IdentityConnection"]));

            services.AddIdentity<IdentityUser, IdentityRole>()
                                        .AddEntityFrameworkStores<IdentityContext>();

            services.Configure<IdentityOptions>(opts =>
            {
                opts.User.RequireUniqueEmail = true;
            });

            //Redirection Path , when user need to pass Authentication
            //Or Authorization.
            
            services.Configure<CookieAuthenticationOptions>(IdentityConstants.ApplicationScheme,
                opts => { opts.LoginPath = "/Users/Login";
                          opts.AccessDeniedPath = "/Users/AccessDenied";
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
                              PizzeriaDbContext ctx,IdentityContext identCtx)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseWebOptimizer();

            app.UseStaticFiles();

            app.UseSession();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {

                endpoints.MapControllerRoute("specificProduct", "Product/{productId:long}",
                   new { Controller = "Home", action = "SpecificProduct" });

                endpoints.MapControllerRoute("category", "Pizza/{CatName}",
                   new { Controller = "Home", action = "AllPizzas" });

                endpoints.MapControllerRoute("category", "Snack/{CatName}",
                  new { Controller = "Home", action = "AllSnacks" });

                endpoints.MapControllerRoute("cart", "Cart/",
                  new { Controller = "Home", action = "SeeCart" });

                endpoints.MapControllerRoute("drinks", "Drinks/{sizeName}",
                    new { Controller = "Home", action = "GetDrinksBySize" });

                endpoints.MapControllerRoute("productForm", "ProductForm/{productName}/{prodId}",
                    new { Controller = "Admin", action = "ProductForm" });

                endpoints.MapControllerRoute("productForm", "ProductForm/{productName}",
                     new { Controller = "Admin", action = "ProductForm" });


                endpoints.MapControllerRoute("sizeForm", "SizeForm/{sizeId}",
                     new { Controller = "Admin", action = "SizeForm" });


                endpoints.MapDefaultControllerRoute();
                endpoints.MapBlazorHub();

                endpoints.MapFallbackToController("AllPizzas", "Home");


                endpoints.MapRazorPages();
            });
           
           IdentitySeedData.CreateAdminAccount(app.ApplicationServices, Configuration,identCtx);
           SeedData.Seed(ctx);
        }
    }
}
