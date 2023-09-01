using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

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
                mySqlOptionsAction: action => action.MigrationsAssembly(typeof(Program).Assembly.FullName)
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

    public static async Task SetupAppDatabaseAsync(this WebApplication app)
    {
        using AsyncServiceScope scope = app.Services.CreateAsyncScope();
        ApplicationDbContext dbContext =
            scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        try
        {
            await dbContext.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            ILogger<Program> logger = app.Services.GetRequiredService<ILogger<Program>>();
            string message = "An error occurred while migrating the database. Fix errors and try again.";
            logger.LogError(ex, message);
            throw;
        }
    }
}