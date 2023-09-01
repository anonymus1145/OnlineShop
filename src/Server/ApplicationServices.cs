using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Shop.Application.Data;
using Shop.Application.Repository;
using Shop.Application.Repository.IRepository;

internal static class ApplicationServices
{
    public static void AddAppServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllersWithViews();
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        {
            string? connectionString = builder.Configuration.GetConnectionString("Default");
            options.UseMySql(
                connectionString: connectionString,
                serverVersion: ServerVersion.AutoDetect(connectionString),
                mySqlOptionsAction: action => action.MigrationsAssembly("ShopProject")
            );
        });

        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    public static void ConfigureAppRequestPipeline(this WebApplication app)
    {
        if (!app.Environment.IsDevelopment())
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
            name: "default",
            pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");
    }
}