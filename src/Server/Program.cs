using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Pomelo.EntityFrameworkCore.MySql;

using Shop.Application.Data;
using Shop.Application.Repository;
using Shop.Application.Repository.IRepository;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
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

app.Run();
