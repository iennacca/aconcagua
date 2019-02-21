﻿using System;
using System.Collections.Generic;
using System.Linq;
using Grpc.Core;
using Aconcagua.Proto;

namespace aconcagua.client
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var channel = new Channel("127.0.0.1:50451", ChannelCredentials.Insecure);
            var client = new TimeseriesDataService.TimeseriesDataServiceClient(channel);
            var empty = new Google.Protobuf.WellKnownTypes.Empty();

            var vrs = client.GetVersion(empty);
            Console.WriteLine($"Version: {vrs.Version}");

            var srq = GetSeriesKeys.CreateRequest(
                new[] { "dmx:.\\..\\..\\..\\..\\data\\sample.dmx" },
                new[] { "description:%" }
            );
            var sr = client.GetSeriesKeys(srq);
            GetSeriesKeys.ShowResponse(sr);

            var mrq = GetMetadata.CreateRequest(
                "dmx:.\\..\\..\\..\\..\\data\\sample.dmx",
                new[] { "911BE", "911BEA", "BCA_GDP" },
                new[] { "scale", "unit", "description" }
            );
            //var mrq = GetMetadata.CreateRequest(
            //    "ecos:\\ECDATA_CPI",
            //    new[] { "312PCPIFBT_IX.A", "612PCPIFBT_IX.M", "612PCPIFBT_IX.Q" },
            //    new[] { "SCALE", "INDICATOR", "COUNTRY" }
            //);

            var mr = client.GetMetadata(mrq);
            GetMetadata.ShowResponse(mr);

            var orq = GetObservations.CreateRequest(
                "dmx:.\\..\\..\\..\\..\\data\\sample.dmx",
                new[] { "911BE", "911BEA", "BCA_GDP" },
                "MA"
            );
            //var orq = GetObservations.CreateRequest(
            //    "ecos:\\ECDATA_CPI",
            //    new[] { "312PCPIFBT_IX.A", "612PCPIFBT_IX.M", "612PCPIFBT_IX.Q" },
            //    "MA"
            //);

            var or = client.GetObservations(orq);
            GetObservations.ShowResponse(or);

            channel.ShutdownAsync().Wait();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }

    internal static class GetSeriesKeys
    {
        public static GetSeriesKeysRequest CreateRequest(IEnumerable<string> sourceNames, IEnumerable<string> filters)
        {
            var request = new GetSeriesKeysRequest();
            request.Requestmetadata.Add(new Dictionary<string, string>() { { "version", "0.9" } });

            request.Sourcenames.Add(sourceNames);
            foreach (var s in filters)
            {
                var ss = s.Split(':');
                request.Filters.Add(ss[0], ss[1]);
            }

            return request;
        }

        public static void ShowResponse(GetSeriesKeysResponse response)
        {
            var i = 0;
            Console.WriteLine($"GetSeriesKeys");
            foreach (var ts in response.Keys)
            {
                Console.WriteLine($"Sourcename[{i}]: {ts.Sourcename}/{ts.Seriesname}");
                i++;
            }
        }
    }

    internal static class GetMetadata
    {
        public static GetMetadataRequest CreateRequest(string source, IEnumerable<string> seriesList, IEnumerable<string> headerList)
        {
            var request = new GetMetadataRequest();
            request.Requestmetadata.Add(new Dictionary<string, string>() { { "version", "0.9" } });

            request.Metadataheaders.Add(headerList);
            request.Keys.Add(SourceSeriesKeyList.Create(source, seriesList));

            return request;
        }

        public static void ShowResponse(GetMetadataResponse response)
        {
            var i = 0;
            var f = string.Join(",", response.Metadataheaders);
            Console.WriteLine($"GetMetadata[{f}]");
            foreach (var ts in response.Seriesdata)
            {
                Console.WriteLine($"Sourcename[{i}]: {ts.Key.Sourcename}/{ts.Key.Seriesname}");

                foreach (var pair in response.Metadataheaders.Zip(ts.Values, Tuple.Create))
                    Console.WriteLine($"    {pair.Item1}: {pair.Item2}");
                i++;
            }
        }
    }

    internal static class GetObservations
    {
        public static GetObservationsRequest CreateRequest(string source, IEnumerable<string> seriesList, string frequencyList)
        {
            var request = new GetObservationsRequest();
            request.Requestmetadata.Add(new Dictionary<string, string>() { { "version", "0.9" } });

            request.Frequencies = frequencyList;
            request.Keys.Add(SourceSeriesKeyList.Create(source, seriesList));

            return request;
        }

        public static void ShowResponse(GetObservationsResponse response)
        {
            var i = 0;

            Console.WriteLine($"GetObservations[{response.Frequencies}]");
            foreach (var ts in response.Seriesdata)
            {
                Console.WriteLine($"Sourcename[{i}]: {ts.Key.Sourcename}/{ts.Key.Seriesname}");

                foreach (var pair in ts.Values.Keys.Zip(ts.Values.Values, Tuple.Create))
                    Console.WriteLine($"    {pair.Item1}: {pair.Item2}");
                i++;
            }
        }

    }

    internal static class SourceSeriesKeyList
    {
        public static IEnumerable<SourceSeriesKey> Create(string sourceName, IEnumerable<string> _seriesList)
        {
            return _seriesList.Select(s => new SourceSeriesKey() { Sourcename = sourceName, Seriesname = s }).ToList();
        }
    }
}
