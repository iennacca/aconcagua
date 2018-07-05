using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using aconcagua.data;

namespace aconcagua.tests
{
    [TestClass]
    public class TimeseriesSourceFactoryTests
    {
        [TestMethod]
        public void CanAccessNullTimeseriesSourceFromNullUriScheme()
        {
            const string tsUrl = "null:\\test";
            var tss = TimeseriesSourceFactory.Factory[tsUrl];
            Assert.IsInstanceOfType(tss, typeof(NullTimeseriesSource));
        }

        [TestMethod]
        public void CanGetExceptionIfSourceNotAUri()
        {
            const string tsUrl = "this.is.not.a.test";
            Assert.ThrowsException<UriFormatException>(() => TimeseriesSourceFactory.Factory[tsUrl]);
        }
    }
}
