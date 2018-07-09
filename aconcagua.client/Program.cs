// Copyright 2015 gRPC authors.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections.Generic;
using System.Linq;
using Grpc.Core;
using Google.Protobuf.Collections;

namespace aconcagua.client
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var channel = new Channel("127.0.0.1:50051", ChannelCredentials.Insecure);

            var client = new Aconcagua.AconcaguaClient(channel);
            var metadataRequest = CreateMetadataRequest();

            var reply = client.GetMetadata(metadataRequest);


            Console.WriteLine($"Sourcename[0]: {reply.Datalist.ElementAt(0).Key.Sourcename}");
            Console.WriteLine($"Seriesname[0]: {reply.Datalist.ElementAt(0).Key.Seriesname}");
            Console.WriteLine($"Data[0][0]: {reply.Datalist.ElementAt(0).Data[0]}");
            Console.WriteLine($"Metadata: {reply.Metadataheaders[0]}");

            channel.ShutdownAsync().Wait();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        private static GetMetadataRequest CreateMetadataRequest()
        {
            var request = new GetMetadataRequest();

            request.Metadataheaders.Add(new [] {
                "h1", "h2"
            });

            //TODO [jc]: what do we do with the requestmetadata?
            request.Requestmetadata.Add(new Dictionary<string, string>() { { "version", "0.9" } });
            request.Keys.AddRange(new [] { new SourceSeriesKey() { Sourcename = "null://test.me", Seriesname = "a01"}, });
            return request;
        }
    }
}
