namespace AracYanimdaApi.Models.Rezervasyon
{
    public class Rezervasyon
    {
        public int RezervationId { get; set; }
        public int VehicleId { get; set; }
        public int UserId { get; set; }
        public String BaslangicTarihi { get; set; }
        public String BitisTarihi { get; set; }
        public string Durum { get; set; }
        public string Tur { get; set; }
    }
}
