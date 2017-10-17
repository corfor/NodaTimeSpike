using System;
using System.Reflection;
using System.Runtime.Caching;
using GeoTimeZone;
using log4net;
using NodaTime;
using TimeZoneNames;

namespace NodaTimeSpike
{
    /// <summary>
    ///     Extension methods for GeoPoint
    /// </summary>
    public static class GeoPointHelper
    {
        private static readonly MemoryCache Cache = new MemoryCache("GeoPointHelperCache");

        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static T AddOrGetExisting<T>(string key, Func<T> valueFactory)
        {
            var newValue = new Lazy<T>(valueFactory);
            var oldValue = Cache.AddOrGetExisting(key, newValue, new CacheItemPolicy()) as Lazy<T>;
            try
            {
                return (oldValue ?? newValue).Value;
            }
            catch
            {
                // Handle cached lazy exception by evicting from cache. Thanks to Denis Borovnev for pointing this out!
                Cache.Remove(key);
                throw;
            }
        }

        /// <summary>
        ///     Lookup short time zone abbreviation from geo coordinates.
        /// </summary>
        /// <param name="geoPoint"></param>
        /// <returns>ET,CT,MT,PT</returns>
        public static string LookupTimeZoneLabel(this GeoPoint geoPoint)
        {
            if (geoPoint == null || Math.Abs(geoPoint.Latitude) < .0001)
                return null;

            try
            {
                var timeZone = GetTimeZone(geoPoint);
                var abbreviations = TZNames.GetAbbreviationsForTimeZone(timeZone, "en-US");

                return abbreviations.Generic; // == "PT"
            }
            catch (Exception e)
            {
                Logger.WarnFormat("Unable to look up timezone for ({0},{1}). {2}", geoPoint.Latitude, geoPoint.Longitude, e.RecursiveErrorMessage());
                return null;
            }
        }

        public static DateTimeOffset GetLocalDateTimeOffset(this GeoPoint geoPoint, DateTime dateTime)
        {
            if (geoPoint == null || Math.Abs(geoPoint.Latitude) < .0001)
                return dateTime;

            var timeZone = GetTimeZone(geoPoint);
            var ldt = new LocalDateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second);
            var zdt = ldt.InZoneLeniently(DateTimeZoneProviders.Tzdb[timeZone]);
            return zdt.ToDateTimeOffset();
        }

        private static string GetTimeZone(GeoPoint geoPoint)
        {
            var key = geoPoint.ToString();
            return AddOrGetExisting(key, () => TimeZoneLookup.GetTimeZone(geoPoint.Latitude, geoPoint.Longitude).Result);
        }
    }
}