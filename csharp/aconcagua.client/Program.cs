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
            var request = CreateMetadataRequest();
            var response = client.GetMetadata(request);
            ShowMetadataResponse(response);

            channel.ShutdownAsync().Wait();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        private static GetMetadataRequest CreateMetadataRequest()
        {
            var request = new GetMetadataRequest();
            request.Requestmetadata.Add(new Dictionary<string, string>() { { "version", "0.9" } });
            request.Metadataheaders.Add(new [] { "scale", "unit" });

            foreach (var i in Enumerable.Range(1,5))
                request.Keys.Add(new [] { new SourceSeriesKey() { Sourcename = "null://test", Seriesname = $"series{i}"} });

            return request;
        }

        private static void ShowMetadataResponse(GetMetadataResponse response)
        {
            var i = 0;

            foreach (var ts in response.Datalist)
            {
                Console.WriteLine($"Sourcename[{i}]: {ts.Key.Sourcename}/{ts.Key.Seriesname}");

                foreach (var dataPoint in ts.Data)
                    Console.WriteLine($"    Data: {dataPoint}");
                i++;
            }
        }
    }
}
