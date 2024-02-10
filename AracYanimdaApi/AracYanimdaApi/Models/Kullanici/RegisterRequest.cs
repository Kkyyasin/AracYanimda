namespace AracYanimdaApi.Models.Kullanici
{
    public class RegisterRequest
    {
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string EPosta { get; set; }
        public string Sifre { get; set; }
        public string Telefon { get; set; }
        public bool SozlesmeKabul { get; set; }
        

    }
}
