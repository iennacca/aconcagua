using System;
using System.Collections.Generic;
using System.Linq;
using Grpc.Core;

namespace aconcagua.client
{
    internal class Program
    {
        private static char[] _delimiters = new[] {','};
        const string sourceName = "dmx:\\C:\\Users\\Jerry\\Projects\\aconcagua\\data\\sample.dmx";
        const string strSeriesList = "911BCA_GDP, 911BCAXGT_GDP,911BCAXT,911BE,911BEA,911BEAB,911BEAI,911BEAM,911BEAO,911BEAP,911BED,911BER,911BF";
        const string strHeaderList = "scale, unit, description";
        const string frequencyList = "MA";

        public static void Main(string[] args)
        {
            var channel = new Channel("127.0.0.1:50051", ChannelCredentials.Insecure);
            var client = new Aconcagua.AconcaguaClient(channel);
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

            var headers = createStringListFrom(strHeaderList);
            request.Metadataheaders.Add(headers);
            request.Keys.Add(createSourceSeriesKeyListFrom(sourceName, strSeriesList));

            return request;
        }

        private static IEnumerable<string> createStringListFrom(string strList)
        {
            return strList.Split(_delimiters, StringSplitOptions.RemoveEmptyEntries).AsEnumerable();
        }

        private static IEnumerable<SourceSeriesKey> createSourceSeriesKeyListFrom(string sourceName, string strSeries)
        {
            return strSeries.Split(_delimiters, StringSplitOptions.RemoveEmptyEntries).
                Select(s => new SourceSeriesKey() {Sourcename = sourceName, Seriesname = s}).ToList();
        }

        private static void ShowMetadataResponse(GetMetadataResponse response)
        {
            var i = 0;

            foreach (var ts in response.Datalist)
            {
                Console.WriteLine($"Sourcename[{i}]: {ts.Key.Sourcename}/{ts.Key.Seriesname}");

                foreach (var pair in ts.Data.Zip(response.Metadataheaders, Tuple.Create))
                    Console.WriteLine($"    {pair.Item2}: {pair.Item1}");
                i++;
            }
        }

        #endregion

        #region GetObservations

        private static GetObservationsRequest CreateObservationsRequest()
        {
            var request = new GetObservationsRequest();
            request.Requestmetadata.Add(new Dictionary<string, string>() { { "version", "0.9" } });

            request.Frequencies = frequencyList;
            request.Keys.Add(createSourceSeriesKeyListFrom(sourceName, strSeriesList));

            return request;
        }

        private static void ShowObservationResponse(GetObservationsResponse response)
        {
            var i = 0;

            foreach (var ts in response.Datalist)
            {
                Console.WriteLine($"Sourcename[{i}]: {ts.Key.Sourcename}/{ts.Key.Seriesname}");

                foreach (var pair in ts.Values.Keys.Zip(ts.Values.Values, Tuple.Create))
                    Console.WriteLine($"    {pair.Item2}: {pair.Item1}");
                i++;
            }
        }

        #endregion
    }
}
