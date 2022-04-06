using Newtonsoft.Json;

namespace Earthquake.Worker.GeonetModels;

public class Properties
{
    [JsonProperty("PublicID")]
    public string PublicId { get; set; }= default!;
    public DateTime Time { get; set; }
    public double Depth { get; set; }
    public double Magnitude { get; set; }
    public int Mmi { get; set; }
    public string Locality { get; set; } = default!;
    public string Quality { get; set; } = default!;
}