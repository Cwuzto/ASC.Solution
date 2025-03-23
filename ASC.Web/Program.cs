using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ASC.Web.Data;
using ASC.Web.Configuration;
using ASC.Web.Services;
using DataAccess.Interfaces;
using DataAccess;
using Microsoft.Extensions.Options;
using ASC.Web.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddCongfig(builder.Configuration)
    .AddMydependencyGroup();

//// Add services to the container.
//var connectionString = 
//    builder.Configuration.GetConnectionString("DefaultConnection") ?? 
//    throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseSqlServer(connectionString));

//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ASCWebContext>();

//builder.Services.AddIdentity<IdentityUser, IdentityRole>((options) => 
//{
//    options.User.RequireUniqueEmail = true;
//}).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

//builder.Services.AddScoped<DbContext, ApplicationDbContext>();

//builder.Services.AddDatabaseDeveloperPageExceptionFilter();

////builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
////    .AddEntityFrameworkStores<ApplicationDbContext>();

//builder.Services.AddOptions();
//builder.Services.Configure<ApplicationSettings>(builder.Configuration.GetSection("AppSettings"));
//builder.Services.AddDistributedMemoryCache();
//builder.Services.AddSession();

//builder.Services.AddControllersWithViews();

////Add application services
//builder.Services.AddTransient<IEmailSender, AuthMessageSender>();
//builder.Services.AddTransient<ISmsSender, AuthMessageSender>();
//builder.Services.AddSingleton<IIdentitySeed, IdentitySeed>();
//builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "areaRoute",
    pattern: "{area:exists}/{Controller=Home}/{action=Index}");
app.MapControllerRoute(
    name: "default",
    pattern: "{Controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
app.UseSession();

using (var scope = app.Services.CreateScope())
{
    var storageSeed = scope.ServiceProvider.GetRequiredService<IIdentitySeed>();
    await storageSeed.Seed(
        scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>(),
        scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>(),
        scope.ServiceProvider.GetRequiredService<IOptions<ApplicationSettings>>());
}
//CreateNavigationCache
using (var scope = app.Services.CreateScope())
{
    var navigationCacheOperations = scope.ServiceProvider.GetRequiredService<INavigationCacheOperations>();
    await navigationCacheOperations.CreateNavigationCacheAsync();
}

app.Run();
