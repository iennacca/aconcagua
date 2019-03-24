using System;
using System.Linq;
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
            Assert.IsTrue(response.Keys.Count > 0);

            request = ClientHandler.GetSeriesKeys.CreateRequest(
                new[] { "dmx:.\\..\\..\\..\\..\\data\\tcd.dmx" },
                new[] { "oname:%" }
            );
            response = ServerHandler.CallGetSeriesKeys(request);
            ClientHandler.GetSeriesKeys.ShowResponse(response);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Keys.Count > 0);
        }

        [TestMethod]
        public void CanCallGetMetadata()
        {
            var request = ClientHandler.GetMetadata.CreateRequest(
                "dmx:.\\..\\..\\..\\..\\data\\sample.dmx",
                new[] { "911BE", "911BEA", "BCA_GDP" },
                new[] { "scale", "unit", "description" }
            );
            var response = ServerHandler.CallGetMetadata(request);
            ClientHandler.GetMetadata.ShowResponse(response);
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

        [TestMethod]
        public void CannotUseWildcardsInGetMetadata()
        {
            const string source = "dmx:.\\..\\..\\..\\..\\data\\sample.dmx";
            var metadata = new[] { "scale", "unit", "description" };

            var request = ClientHandler.GetMetadata.CreateRequest(
                source,
                new[] { "BCA_GD%" },
                metadata
            );
            var response = ServerHandler.CallGetMetadata(request);
            ClientHandler.GetMetadata.ShowResponse(response);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Seriesdata.Count == 0);

            request = ClientHandler.GetMetadata.CreateRequest(
                "dmx:.\\..\\..\\..\\..\\data\\sample.dmx",
                new[] { "BCA_GDP" },
                new[] { "scale", "unit", "description" }
            );
            response = ServerHandler.CallGetMetadata(request);
            ClientHandler.GetMetadata.ShowResponse(response);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Seriesdata.Count > 0);
        }

        [TestMethod]
        public void CannotUseWildcardsInGetObservations()
        {
            var request = ClientHandler.GetObservations.CreateRequest(
                "dmx:.\\..\\..\\..\\..\\data\\sample.dmx",
                new[] { "911B%" },
                "MA"
            );
            var response = ServerHandler.CallGetObservations(request);
            ClientHandler.GetObservations.ShowResponse(response);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Seriesdata.Count == 0);

            request = ClientHandler.GetObservations.CreateRequest(
                "dmx:.\\..\\..\\..\\..\\data\\sample.dmx",
                new[] { "911BE" },
                "MA"
            );
            response = ServerHandler.CallGetObservations(request);
            ClientHandler.GetObservations.ShowResponse(response);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Seriesdata.Count > 0);
        }

        [TestMethod]
        public void CanGetMultipleFrequenciesFromGetObservations()
        {
            var request = ClientHandler.GetObservations.CreateRequest(
                "dmx:.\\..\\..\\..\\..\\data\\sample.dmx",
                new[] { "BCA_GDP" },
                "MQA"
            );
            var response = ServerHandler.CallGetObservations(request);
            ClientHandler.GetObservations.ShowResponse(response);

            foreach (var s in response.Seriesdata)
            {
                Console.WriteLine($"{s.Key.Sourcename}/{s.Key.Seriesname}");

                Console.WriteLine("    M");
                foreach (var ok in s.Values.Keys.Where((k) => k.Contains("M")))
                    Console.WriteLine($"    {ok}");
                Console.WriteLine("    A");
                foreach (var ok in s.Values.Keys.Where((k) => k.Contains("A")))
                    Console.WriteLine($"    {ok}");
            }
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public void StressTest()
        {
            var start = DateTime.Now;

            var gskRequest = ClientHandler.GetSeriesKeys.CreateRequest(
                new[] { "dmx:.\\..\\..\\..\\..\\data\\tcd.dmx" },
                new[] { "oname:%" }
            );
            var gskResponse = ServerHandler.CallGetSeriesKeys(gskRequest);
            ClientHandler.GetSeriesKeys.ShowResponse(gskResponse);
            Assert.IsNotNull(gskResponse);
            Assert.IsTrue(gskResponse.Keys.Count > 0);

            var goRequest = ClientHandler.GetObservations.CreateRequest(
                "dmx:.\\..\\..\\..\\..\\data\\tcd.dmx",
                gskResponse.Keys.Select(k => k.Seriesname),
                "MQA"
            );
            var goResponse = ServerHandler.CallGetObservations(goRequest);
            ClientHandler.GetObservations.ShowResponse(goResponse);

            var duration = DateTime.Now - start;
            Console.WriteLine($"StressTest done in {duration}");
        }

        [TestMethod]
        public void CanHandleTimeseriesLevelErrors00()
        {
            var goRequest = ClientHandler.GetObservations.CreateRequest(
                "dmx:.\\..\\..\\..\\..\\data\\tcd.dmx",
                new []{ "628NGP" },
                "MQA"
            );
            var goResponse = ServerHandler.CallGetObservations(goRequest);
            ClientHandler.GetObservations.ShowResponse(goResponse);
            Assert.IsNotNull(goResponse);
            Assert.IsTrue(goResponse.Seriesdata.Count > 0);
        }

        [TestMethod]
        public void CanHandleTimeseriesLevelErrors01()
        {
            var goRequest = ClientHandler.GetObservations.CreateRequest(
                "dmx:.\\..\\..\\..\\..\\data\\tcd.dmx",
                new[] { "628NGD%" },
                "MQA"
            );
            var goResponse = ServerHandler.CallGetObservations(goRequest);
            ClientHandler.GetObservations.ShowResponse(goResponse);
            Assert.IsNotNull(goResponse);
            Assert.IsTrue(goResponse.Seriesdata.Count > 0);
        }

    }
}
