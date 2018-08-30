using System;
using System.Collections.Generic;
using System.Linq;
using Grpc.Core;

namespace aconcagua.client
{
    internal class Program
    {
        static readonly string _sourceName = "dmx:\\C:\\Users\\Jerry\\Projects\\aconcagua\\data\\sample.dmx";
        static readonly IEnumerable<string> _seriesList = new [] {"911BE", "911BEA", "BCA_GDP"};
        static readonly IEnumerable<string> _headerList = new [] {"scale", "unit", "description"};
        static string _frequencyList = "MA";

        public static void Main(string[] args)
        {
            var channel = new Channel("127.0.0.1:50051", ChannelCredentials.Insecure);
            var client = new Aconcagua.AconcaguaClient(channel);
            var empty = new Google.Protobuf.WellKnownTypes.Empty();

            var vrs = client.GetVersion(empty);
            Console.WriteLine($"Version: {vrs.Version}");

            var mrq = CreateMetadataRequest();
            var mrs = client.GetMetadata(mrq);
            ShowMetadataResponse(mrs);

            var orq = CreateObservationsRequest();
            var or = client.GetObservations(orq);
            ShowObservationResponse(or);

            channel.ShutdownAsync().Wait();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        #region GetMetadata

        private static GetMetadataRequest CreateMetadataRequest()
        {
            var request = new GetMetadataRequest();
            request.Requestmetadata.Add(new Dictionary<string, string>() { { "version", "0.9" } });

            request.Metadataheaders.Add(_headerList);
            request.Keys.Add(CreateSourceSeriesKeyListFrom(_sourceName, _seriesList));

            return request;
        }

        private static void ShowMetadataResponse(GetMetadataResponse response)
        {
            var i = 0;

            foreach (var ts in response.Datalist)
            {
                Console.WriteLine($"Sourcename[{i}]: {ts.Key.Sourcename}/{ts.Key.Seriesname}");

                foreach (var pair in response.Metadataheaders.Zip(ts.Data, Tuple.Create))
                    Console.WriteLine($"    {pair.Item1}: {pair.Item2}");
                i++;
            }
        }

        #endregion

        #region GetObservations

        private static GetObservationsRequest CreateObservationsRequest()
        {
            var request = new GetObservationsRequest();
            request.Requestmetadata.Add(new Dictionary<string, string>() { { "version", "0.9" } });

            request.Frequencies = _frequencyList;
            request.Keys.Add(CreateSourceSeriesKeyListFrom(_sourceName, _seriesList));

            return request;
        }

        private static void ShowObservationResponse(GetObservationsResponse response)
        {
            var i = 0;

            foreach (var ts in response.Datalist)
            {
                Console.WriteLine($"Sourcename[{i}]: {ts.Key.Sourcename}/{ts.Key.Seriesname}");

                foreach (var pair in ts.Values.Keys.Zip(ts.Values.Values, Tuple.Create))
                    Console.WriteLine($"    {pair.Item1}: {pair.Item2}");
                i++;
            }
        }

        #endregion

        private static IEnumerable<SourceSeriesKey> CreateSourceSeriesKeyListFrom(string sourceName, IEnumerable<string> _seriesList)
        {
            return _seriesList.Select(s => new SourceSeriesKey() { Sourcename = sourceName, Seriesname = s }).ToList();
        }
    }
}
