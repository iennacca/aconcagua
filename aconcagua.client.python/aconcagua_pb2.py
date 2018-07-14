# Generated by the protocol buffer compiler.  DO NOT EDIT!
# source: aconcagua.proto

import sys
_b=sys.version_info[0]<3 and (lambda x:x) or (lambda x:x.encode('latin1'))
from google.protobuf import descriptor as _descriptor
from google.protobuf import message as _message
from google.protobuf import reflection as _reflection
from google.protobuf import symbol_database as _symbol_database
from google.protobuf import descriptor_pb2
# @@protoc_insertion_point(imports)

_sym_db = _symbol_database.Default()




DESCRIPTOR = _descriptor.FileDescriptor(
  name='aconcagua.proto',
  package='',
  syntax='proto3',
  serialized_pb=_b('\n\x0f\x61\x63oncagua.proto\"\xc8\x01\n\x12GetMetadataRequest\x12\x41\n\x0frequestmetadata\x18\x01 \x03(\x0b\x32(.GetMetadataRequest.RequestmetadataEntry\x12\x17\n\x0fmetadataheaders\x18\x02 \x03(\t\x12\x1e\n\x04keys\x18\x03 \x03(\x0b\x32\x10.SourceSeriesKey\x1a\x36\n\x14RequestmetadataEntry\x12\x0b\n\x03key\x18\x01 \x01(\t\x12\r\n\x05value\x18\x02 \x01(\t:\x02\x38\x01\"\xbf\x01\n\x10GetMetadataReply\x12;\n\rreplymetadata\x18\x01 \x03(\x0b\x32$.GetMetadataReply.ReplymetadataEntry\x12\x17\n\x0fmetadataheaders\x18\x02 \x03(\t\x12\x1f\n\x08\x64\x61talist\x18\x03 \x03(\x0b\x32\r.MetadataList\x1a\x34\n\x12ReplymetadataEntry\x12\x0b\n\x03key\x18\x01 \x01(\t\x12\r\n\x05value\x18\x02 \x01(\t:\x02\x38\x01\"\xd3\x01\n\x16GetObservationsRequest\x12\x45\n\x0frequestmetadata\x18\x01 \x03(\x0b\x32,.GetObservationsRequest.RequestmetadataEntry\x12\x1a\n\x12observationheaders\x18\x02 \x03(\t\x12\x1e\n\x04keys\x18\x03 \x03(\x0b\x32\x10.SourceSeriesKey\x1a\x36\n\x14RequestmetadataEntry\x12\x0b\n\x03key\x18\x01 \x01(\t\x12\r\n\x05value\x18\x02 \x01(\t:\x02\x38\x01\"\xce\x01\n\x14GetObservationsReply\x12?\n\rreplymetadata\x18\x01 \x03(\x0b\x32(.GetObservationsReply.ReplymetadataEntry\x12\x1a\n\x12observationheaders\x18\x02 \x03(\t\x12#\n\x08\x64\x61talist\x18\x03 \x03(\x0b\x32\x11.ObservationsList\x1a\x34\n\x12ReplymetadataEntry\x12\x0b\n\x03key\x18\x01 \x01(\t\x12\r\n\x05value\x18\x02 \x01(\t:\x02\x38\x01\";\n\x0cMetadataList\x12\x1d\n\x03key\x18\x01 \x01(\x0b\x32\x10.SourceSeriesKey\x12\x0c\n\x04\x64\x61ta\x18\x02 \x03(\t\"?\n\x10ObservationsList\x12\x1d\n\x03key\x18\x01 \x01(\x0b\x32\x10.SourceSeriesKey\x12\x0c\n\x04\x64\x61ta\x18\x02 \x03(\x02\"9\n\x0fSourceSeriesKey\x12\x12\n\nsourcename\x18\x01 \x01(\t\x12\x12\n\nseriesname\x18\x02 \x01(\t2\x89\x01\n\tAconcagua\x12\x37\n\x0bGetMetadata\x12\x13.GetMetadataRequest\x1a\x11.GetMetadataReply\"\x00\x12\x43\n\x0fGetObservations\x12\x17.GetObservationsRequest\x1a\x15.GetObservationsReply\"\x00\x42+\n\x11io.grpc.aconcaguaB\x0e\x41\x63oncaguaProtoP\x01\xa2\x02\x03\x41\x43\x41\x62\x06proto3')
)




