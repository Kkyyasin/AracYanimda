using AracYanimdaWeb.Models.Admin;
using AracYanimdaWeb.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using AracYanimdaWeb.Models;
[Authorize]
public class PaymentController : Controller
{
    private readonly RezervasyonApiService _apiService;

    public PaymentController()
    {
        _apiService = new RezervasyonApiService();
    }
    public async Task<IActionResult> List()
    {
        List<Payment> payments = await _apiService.GetPayments();

        return View(payments);
    }
    public async Task<IActionResult> Index(int id)
    {
        Fatura fatura = await _apiService.GetFatura(id);
        return View(fatura);
    }

}
