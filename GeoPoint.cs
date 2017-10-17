using System;
using System.Device.Location;
using Newtonsoft.Json;

namespace NodaTimeSpike
{
    [Serializable]
    public class GeoPoint
    {
        public const double MetersPerMile = 1609.344;

        /// <summary>
        ///     Geographic center of the United States.
        /// </summary>
        public static readonly GeoPoint Default = new GeoPoint(39.8333333, -98.585522);

        public GeoPoint(double latitude, double longitude)
        {
            if (latitude > 90 || latitude < -90)
                throw new ArgumentOutOfRangeException(nameof(latitude),
                    "Must be in the range from -90 to +90 degrees, inclusive.");
            if (longitude > 180 || longitude < -180)
                throw new ArgumentOutOfRangeException(nameof(latitude),
                    "Must be in the range from -180 to +180 degrees, inclusive.");
            Latitude = latitude;
            Longitude = longitude;
        }

        [JsonProperty(Order = 1)]
        [JsonRequired]
        public double Latitude { get; private set; }

        [JsonProperty(Order = 2)]
        [JsonRequired]
        public double Longitude { get; private set; }

        /// <summary>
        ///     Returns the distance between the latitude and longitude coordinates that are specified by this
        ///     <see cref="T:AppointmentScheduling.Core.GeoPointDetail" /> and another specified
        ///     <see cref="T:AppointmentScheduling.Core.GeoPointDetail" />.
        /// </summary>
        /// <returns>Distance between points, in miles.</returns>
        public double GetDistance(GeoPoint other)
        {
            return GetDistance(this, other);
        }

        /// <summary>
        ///     Returns the distance between the latitude and longitude coordinates that are specified by this
        ///     <see cref="T:AppointmentScheduling.Core.GeoPointDetail" /> and another specified
        ///     <see cref="T:AppointmentScheduling.Core.GeoPointDetail" />.
        /// </summary>
        /// <returns>Distance between points, in miles.</returns>
        public static double GetDistance(GeoPoint a, GeoPoint b)
        {
            if (a == null) throw new ArgumentNullException(nameof(a));
            if (b == null) throw new ArgumentNullException(nameof(b));
            var coordA = new GeoCoordinate(a.Latitude, a.Longitude);
            var coordB = new GeoCoordinate(b.Latitude, b.Longitude);

            var meters = coordA.GetDistanceTo(coordB);
            return meters / MetersPerMile;
        }

        public override string ToString()
        {
            return $"{Latitude:0.00}, {Longitude:0.00}";
        }
    }
}