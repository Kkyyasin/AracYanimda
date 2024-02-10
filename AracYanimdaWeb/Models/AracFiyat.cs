using System.ComponentModel.DataAnnotations;
namespace AracYanimdaWeb.Models;
public class AracFiyat
{
    [Required(ErrorMessage = "Gunluk fiyat gereklidir.")]
    [Range(0, 1000, ErrorMessage = "Gunluk fiyat max 1000 olmalıdır.")]
    public decimal FiyatGunluk { get; set; }

    [Required(ErrorMessage = "Dakika fiyatı gereklidir.")]
    [Range(0, 100, ErrorMessage = "Dakika fiyatı max 100 olmalıdır.")]
    public decimal FiyatDakika { get; set; }

    [Required(ErrorMessage = "Km sınırlı ücretsiz gereklidir.")]
    [Range(0, int.MaxValue, ErrorMessage = "Km sınırlı ücretsiz pozitif bir değer olmalıdır.")]
    public int KmSiniriUcretsiz { get; set; }

    [Required(ErrorMessage = "İlave km ücreti gereklidir.")]
    [Range(0, double.MaxValue, ErrorMessage = "İlave km ücreti pozitif bir değer olmalıdır.")]
    public decimal IlaveKmUcreti { get; set; }
}