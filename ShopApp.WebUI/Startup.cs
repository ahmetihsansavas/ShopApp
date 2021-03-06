using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShopApp.Business.Abstract;
using ShopApp.Business.Concrete;
using ShopApp.DataAccess.Abstract;
using ShopApp.DataAccess.Concrete.EfCore;
using ShopApp.DataAccess.Concrete.Memory;
using ShopApp.WebUI.Identity;
using ShopApp.WebUI.Middlewares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApp.WebUI
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IConfiguration Configuration { get; set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationIdentityDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection")));
            //services AddIdentity ile Identity i uygulamaya tanýtýyoruz, Uyg. 
            //Identity sýnýfýný belirtiyoruz ve hazýr rolleri kullanmak için Identity rol ile kullanýyoruz , datalarý nerede saklyacaðýmýzý belirtiyoruz
            //kullanýcýnýn mailini veya þifresini deðiþt. benzersiz bir token vermek için AddDefaultTokenProviders
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationIdentityDbContext>()
                .AddDefaultTokenProviders();
            //kullanýcý iþlemlerinin ayarlarý
            services.Configure<IdentityOptions>(options => 
            {
                //password ayarlarý...
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;

                options.Lockout.MaxFailedAccessAttempts = 6;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2);
                options.Lockout.AllowedForNewUsers = true;
                //options.User.AllowedUserNameCharacters = "ýüöð"

                options.User.RequireUniqueEmail = true;
                // kullanýcýnýn email i ni onaylý olmasý için...
                options.SignIn.RequireConfirmedEmail = true;
                options.SignIn.RequireConfirmedPhoneNumber = false;


            });
            //cookie ler yardýmýyla uyg ya giriþ yapýldýðý bilgisini tutuyoruz.
            services.ConfigureApplicationCookie(options => 
            {
                options.LoginPath = "/account/login";
                options.LogoutPath = "/account/logout";
                options.AccessDeniedPath = "/account/accessdenied";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60); // cookie nin saklanma süresi...
                //slidingexpirations deðiþkeni cookie nin belirli dk sonrasýnda çýkýþ yapýp yeniden giriþ yapmasýný saðlamak
                options.SlidingExpiration = true;
                //
                options.Cookie = new CookieBuilder
                {
                    //httponly dersek scriptler cookie lere ulaþamazlar. güvenlik açýsýndan önemli
                    HttpOnly = true,
                    Name ="ShopApp.Security.Cookie" , //cookie mizin ismi istediðimiz gibi deðer verebiliriz...
                    SameSite = SameSiteMode.Strict //cross site ataklarýný engellemek için kull. yani giris yapmak icin kull. 
                    //cookie kullanýcýnýn tarayýcýsýnda saklanýr

                };

            });

            //Dependecy Injection 
            services.AddScoped<IProductDal, EfCoreProductDal>();
            services.AddScoped<ICategoryDal, EfCoreCategoryDal>();
            services.AddScoped<ICartDal, EfCoreCartDal>();
            services.AddScoped<IOrderDal,EfCoreOrderDal>();

            services.AddScoped<IProductService, ProductManager>();
            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<ICartService, CartManager>();
            services.AddScoped<IOrderService,OrderManager>();
            
            
            services.AddMvc(option => option.EnableEndpointRouting = false);
            // IProduct , EfCoreProductDal
            // IProductDal , MySqlProductDal
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                SeedDatabase.Seed();
            }
            app.UseStaticFiles();
            // app.CustomStaticFiles();

            //app.UseMvcWithDefaultRoute();
            //Projemizde Identity Kullanacaðýmýz zaman  app.useAuthentication();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
              routes.MapRoute(
                 name: "adminCategories",
                 template: "admin/categories",
                 defaults: new { controller = "Admin", action = "CategoriesList" }
                 );
              routes.MapRoute(
                 name: "adminCategoriesEdit",
                 template: "admin/categories/{id?}",
                 defaults: new { controller = "Admin", action = "EditCategory" }
                 );

                routes.MapRoute(
                   name: "admin",
                   template: "admin/products",
                   defaults: new { controller = "Admin", action = "ProductList" }
                   );
                routes.MapRoute(
                  name: "adminProducts",
                  template: "admin/products/{id?}",
                  defaults: new { controller = "Admin", action = "EditProduct" }
                  );
                routes.MapRoute(
                 name: "cart",
                 template: "cart",
                 defaults: new { controller = "Cart", action = "Index" }
                 );
                routes.MapRoute(
                name: "checkout",
                template: "checkout",
                defaults: new { controller = "Cart", action = "Checkout" }
                 );
                routes.MapRoute(
                    name: "products",
                    template: "products/{category?}",
                    defaults: new {controller = "Shop" , action="List"}
                    );
                routes.MapRoute(
                    name: "orders",
                    template: "orders",
                    defaults: new { controller = "Cart", action = "GetOrders" }
                    );
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}"
                    );
                routes.MapRoute(
                    name: "shop",
                    template: "shop/{text?}",
                    defaults : new {controller = "Shop" ,action ="Index"}
                    );
            }
            );
            //Admin kullanýcýsý eklemek için  uyg baslarken 
            SeedIdentity.Seed(userManager, roleManager, Configuration).Wait();
        }
    }
}
