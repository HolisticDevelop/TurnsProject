using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Support.Models;
using Support.Data;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<SupportContext>(options =>
        options.UseSqlite(builder.Configuration.GetConnectionString("SupportContext") ?? throw new InvalidOperationException("Connection string 'SupportContext' not found.")));
}
else
{
    builder.Services.AddDbContext<SupportContext>(options =>
        options.UseSqlite(builder.Configuration.GetConnectionString("ProductionSupportContext") ?? throw new InvalidOperationException("Connection string 'SupportContext' not found.")));
}

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Turns}/{action=Index}/{id?}");

app.Run();

