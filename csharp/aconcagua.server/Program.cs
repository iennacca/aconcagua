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
using System.Threading.Tasks;
using aconcagua.data;
using Grpc.Core;

namespace aconcagua.server
{
    internal class AconcaguaServer : Aconcagua.AconcaguaBase
    {
        public override Task<GetMetadataResponse> GetMetadata(GetMetadataRequest request, ServerCallContext context)
        {
            Console.WriteLine("GetMetadata() called");
            return Task.FromResult(CreateMetadataResponse(request));
        }

        public override Task<GetObservationsResponse> GetObservations(GetObservationsRequest request, ServerCallContext context)
        {
            Console.WriteLine("GetObservations() called");
            return Task.FromResult(new GetObservationsResponse());
        }


        // Helper functions
        private static GetMetadataResponse CreateMetadataResponse(GetMetadataRequest request)
        {
            var reply = new GetMetadataResponse();
            try
            {
                var tssFactory = TimeseriesSourceFactory.Factory;

                //TODO [jc]: what do we do with the requestmetadata?
                reply.Responsemetadata.Add(request.Requestmetadata);
                reply.Metadataheaders.Add(request.Metadataheaders);

                foreach (var ssKey in request.Keys)
                {
                    var tsList = tssFactory[ssKey.Sourcename].Get(
                        new[] {new TimeseriesKey(ssKey.Seriesname)},
                        request.Metadataheaders);

                    foreach (var ts in tsList)
                    {
                        var m = new MetadataList
                        {
                            Key = new SourceSeriesKey()
                            {
                                Sourcename = ts.SourceKey.Key.ToString(),
                                Seriesname = ts.SeriesKey.Key
                            }
                        };
                        m.Data.AddRange(ts.HeaderData.Values);
                        reply.Datalist.Add(m);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            return reply;
        }
    }

    internal class Program
    {
        private const int Port = 50051;

        public static void Main(string[] args)
        {
            var server = new Server
            {
                Services = { Aconcagua.BindService(new AconcaguaServer()) },
                Ports = { new ServerPort("localhost", Port, ServerCredentials.Insecure) }
            };
            server.Start();

            Console.WriteLine("Aconcagua server listening on port " + Port);
            Console.WriteLine("UserName: " + Environment.UserName);
            Console.WriteLine($"UserDomainName: {Environment.UserDomainName}");
            Console.WriteLine("Press any key to stop the server...");
            Console.ReadKey();

            server.ShutdownAsync().Wait();
        }
    }
}
