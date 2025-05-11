using Microsoft.EntityFrameworkCore;
using BiblioExe.Models;
using Microsoft.AspNetCore.Authentication.Cookies;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ContextoBD>(
    //opt => opt.UseSqlServer("Data Source=.;Initial Catalog=BiblioExe;Integrated Security=True")
    //opt => opt.UseSqlServer("Data Source=IVANNROMERO1010\\SQLEXPRESS;Initial Catalog=BiblioExe;Integrated Security=True")
    opt => opt.UseSqlServer("Server=tcp:biblioexe.database.windows.net,1433;Initial Catalog=BiblioExe;Persist Security Info=False;User ID=administrador;Password=@rturo2006;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;")
);
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(opt =>
    {
        opt.LoginPath = "/Usuarios/Login";
        opt.Cookie.Name = "AgendaV1";
        opt.ExpireTimeSpan = TimeSpan.FromMinutes(5);
        opt.LogoutPath = "/Usuarios/Logout";
        opt.AccessDeniedPath = "/Home/AccesoRestringido";
        opt.Cookie.HttpOnly = true;
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
