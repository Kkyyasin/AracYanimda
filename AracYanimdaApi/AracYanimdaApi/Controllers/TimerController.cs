using AracYanimdaApi.Data.Service;
using AracYanimdaApi.Models.Arac;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;

namespace AracYanimdaApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TimerController : ControllerBase
    {
        private readonly TimerService timerService;
        public TimerController()
        {
            timerService = new TimerService();
        }
        private Timer _timer,_timer2;

        [HttpGet("start")]
        public IActionResult StartTimer()
        {
            _timer = new Timer(Saatlik, null, TimeSpan.Zero, TimeSpan.FromSeconds(10));
            _timer2 = new Timer(Gunluk, null, TimeSpan.Zero, TimeSpan.FromSeconds(60));
            return Ok("Timer started!");
        }

        [HttpGet("stop")]
        public IActionResult StopTimer()
        {
            _timer?.Dispose();
            _timer2?.Dispose();
            return Ok("Timer stopped!");
        }

        private void Saatlik(object state)
        {
            Console.WriteLine(timerService.RunSaat());
        }
        private void Gunluk(object state)
        {
            Console.WriteLine(timerService.RunGunluk());
        }
    }
}
