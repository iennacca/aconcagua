# Generated by the gRPC Python protocol compiler plugin. DO NOT EDIT!
import grpc

import aconcagua_pb2 as aconcagua__pb2
from google.protobuf import empty_pb2 as google_dot_protobuf_dot_empty__pb2


class AconcaguaStub(object):
  # missing associated documentation comment in .proto file
  pass

  def __init__(self, channel):
    """Constructor.

    Args:
      channel: A grpc.Channel.
    """
    self.GetVersion = channel.unary_unary(
        '/Aconcagua/GetVersion',
        request_serializer=google_dot_protobuf_dot_empty__pb2.Empty.SerializeToString,
        response_deserializer=aconcagua__pb2.GetVersionResponse.FromString,
        )
    self.GetMetadata = channel.unary_unary(
        '/Aconcagua/GetMetadata',
        request_serializer=aconcagua__pb2.GetMetadataRequest.SerializeToString,
        response_deserializer=aconcagua__pb2.GetMetadataResponse.FromString,
        )
    self.GetObservations = channel.unary_unary(
        '/Aconcagua/GetObservations',
        request_serializer=aconcagua__pb2.GetObservationsRequest.SerializeToString,
        response_deserializer=aconcagua__pb2.GetObservationsResponse.FromString,
        )


class AconcaguaServicer(object):
  # missing associated documentation comment in .proto file
  pass

  def GetVersion(self, request, context):
    # missing associated documentation comment in .proto file
    pass
    context.set_code(grpc.StatusCode.UNIMPLEMENTED)
    context.set_details('Method not implemented!')
    raise NotImplementedError('Method not implemented!')

  def GetMetadata(self, request, context):
    # missing associated documentation comment in .proto file
    pass
    context.set_code(grpc.StatusCode.UNIMPLEMENTED)
    context.set_details('Method not implemented!')
    raise NotImplementedError('Method not implemented!')

  def GetObservations(self, request, context):
    # missing associated documentation comment in .proto file
    pass
    context.set_code(grpc.StatusCode.UNIMPLEMENTED)
    context.set_details('Method not implemented!')
    raise NotImplementedError('Method not implemented!')


def add_AconcaguaServicer_to_server(servicer, server):
  rpc_method_handlers = {
      'GetVersion': grpc.unary_unary_rpc_method_handler(
          servicer.GetVersion,
          request_deserializer=google_dot_protobuf_dot_empty__pb2.Empty.FromString,
          response_serializer=aconcagua__pb2.GetVersionResponse.SerializeToString,
      ),
      'GetMetadata': grpc.unary_unary_rpc_method_handler(
          servicer.GetMetadata,
          request_deserializer=aconcagua__pb2.GetMetadataRequest.FromString,
          response_serializer=aconcagua__pb2.GetMetadataResponse.SerializeToString,
      ),
      'GetObservations': grpc.unary_unary_rpc_method_handler(
          servicer.GetObservations,
          request_deserializer=aconcagua__pb2.GetObservationsRequest.FromString,
          response_serializer=aconcagua__pb2.GetObservationsResponse.SerializeToString,
      ),
  }
  generic_handler = grpc.method_handlers_generic_handler(
      'Aconcagua', rpc_method_handlers)
  server.add_generic_rpc_handlers((generic_handler,))
