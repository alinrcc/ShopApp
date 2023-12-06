using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using ShopApp.Business.Abstract;
using ShopApp.Business.Concrete;
using ShopApp.DataAccess.Abstract;
using ShopApp.DataAccess.Concrete;
using ShopApp.DataAccess.Concrete.EfCore;
using ShopApp.Web.EmailServices;
using ShopApp.Web.Identity;

namespace ShopApp.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationIdentityDbContext>(options =>
                options.UseSqlServer(connectionString));
           


            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationIdentityDbContext>()
            .AddDefaultTokenProviders();
            builder.Services.Configure<IdentityOptions>(options =>
            {
                // password

                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;

                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.AllowedForNewUsers = true;

                //options.User.AllowedUserNameCharacters = "abcdefghijklmnoprstuvwxyzq"; opsiyon býrakýlabilir 
                options.User.RequireUniqueEmail = true;
                

                options.SignIn.RequireConfirmedEmail = true;
                options.SignIn.RequireConfirmedPhoneNumber = false;
            });

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.AccessDeniedPath = "/Account/Accessdenied";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                options.SlidingExpiration = true;
                options.Cookie = new CookieBuilder
                {
                    HttpOnly = true,
                    Name = ".ShopApp.Security.Cookie",
                    SameSite = SameSiteMode.Strict
                };
            });
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<ICartRepository, CartRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();

            builder.Services.AddScoped<IProductService, ProductManager>();
            builder.Services.AddScoped<ICategoryService, CategoryManager>();
            builder.Services.AddScoped<ICartService, CartManager>();
            builder.Services.AddScoped<IOrderService, OrderManager>();

            builder.Services.AddScoped<IEmailSender, SmtpEmailSender>(i =>new SmtpEmailSender(
            builder.Configuration["EmailSender:Host"],
            builder.Configuration.GetValue<int>("EmailSender:Port"),
            builder.Configuration.GetValue<bool>("EmailSender:EnableSSL"),
            builder.Configuration["EmailSender:Username"],
            builder.Configuration["EmailSender:Password"])
);
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
                SeedDatabase.Seed();
              
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
            name: "adminProducts",
            pattern: "admin/products",
            defaults: new { controller = "Admin", action = "ProductList" }
            );

            app.MapControllerRoute(
                name: "adminEditProduct",
                pattern: "admin/products/{id?}",
                defaults: new { controller = "Admin", action = "EditProduct" }
            );

            app.MapControllerRoute(
                name: "cart",
                pattern: "cart",
                defaults: new { controller = "Cart", action = "Index" }
            );

            app.MapControllerRoute(
                name: "orders",
                pattern: "orders",
                defaults: new { controller = "Cart", action = "GetOrders" }
            );

            app.MapControllerRoute(
                name: "checkout",
                pattern: "checkout",
                defaults: new { controller = "Cart", action = "Checkout" }
            );

            app.MapControllerRoute(
                name: "products",
                pattern: "products/{category?}",
                defaults: new { controller = "Shop", action = "List" }
            );

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}"
            );
            
            app.MapRazorPages();
            app.Run();
        }
    }
}