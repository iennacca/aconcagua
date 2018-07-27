// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: aconcagua.proto
// </auto-generated>
// Original file comments:
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
//
#pragma warning disable 0414, 1591
#region Designer generated code

using grpc = global::Grpc.Core;

public static partial class Aconcagua
{
  static readonly string __ServiceName = "Aconcagua";

  static readonly grpc::Marshaller<global::Google.Protobuf.WellKnownTypes.Empty> __Marshaller_Empty = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Google.Protobuf.WellKnownTypes.Empty.Parser.ParseFrom);
  static readonly grpc::Marshaller<global::GetVersionResponse> __Marshaller_GetVersionResponse = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::GetVersionResponse.Parser.ParseFrom);
  static readonly grpc::Marshaller<global::GetMetadataRequest> __Marshaller_GetMetadataRequest = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::GetMetadataRequest.Parser.ParseFrom);
  static readonly grpc::Marshaller<global::GetMetadataResponse> __Marshaller_GetMetadataResponse = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::GetMetadataResponse.Parser.ParseFrom);
  static readonly grpc::Marshaller<global::GetObservationsRequest> __Marshaller_GetObservationsRequest = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::GetObservationsRequest.Parser.ParseFrom);
  static readonly grpc::Marshaller<global::GetObservationsResponse> __Marshaller_GetObservationsResponse = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::GetObservationsResponse.Parser.ParseFrom);

  static readonly grpc::Method<global::Google.Protobuf.WellKnownTypes.Empty, global::GetVersionResponse> __Method_GetVersion = new grpc::Method<global::Google.Protobuf.WellKnownTypes.Empty, global::GetVersionResponse>(
      grpc::MethodType.Unary,
      __ServiceName,
      "GetVersion",
      __Marshaller_Empty,
      __Marshaller_GetVersionResponse);

  static readonly grpc::Method<global::GetMetadataRequest, global::GetMetadataResponse> __Method_GetMetadata = new grpc::Method<global::GetMetadataRequest, global::GetMetadataResponse>(
      grpc::MethodType.Unary,
      __ServiceName,
      "GetMetadata",
      __Marshaller_GetMetadataRequest,
      __Marshaller_GetMetadataResponse);

  static readonly grpc::Method<global::GetObservationsRequest, global::GetObservationsResponse> __Method_GetObservations = new grpc::Method<global::GetObservationsRequest, global::GetObservationsResponse>(
      grpc::MethodType.Unary,
      __ServiceName,
      "GetObservations",
      __Marshaller_GetObservationsRequest,
      __Marshaller_GetObservationsResponse);

  /// <summary>Service descriptor</summary>
  public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
  {
    get { return global::AconcaguaReflection.Descriptor.Services[0]; }
  }

  /// <summary>Base class for server-side implementations of Aconcagua</summary>
  public abstract partial class AconcaguaBase
  {
    public virtual global::System.Threading.Tasks.Task<global::GetVersionResponse> GetVersion(global::Google.Protobuf.WellKnownTypes.Empty request, grpc::ServerCallContext context)
    {
      throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
    }

    public virtual global::System.Threading.Tasks.Task<global::GetMetadataResponse> GetMetadata(global::GetMetadataRequest request, grpc::ServerCallContext context)
    {
      throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
    }

    public virtual global::System.Threading.Tasks.Task<global::GetObservationsResponse> GetObservations(global::GetObservationsRequest request, grpc::ServerCallContext context)
    {
      throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
    }

  }

  /// <summary>Client for Aconcagua</summary>
  public partial class AconcaguaClient : grpc::ClientBase<AconcaguaClient>
  {
    /// <summary>Creates a new client for Aconcagua</summary>
    /// <param name="channel">The channel to use to make remote calls.</param>
    public AconcaguaClient(grpc::Channel channel) : base(channel)
    {
    }
    /// <summary>Creates a new client for Aconcagua that uses a custom <c>CallInvoker</c>.</summary>
    /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
    public AconcaguaClient(grpc::CallInvoker callInvoker) : base(callInvoker)
    {
    }
    /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
    protected AconcaguaClient() : base()
    {
    }
    /// <summary>Protected constructor to allow creation of configured clients.</summary>
    /// <param name="configuration">The client configuration.</param>
    protected AconcaguaClient(ClientBaseConfiguration configuration) : base(configuration)
    {
    }

    public virtual global::GetVersionResponse GetVersion(global::Google.Protobuf.WellKnownTypes.Empty request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
    {
      return GetVersion(request, new grpc::CallOptions(headers, deadline, cancellationToken));
    }
    public virtual global::GetVersionResponse GetVersion(global::Google.Protobuf.WellKnownTypes.Empty request, grpc::CallOptions options)
    {
      return CallInvoker.BlockingUnaryCall(__Method_GetVersion, null, options, request);
    }
    public virtual grpc::AsyncUnaryCall<global::GetVersionResponse> GetVersionAsync(global::Google.Protobuf.WellKnownTypes.Empty request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
    {
      return GetVersionAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
    }
    public virtual grpc::AsyncUnaryCall<global::GetVersionResponse> GetVersionAsync(global::Google.Protobuf.WellKnownTypes.Empty request, grpc::CallOptions options)
    {
      return CallInvoker.AsyncUnaryCall(__Method_GetVersion, null, options, request);
    }
    public virtual global::GetMetadataResponse GetMetadata(global::GetMetadataRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
    {
      return GetMetadata(request, new grpc::CallOptions(headers, deadline, cancellationToken));
    }
    public virtual global::GetMetadataResponse GetMetadata(global::GetMetadataRequest request, grpc::CallOptions options)
    {
      return CallInvoker.BlockingUnaryCall(__Method_GetMetadata, null, options, request);
    }
    public virtual grpc::AsyncUnaryCall<global::GetMetadataResponse> GetMetadataAsync(global::GetMetadataRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
    {
      return GetMetadataAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
    }
    public virtual grpc::AsyncUnaryCall<global::GetMetadataResponse> GetMetadataAsync(global::GetMetadataRequest request, grpc::CallOptions options)
    {
      return CallInvoker.AsyncUnaryCall(__Method_GetMetadata, null, options, request);
    }
    public virtual global::GetObservationsResponse GetObservations(global::GetObservationsRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
    {
      return GetObservations(request, new grpc::CallOptions(headers, deadline, cancellationToken));
    }
    public virtual global::GetObservationsResponse GetObservations(global::GetObservationsRequest request, grpc::CallOptions options)
    {
      return CallInvoker.BlockingUnaryCall(__Method_GetObservations, null, options, request);
    }
    public virtual grpc::AsyncUnaryCall<global::GetObservationsResponse> GetObservationsAsync(global::GetObservationsRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
    {
      return GetObservationsAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
    }
    public virtual grpc::AsyncUnaryCall<global::GetObservationsResponse> GetObservationsAsync(global::GetObservationsRequest request, grpc::CallOptions options)
    {
      return CallInvoker.AsyncUnaryCall(__Method_GetObservations, null, options, request);
    }
    /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
    protected override AconcaguaClient NewInstance(ClientBaseConfiguration configuration)
    {
      return new AconcaguaClient(configuration);
    }
  }

  /// <summary>Creates service definition that can be registered with a server</summary>
  /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
  public static grpc::ServerServiceDefinition BindService(AconcaguaBase serviceImpl)
  {
    return grpc::ServerServiceDefinition.CreateBuilder()
        .AddMethod(__Method_GetVersion, serviceImpl.GetVersion)
        .AddMethod(__Method_GetMetadata, serviceImpl.GetMetadata)
        .AddMethod(__Method_GetObservations, serviceImpl.GetObservations).Build();
  }

}
#endregion
