namespace AracYanimdaApi.Models.Odeme
{
    public class PaymentMethod
    {
        public int PaymentMethodId { get; set; }
        public string AdSoyad { get; set; }
        public string KartNumarasi { get; set; }
        public string Cvv { get; set; }
        public string SonKullanma { get; set; }
        public int UserId { get; set; }
    }
}
