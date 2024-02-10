namespace AracYanimdaWeb.Generator;
public class IstanbulRandomLocationGenerator
{
    private readonly Random _random;

    public IstanbulRandomLocationGenerator()
    {
        _random = new Random();
    }

    public (double latitude, double longitude) GenerateRandomLocation()
    {
        double minLatitude = 40.6782;
        double maxLatitude = 41.3851;
        double minLongitude = 28.2048;
        double maxLongitude = 29.3598;

        double latitude = _random.NextDouble() * (maxLatitude - minLatitude) + minLatitude;
        double longitude = _random.NextDouble() * (maxLongitude - minLongitude) + minLongitude;

        return (latitude, longitude);
    }
}
