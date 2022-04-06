using System.Reflection;
using Earthquake.BusinessLayer.Commands;
using Earthquake.Worker;
using MediatR;
using MongoDB.Driver;

IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

IHost host = Host.CreateDefaultBuilder(args)
    // .ConfigureServices(services => { services.AddHostedService<Worker>(); })
    .ConfigureServices(services =>
    {
        //services.AddHostedService<GeonetService>();
        services.AddHostedService<Worker>();
        services.AddMediatR(typeof(AddEarthquakeCommand).GetTypeInfo().Assembly);
        services.AddSingleton<IMongoClient>(s => new MongoClient(configuration.GetConnectionString("MongoDb")));
    })
    .Build();

await host.RunAsync();