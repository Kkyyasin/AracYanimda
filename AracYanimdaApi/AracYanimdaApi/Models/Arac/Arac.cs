namespace AracYanimdaApi.Models.Arac
{
    public class Arac
    {
        public int VehicleId { get; set; }
        public string Marka { get; set; }
        public string Model { get; set; }
        public int Yil { get; set; }
        public string Plaka { get; set; }
        public string YakitTipi { get; set; }
        public string YakitDurumu { get; set; }
        public int LocationId { get; set; }
        public int FiyatId { get; set; }

    }
}
