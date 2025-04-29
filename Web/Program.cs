using Lefish.Common.Settings;
using Lefish.Persistence;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.DataProtection;
using System.Runtime.Loader;

namespace Lefish.Web;

public class Program
{
    public static void Main(string[] args)
    {
        // App
        var builder = WebApplication.CreateBuilder(args);

        // Configuration
        builder.Configuration
            .AddJsonFile("Config/Database.json", optional: false, reloadOnChange: true);

        var connectionStringFactory = new ConnectionStringFactory(
            builder.Environment,
            builder.Configuration);

        var connectionString = connectionStringFactory.GetConnectionString();

        builder.Services.Configure<UserAccountConfiguration>(
            builder.Configuration.GetSection("User:Account"));

        // Dynamic dependency injection
        var files = Directory.GetFiles(
            AppDomain.CurrentDomain.BaseDirectory,
            "Lefish*.dll");

        var assemblies = files
            .Select(p => AssemblyLoadContext.Default.LoadFromAssemblyPath(p));

        builder.Services
            .Scan(p => p.FromAssemblies(assemblies)
            .AddClasses()
            .AsMatchingInterface());

        // Services
        var contentRootPath = $"{builder.Environment.ContentRootPath}\\SessionKeys";

        builder.Services
            .AddDataProtection()
            .PersistKeysToFileSystem(new DirectoryInfo(contentRootPath));

        builder.Services
            .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromDays(1);
                options.SlidingExpiration = true;
                options.AccessDeniedPath = "/help/notpermitted";
                options.LoginPath = "/Session/SignIn";
                options.LogoutPath = "/Session/SignOut";
                options.EventsType = typeof(CookieValidator);
            });

        builder.Services.AddScoped<CookieValidator>();
        builder.Services.AddScoped<IUserToken, UserToken>();

        builder.Services.AddHttpContextAccessor();

        builder.Services.AddAuthorization(options =>
        {
            options.FallbackPolicy = options.DefaultPolicy;
        });

        builder.Services.AddDbContext<DatabaseService>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        // API controllers
        builder.Services.AddControllers();

        // Razor
        builder.Services.AddRazorPages();

        // App
        var app = builder.Build();

        // Middleware
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/error");

            // The default HSTS value is 30 days.
            // You may want to change this for production scenarios.
            // https://aka.ms/aspnetcore-hsts.

            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseCookiePolicy(
            new CookiePolicyOptions
            {
                HttpOnly = HttpOnlyPolicy.Always,
                MinimumSameSitePolicy = SameSiteMode.Lax,
                Secure = CookieSecurePolicy.Always,
            });

        app.UseRouting();
        
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseAntiforgery();

        app.MapRazorPages();

        //app.UseEndpoints(endpoints => endpoints.MapControllers());

        //app.MapFallbackToPage("/Payload");

        app.Use(async (context, next) =>
        {
            context.Response.Headers.XFrameOptions = "DENY";
            await next();
        });

        // Run
        app.Run();
    }
}
