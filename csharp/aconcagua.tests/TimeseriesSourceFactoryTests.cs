using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using aconcagua.data;
using aconcagua.data.dmx;
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
            const string tsSource = "null:///test";
            var tsKey = new TimeseriesKey("seriesKey01");

            var tss = TimeseriesSourceFactory.Factory[tsSource];
            var tsList = tss.GetMetadata(new[] {tsKey}, new[] {"header01", "header02"}).ToList();

            Assert.IsTrue(tsList.Count() == 1);
            Assert.IsTrue(tsList.ElementAt(0).SeriesKey.Equals(tsKey));
        }

        [TestMethod]
        public void CanAccessDMXTimeseriesSource()
        {
            // Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\Jerry\Projects\aconcagua\data\sample.dmx
            const string tsSource = "dmx:///C:/Users/Jerry\\Projects/aconcagua/data/sample.dmx";
            const string seriesName = "111NGDP,112NGDP";
            // const string headers = "oname,description"; 

            var tsKey = new TimeseriesKey(seriesName);
            var tss = TimeseriesSourceFactory.Factory[tsSource];

            Assert.IsInstanceOfType(tss, typeof(DMXTimeseriesSource));
            // var r = tss.Get(new List<TimeseriesKey>(), )
        }

        [TestMethod]
        public void CanTransformTimeseriesKeyListToString()
        {
            const int numKeys = 10;

            var keys = Enumerable.Range(1, numKeys).Select(i => new TimeseriesKey($"test{i}")).ToList();

            var strKeys = keys.ToTimeseriesKeyString(QuotedSplitStringOptions.NonQuoted);
            Assert.IsTrue(!string.IsNullOrEmpty(strKeys));

            var listKeys = strKeys.ToTimeseriesKeys();
            Assert.IsTrue(listKeys.Count() == numKeys);
        }
    }
}
