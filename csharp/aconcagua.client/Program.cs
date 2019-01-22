using System;
using System.Collections.Generic;
using System.Linq;
using Grpc.Core;

namespace aconcagua.client
{
    internal class Program
    {

        public static void Main(string[] args)
        {
            var channel = new Channel("127.0.0.1:50051", ChannelCredentials.Insecure);
            var client = new Aconcagua.AconcaguaClient(channel);
            var empty = new Google.Protobuf.WellKnownTypes.Empty();

            var vrs = client.GetVersion(empty);
            Console.WriteLine($"Version: {vrs.Version}");

            // DMX request
            var req = new Request(
                "dmx:.\\..\\..\\..\\..\\data\\sample.dmx",
                new[] { "911BE", "911BEA", "BCA_GDP" },
                new[] { "scale", "unit", "description" },
                "MA"
                );

            // ECOS request
            //var req = new Request(
            //    "ecos:\\ECDATA_CPI",
            //    new[] { "312PCPIFBT_IX.A", "612PCPIFBT_IX.M", "612PCPIFBT_IX.Q" },
            //    new[] { "SCALE", "INDICATOR", "COUNTRY" },
            //    "MA"
            //);


            var mrq = req.CreateMetadataRequest();
            var mrs = client.GetMetadata(mrq);
            ShowMetadataResponse(mrs);

            var orq = req.CreateObservationsRequest();
            var or = client.GetObservations(orq);
            ShowObservationResponse(or);

            channel.ShutdownAsync().Wait();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
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
    }

    internal class Request
    {
        private readonly string _sourceName;
        private readonly IEnumerable<string> _seriesList;
        private readonly IEnumerable<string> _headerList;
        private readonly string _frequencyList;

        public Request(string sourceName, IEnumerable<string> seriesList, IEnumerable<string> headerList, string frequencyList)
        {
            _sourceName = sourceName;
            _seriesList = seriesList;
            _headerList = headerList;
            _frequencyList = frequencyList;
        }

        public GetMetadataRequest CreateMetadataRequest()
        {
            var request = new GetMetadataRequest();
            request.Requestmetadata.Add(new Dictionary<string, string>() { { "version", "0.9" } });

            request.Metadataheaders.Add(_headerList);
            request.Keys.Add(CreateSourceSeriesKeyListFrom(_sourceName, _seriesList));

            return request;
        }

        public GetObservationsRequest CreateObservationsRequest()
        {
            var request = new GetObservationsRequest();
            request.Requestmetadata.Add(new Dictionary<string, string>() { { "version", "0.9" } });

            request.Frequencies = _frequencyList;
            request.Keys.Add(CreateSourceSeriesKeyListFrom(_sourceName, _seriesList));

            return request;
        }

        private IEnumerable<SourceSeriesKey> CreateSourceSeriesKeyListFrom(string sourceName, IEnumerable<string> _seriesList)
        {
            return _seriesList.Select(s => new SourceSeriesKey() { Sourcename = sourceName, Seriesname = s }).ToList();
        }
    }
}
