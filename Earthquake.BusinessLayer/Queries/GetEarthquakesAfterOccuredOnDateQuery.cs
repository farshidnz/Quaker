using Earthquake.BusinessLayer.Models;
using MediatR;
using MongoDB.Driver;

namespace Earthquake.BusinessLayer.Queries
{
    public sealed record GetEarthquakesAfterOccuredOnDateQuery(DateTime FromOccuredOn, int Mmi) : IRequest<List<Earthquakes>>
    {

        public class GetEarthquakesAfterOccuredOnDateQueryHandler : IRequestHandler<GetEarthquakesAfterOccuredOnDateQuery, List<Earthquakes>>
        {
            private readonly IMongoClient _mongoClient;
            private const string DatabaseName = "Quaker";
            private const string CollectionName = "earthQuakes";

            public GetEarthquakesAfterOccuredOnDateQueryHandler(IMongoClient mongoClient)
            {
                _mongoClient = mongoClient;
            }

            public async Task<List<Earthquakes>> Handle(GetEarthquakesAfterOccuredOnDateQuery request, CancellationToken cancellationToken)
            {
                var db = _mongoClient.GetDatabase(DatabaseName);
                var collection = db.GetCollection<Earthquakes>(CollectionName);

                var eqs = await collection.FindAsync(c => c.OccuredOn > request.FromOccuredOn && c.MMI == request.Mmi, cancellationToken: cancellationToken);
                return await eqs.ToListAsync(cancellationToken);
            }
        }
    }
}
