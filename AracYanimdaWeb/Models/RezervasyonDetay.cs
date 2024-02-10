namespace AracYanimdaWeb.Models;
public class RezervasyonDetay
{
    public string AracIsmi { get; set; }
    public string Plaka { get; set; }
    public DateTime BaslangicTarihi { get; set; }
    public DateTime? BitisTarihi { get; set; }
    public string RezervasyonDurum { get; set; }
}