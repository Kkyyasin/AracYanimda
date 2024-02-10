using AracYanimdaApi.Data.Repository;
using AracYanimdaApi.Data.Service;
using AracYanimdaApi.Models.Admin;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace AracYanimdaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController: ControllerBase
    {
       
            private readonly AdminService _adminService;
            public AdminController()
            {
            _adminService = new AdminService();
            }
        [HttpPost("login")]
        public IActionResult Login([FromBody] Admin admin)
        {
            var email=admin.Email;
            var password=admin.Password;
            var a= _adminService.Login(email, password);
            if(a==null)
            {
                return BadRequest(null);
            }
            return Ok(a);
        }
        [HttpPost("register")]
        public IActionResult Register([FromBody] Admin admin)
        {
            if (_adminService.Register(admin))
                return Ok("Admin başarıyla kaydedildi...");
            else
                return BadRequest("Kayıt başarısız");
        }
       
    }
}
