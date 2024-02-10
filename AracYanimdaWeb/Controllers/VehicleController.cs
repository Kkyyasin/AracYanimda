using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AracYanimdaWeb.Models;
using AracYanimdaWeb.Service;
using AracYanimdaWeb.Generator;
namespace AracYanimdaWeb.Controllers;
using Microsoft.AspNetCore.Authorization;
[Authorize]
public class VehicleController : Controller
{

    private readonly ApiService _apiService;

    public VehicleController()
    {
        _apiService = new ApiService();
    }
    public async Task<IActionResult> List(string searchTerm)
    {
        IEnumerable<VehicleListViewModel> vehiclelist = await _apiService.GetVehicleListJson();

        if (!string.IsNullOrEmpty(searchTerm))
        {
            vehiclelist = vehiclelist.Where(v =>
                v.Marka.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                v.Model.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                v.Plaka.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
            );
        }

        return View(vehiclelist);
    }
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(VehicleListViewModel data)
    {
        IstanbulRandomLocationGenerator ıstanbulRandomLocationGenerator = new IstanbulRandomLocationGenerator();
        (double latitude, double longitude) = ıstanbulRandomLocationGenerator.GenerateRandomLocation();
        LocationListViewModel locationListViewModel = new LocationListViewModel()
        {
            Latitude = Convert.ToDecimal(latitude),
            Longitude = Convert.ToDecimal(longitude),
            Address = "Türkiye, Istanbul",
            City = "Istanbul"
        };
        int location = await _apiService.CreateLocation(locationListViewModel);

        int b = await _apiService.GetFiyat(data.Marka, data.Model, data.YakitTipi);
        AracFiyatRandomizer a = new AracFiyatRandomizer();
        AracFiyat aracFiyat = a.GenerateRandomAracFiyat(b);
        Console.WriteLine(aracFiyat);
        int fiyat_id = await _apiService.CreateAracFiyat(aracFiyat);
        Random rand = new Random();
        TurkishPlakaGenerator plakaGenerator = new TurkishPlakaGenerator();
        int yil = rand.Next(2010, 2023);
        string plaka = plakaGenerator.GenerateRandomPlate();
        VehicleListViewModel vehicle = new VehicleListViewModel()
        {
            Marka = data.Marka,
            Model = data.Model,
            Yil = yil,
            Plaka = plaka,
            YakitTipi = data.YakitTipi,
            YakitDurumu = "Dolu",
            LocationId = location,
            FiyatId = fiyat_id
        };

        _apiService.CreateVehicle(vehicle);
        return RedirectToAction("List");
    }
    public async Task<IActionResult> Index(int id)
    {
        VehicleListViewModel vehicleListViewModel = await _apiService.GetByVehicle(id);
        Console.WriteLine(vehicleListViewModel.LocationId);
        LocationListViewModel locationListViewModel = await _apiService.GetByLocation(vehicleListViewModel.LocationId);
        ViewBag.Location = locationListViewModel;
        Console.WriteLine(locationListViewModel.Latitude);
        AracFiyat aracFiyat = await _apiService.GetByFiyat(vehicleListViewModel.FiyatId);
        ViewBag.FiyatBilgileri = aracFiyat;
        List<Bakim> bakim = await _apiService.GetByBakim(id);
        ViewBag.Bakimlar = bakim;

        return View(vehicleListViewModel);
    }
    [HttpPost]
    public async Task<IActionResult> FiyatGuncelle(decimal gunlukFiyat, decimal dakikaFiyat, int Id)
    {
        Console.WriteLine(await _apiService.FiyatGuncelle(gunlukFiyat, dakikaFiyat, Id));
        return RedirectToAction("Index", new { id = Id });
    }
    [HttpPost]
    public async Task<ActionResult> BakimEkle(int vehicleId, string bakimAciklamasi, DateTime bakimTarihi)
    {
        Bakim bakim = new Bakim()
        {
            BakimAciklamasi = bakimAciklamasi,
            VehicleId = vehicleId,
            BakimTarihi = bakimTarihi
        };
        Console.WriteLine(await _apiService.BakimEkle(bakim));
        return RedirectToAction("Index", new { id = vehicleId });
    }

    public async Task<ActionResult> BakimSil(int bakimId, int vehicleid)
    {
        Console.WriteLine(await _apiService.BakimSil(bakimId));
        return RedirectToAction("Index", new { id = vehicleid });
    }
    public async Task<ActionResult> Delete(int id)
    {
        Console.WriteLine(await _apiService.AracSil(id));
        return RedirectToAction("List");
    }

}