_GETMETADATAREQUEST_REQUESTMETADATAENTRY = _descriptor.Descriptor(
  name='RequestmetadataEntry',
  full_name='GetMetadataRequest.RequestmetadataEntry',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  fields=[
    _descriptor.FieldDescriptor(
      name='key', full_name='GetMetadataRequest.RequestmetadataEntry.key', index=0,
      number=1, type=9, cpp_type=9, label=1,
      has_default_value=False, default_value=_b("").decode('utf-8'),
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None, file=DESCRIPTOR),
    _descriptor.FieldDescriptor(
      name='value', full_name='GetMetadataRequest.RequestmetadataEntry.value', index=1,
      number=2, type=9, cpp_type=9, label=1,
      has_default_value=False, default_value=_b("").decode('utf-8'),
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None, file=DESCRIPTOR),
  ],
  extensions=[
  ],
  nested_types=[],
  enum_types=[
  ],
  options=_descriptor._ParseOptions(descriptor_pb2.MessageOptions(), _b('8\001')),
  is_extendable=False,
  syntax='proto3',
  extension_ranges=[],
  oneofs=[
  ],
  serialized_start=166,
  serialized_end=220,
)

_GETMETADATAREQUEST = _descriptor.Descriptor(
  name='GetMetadataRequest',
  full_name='GetMetadataRequest',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  fields=[
    _descriptor.FieldDescriptor(
      name='requestmetadata', full_name='GetMetadataRequest.requestmetadata', index=0,
      number=1, type=11, cpp_type=10, label=3,
      has_default_value=False, default_value=[],
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None, file=DESCRIPTOR),
    _descriptor.FieldDescriptor(
      name='metadataheaders', full_name='GetMetadataRequest.metadataheaders', index=1,
      number=2, type=9, cpp_type=9, label=3,
      has_default_value=False, default_value=[],
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None, file=DESCRIPTOR),
    _descriptor.FieldDescriptor(
      name='keys', full_name='GetMetadataRequest.keys', index=2,
      number=3, type=11, cpp_type=10, label=3,
      has_default_value=False, default_value=[],
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None, file=DESCRIPTOR),
  ],
  extensions=[
  ],
  nested_types=[_GETMETADATAREQUEST_REQUESTMETADATAENTRY, ],
  enum_types=[
  ],
  options=None,
  is_extendable=False,
  syntax='proto3',
  extension_ranges=[],
  oneofs=[
  ],
  serialized_start=20,
  serialized_end=220,
)


_GETMETADATAREPLY_REPLYMETADATAENTRY = _descriptor.Descriptor(
  name='ReplymetadataEntry',
  full_name='GetMetadataReply.ReplymetadataEntry',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  fields=[
    _descriptor.FieldDescriptor(
      name='key', full_name='GetMetadataReply.ReplymetadataEntry.key', index=0,
      number=1, type=9, cpp_type=9, label=1,
      has_default_value=False, default_value=_b("").decode('utf-8'),
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None, file=DESCRIPTOR),
    _descriptor.FieldDescriptor(
      name='value', full_name='GetMetadataReply.ReplymetadataEntry.value', index=1,
      number=2, type=9, cpp_type=9, label=1,
      has_default_value=False, default_value=_b("").decode('utf-8'),
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None, file=DESCRIPTOR),
  ],
  extensions=[
  ],
  nested_types=[],
  enum_types=[
  ],
  options=_descriptor._ParseOptions(descriptor_pb2.MessageOptions(), _b('8\001')),
  is_extendable=False,
  syntax='proto3',
  extension_ranges=[],
  oneofs=[
  ],
  serialized_start=362,
  serialized_end=414,
)

_GETMETADATAREPLY = _descriptor.Descriptor(
  name='GetMetadataReply',
  full_name='GetMetadataReply',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  fields=[
    _descriptor.FieldDescriptor(
      name='replymetadata', full_name='GetMetadataReply.replymetadata', index=0,
      number=1, type=11, cpp_type=10, label=3,
      has_default_value=False, default_value=[],
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None, file=DESCRIPTOR),
    _descriptor.FieldDescriptor(
      name='metadataheaders', full_name='GetMetadataReply.metadataheaders', index=1,
      number=2, type=9, cpp_type=9, label=3,
      has_default_value=False, default_value=[],
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None, file=DESCRIPTOR),
    _descriptor.FieldDescriptor(
      name='datalist', full_name='GetMetadataReply.datalist', index=2,
      number=3, type=11, cpp_type=10, label=3,
      has_default_value=False, default_value=[],
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None, file=DESCRIPTOR),
  ],
  extensions=[
  ],
  nested_types=[_GETMETADATAREPLY_REPLYMETADATAENTRY, ],
  enum_types=[
  ],
  options=None,
  is_extendable=False,
  syntax='proto3',
  extension_ranges=[],
  oneofs=[
  ],
  serialized_start=223,
  serialized_end=414,
)


