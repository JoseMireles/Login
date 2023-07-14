//1.- PRIMERO =================================================================
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using WebApplication1;

var builder = WebApplication.CreateBuilder(args);

//Conexion SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(opciones =>
opciones.UseSqlServer(builder.Configuration.GetConnectionString("SOLVEX")));


builder.Services.AddControllersWithViews();

//2.- SEGUNDO - CONFIGURAR NUESTRA COOKIE DE AUTENTICACION =================
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option => {
        option.LoginPath = "/Acceso/Index";
        option.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        option.AccessDeniedPath = "/Home/Privacy";
    });

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

//3.- TERCERO ==================================================================
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Acceso}/{action=Index}/{id?}");

app.Run();
