using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using aconcagua.data;
using aconcagua.data.factory;

namespace aconcagua.tests
{
    [TestClass]
    public class TimeseriesSourceFactoryTests
    {
        [TestMethod]
        public void CanAccessNullTimeseriesSourceFromNullUriScheme()
        {
            const string tsSource = "null://test";
            var tss = TimeseriesSourceFactory.Factory[tsSource];
            Assert.IsInstanceOfType(tss, typeof(NullTimeseriesSource));
        }

        [TestMethod]
        public void CanGetExceptionIfSourceNotAUri()
        { 
            const string tsUrl = "this.is.not.a.test";
            Assert.ThrowsException<UriFormatException>(() => TimeseriesSourceFactory.Factory[tsUrl]);
        }

        [TestMethod]
        public void CanGetNullMetadataFromNullTimeseriesSource()
        {
            const string tsSource = "null://test";
            var tsKey = new TimeseriesKey("seriesKey01");

            var tss = TimeseriesSourceFactory.Factory[tsSource];
            var tsList = tss.Get(new[] {tsKey}, new[] {"header01", "header02"}).ToList();

            Assert.IsTrue(tsList.Count() == 1);
            Assert.IsTrue(tsList.ElementAt(0).SeriesKey == tsKey);
        }

        [TestMethod]
        public void CanAccessDMXTimeseriesSource()
        {
            const string tsSource = "dmx:///c:/temp/test.dmx";
            const string seriesName = "111NGDP";

            var tsKey = new TimeseriesKey(seriesName);
            var tss = TimeseriesSourceFactory.Factory[tsSource];

            Assert.IsNotInstanceOfType(tss, typeof(NullTimeseriesSource));
        }
    }
}
