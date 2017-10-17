using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NodaTimeSpike
{
    [TestClass]
    public class GeoPointHelperTests
    {
        /// <summary>
        /// First call is slow, so get that out of the way so timings are more meaningful.
        /// </summary>
        /// <param name="testContext"></param>
        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            const string dateTime = "2017-07-04 20:30:40";
            var geoPoint = new GeoPoint(41.866422, -87.616988);
            var d = DateTime.Parse(dateTime);

            var label = geoPoint.LookupTimeZoneLabel();
            label.Should().NotBeNullOrEmpty();
            var date = geoPoint.GetLocalDateTimeOffset(d);
            date.Should().NotBeSameDateAs(default(DateTimeOffset));
        }

        [TestMethod]
        public void ChicagoSummer()
        {
            const string dateTime = "2017-07-04 20:30:40";
            var geoPoint = new GeoPoint(41.866422, -87.616988);
            var d = DateTime.Parse(dateTime);

            var label = geoPoint.LookupTimeZoneLabel();
            label.Should().Be("CT");

            var date = geoPoint.GetLocalDateTimeOffset(d);
            var s = date.ToString("yyyy-MM-ddTHH\\:mm\\:sszzz");
            Console.WriteLine(s);
            s.Should().Be("2017-07-04T20:30:40-05:00");
        }

        [TestMethod]
        public void ChicagoChristmas()
        {
            const string dateTime = "2017-12-25 20:30:40";
            var geoPoint = new GeoPoint(41.866422, -87.616988);
            var d = DateTime.Parse(dateTime);

            var label = geoPoint.LookupTimeZoneLabel();
            label.Should().Be("CT");

            var date = geoPoint.GetLocalDateTimeOffset(d);
            var s = date.ToString("yyyy-MM-ddTHH\\:mm\\:sszzz");
            Console.WriteLine(s);
            s.Should().Be("2017-12-25T20:30:40-06:00");
        }


        [TestMethod]
        public void KnoxIndependanceDay()
        {
            const string dateTime = "2017-07-04 20:30:40";
            var geoPoint = new GeoPoint(41.285433, -86.626029);
            var d = DateTime.Parse(dateTime);

            var label = geoPoint.LookupTimeZoneLabel();
            label.Should().Be("CT");

            var date = geoPoint.GetLocalDateTimeOffset(d);
            date.ShouldBeEquivalentTo(new DateTimeOffset(2017, 07, 04, 20, 30, 40, TimeSpan.FromHours(-5)));
            var s = date.ToString("yyyy-MM-ddTHH\\:mm\\:sszzz");
            Console.WriteLine(s);
            s.Should().Be("2017-07-04T20:30:40-05:00");
        }

        [TestMethod]
        public void KnoxChristmas()
        {
            var geoPoint = new GeoPoint(41.285433, -86.626029);
            var label = geoPoint.LookupTimeZoneLabel();
            label.Should().Be("CT");

            var dateTime = DateTime.Parse("2017-12-25 20:30:40");
            var date = geoPoint.GetLocalDateTimeOffset(dateTime);

            date.ShouldBeEquivalentTo(new DateTimeOffset(2017, 12, 25, 20, 30, 40, TimeSpan.FromHours(-6)));
            var s = date.ToString("yyyy-MM-ddTHH\\:mm\\:sszzz");
            Console.WriteLine(s);
            s.Should().Be("2017-12-25T20:30:40-06:00");
        }

        [TestMethod]
        public void FortWayneIndependanceDay()
        {
            var dateTime = DateTime.Parse("2017-07-04 20:30:40");
            var geoPoint = new GeoPoint(40.977506, -85.196059);
            var date = geoPoint.GetLocalDateTimeOffset(dateTime);

            var label = geoPoint.LookupTimeZoneLabel();
            label.Should().Be("ET");

            date.ShouldBeEquivalentTo(new DateTimeOffset(2017, 07, 04, 20, 30, 40, TimeSpan.FromHours(-4)));
            var s = date.ToString("yyyy-MM-ddTHH\\:mm\\:sszzz");
            Console.WriteLine(s);
            s.Should().Be("2017-07-04T20:30:40-04:00");
        }

        [TestMethod]
        public void FortWayneChristmas()
        {
            var dateTime = DateTime.Parse("2017-12-25 20:30:40");
            var geoPoint = new GeoPoint(40.977506, -85.196059);
            var date = geoPoint.GetLocalDateTimeOffset(dateTime);

            var label = geoPoint.LookupTimeZoneLabel();
            label.Should().Be("ET");

            date.ShouldBeEquivalentTo(new DateTimeOffset(2017, 12, 25, 20, 30, 40, TimeSpan.FromHours(-5)));
            var s = date.ToString("yyyy-MM-ddTHH\\:mm\\:sszzz");
            Console.WriteLine(s);
            s.Should().Be("2017-12-25T20:30:40-05:00");
        }

        [TestMethod]
        public void TucsonIndependanceDay()
        {
            var dateTime = DateTime.Parse("2017-07-04 20:30:40");
            var geoPoint = new GeoPoint(32.114510, -110.939259);
            var date = geoPoint.GetLocalDateTimeOffset(dateTime);

            var label = geoPoint.LookupTimeZoneLabel();
            label.Should().Be("MT");

            date.ShouldBeEquivalentTo(new DateTimeOffset(2017, 07, 04, 20, 30, 40, TimeSpan.FromHours(-7)));
            var s = date.ToString("yyyy-MM-ddTHH\\:mm\\:sszzz");
            Console.WriteLine(s);
            s.Should().Be("2017-07-04T20:30:40-07:00");
        }

        [TestMethod]
        public void TucsonChristmas()
        {
            var dateTime = DateTime.Parse("2017-12-25 20:30:40");
            var geoPoint = new GeoPoint(32.114510, -110.939259);
            var date = geoPoint.GetLocalDateTimeOffset(dateTime);

            var label = geoPoint.LookupTimeZoneLabel();
            label.Should().Be("MT");

            date.ShouldBeEquivalentTo(new DateTimeOffset(2017, 12, 25, 20, 30, 40, TimeSpan.FromHours(-7)));
            var s = date.ToString("yyyy-MM-ddTHH\\:mm\\:sszzz");
            Console.WriteLine(s);
            s.Should().Be("2017-12-25T20:30:40-07:00");
        }
    }
}