using Newtonsoft.Json;

namespace Earthquake.Worker.GeonetModels;

public class Feature
{
    public string Type { get; set; } = default!;
    
    [JsonProperty("geometry")]
    public Geometry Geometry { get; set; }
    
    [JsonProperty("properties")]
    public Properties Properties { get; set; }
}