_GETOBSERVATIONSREQUEST_REQUESTMETADATAENTRY = _descriptor.Descriptor(
  name='RequestmetadataEntry',
  full_name='GetObservationsRequest.RequestmetadataEntry',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  fields=[
    _descriptor.FieldDescriptor(
      name='key', full_name='GetObservationsRequest.RequestmetadataEntry.key', index=0,
      number=1, type=9, cpp_type=9, label=1,
      has_default_value=False, default_value=_b("").decode('utf-8'),
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None, file=DESCRIPTOR),
    _descriptor.FieldDescriptor(
      name='value', full_name='GetObservationsRequest.RequestmetadataEntry.value', index=1,
      number=2, type=9, cpp_type=9, label=1,
      has_default_value=False, default_value=_b("").decode('utf-8'),
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None, file=DESCRIPTOR),
  ],
  extensions=[
  ],
  nested_types=[],
  enum_types=[
  ],
  options=_descriptor._ParseOptions(descriptor_pb2.MessageOptions(), _b('8\001')),
  is_extendable=False,
  syntax='proto3',
  extension_ranges=[],
  oneofs=[
  ],
  serialized_start=166,
  serialized_end=220,
)

_GETOBSERVATIONSREQUEST = _descriptor.Descriptor(
  name='GetObservationsRequest',
  full_name='GetObservationsRequest',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  fields=[
    _descriptor.FieldDescriptor(
      name='requestmetadata', full_name='GetObservationsRequest.requestmetadata', index=0,
      number=1, type=11, cpp_type=10, label=3,
      has_default_value=False, default_value=[],
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None, file=DESCRIPTOR),
    _descriptor.FieldDescriptor(
      name='observationheaders', full_name='GetObservationsRequest.observationheaders', index=1,
      number=2, type=9, cpp_type=9, label=3,
      has_default_value=False, default_value=[],
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None, file=DESCRIPTOR),
    _descriptor.FieldDescriptor(
      name='keys', full_name='GetObservationsRequest.keys', index=2,
      number=3, type=11, cpp_type=10, label=3,
      has_default_value=False, default_value=[],
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None, file=DESCRIPTOR),
  ],
  extensions=[
  ],
  nested_types=[_GETOBSERVATIONSREQUEST_REQUESTMETADATAENTRY, ],
  enum_types=[
  ],
  options=None,
  is_extendable=False,
  syntax='proto3',
  extension_ranges=[],
  oneofs=[
  ],
  serialized_start=417,
  serialized_end=628,
)


_GETOBSERVATIONSREPLY_REPLYMETADATAENTRY = _descriptor.Descriptor(
  name='ReplymetadataEntry',
  full_name='GetObservationsReply.ReplymetadataEntry',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  fields=[
    _descriptor.FieldDescriptor(
      name='key', full_name='GetObservationsReply.ReplymetadataEntry.key', index=0,
      number=1, type=9, cpp_type=9, label=1,
      has_default_value=False, default_value=_b("").decode('utf-8'),
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None, file=DESCRIPTOR),
    _descriptor.FieldDescriptor(
      name='value', full_name='GetObservationsReply.ReplymetadataEntry.value', index=1,
      number=2, type=9, cpp_type=9, label=1,
      has_default_value=False, default_value=_b("").decode('utf-8'),
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None, file=DESCRIPTOR),
  ],
  extensions=[
  ],
  nested_types=[],
  enum_types=[
  ],
  options=_descriptor._ParseOptions(descriptor_pb2.MessageOptions(), _b('8\001')),
  is_extendable=False,
  syntax='proto3',
  extension_ranges=[],
  oneofs=[
  ],
  serialized_start=362,
  serialized_end=414,
)

