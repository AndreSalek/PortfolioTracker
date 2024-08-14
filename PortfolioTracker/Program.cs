using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PortfolioTracker.Common.Enums;
using PortfolioTracker.Common.Helpers;
using PortfolioTracker.Common.Interfaces;
using PortfolioTracker.Common.Logging;
using PortfolioTracker.Data;
using PortfolioTracker.ViewModels;

namespace PortfolioTracker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetSection("PortfolioDatabase").Value ?? throw new InvalidOperationException("Connection string 'PortfolioDatabase' not found.");
            Dictionary<Enum, string[]> platformKeyMap = new Dictionary<Enum, string[]>()
            {
                { Platform.Coinbase, new []{ nameof(PlatformKeyData.ApiSecret), nameof(PlatformKeyData.Passphrase) } },
                { Platform.Kraken, new []{ nameof(PlatformKeyData.ApiSecret)} }
            };
           
            // Database context and Microsoft Identity options and services
            builder.Services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddIdentity<User, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
               .AddEntityFrameworkStores<ApplicationDbContext>()
               .AddDefaultUI()
               .AddDefaultTokenProviders();
            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Lockout.MaxFailedAccessAttempts = 3;
                options.SignIn.RequireConfirmedAccount = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
                options.SignIn.RequireConfirmedEmail = false;

                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                options.Password.RequiredLength = 10;
            });

            // Logging providers
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();
            builder.Logging.AddDebug();
            builder.Logging.AddDbLogger(options =>
            {
                // Bind database options from appsettings to DbLoggerOptions properties
                builder.Configuration.GetSection("Logging").GetSection("Database").GetSection("Options").Bind(options);
            });
            // Data models and viewmodels mapping service
            builder.Services.AddAutoMapper(mapperConfig =>
            {
                mapperConfig.CreateMap<PlatformKeyData, PlatformKeyDataViewModel>().ReverseMap();
            });

            // Other services
            builder.Services.AddSingleton<Dictionary<Enum, string[]>>(platformKeyMap);
            builder.Services.AddHttpClient("Kraken", httpClient =>
            {
                httpClient.BaseAddress = new Uri("https://api.kraken.com/");

                httpClient.DefaultRequestHeaders.Add("", "");
            });
            
            builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
            var app = builder.Build();
            
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();

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
