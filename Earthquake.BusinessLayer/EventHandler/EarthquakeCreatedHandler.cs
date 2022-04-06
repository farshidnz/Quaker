using Earthquake.BusinessLayer.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using Tweetinvi;
using Tweetinvi.Parameters;

namespace Earthquake.BusinessLayer.EventHandler
{
    public class EarthquakeCreatedHandler : INotificationHandler<EarthquakeCreated>
    {
        private readonly ILogger<EarthquakeCreatedHandler> _logger;

        public EarthquakeCreatedHandler(ILogger<EarthquakeCreatedHandler> logger)
        {
            _logger = logger;
        }

        public async Task Handle(EarthquakeCreated notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Notification is handling " + notification.earthquake.PublicId);
            // var apikey = "";
            // var apiSecret = "";
            // var accessToken = "";
            // var accessSecret = "";
            //
            // var twitterClient = new TwitterClient(apikey, apiSecret,accessToken,accessSecret );
            // await twitterClient.Tweets.PublishTweetAsync("Test new eq");
        }
    }
}
