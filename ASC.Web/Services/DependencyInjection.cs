using ASC.Web.Configuration;
using ASC.Web.Data;
using DataAccess;
using DataAccess.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ASC.Web.Services
{
    public static class DependencyInjection
    {
        //Configure Services
        public static IServiceCollection AddConfig(this IServiceCollection services, IConfiguration config)        {
            //Add AddDbContext with connection string to mirage database
            var connectionstring = config.GetConnectionString("DefaultConnection")??
                throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionstring));
            //Add options and get data from appsettings.json with "Appsettings"
            services.AddOptions();//IOption
            services.Configure<ApplicationSettings>(config.GetSection("Appsettings"));

            //Using a Gmail Authentication Provider for Customer Authentication
            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    IConfigurationSection googleAuthNSection = config.GetSection("Authentication:Google");
                    options.ClientId = config["Google:Identity:ClientID"];
                    options.ClientSecret = config["Google:Identity:ClientSecret"];
                });

            return services;
        }
        //Add service
        public static IServiceCollection AddMydependencyGroup(this IServiceCollection services)
        {
            //Add ApplicationDbContext
            services.AddScoped<DbContext, ApplicationDbContext>();

            //Add IdentityUser IdentityUser
            services.AddIdentity<IdentityUser, IdentityRole>((options) =>
            {
                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders().AddDefaultUI();

            //Add Services
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
            services.AddSingleton<IIdentitySeed, IdentitySeed>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //Add Cache, Session
            services.AddSession();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddDistributedMemoryCache();//
            services.AddSingleton<INavigationCacheOperations, NavigationCacheOperations>();

            //Add RazorPages , MVC
            services.AddRazorPages();
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddControllersWithViews();
            return services;
        }
    }
}