_GETOBSERVATIONSREPLY = _descriptor.Descriptor(
  name='GetObservationsReply',
  full_name='GetObservationsReply',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  fields=[
    _descriptor.FieldDescriptor(
      name='replymetadata', full_name='GetObservationsReply.replymetadata', index=0,
      number=1, type=11, cpp_type=10, label=3,
      has_default_value=False, default_value=[],
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None, file=DESCRIPTOR),
    _descriptor.FieldDescriptor(
      name='observationheaders', full_name='GetObservationsReply.observationheaders', index=1,
      number=2, type=9, cpp_type=9, label=3,
      has_default_value=False, default_value=[],
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None, file=DESCRIPTOR),
    _descriptor.FieldDescriptor(
      name='datalist', full_name='GetObservationsReply.datalist', index=2,
      number=3, type=11, cpp_type=10, label=3,
      has_default_value=False, default_value=[],
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None, file=DESCRIPTOR),
  ],
  extensions=[
  ],
  nested_types=[_GETOBSERVATIONSREPLY_REPLYMETADATAENTRY, ],
  enum_types=[
  ],
  options=None,
  is_extendable=False,
  syntax='proto3',
  extension_ranges=[],
  oneofs=[
  ],
  serialized_start=631,
  serialized_end=837,
)


_METADATALIST = _descriptor.Descriptor(
  name='MetadataList',
  full_name='MetadataList',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  fields=[
    _descriptor.FieldDescriptor(
      name='key', full_name='MetadataList.key', index=0,
      number=1, type=11, cpp_type=10, label=1,
      has_default_value=False, default_value=None,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None, file=DESCRIPTOR),
    _descriptor.FieldDescriptor(
      name='data', full_name='MetadataList.data', index=1,
      number=2, type=9, cpp_type=9, label=3,
      has_default_value=False, default_value=[],
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None, file=DESCRIPTOR),
  ],
  extensions=[
  ],
  nested_types=[],
  enum_types=[
  ],
  options=None,
  is_extendable=False,
  syntax='proto3',
  extension_ranges=[],
  oneofs=[
  ],
  serialized_start=839,
  serialized_end=898,
)


_OBSERVATIONSLIST = _descriptor.Descriptor(
  name='ObservationsList',
  full_name='ObservationsList',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  fields=[
    _descriptor.FieldDescriptor(
      name='key', full_name='ObservationsList.key', index=0,
      number=1, type=11, cpp_type=10, label=1,
      has_default_value=False, default_value=None,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None, file=DESCRIPTOR),
    _descriptor.FieldDescriptor(
      name='data', full_name='ObservationsList.data', index=1,
      number=2, type=2, cpp_type=6, label=3,
      has_default_value=False, default_value=[],
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None, file=DESCRIPTOR),
  ],
  extensions=[
  ],
  nested_types=[],
  enum_types=[
  ],
  options=None,
  is_extendable=False,
  syntax='proto3',
  extension_ranges=[],
  oneofs=[
  ],
  serialized_start=900,
  serialized_end=963,
)


_SOURCESERIESKEY = _descriptor.Descriptor(
  name='SourceSeriesKey',
  full_name='SourceSeriesKey',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  fields=[
    _descriptor.FieldDescriptor(
      name='sourcename', full_name='SourceSeriesKey.sourcename', index=0,
      number=1, type=9, cpp_type=9, label=1,
      has_default_value=False, default_value=_b("").decode('utf-8'),
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None, file=DESCRIPTOR),
    _descriptor.FieldDescriptor(
      name='seriesname', full_name='SourceSeriesKey.seriesname', index=1,
      number=2, type=9, cpp_type=9, label=1,
      has_default_value=False, default_value=_b("").decode('utf-8'),
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None, file=DESCRIPTOR),
  ],
  extensions=[
  ],
  nested_types=[],
  enum_types=[
  ],
  options=None,
  is_extendable=False,
  syntax='proto3',
  extension_ranges=[],
  oneofs=[
  ],
  serialized_start=965,
  serialized_end=1022,
)

