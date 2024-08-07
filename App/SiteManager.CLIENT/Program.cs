using Microsoft.AspNetCore.Authentication.Cookies;
using Serilog.Events;
using Serilog;
using SiteManager.CLIENT.Services.Abstractions;
using SiteManager.CLIENT.Services.Concrete;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();
builder.Services.AddControllersWithViews()
    .AddRazorRuntimeCompilation();
builder.Services.AddHttpClient();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.HttpOnly = true;
        options.ExpireTimeSpan = TimeSpan.FromDays(2);
        options.LoginPath = "/account/login";
        options.LogoutPath = "/account/logout";
        options.AccessDeniedPath = "/account/accessdenied";
        options.SlidingExpiration = true;
    });


builder.Services.AddHttpContextAccessor();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddHttpClient("CatalogAPI", client =>
{
    client.BaseAddress = new Uri("https://localhost:7180/api/");
});
builder.Services.AddHttpClient("AuthenticationAPI", client =>
{
    client.BaseAddress = new Uri("https://localhost:7142/");
}).AddHttpMessageHandler<TokenHandler>();

builder.Services.AddTransient<TokenHandler>();

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapAreaControllerRoute(
    name: "Authentication",
    areaName: "Authentication",
    pattern: "Authentication/{controller=Home}/{action=Index}/{id?}"
);

app.MapAreaControllerRoute(
    name: "User",
    areaName: "User",
    pattern: "User/{controller=Home}/{action=Index}/{id?}"
);

app.MapControllerRoute(
    name: "TokenTest",
    pattern: "tokentest",
    defaults: new { controller = "TokenTest", action = "Index" }
);

app.Run();
