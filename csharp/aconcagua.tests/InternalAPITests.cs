using aconcagua.client;
using aconcagua.server;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace aconcagua.tests
{
    [TestClass]
    public class InternalAPITests
    {
        /// <summary>
        ///  Gets or sets the test context which provides
        ///  information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void CanCallGetVersion()
        {
            var request = ClientHandler.GetVersion.CreateRequest();
            var response = ServerHandler.CallGetVersion(request);
            Assert.AreEqual(response.Version, "1.0");
        }

        [TestMethod]
        public void CanCallGetSeriesKeys()
        {
            var request = ClientHandler.GetSeriesKeys.CreateRequest(
                new[] { "dmx:.\\..\\..\\..\\..\\data\\sample.dmx" },
                new[] {"description:%"}
            );
            var response = ServerHandler.CallGetSeriesKeys(request);
            ClientHandler.GetSeriesKeys.ShowResponse(response);
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public void CanCallGetObservations()
        {
            var request = ClientHandler.GetObservations.CreateRequest(
                "dmx:.\\..\\..\\..\\..\\data\\sample.dmx",
                new[] { "911BE", "911BEA", "BCA_GDP" },
                "MA"
            );
            var response = ServerHandler.CallGetObservations(request);
            ClientHandler.GetObservations.ShowResponse(response);
            Assert.IsNotNull(response);
        }
    }
}
