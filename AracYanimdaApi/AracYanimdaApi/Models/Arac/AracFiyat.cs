namespace AracYanimdaApi.Models.Arac
{
    public class AracFiyat
    {
        public int FiyatId { get; set; }
        public decimal FiyatGunluk { get; set; }
        public decimal FiyatDakika { get; set; }
        public int KmSiniriUcretsiz { get; set; }
        public decimal IlaveKmUcreti { get; set; }
    }
}
