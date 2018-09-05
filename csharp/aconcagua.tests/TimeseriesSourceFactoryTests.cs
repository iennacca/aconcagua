using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using aconcagua.data;
using aconcagua.data.dmx;
using aconcagua.data.ecos;
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
            const string tsSource = "dmx:.\\..\\..\\..\\..\\data\\sample.dmx";
            const string seriesNames = "911BE,911BEA,BCA_GDP";
            const string headers = "oname,description,scale"; 

            var tsKeys = seriesNames.ToTimeseriesKeys();
            var headerList = headers.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();

            var tss = TimeseriesSourceFactory.Factory[tsSource];
            Assert.IsInstanceOfType(tss, typeof(DMXTimeseriesSource));

            var result = tss.GetMetadata(tsKeys, headerList);
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void CanAccessECOSTimeseriesSource()
        {
            // Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\Jerry\Projects\aconcagua\data\sample.dmx
            const string tsSource = "ECOS://ECDATA_CPI";
            const string seriesNames = "312PCPIFBT_IX.A, 612PCPIFBT_IX.M, 612PCPIFBT_IX.Q";
            const string headers = "SCALE,INDICATOR,COUNTRY";

            var tsKeys = seriesNames.ToTimeseriesKeys();
            var headerList = headers.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();

            var tss = TimeseriesSourceFactory.Factory[tsSource];
            Assert.IsInstanceOfType(tss, typeof(ECOSTimeseriesSource));

            var result = tss.GetMetadata(tsKeys, headerList);
            Assert.IsTrue(result.Any());
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

        [TestMethod]
        public void CanCreateQueriesCorrectly()
        {
            var actualresult = GetObservationsQuery.CreateOValueFieldNamesFrom(FrequencyIndicator.Monthly);
            var expectedresult = "OValue1 AS M1, OValue2 AS M2, OValue3 AS M3, OValue4 AS M4, OValue5 AS M5, OValue6 AS M6, OValue7 AS M7, OValue8 AS M8, OValue9 AS M9, OValue10 AS M10, OValue11 AS M11, OValue12 AS M12";
            Assert.AreEqual(actualresult, expectedresult);

            actualresult = GetObservationsQuery.CreateOValueFieldNamesFrom(FrequencyIndicator.Quarterly);
            expectedresult = "OValue13 AS Q1, OValue14 AS Q2, OValue15 AS Q3, OValue16 AS Q4";
            Assert.AreEqual(actualresult, expectedresult);
        }
    }
}
