namespace AracYanimdaApi.Models.Arac
{
    public class AracAll
    {
        public int VehicleId { get; set; }
        public string Marka { get; set; }
        public string Model { get; set; }
        public int Yil { get; set; }
        public string Plaka { get; set; }
        public string YakitTipi { get; set; }
        public string YakitDurumu { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public decimal FiyatGunluk { get; set; }
        public decimal FiyatDakika { get; set; }
        public int KmSiniriUcretsiz { get; set; }
        public decimal IlaveKmUcreti { get; set; }
        public string Durum {  get; set; }
    }
}
