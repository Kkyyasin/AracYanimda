namespace AracYanimdaApi.Models.Kullanici
{
    public class Licenses
    {
            public int LicenseId { get; set; }
            public string EhliyetTipi { get; set; }
            public string EhliyetNo { get; set; }
            public string? SonGecerlilikTarihi { get; set; }
            public int UserId { get; set; }
    }
}

