using AJAX.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//這裡的DemoConnection要跟「appsettings.json」中ConnectionStrings的要連接的資料庫的名稱相同(自己取名)
builder.Services.AddDbContext<DemoContext>(
    options=>options.UseSqlServer(
        builder.Configuration.GetConnectionString("DemoConnection"))
    );
var app = builder.Build();

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
    pattern: "{controller=Homework}/{action=Register}/{id?}");

app.Run();
