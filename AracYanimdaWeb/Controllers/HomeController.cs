using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AracYanimdaWeb.Service;
using AracYanimdaWeb.Models;
namespace AracYanimdaWeb.Controllers;
using Microsoft.AspNetCore.Authorization;
[Authorize]
public class HomeController : Controller
{

    private readonly RezervasyonApiService _apiService;

    public HomeController()
    {
        _apiService = new RezervasyonApiService();
    }
    [HttpGet]
    public async Task<IActionResult> Index()
    {

        IEnumerable<Rezervasyon> list = await _apiService.GetRezervasyonAll();

        return View(list);
    }
    [HttpPost]
    public async Task<IActionResult> Index(string metinArama, string durumSecimi, decimal? minFiyat, decimal? maxFiyat)
    {


        IEnumerable<Rezervasyon> list = await _apiService.GetRezervasyonAll();



        if (!string.IsNullOrEmpty(metinArama))
        {
            list = list.Where(r => r.AracIsmi.Contains(metinArama) || r.KullaniciIsmı.Contains(metinArama));
        }

        if (!string.IsNullOrEmpty(durumSecimi))
        {
            list = list.Where(r => r.Durum == durumSecimi);
        }
        if (minFiyat.HasValue && maxFiyat.HasValue)
        {
            list = list.Where(r => r.Miktar >= minFiyat && r.Miktar <= maxFiyat);
        }

        return View(list);

    }
    public async Task<IActionResult> Rezervasyon(int id)
    {
        Rezervasyon rezervasyon = await _apiService.GetByRezervasyonId(id);
        UserListViewModel user = await _apiService.GetByRezervasyonUserId(id);
        VehicleListViewModel vehicleListViewModel = await _apiService.GetByRezervasyonVehicleId(id);
        Payment payment = await _apiService.GetByRezervasyonPaymentId(id);
        ViewBag.user = user;
        ViewBag.payment = payment;
        ViewBag.vehicle = vehicleListViewModel;
        return View(rezervasyon);
    }
    public async Task<IActionResult> Login()
    {
        return View();
    }
    public async Task<IActionResult> Register()
    {
        return View();
    }

}
