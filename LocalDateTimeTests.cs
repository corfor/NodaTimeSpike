using System;
using FluentAssertions;
using GeoTimeZone;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NodaTime;
using NodaTime.Text;
using TimeZoneNames;

namespace NodaTimeSpike
{
    [TestClass]
    public class LocalDateTimeTests
    {
        [TestMethod]
        public void ChicagoChristmas()
        {
            const string dateTime = "2017-12-25 20:30:40";
            const string timeZone = "America/Chicago";

            var pattern = LocalDateTimePattern.CreateWithInvariantCulture("yyyy-MM-dd HH:mm:ss");
            var ldt = pattern.Parse(dateTime).Value;
            var zdt = ldt.InZoneLeniently(DateTimeZoneProviders.Tzdb[timeZone]);
            var date = zdt.ToDateTimeOffset();
            date.ShouldBeEquivalentTo(new DateTimeOffset(2017, 12, 25, 20, 30, 40, TimeSpan.FromHours(-6)));
            Console.WriteLine(date.ToString("s"));
        }

        [TestMethod]
        public void ChicagoIndependanceDay()
        {
            const string dateTime = "2017-07-04 20:30:40";
            const string timeZone = "America/Chicago";
            var d = DateTime.Parse(dateTime);

            var ldt = new LocalDateTime(d.Year, d.Month, d.Day, d.Hour, d.Minute, d.Second);
            var zdt = ldt.InZoneLeniently(DateTimeZoneProviders.Tzdb[timeZone]);
            var date = zdt.ToDateTimeOffset();
            date.ShouldBeEquivalentTo(new DateTimeOffset(2017, 07, 04, 20, 30, 40, TimeSpan.FromHours(-5)));
            Console.WriteLine(date.ToString("s"));
        }

        [TestMethod]
        public void KnoxIndependanceDay()
        {
            var geoPoint = new GeoPoint(41.285433, -86.626029);
            var d = DateTime.Parse("2017-07-04 20:30:40");

            var date = geoPoint.GetLocalDateTimeOffset(d);

            date.ShouldBeEquivalentTo(new DateTimeOffset(2017, 07, 04, 20, 30, 40, TimeSpan.FromHours(-5)));
            Console.WriteLine(date.ToString("s"));
        }

        [TestMethod]
        public void KnoxChristmas()
        {
            const string dateTime = "2017-12-25 20:30:40";
            var timeZone = TimeZoneLookup.GetTimeZone(41.285433, -86.626029).Result;
            timeZone.Should().Be("America/Indiana/Knox");
            var abbreviation = TZNames.GetAbbreviationsForTimeZone(timeZone, "en-US").Generic;
            abbreviation.Should().Be("CT");

            var pattern = LocalDateTimePattern.CreateWithInvariantCulture("yyyy-MM-dd HH:mm:ss");
            var ldt = pattern.Parse(dateTime).Value;
            var zdt = ldt.InZoneLeniently(DateTimeZoneProviders.Tzdb[timeZone]);
            var date = zdt.ToDateTimeOffset();
            date.ShouldBeEquivalentTo(new DateTimeOffset(2017, 12, 25, 20, 30, 40, TimeSpan.FromHours(-6)));
            Console.WriteLine(date.ToString("s"));
        }

        [TestMethod]
        public void FortWayneIndependanceDay()
        {
            const string dateTime = "2017-07-04 20:30:40";
            var timeZone = TimeZoneLookup.GetTimeZone(40.977506, -85.196059).Result;
            var d = DateTime.Parse(dateTime);
            var abbreviation = TZNames.GetAbbreviationsForTimeZone(timeZone, "en-US").Generic;
            abbreviation.Should().Be("ET");

            var ldt = new LocalDateTime(d.Year, d.Month, d.Day, d.Hour, d.Minute, d.Second);
            var zdt = ldt.InZoneLeniently(DateTimeZoneProviders.Tzdb[timeZone]);
            var date = zdt.ToDateTimeOffset();
            date.ShouldBeEquivalentTo(new DateTimeOffset(2017, 07, 04, 20, 30, 40, TimeSpan.FromHours(-4)));
            Console.WriteLine(date.ToString("s"));
        }

        [TestMethod]
        public void FortWayneChristmas()
        {
            const string dateTime = "2017-12-25 20:30:40";
            var timeZone = TimeZoneLookup.GetTimeZone(40.977506, -85.196059).Result;
            timeZone.Should().Be("America/Indiana/Indianapolis");
            var abbreviation = TZNames.GetAbbreviationsForTimeZone(timeZone, "en-US").Generic;
            abbreviation.Should().Be("ET");

            var pattern = LocalDateTimePattern.CreateWithInvariantCulture("yyyy-MM-dd HH:mm:ss");
            var ldt = pattern.Parse(dateTime).Value;
            var zdt = ldt.InZoneLeniently(DateTimeZoneProviders.Tzdb[timeZone]);
            var date = zdt.ToDateTimeOffset();
            date.ShouldBeEquivalentTo(new DateTimeOffset(2017, 12, 25, 20, 30, 40, TimeSpan.FromHours(-5)));
            Console.WriteLine(date.ToString("s"));
        }

        [TestMethod]
        public void TucsonIndependanceDay()
        {
            const string dateTime = "2017-07-04 20:30:40";
            var timeZone = TimeZoneLookup.GetTimeZone(32.114510, -110.939259).Result;
            var d = DateTime.Parse(dateTime);
            var abbreviation = TZNames.GetAbbreviationsForTimeZone(timeZone, "en-US").Generic;
            abbreviation.Should().Be("MT");

            var ldt = new LocalDateTime(d.Year, d.Month, d.Day, d.Hour, d.Minute, d.Second);
            var zdt = ldt.InZoneLeniently(DateTimeZoneProviders.Tzdb[timeZone]);
            var date = zdt.ToDateTimeOffset();
            date.ShouldBeEquivalentTo(new DateTimeOffset(2017, 07, 04, 20, 30, 40, TimeSpan.FromHours(-7)));
            var s = date.ToString("yyyy-MM-ddTHH\\:mm\\:sszzz");
            Console.WriteLine(s);
            s.Should().Be("2017-07-04T20:30:40-07:00");
        }

        [TestMethod]
        public void TucsonChristmas()
        {
            const string dateTime = "2017-12-25 20:30:40";
            var timeZone = TimeZoneLookup.GetTimeZone(32.114510, -110.939259).Result;
            timeZone.Should().Be("America/Phoenix");
            var abbreviation = TZNames.GetAbbreviationsForTimeZone(timeZone, "en-US").Generic;
            abbreviation.Should().Be("MT");

            var pattern = LocalDateTimePattern.CreateWithInvariantCulture("yyyy-MM-dd HH:mm:ss");
            var ldt = pattern.Parse(dateTime).Value;
            var zdt = ldt.InZoneLeniently(DateTimeZoneProviders.Tzdb[timeZone]);
            var date = zdt.ToDateTimeOffset();
            date.ShouldBeEquivalentTo(new DateTimeOffset(2017, 12, 25, 20, 30, 40, TimeSpan.FromHours(-7)));
            var s = date.ToString("yyyy-MM-ddTHH\\:mm\\:sszzz");
            Console.WriteLine(s);
            s.Should().Be("2017-12-25T20:30:40-07:00");
        }
    }
}