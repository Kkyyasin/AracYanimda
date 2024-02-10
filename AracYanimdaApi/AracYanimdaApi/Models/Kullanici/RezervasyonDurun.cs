namespace AracYanimdaApi.Models.Kullanici
{
    public class RezervasyonDurun
    {
        public string AracIsmi { get; set; }
        public string Plaka { get; set; }
        public string BaslangicTarihi { get; set; }
        public string BitisTarihi { get; set; }
        public string RezervasyonDurum { get; set; }
        public Decimal Miktar {  get; set; }
    }
}