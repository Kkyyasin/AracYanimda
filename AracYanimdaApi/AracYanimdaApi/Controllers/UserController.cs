using Microsoft.AspNetCore.Mvc;
using AracYanimdaApi.Data.Service;
using AracYanimdaApi.Models.Kullanici;
using System.Runtime.CompilerServices;
using AracYanimdaApi.Models.Odeme;

namespace AracYanimdaApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController()
        {
            _userService = new UserService();
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            IEnumerable<User> users = _userService.GetAllUsers();
            if (users == null)
            {
                return NotFound(); 
            }
            return Ok(users); 
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
          
            var email = loginRequest.EPosta;
            var password = loginRequest.Sifre;
            var user = _userService.Authenticate(email, password);

            if (user == null)
            {
                return BadRequest(new { message = $"E-posta veya şifre yanlış." });
            }

            

            return Ok(user);
        }
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequest registerRequest) {

            if (_userService.Register(registerRequest)){
                return Ok("Kullanıcı başarıyla kaydedildi.");
            }
            else
            {
                return BadRequest("Kullanıcı kaydedilemedi. Lütfen tekrar deneyin.");
            }
        }
        [HttpGet("id")]
        public IActionResult GetUserById(int id)
        {
            User user = _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound(); // Veri bulunamazsa NotFound dönebilirsiniz.
            }
            return Ok(user); // Veriler bulunursa 200 OK ile döner.
        }
        [HttpPost("email")]
        public IActionResult EmailKontrol([FromBody] User eposta)
        {
            bool durum = _userService.EmailKontrol(eposta.EPosta);
            if (durum)
            {
                return Ok(durum);
            }
            else
            {
                return Ok(durum);
            }
        }
        [HttpGet("users/user")]
        public IActionResult GetByUserId([FromQuery] int Id)
        {
            User user=_userService.GetUserById(Id); 
            if (user != null) 
                return Ok(user);
            return BadRequest();
        }
        [HttpGet("users/license")]
        public IActionResult GetByLicenseId([FromQuery] int Id)
        {
            Licenses user = _userService.GetLicenseById(Id);
            if (user != null)
                return Ok(user);
            return BadRequest();
        }
        [HttpGet("users/rezervasyon")]
        public IActionResult GetByRezervasyonId([FromQuery] int Id)
        {
            List<RezervasyonDurun> user = _userService.GetRezervasyonById(Id);
            if (user != null)
                return Ok(user);
            return BadRequest();
        }
        [HttpGet("users/paymentmethods")]
        public IActionResult GetByPaymentMethodsById([FromQuery] int Id)
        {
            List<PaymentMethod> paymentMethods = _userService.GetPaymentMethodsById(Id);
            if (paymentMethods != null)
                return Ok(paymentMethods);
            return BadRequest();
        }
        [HttpGet("users/payments")]
        public IActionResult GetByPaymentsById([FromQuery] int Id)
        {
            List<Payment> payments = _userService.GetPaymentById(Id);
            if (payments != null)
                return Ok(payments);
            return BadRequest();
        }
        [HttpPost("users/email/update")]
        public IActionResult UpdateEmail([FromQuery] int Id, [FromQuery] string email)
        {
            if(_userService.UpdateEmail(Id, email))
                return Ok("Email Güncellendi...");
            return BadRequest("Email Güncellenmedi...");
        }
        [HttpPost("users/phone/update")]
        public IActionResult UpdateTelefon([FromQuery] int Id, [FromQuery] string tel)
        {
            if (_userService.UpdateTelefon(Id, tel))
                return Ok("Telefon Güncellendi...");
            return BadRequest("Telefon Güncellenmedi...");
        }
        [HttpPost("users/durum/update")]
        public IActionResult UpdateDurum([FromQuery] int Id, [FromQuery]  string durum)
        {
            if (_userService.UpdateDurum(Id, durum))
                return Ok("Durum Güncellendi...");
            return BadRequest("Durum Güncellenmedi...");
        }
        [HttpPost("users/user/delete")]
        public IActionResult KullaniciSil([FromBody] int Id)
        {
            if (_userService.KullaniciSil(Id))
                return Ok("Kullanıcı Silindi...");
            return BadRequest("Kullanıcı Silinemedi...");
        }
        [HttpGet("rezervasyonkontrol")]
        public IActionResult RezervasyonVarmi([FromQuery] int user_id)
        {
            var a = _userService.RezervasyonVarmi(user_id);
            if (a == 0)
               return BadRequest();
           return Ok(a);

        }
        [HttpPost("license/create")]
        public IActionResult EhliyetEkle([FromBody] Licenses licenses)
        {
            if (_userService.EhliyetEkle(licenses))
                return Ok();
             return BadRequest();
        }
        [HttpGet("license")]
        public IActionResult EhliyetBilgileri([FromQuery] int user_id)
        {
            var bilgiler=_userService.EhliyetBilgileri(user_id);
            if(bilgiler==null)
                return BadRequest();
            return Ok(bilgiler);

        }



    }
}


