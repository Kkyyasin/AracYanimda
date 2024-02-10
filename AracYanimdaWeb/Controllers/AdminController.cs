using AracYanimdaWeb.Models.Admin;
using AracYanimdaWeb.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

public class AdminController : Controller
{
    private readonly AdminApiService _apiService;

    public AdminController()
    {
        _apiService = new AdminApiService();
    }
    public IActionResult Login()
    {
        // Giriş sayfasını göster
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string loginEmail, string loginPassword)
    {
        // Kullanıcı adı ve şifreyi doğrula. Bu örnek için basit bir kontrol yapıyoruz.
        // Gerçek bir uygulamada veritabanı veya başka bir servisle doğrulama yapılmalı.
        Admin admin = new Admin
        {
            AdminId = 0,
            Username = "",
            Email = loginEmail,
            Password = loginPassword
        };
        Admin kontrol = await _apiService.Login(admin);
        Console.WriteLine(kontrol.Username);
        if (kontrol != null)
        {

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, kontrol.Username),
                new Claim(ClaimTypes.Email, kontrol.Email),
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true, // "Beni Hatırla" için
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(4), // 4 saat sonra sona erecek bir oturum için
                RedirectUri = "/Home/Index"
            };


            // Kullanıcıyı cookie ile yetkilendir
            await HttpContext.SignInAsync(
     "CookieAuth", // Şema adını burada güncelleyin
     new ClaimsPrincipal(claimsIdentity),
     authProperties);


            // Başarılı girişten sonra yönlendirme
            return RedirectToAction("Index", "Home");
        }

        // Hatalı giriş durumunda tekrar login sayfasını göster
        return View();
    }

    public IActionResult Register()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Register(string registerUsername, string registerEmail, string registerPassword)
    {
        Admin admin = new Admin
        {
            Username = registerUsername,
            Email = registerEmail,
            Password = registerPassword
        };
        Console.WriteLine(await _apiService.Register(admin));
        return RedirectToAction("Login");
    }
    public async Task<IActionResult> Logout()
    {
        // Kullanıcıyı çıkış yaptır
        await HttpContext.SignOutAsync("CookieAuth");  // "CookieAuth" şema adınız ne ise onu kullanın

        // Çıkış yaptıktan sonra kullanıcıyı ana sayfaya veya giriş sayfasına yönlendir
        return RedirectToAction("Login", "Admin");
    }
}
