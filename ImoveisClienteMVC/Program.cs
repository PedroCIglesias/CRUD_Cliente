using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ImoveisClienteMVC.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ImoveisClienteMVCContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ImoveisClienteMVCContext") ?? throw new InvalidOperationException("Connection string 'ImoveisClienteMVCContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Cliente}/{action=Index}/{id?}");

app.Run();
