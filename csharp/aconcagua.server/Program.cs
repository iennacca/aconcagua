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
using System.Threading.Tasks;
using aconcagua.data;
using aconcagua.data.factory;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Aconcagua.Proto;
using Google.Rpc;
using infrastructure;

namespace aconcagua.server
{
    internal class Program
    {
        private const int Port = 50451;

        public static void Main(string[] args)
        {
            var server = new Server
            {
                Services = { TimeseriesDataService.BindService(new AconcaguaServer()) },
                Ports = { new ServerPort("localhost", Port, ServerCredentials.Insecure) }
            };

            IOCContainer.Logger.Info($"Aconcagua server running");
            IOCContainer.Logger.Info($"Port: {Port}");
            IOCContainer.Logger.Info($"UserName: {Environment.UserName}");
            IOCContainer.Logger.Info($"UserDomainName: {Environment.UserDomainName}");
            IOCContainer.Logger.Info("Press any key to stop the server...");
            server.Start();
            Console.ReadKey();

            server.ShutdownAsync().Wait();
        }
    }

    internal class AconcaguaServer : TimeseriesDataService.TimeseriesDataServiceBase
    {
        public override Task<GetVersionResponse> GetVersion(Empty request, ServerCallContext context)
        {
            IOCContainer.Logger.Info("GetVersion() called");
            return Task.FromResult(ServerHandler.CallGetVersion(request));
        }

        public override Task<GetSeriesKeysResponse> GetSeriesKeys(GetSeriesKeysRequest request, ServerCallContext context)
        {
            IOCContainer.Logger.Info("GetSeriesKeys() called");
            return Task.FromResult(ServerHandler.CallGetSeriesKeys(request));
        }

        public override Task<GetMetadataResponse> GetMetadata(GetMetadataRequest request, ServerCallContext context)
        {
            IOCContainer.Logger.Info("GetMetadata() called");
            return Task.FromResult(ServerHandler.CallGetMetadata(request));
        }

        public override Task<GetObservationsResponse> GetObservations(GetObservationsRequest request, ServerCallContext context)
        {
            IOCContainer.Logger.Info("GetObservations() called");
            return Task.FromResult(ServerHandler.CallGetObservations(request));
        }
    }

    internal class ServerHandler
    {
        public static GetVersionResponse CallGetVersion(Empty request)
        {
            return new GetVersionResponse() { Version = "1.0" };
        }

        public static GetSeriesKeysResponse CallGetSeriesKeys(GetSeriesKeysRequest request)
        {
            var response = new GetSeriesKeysResponse();
            try
            {
                var tssFactory = TimeseriesSourceFactory.Factory;

                response.Responsemetadata.Add(request.Requestmetadata);

                foreach (var sourcename in request.Sourcenames)
                {
                    var d = new Dictionary<string, string>();
                    foreach (var kv in request.Filters)
                        d[kv.Key] = kv.Value;

                    response.Keys.AddRange(
                        tssFactory[sourcename].GetSeriesKeys(d).Select(
                            t => new SourceSeriesKey() { Seriesname = t.Key, Sourcename = sourcename }));
                }
            }
            catch (Exception ex)
            {
                IOCContainer.Logger.Error(ex.Message);
                throw;
            }

            IOCContainer.Logger.Info($"GetSeriesKeyResponse: {response.Keys.Count} series found");
            return response;
        }

        public static GetMetadataResponse CallGetMetadata(GetMetadataRequest request)
        {
            var response = new GetMetadataResponse();
            try
            {
                var tssFactory = TimeseriesSourceFactory.Factory;

                //TODO [jc]: what do we do with the requestmetadata?
                response.Responsemetadata.Add(request.Requestmetadata);
                response.Metadataheaders.Add(request.Metadataheaders);

                foreach (var ssKey in request.Keys)
                {
                    var tsList = tssFactory[ssKey.Sourcename].GetMetadata(
                        new[] { new TimeseriesKey(ssKey.Seriesname) },
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
                        m.Values.AddRange(ts.Metadata.Values);
                        response.Seriesdata.Add(m);
                    }
                }
            }
            catch (Exception ex)
            {
                IOCContainer.Logger.Error(ex.Message);
                throw;
            }
            IOCContainer.Logger.Info($"GetMetadataResponse: {response.Seriesdata.Count} series found");
            return response;
        }

        public static GetObservationsResponse CallGetObservations(GetObservationsRequest request)
        {
            var response = new GetObservationsResponse();
            var tssFactory = TimeseriesSourceFactory.Factory;

            //TODO [jc]: what do we do with the requestmetadata?
            response.Responsemetadata.Add(request.Requestmetadata);

            foreach (var ssKey in request.Keys)
            {
                var tsList = tssFactory[ssKey.Sourcename].GetObservations(
                    new[] { new TimeseriesKey(ssKey.Seriesname) },
                    request.Frequencies);


                foreach (var ts in tsList)
                {
                    var o = new ObservationsList
                    {
                        Key = new SourceSeriesKey()
                        {
                            Sourcename = ts.SourceKey.Key.ToString(),
                            Seriesname = ts.SeriesKey.Key
                        }
                    };
                    o.Values.Add(ts.Observations);
                    o.Messagestatus = new Google.Rpc.Status();
                    o.Messagestatus = new Google.Rpc.Status
                    {
                        Code = ts.Status.Code,
                        Message = ts.Status.Message
                    };

                    response.Seriesdata.Add(o);
                }
            }
            response.Frequencies = request.Frequencies;
            IOCContainer.Logger.Info($"GetObservationsResponse: {response.Seriesdata.Count} series found");
            return response;
        }
    }
}
