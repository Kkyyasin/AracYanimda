using AracYanimdaApi.Data.Repository;
using AracYanimdaApi.Data.Service;
using AracYanimdaApi.Models.Arac;
using AracYanimdaApi.Models.Kullanici;
using AracYanimdaApi.Models.Odeme;
using AracYanimdaApi.Models.Rezervasyon;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace AracYanimdaApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class RezervasyonController :ControllerBase
    {
        private readonly RezervasyonService _rezervasyonService;
        public RezervasyonController()
        {
            _rezervasyonService = new RezervasyonService();
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            List<RezervasyonAll> list = _rezervasyonService.GetRezervasyonAll();
            if(list!=null)
                return Ok(list);
            return BadRequest();
        }
        [HttpPost("create")]
        public IActionResult CreateRezervasyon([FromBody] Rezervasyon rezervasyon, [FromQuery] int paymentMethodId)
        { var id = _rezervasyonService.CreateRezervasyon(rezervasyon,paymentMethodId);
            if (id!=-1)
                return Ok(id);
            return BadRequest(-1);
        }
        [HttpGet("rezervasyon")]
        public IActionResult GetByRezervasyonId(int rezervasyon_id)
        {
            Rezervasyon rezervasyon = _rezervasyonService.GetByRezervasyonId(rezervasyon_id);
            if (rezervasyon != null)
                return Ok(rezervasyon);
            return BadRequest();
        }
        [HttpGet("rezervasyon/user")]
        public IActionResult GetByUserRezervasyonId(int rezervasyon_id)
        {
            User user = _rezervasyonService.GetByUserRezervasyonId(rezervasyon_id);
            if (user != null)
                return Ok(user);
            return BadRequest();
        }
        [HttpGet("rezervasyon/vehicle")]
        public IActionResult GetByVehiclesId(int rezervasyon_id)
        {
            Arac arac = _rezervasyonService.GetByVehiclesId(rezervasyon_id);
            if (arac != null)
                return Ok(arac);
            return BadRequest();
        }
        [HttpGet("rezervasyon/payment")]
        public IActionResult GetByPaymentsRezervasyonId(int rezervasyon_id)
        {
            Payment payment = _rezervasyonService.GetByPaymentsRezervasyonId(rezervasyon_id);
            if (payment != null)
                return Ok(payment);
            return BadRequest();
        }
    }
}
