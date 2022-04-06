namespace Earthquake.Worker.GeonetModels;

public class Root
{
    public string Type { get; set; } = default!;
    public List<Feature> Features { get; set; } = new();
}