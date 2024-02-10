namespace AracYanimdaWeb.Models;
public class Licenses
{
    public int LicenseId { get; set; }
    public string EhliyetTipi { get; set; }
    public string EhliyetNo { get; set; }
    public DateTime? SonGecerlilikTarihi { get; set; }
    public int UserId { get; set; }
}