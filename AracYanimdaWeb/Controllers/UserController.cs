using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AracYanimdaWeb.Models;
using AracYanimdaWeb.Service;
namespace AracYanimdaWeb.Controllers;
using Microsoft.AspNetCore.Authorization;
[Authorize]
public class UserController : Controller
{

    private readonly UserApiService _apiService;

    public UserController()
    {
        _apiService = new UserApiService();
    }
    public async Task<IActionResult> List(string searchTerm)
    {
        IEnumerable<UserListViewModel> userList = await _apiService.GetUserListJson();
        if (!string.IsNullOrEmpty(searchTerm))
        {
            userList = userList.Where(v =>
                v.Ad.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                v.Soyad.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                v.EPosta.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                (v.Ad + ' ' + v.Soyad).Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
            );
        }
        return View(userList);
    }
    public async Task<IActionResult> Index(int id)
    {
        UserListViewModel user = await _apiService.GetByUserId(id);
        ViewBag.License = await _apiService.GetByLicenseId(id);
        Console.WriteLine("sd");
        List<RezervasyonDetay> rezervasyonDetays = await _apiService.GetByRezervasyonId(id);
        List<PaymentMethod> paymentMethods = await _apiService.GetByPaymentMethodsId(id);
        List<Payment> payments = await _apiService.GetByPaymentsId(id);
        ViewBag.paymentMethods = paymentMethods;
        ViewBag.Rezervasyon = rezervasyonDetays;
        ViewBag.payments = payments;

        return View(user);
    }

    public async Task<IActionResult> UpdateEmail(int userid, string emailInput)
    {
        Console.WriteLine(await _apiService.UpdateEmail(userid, emailInput));
        return RedirectToAction("Index", new { id = userid });
    }
    public async Task<IActionResult> UpdatePhone(int userid, string telefonInput)
    {
        Console.WriteLine(await _apiService.UpdateTelefon(userid, telefonInput));
        return RedirectToAction("Index", new { id = userid });
    }
    public async Task<IActionResult> UpdateDurum(int userid, string durumSelect)
    {
        Console.WriteLine(durumSelect);
        Console.WriteLine(await _apiService.UpdateDurum(userid, durumSelect));
        return RedirectToAction("Index", new { id = userid });
    }
    public async Task<ActionResult> Delete(int id)
    {
        Console.WriteLine(await _apiService.KullaniciSil(id));
        return RedirectToAction("List");
    }


}
