using MongoDB.Bson.Serialization.Attributes;

namespace Earthquake.BusinessLayer.Models;

public record class Earthquakes : IEarthquakeCollection
{
    [BsonId] public Guid Id { get; init; }
    public string PublicId { get; init; } = default!;
    public string Data { get; init; } = default!;
    public int MMI { get; init; } = default!;
    public DateTime OccuredOn { get; init; }
    public DateTime CreatedOn { get; init; }
    public DateTime? UpdatedOn { get; init; }
    public string Source { get; init; } = default!;
}