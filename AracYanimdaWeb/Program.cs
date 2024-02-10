using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication("CookieAuth")
    .AddCookie("CookieAuth", options =>
    {
        options.Cookie.Name = "UserLoginCookie";
        options.LoginPath = "/Admin/Login"; // Giriş yapma sayfasına yönlendirme
    });
    
builder.Services.AddAuthorization();
var app = builder.Build();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "BakimSil",
        pattern: "Vehicle/BakimSil/{bakimId}/{vehicleId}",
        defaults: new { controller = "Vehicle", action = "BakimSil" }
    );

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Admin}/{action=Login}/{id?}");
});

// Diğer middleware'ler
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseExceptionHandler("/Home/Error");
app.UseHsts();

app.Run();
