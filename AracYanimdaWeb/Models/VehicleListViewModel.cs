using System.ComponentModel.DataAnnotations;
namespace AracYanimdaWeb.Models;
public class VehicleListViewModel
{
    [Key]
    public int VehicleId { get; set; }

    [Required(ErrorMessage = "Marka alanı gereklidir.")]
    public string Marka { get; set; }

    [Required(ErrorMessage = "Model alanı gereklidir.")]
    public string Model { get; set; }

    [Required(ErrorMessage = "Yıl alanı gereklidir.")]
    [Range(1900, int.MaxValue, ErrorMessage = "Geçerli bir yıl giriniz.")]
    public int Yil { get; set; }

    [Required(ErrorMessage = "Plaka alanı gereklidir.")]
    public string Plaka { get; set; }

    [Required(ErrorMessage = "Yakıt Tipi alanı gereklidir.")]

    public string YakitTipi { get; set; }
    public string YakitDurumu { get; set; }

    [Required(ErrorMessage = "LocationId alanı gereklidir.")]
    public int LocationId { get; set; }

    [Required(ErrorMessage = "FiyatId alanı gereklidir.")]
    public int FiyatId { get; set; }


}