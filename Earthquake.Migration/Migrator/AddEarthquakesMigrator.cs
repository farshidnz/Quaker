using Microsoft.Extensions.Hosting;

namespace Earthquake.Migration.Migrator;

public class AddEarthquakesMigrator : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        //call geonet
        //add all the data
        //stop when no new data
        throw new NotImplementedException();
    }
}