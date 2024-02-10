using AracYanimdaApi.Data.Repository;
using AracYanimdaApi.Data.Service;
using AracYanimdaApi.Models.Odeme;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace AracYanimdaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase {



        private readonly PaymentService paymentService;
        public PaymentController()
        {
            paymentService = new PaymentService();
        }
        [HttpGet]
        public IActionResult GetPayments()
        {
            List<Payment> payment = paymentService.GetPayments();
            if (payment == null)
                return BadRequest();
            return Ok(payment);
        }
        [HttpPost("paymentmethod/create")]
        public IActionResult CreatePaymentMethod([FromBody] PaymentMethod payment_method)
        {

            if (paymentService.CreatePaymentMethod(payment_method))
            {
                return Ok();
            }
            return BadRequest();
        }
        [HttpGet("bilgi")]
        public IActionResult GetBilgi([FromQuery] int rezervation_id)
        {
            Bilgi bilgi = paymentService.GetBilgi(rezervation_id);
            if (bilgi == null)
                return BadRequest();
            return Ok(bilgi);
        }
        [HttpPost("tamamla")]
        public IActionResult OdemeTamamla(int rezervation_id)
        {
            if (paymentService.OdemeTamamla(rezervation_id))
                return Ok("Odeme tamamlandi");
            return BadRequest();
        }
        [HttpGet("paymentmethods")]
        public IActionResult GetPaymentMethods(int user_id)
        {
           List<PaymentMethod> paymentMethods=paymentService.GetPaymentMethods(user_id);
            if(paymentMethods==null)
                return BadRequest();
            return Ok(paymentMethods);
        }
        [HttpGet("fatura")]
        public IActionResult GetFatura(int payment_id)
        {
            Fatura fatura=paymentService.GetFatura(payment_id);
            if(fatura==null)
                return BadRequest();
            return Ok(fatura);
        }
    }
}
