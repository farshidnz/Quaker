using Earthquake.BusinessLayer.Models;
using MediatR;
using MongoDB.Driver;

namespace Earthquake.BusinessLayer.Queries
{
    public sealed record GetEarthquakesByPublicIdQuery(string PublicId) : IRequest<Earthquakes?>
    {

        public class GetEarthquakesByPublicIdQueryHandler : IRequestHandler<GetEarthquakesByPublicIdQuery, Earthquakes?>
        {
            private readonly IMongoClient _mongoClient;
            private const string DatabaseName = "Quaker";
            private const string CollectionName = "earthQuakes";

            public GetEarthquakesByPublicIdQueryHandler(IMongoClient mongoClient)
            {
                _mongoClient = mongoClient;
            }

            public async Task<Earthquakes?> Handle(GetEarthquakesByPublicIdQuery request, CancellationToken cancellationToken)
            {
                var db = _mongoClient.GetDatabase(DatabaseName);
                var collection = db.GetCollection<Earthquakes>(CollectionName);

                var filter = Builders<Earthquakes>.Filter.Eq("PublicId", request.PublicId);
                var eqs = await collection.FindAsync(filter, cancellationToken: cancellationToken);
                return await eqs.SingleOrDefaultAsync(cancellationToken);
            }
        }
    }
}
