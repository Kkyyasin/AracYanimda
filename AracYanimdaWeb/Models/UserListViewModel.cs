using System.ComponentModel.DataAnnotations;
namespace AracYanimdaWeb.Models;

public class UserListViewModel
{


    public int UserId { get; set; }

    [Required]
    [MaxLength(50)]
    public string Ad { get; set; }

    [Required]
    [MaxLength(50)]
    public string Soyad { get; set; }

    [Required]
    [EmailAddress]
    [MaxLength(100)]
    public string EPosta { get; set; }

    [Required]
    [MaxLength(100)]
    public string Sifre { get; set; }

    [Required]
    [MaxLength(20)]
    public string Telefon { get; set; }

    public bool? SozlesmeKabul { get; set; }

    [Required]
    public DateTime KayitTarihi { get; set; }

    [MaxLength(50)]
    public string Durum { get; set; }
}