_GETMETADATAREQUEST_REQUESTMETADATAENTRY.containing_type = _GETMETADATAREQUEST
_GETMETADATAREQUEST.fields_by_name['requestmetadata'].message_type = _GETMETADATAREQUEST_REQUESTMETADATAENTRY
_GETMETADATAREQUEST.fields_by_name['keys'].message_type = _SOURCESERIESKEY
_GETMETADATAREPLY_REPLYMETADATAENTRY.containing_type = _GETMETADATAREPLY
_GETMETADATAREPLY.fields_by_name['replymetadata'].message_type = _GETMETADATAREPLY_REPLYMETADATAENTRY
_GETMETADATAREPLY.fields_by_name['datalist'].message_type = _METADATALIST
_GETOBSERVATIONSREQUEST_REQUESTMETADATAENTRY.containing_type = _GETOBSERVATIONSREQUEST
_GETOBSERVATIONSREQUEST.fields_by_name['requestmetadata'].message_type = _GETOBSERVATIONSREQUEST_REQUESTMETADATAENTRY
_GETOBSERVATIONSREQUEST.fields_by_name['keys'].message_type = _SOURCESERIESKEY
_GETOBSERVATIONSREPLY_REPLYMETADATAENTRY.containing_type = _GETOBSERVATIONSREPLY
_GETOBSERVATIONSREPLY.fields_by_name['replymetadata'].message_type = _GETOBSERVATIONSREPLY_REPLYMETADATAENTRY
_GETOBSERVATIONSREPLY.fields_by_name['datalist'].message_type = _OBSERVATIONSLIST
_METADATALIST.fields_by_name['key'].message_type = _SOURCESERIESKEY
_OBSERVATIONSLIST.fields_by_name['key'].message_type = _SOURCESERIESKEY
DESCRIPTOR.message_types_by_name['GetMetadataRequest'] = _GETMETADATAREQUEST
DESCRIPTOR.message_types_by_name['GetMetadataReply'] = _GETMETADATAREPLY
DESCRIPTOR.message_types_by_name['GetObservationsRequest'] = _GETOBSERVATIONSREQUEST
DESCRIPTOR.message_types_by_name['GetObservationsReply'] = _GETOBSERVATIONSREPLY
DESCRIPTOR.message_types_by_name['MetadataList'] = _METADATALIST
DESCRIPTOR.message_types_by_name['ObservationsList'] = _OBSERVATIONSLIST
DESCRIPTOR.message_types_by_name['SourceSeriesKey'] = _SOURCESERIESKEY
_sym_db.RegisterFileDescriptor(DESCRIPTOR)

GetMetadataRequest = _reflection.GeneratedProtocolMessageType('GetMetadataRequest', (_message.Message,), dict(

  RequestmetadataEntry = _reflection.GeneratedProtocolMessageType('RequestmetadataEntry', (_message.Message,), dict(
    DESCRIPTOR = _GETMETADATAREQUEST_REQUESTMETADATAENTRY,
    __module__ = 'aconcagua_pb2'
    # @@protoc_insertion_point(class_scope:GetMetadataRequest.RequestmetadataEntry)
    ))
  ,
  DESCRIPTOR = _GETMETADATAREQUEST,
  __module__ = 'aconcagua_pb2'
  # @@protoc_insertion_point(class_scope:GetMetadataRequest)
  ))
_sym_db.RegisterMessage(GetMetadataRequest)
_sym_db.RegisterMessage(GetMetadataRequest.RequestmetadataEntry)

GetMetadataReply = _reflection.GeneratedProtocolMessageType('GetMetadataReply', (_message.Message,), dict(

  ReplymetadataEntry = _reflection.GeneratedProtocolMessageType('ReplymetadataEntry', (_message.Message,), dict(
    DESCRIPTOR = _GETMETADATAREPLY_REPLYMETADATAENTRY,
    __module__ = 'aconcagua_pb2'
    # @@protoc_insertion_point(class_scope:GetMetadataReply.ReplymetadataEntry)
    ))
  ,
  DESCRIPTOR = _GETMETADATAREPLY,
  __module__ = 'aconcagua_pb2'
  # @@protoc_insertion_point(class_scope:GetMetadataReply)
  ))
_sym_db.RegisterMessage(GetMetadataReply)
_sym_db.RegisterMessage(GetMetadataReply.ReplymetadataEntry)

GetObservationsRequest = _reflection.GeneratedProtocolMessageType('GetObservationsRequest', (_message.Message,), dict(

  RequestmetadataEntry = _reflection.GeneratedProtocolMessageType('RequestmetadataEntry', (_message.Message,), dict(
    DESCRIPTOR = _GETOBSERVATIONSREQUEST_REQUESTMETADATAENTRY,
    __module__ = 'aconcagua_pb2'
    # @@protoc_insertion_point(class_scope:GetObservationsRequest.RequestmetadataEntry)
    ))
  ,
  DESCRIPTOR = _GETOBSERVATIONSREQUEST,
  __module__ = 'aconcagua_pb2'
  # @@protoc_insertion_point(class_scope:GetObservationsRequest)
  ))
_sym_db.RegisterMessage(GetObservationsRequest)
_sym_db.RegisterMessage(GetObservationsRequest.RequestmetadataEntry)

