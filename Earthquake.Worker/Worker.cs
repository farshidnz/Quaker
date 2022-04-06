using System.Text.Json;
using Earthquake.BusinessLayer.Commands;
using Earthquake.BusinessLayer.Queries;
using Earthquake.Worker.GeonetModels;
using MediatR;

namespace Earthquake.Worker;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly ISender _sender;

    public Worker(ILogger<Worker> logger, ISender sender)
    {
        _logger = logger;
        _sender = sender;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

            var quakes = await GetNzQuakes(stoppingToken);
            await SaveNewEarthquakes(quakes, stoppingToken);
            
            await Task.Delay(5000, stoppingToken);
        }
    }

    private async Task SaveNewEarthquakes(IEnumerable<Root> quakes,CancellationToken stoppingToken)
    {
        foreach (var feature in quakes.SelectMany(quake => quake.Features).OrderBy(f => f.Properties.Time))
        {
            var eq = await _sender.Send(new GetEarthquakesByPublicIdQuery(feature.Properties.PublicId),
                stoppingToken);
            
            if (eq != null) continue;
            
            await _sender.Send(new AddEarthquakeCommand()
            {
                Data = JsonSerializer.Serialize(feature),
                PublicId = feature.Properties.PublicId,
                MMI = feature.Properties.Mmi,
                OccuredOn = feature.Properties.Time
            }, stoppingToken);
        }
    }

    private async Task<IEnumerable<Root>> GetNzQuakes(CancellationToken cancellationToken)
    {
        using var client = new HttpClient();
        var mmiMinus1 = client.GetStreamAsync($"https://api.geonet.org.nz/quake?MMI={-1}", cancellationToken);
        var mmi0 = client.GetStreamAsync($"https://api.geonet.org.nz/quake?MMI={0}", cancellationToken);
        var mmi1 = client.GetStreamAsync($"https://api.geonet.org.nz/quake?MMI={1}", cancellationToken);
        var mmi2 = client.GetStreamAsync($"https://api.geonet.org.nz/quake?MMI={2}", cancellationToken);
        var mmi3 = client.GetStreamAsync($"https://api.geonet.org.nz/quake?MMI={3}", cancellationToken);
        var mmi4 = client.GetStreamAsync($"https://api.geonet.org.nz/quake?MMI={4}", cancellationToken);
        var mmi5 = client.GetStreamAsync($"https://api.geonet.org.nz/quake?MMI={5}", cancellationToken);
        var mmi6 = client.GetStreamAsync($"https://api.geonet.org.nz/quake?MMI={6}", cancellationToken);
        var mmi7 = client.GetStreamAsync($"https://api.geonet.org.nz/quake?MMI={7}", cancellationToken);
        var mmi8 = client.GetStreamAsync($"https://api.geonet.org.nz/quake?MMI={8}", cancellationToken);

        var tasks = new List<Task>
        {
            mmiMinus1, mmi0, mmi1, mmi2, mmi3, mmi4, mmi5, mmi6, mmi7, mmi8
        };
        await Task.WhenAll(tasks);
        var serializeOption = new JsonSerializerOptions() {PropertyNameCaseInsensitive = true};
        return new List<Root>()
        {
            (await JsonSerializer.DeserializeAsync<Root>(mmiMinus1.Result, serializeOption, cancellationToken))!,
            (await JsonSerializer.DeserializeAsync<Root>(mmi0.Result, serializeOption, cancellationToken))!,
            (await JsonSerializer.DeserializeAsync<Root>(mmi1.Result, serializeOption, cancellationToken))!,
            (await JsonSerializer.DeserializeAsync<Root>(mmi2.Result, serializeOption, cancellationToken))!,
            (await JsonSerializer.DeserializeAsync<Root>(mmi3.Result, serializeOption, cancellationToken))!,
            (await JsonSerializer.DeserializeAsync<Root>(mmi4.Result, serializeOption, cancellationToken))!,
            (await JsonSerializer.DeserializeAsync<Root>(mmi5.Result, serializeOption, cancellationToken))!,
            (await JsonSerializer.DeserializeAsync<Root>(mmi7.Result, serializeOption, cancellationToken))!,
            (await JsonSerializer.DeserializeAsync<Root>(mmi8.Result, serializeOption, cancellationToken))!
        };
    }

    public override Task StopAsync(CancellationToken stoppingToken)
    {
        return Task.CompletedTask;
    }
}