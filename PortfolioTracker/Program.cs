using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PortfolioTracker.Common.Enums;
using PortfolioTracker.Data;
using PortfolioTracker.Models;
using PortfolioTracker.Services;

namespace PortfolioTracker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            Dictionary<Enum, string[]> platformKeyMap = new Dictionary<Enum, string[]>()
            {
                { Platform.Coinbase, new []{ nameof(PlatformKeyData.ApiSecret), nameof(PlatformKeyData.Passphrase) } },
                { Platform.Kraken, new []{ nameof(PlatformKeyData.ApiSecret)} }
            };
            var connectionString = builder.Configuration["PortfolioDatabase"] ?? throw new InvalidOperationException("Connection string 'PortfolioDatabase' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddIdentity<User, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
               .AddEntityFrameworkStores<ApplicationDbContext>()
               .AddDefaultUI()
               .AddDefaultTokenProviders();

            // Add services to the container.
            builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
            builder.Services.AddSingleton<Dictionary<Enum, string[]>>(platformKeyMap);
            builder.Services.AddHttpClient("Kraken", httpClient =>
            {
                httpClient.BaseAddress = new Uri("https://api.kraken.com/");

                httpClient.DefaultRequestHeaders.Add("", "");
            });
            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Lockout.MaxFailedAccessAttempts = 3;
                options.SignIn.RequireConfirmedAccount = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
                options.SignIn.RequireConfirmedEmail = false;

				options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
				options.Password.RequiredLength = 10;
            });

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();

            }
            // Configure the HTTP request pipeline.
            else 
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
