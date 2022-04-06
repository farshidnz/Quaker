using Earthquake.BusinessLayer.Models;
using MediatR;

namespace Earthquake.BusinessLayer.Events
{
    public class EarthquakeCreated : INotification
    {
        public EarthquakeCreated(Earthquakes newEarthquake, DateTime time)
        {
            earthquake = newEarthquake;
            OccurredOn = time;
        }

        public Earthquakes earthquake { get; }
        public DateTime OccurredOn { get; }
    }
}
