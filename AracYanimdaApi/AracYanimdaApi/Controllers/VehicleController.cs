using AracYanimdaApi.Data.Repository;
using AracYanimdaApi.Data.Service;
using AracYanimdaApi.Models.Arac;
using AracYanimdaApi.Models.Kullanici;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace AracYanimdaApi.Controllers

{
    [ApiController]
    [Route("api/[controller]")]
    public class VehicleController : ControllerBase
    {
        private readonly VehicleService _vehicleService;
        public VehicleController()
        {
            _vehicleService = new VehicleService();
        }
        [HttpGet]
        public IActionResult GetAllVehicles()
        {
            IEnumerable<Arac> vehicles = _vehicleService.GetAllVehicles();
            if(vehicles==null)
                return NotFound();
            return Ok(vehicles);
        }
        [HttpGet("vehicles")]
        public IActionResult GetVehicleAll() {
            IEnumerable<AracAll> vehicles = _vehicleService.GetAracAll();
            if (vehicles == null)
                return NotFound();
            return Ok(vehicles);
        }
        [HttpPost("create")]
        public IActionResult Create([FromBody] Arac arac)
        {
            if (_vehicleService.Create(arac))
            {
                return Ok("Arac başarıyla kaydedildi.");
            }
            else
            {
                return BadRequest("Arac kaydedilemedi. Lütfen tekrar deneyin.");
            }
        }
        [HttpGet("locations")]
        public IActionResult GetAllAracKonum()
        {
            IEnumerable<AracKonum> vehicles = _vehicleService.GetAracKonum();
            if (vehicles == null)
                return NotFound();
            return Ok(vehicles);
        }
        [HttpPost("konum/create")]
        public IActionResult CreateLocation([FromBody] Location location)
        {
            int a = _vehicleService.CreateLocation(location);
            if (a>0)
                return Ok(a);
            return BadRequest("Konum Eklenemedi...");
        }
        [HttpPost("fiyat/create")]
        public IActionResult CreateAracFiyat([FromBody] AracFiyat aracFiyat)
        {
            int a = _vehicleService.CreateAracFiyat(aracFiyat);
            if (a > 0)
                return Ok(a);
            return BadRequest("Konum Eklenemedi...");
        }
        
        [HttpGet("marka")]
        public IActionResult GetAracMarka()
        {
            List<string> list=_vehicleService.AracMarka();
            if (list == null)
                return NotFound();
            return Ok(list);
        }
        [HttpGet("model")]
        public IActionResult GetModel(string marka)
        {
            List<string> list = _vehicleService.AracModel(marka);
            if (list == null)
                return NotFound();
            return Ok(list);
        }
        [HttpGet("yakit")]
        public IActionResult GetYakit(string marka, string model)
        {
            List<string> list = _vehicleService.AracYakit(marka,model);
            if (list == null)
                return NotFound();
            return Ok(list);
        }
        [HttpGet("fiyat")]
        public IActionResult GetFiyat(string marka, string model,string yakit)
        {
           int a = _vehicleService.AracFiyat(marka, model,yakit);
            if (a == null)
                return NotFound();
            return Ok(a);
        }
        [HttpGet("vehicle/index")]
        public IActionResult GetByVehicleId(int id)
        {
            Arac arac=_vehicleService.GetByVehicleId(id);
            if(arac != null)
                return Ok(arac);
            return NotFound();
        }
        [HttpGet("vehicle/fiyat")]
        public IActionResult GetByFiyatId(int id)
        {
           AracFiyat aracFiyat=_vehicleService.GetByFiyatId(id);
            if(aracFiyat != null)
                return Ok(aracFiyat);
            return NotFound();
        }
        [HttpGet("vehicle/location")]
        public IActionResult GetByLocationId(int id)
        {
           Location location=_vehicleService.GetByLocationId(id);
            if (location != null)
                return Ok(location);
            return NotFound();
        }
        [HttpGet("vehicle/bakim")]
        public IActionResult GetByBakımId(int id)
        {
            List<Bakim> bakim = _vehicleService.GetByBakımId(id);
            if(bakim != null)
                return Ok(bakim);
            return NotFound();
        }
        [HttpPost("vehicle/fiyat/update")]
        public IActionResult FiyatGuncelle([FromBody] AracFiyat aracFiyat) {
            if(_vehicleService.FiyatGuncelle(aracFiyat.FiyatGunluk, aracFiyat.FiyatDakika, aracFiyat.FiyatId))
                return Ok("Fiyat Güncellendi...");
            return BadRequest("Fiyat Güncellenemedi...");
        }
        [HttpPost("vehicle/bakim/create")]
        public IActionResult BakimEkle([FromBody] Bakim bakim)
        {
            if (_vehicleService.BakimEkle(bakim.VehicleId, bakim.BakimAciklamasi, bakim.BakimTarihi))
                return Ok("Bakım Eklendi...");
            return BadRequest("Bakım Ekleme Başarısız...");
        }
        [HttpPost("vehicle/bakim/delete")]
        public IActionResult BakimSil([FromBody] int Id)
        {
            if (_vehicleService.BakimSil(Id))
                return Ok("Bakım Silindi...");
            return BadRequest("Bakım Silinemedi...");
        }
        [HttpPost("vehicle/vehicle/delete")]
        public IActionResult AracSil([FromBody] int Id)
        {
            if (_vehicleService.AracSil(Id))
                return Ok("Araç Silindi...");
            return BadRequest("Araç Silinemedi...");
        }

    }
}