GetObservationsReply = _reflection.GeneratedProtocolMessageType('GetObservationsReply', (_message.Message,), dict(

  ReplymetadataEntry = _reflection.GeneratedProtocolMessageType('ReplymetadataEntry', (_message.Message,), dict(
    DESCRIPTOR = _GETOBSERVATIONSREPLY_REPLYMETADATAENTRY,
    __module__ = 'aconcagua_pb2'
    # @@protoc_insertion_point(class_scope:GetObservationsReply.ReplymetadataEntry)
    ))
  ,
  DESCRIPTOR = _GETOBSERVATIONSREPLY,
  __module__ = 'aconcagua_pb2'
  # @@protoc_insertion_point(class_scope:GetObservationsReply)
  ))
_sym_db.RegisterMessage(GetObservationsReply)
_sym_db.RegisterMessage(GetObservationsReply.ReplymetadataEntry)

MetadataList = _reflection.GeneratedProtocolMessageType('MetadataList', (_message.Message,), dict(
  DESCRIPTOR = _METADATALIST,
  __module__ = 'aconcagua_pb2'
  # @@protoc_insertion_point(class_scope:MetadataList)
  ))
_sym_db.RegisterMessage(MetadataList)

ObservationsList = _reflection.GeneratedProtocolMessageType('ObservationsList', (_message.Message,), dict(
  DESCRIPTOR = _OBSERVATIONSLIST,
  __module__ = 'aconcagua_pb2'
  # @@protoc_insertion_point(class_scope:ObservationsList)
  ))
_sym_db.RegisterMessage(ObservationsList)

SourceSeriesKey = _reflection.GeneratedProtocolMessageType('SourceSeriesKey', (_message.Message,), dict(
  DESCRIPTOR = _SOURCESERIESKEY,
  __module__ = 'aconcagua_pb2'
  # @@protoc_insertion_point(class_scope:SourceSeriesKey)
  ))
_sym_db.RegisterMessage(SourceSeriesKey)


DESCRIPTOR.has_options = True
DESCRIPTOR._options = _descriptor._ParseOptions(descriptor_pb2.FileOptions(), _b('\n\021io.grpc.aconcaguaB\016AconcaguaProtoP\001\242\002\003ACA'))
_GETMETADATAREQUEST_REQUESTMETADATAENTRY.has_options = True
_GETMETADATAREQUEST_REQUESTMETADATAENTRY._options = _descriptor._ParseOptions(descriptor_pb2.MessageOptions(), _b('8\001'))
_GETMETADATAREPLY_REPLYMETADATAENTRY.has_options = True
_GETMETADATAREPLY_REPLYMETADATAENTRY._options = _descriptor._ParseOptions(descriptor_pb2.MessageOptions(), _b('8\001'))
_GETOBSERVATIONSREQUEST_REQUESTMETADATAENTRY.has_options = True
_GETOBSERVATIONSREQUEST_REQUESTMETADATAENTRY._options = _descriptor._ParseOptions(descriptor_pb2.MessageOptions(), _b('8\001'))
_GETOBSERVATIONSREPLY_REPLYMETADATAENTRY.has_options = True
_GETOBSERVATIONSREPLY_REPLYMETADATAENTRY._options = _descriptor._ParseOptions(descriptor_pb2.MessageOptions(), _b('8\001'))

_ACONCAGUA = _descriptor.ServiceDescriptor(
  name='Aconcagua',
  full_name='Aconcagua',
  file=DESCRIPTOR,
  index=0,
  options=None,
  serialized_start=1025,
  serialized_end=1162,
  methods=[
  _descriptor.MethodDescriptor(
    name='GetMetadata',
    full_name='Aconcagua.GetMetadata',
    index=0,
    containing_service=None,
    input_type=_GETMETADATAREQUEST,
    output_type=_GETMETADATAREPLY,
    options=None,
  ),
  _descriptor.MethodDescriptor(
    name='GetObservations',
    full_name='Aconcagua.GetObservations',
    index=1,
    containing_service=None,
    input_type=_GETOBSERVATIONSREQUEST,
    output_type=_GETOBSERVATIONSREPLY,
    options=None,
  ),
])
_sym_db.RegisterServiceDescriptor(_ACONCAGUA)

DESCRIPTOR.services_by_name['Aconcagua'] = _ACONCAGUA

# @@protoc_insertion_point(module_scope)
