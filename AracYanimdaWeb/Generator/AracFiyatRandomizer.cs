using System;
using AracYanimdaWeb.Models;
namespace AracYanimdaWeb.Generator;
public class AracFiyatRandomizer
{
    private readonly Random _random;

    public AracFiyatRandomizer()
    {
        _random = new Random();
    }

    public AracFiyat GenerateRandomAracFiyat(int fiyat)
    {
        AracFiyat aracFiyat = new AracFiyat
        {

            FiyatGunluk = fiyat / 100, // 100 ile 500 arasında rastgele bir değer oluşturur
            FiyatDakika = fiyat / 50000, // 1 ile 10 arasında rastgele bir değer oluşturur
            KmSiniriUcretsiz = _random.Next(50, 200), // 50 ile 200 arasında rastgele bir KmSiniriUcretsiz oluşturur
            IlaveKmUcreti = Math.Round((decimal)(_random.NextDouble() * (10 - 1) + 1), 2) // 1 ile 10 arasında rastgele bir değer oluşturur
        };

        return aracFiyat;
    }
}
