using System;

public class TurkishPlakaGenerator
{
    private readonly Random _random;

    public TurkishPlakaGenerator()
    {
        _random = new Random();
    }

    public string GenerateRandomPlate()
    {
        string plate = GenerateRandomCityCode();
        plate += " " + GenerateRandomLetter() + GenerateRandomLetter() + " " + GenerateRandomNumber() + GenerateRandomNumber() + GenerateRandomNumber();

        return plate;
    }

    private string GenerateRandomCityCode()
    {
        string[] cityCodes = { "01", "34", "06", "35", "16", "41", "33", "67", "55", "07", "58", "27", "59", "26", "18", "17", "16", "77", "32", "45", "54", "21", "51", "42" };
        return cityCodes[_random.Next(cityCodes.Length)];
    }

    private char GenerateRandomLetter()
    {
        int num = _random.Next(0, 25); // 25 harf var
        char letter = (char)('A' + num);
        return letter;
    }

    private int GenerateRandomNumber()
    {
        return _random.Next(0, 10); // 10 rakam var
    }
}
