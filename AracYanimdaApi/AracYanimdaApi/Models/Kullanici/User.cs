namespace AracYanimdaApi.Models.Kullanici
{
    public class User
    {
        public int? UserId { get; set; }
        public string? Ad { get; set; }
        public string? Soyad { get; set; }
        public string? EPosta { get; set; }
        public string? Sifre { get; set; }
        public string? Telefon { get; set; }
        public bool? SozlesmeKabul { get; set; }
        public DateTime? KayitTarihi  { get; set; }
        public string? Durum { get; set; }
    }
}
