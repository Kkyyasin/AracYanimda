namespace AracYanimdaApi.Models.Odeme
{
    public class Fatura
    {
        public int PaymentId { get; set; }
        //Kullancı Bilgileri
        public string KullaniciIsmı { get; set; }
        //Arac Bilgileri
        public string AracIsmi { get; set; }
        //Rezervasyon
        public String BaslangicTarihi { get; set; }
        public String? BitisTarihi { get; set; }
        public Decimal Miktar { get; set; }
        
        public string Tur { get; set; } //Tur olarak günlük mü saatlik mi
        //Kart Bilgileri
        public string AdSoyad { get; set; }
        public string KartNumarasi { get; set; }
    }
}
