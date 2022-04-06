using Earthquake.BusinessLayer.Events;
using Earthquake.BusinessLayer.Models;
using MediatR;
using MongoDB.Driver;

namespace Earthquake.BusinessLayer.Commands
{
    public record AddEarthquakeCommand : IRequest
    {
        public string Data { get; init; } = default!;
        public string PublicId { get; init; } = default!;
        public int MMI { get; init; }
        public DateTime OccuredOn { get; init; }

        public class AddEarthquakeCommandHandler : AsyncRequestHandler<AddEarthquakeCommand>
        {
            private readonly IMongoClient _mongoClient;
            private IMediator _mediator;
            private const string DatabaseName = "Quaker";
            private const string CollectionName = "earthQuakes";

            public AddEarthquakeCommandHandler(IMongoClient mongoClient, IMediator mediator)
            {
                _mongoClient = mongoClient;
                _mediator = mediator;
            }

            protected override async Task Handle(AddEarthquakeCommand request, CancellationToken cancellationToken)
            {
                var db = _mongoClient.GetDatabase(DatabaseName);
                var collection = db.GetCollection<Earthquakes>(CollectionName);

                var eq = new Earthquakes()
                {
                    Data = request.Data,
                    MMI = request.MMI,
                    PublicId = request.PublicId,
                    Source = "geonet",
                    OccuredOn = request.OccuredOn,
                    CreatedOn = DateTime.UtcNow
                };
                await collection.InsertOneAsync(eq, cancellationToken: cancellationToken);

                await _mediator.Publish(new EarthquakeCreated(eq, DateTime.UtcNow), cancellationToken);

            }
        }
    }
}