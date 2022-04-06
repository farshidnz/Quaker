namespace Earthquake.Worker.GeonetModels;

public class Geometry
{
    public string Type { get; set; } = default!;
    public List<double> Coordinates { get; set; } = new();
}