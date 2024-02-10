namespace AracYanimdaApi.Models.Rezervasyon
{
    public class RezervasyonAll
    {
        public int RezervationId { get; set; }
        public string KullaniciIsmı {  get; set; }
        public string AracIsmi { get; set; }
        public String BaslangicTarihi { get; set; }
        public String? BitisTarihi { get; set; }
        public string Durum { get; set; }
        public Decimal Miktar { get; set; }
        public string OdemeDurumu { get; set; }
        public string Tur { get; set; }
    }
}
