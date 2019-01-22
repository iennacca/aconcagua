/**
 * @fileoverview gRPC-Web generated client stub for 
 * @enhanceable
 * @public
 */

// GENERATED CODE -- DO NOT EDIT!



const grpc = {};
grpc.web = require('grpc-web');


var google_api_annotations_pb = require('./google/api/annotations_pb.js')

var google_protobuf_empty_pb = require('google-protobuf/google/protobuf/empty_pb.js')
const proto = require('./aconcagua_pb.js');

/**
 * @param {string} hostname
 * @param {?Object} credentials
 * @param {?Object} options
 * @constructor
 * @struct
 * @final
 */
proto.AconcaguaClient =
    function(hostname, credentials, options) {
  if (!options) options = {};
  options['format'] = 'text';

  /**
   * @private @const {!grpc.web.GrpcWebClientBase} The client
   */
  this.client_ = new grpc.web.GrpcWebClientBase(options);

  /**
   * @private @const {string} The hostname
   */
  this.hostname_ = hostname;

  /**
   * @private @const {?Object} The credentials to be used to connect
   *    to the server
   */
  this.credentials_ = credentials;

  /**
   * @private @const {?Object} Options for the client
   */
  this.options_ = options;
};


/**
 * @param {string} hostname
 * @param {?Object} credentials
 * @param {?Object} options
 * @constructor
 * @struct
 * @final
 */
proto.AconcaguaPromiseClient =
    function(hostname, credentials, options) {
  if (!options) options = {};
  options['format'] = 'text';

  /**
   * @private @const {!proto.AconcaguaClient} The delegate callback based client
   */
  this.delegateClient_ = new proto.AconcaguaClient(
      hostname, credentials, options);

};


/**
 * @const
 * @type {!grpc.web.AbstractClientBase.MethodInfo<
 *   !proto.google.protobuf.Empty,
 *   !proto.GetVersionResponse>}
 */
const methodInfo_Aconcagua_GetVersion = new grpc.web.AbstractClientBase.MethodInfo(
  proto.GetVersionResponse,
  /** @param {!proto.google.protobuf.Empty} request */
  function(request) {
    return request.serializeBinary();
  },
  proto.GetVersionResponse.deserializeBinary
);


/**
 * @param {!proto.google.protobuf.Empty} request The
 *     request proto
 * @param {!Object<string, string>} metadata User defined
 *     call metadata
 * @param {function(?grpc.web.Error, ?proto.GetVersionResponse)}
 *     callback The callback function(error, response)
 * @return {!grpc.web.ClientReadableStream<!proto.GetVersionResponse>|undefined}
 *     The XHR Node Readable Stream
 */
proto.AconcaguaClient.prototype.getVersion =
    function(request, metadata, callback) {
  return this.client_.rpcCall(this.hostname_ +
      '/Aconcagua/GetVersion',
      request,
      metadata,
      methodInfo_Aconcagua_GetVersion,
      callback);
};


/**
 * @param {!proto.google.protobuf.Empty} request The
 *     request proto
 * @param {!Object<string, string>} metadata User defined
 *     call metadata
 * @return {!Promise<!proto.GetVersionResponse>}
 *     The XHR Node Readable Stream
 */
proto.AconcaguaPromiseClient.prototype.getVersion =
    function(request, metadata) {
  return new Promise((resolve, reject) => {
    this.delegateClient_.getVersion(
      request, metadata, (error, response) => {
        error ? reject(error) : resolve(response);
      });
  });
};


/**
 * @const
 * @type {!grpc.web.AbstractClientBase.MethodInfo<
 *   !proto.GetMetadataRequest,
 *   !proto.GetMetadataResponse>}
 */
const methodInfo_Aconcagua_GetMetadata = new grpc.web.AbstractClientBase.MethodInfo(
  proto.GetMetadataResponse,
  /** @param {!proto.GetMetadataRequest} request */
  function(request) {
    return request.serializeBinary();
  },
  proto.GetMetadataResponse.deserializeBinary
);


/**
 * @param {!proto.GetMetadataRequest} request The
 *     request proto
 * @param {!Object<string, string>} metadata User defined
 *     call metadata
 * @param {function(?grpc.web.Error, ?proto.GetMetadataResponse)}
 *     callback The callback function(error, response)
 * @return {!grpc.web.ClientReadableStream<!proto.GetMetadataResponse>|undefined}
 *     The XHR Node Readable Stream
 */
proto.AconcaguaClient.prototype.getMetadata =
    function(request, metadata, callback) {
  return this.client_.rpcCall(this.hostname_ +
      '/Aconcagua/GetMetadata',
      request,
      metadata,
      methodInfo_Aconcagua_GetMetadata,
      callback);
};


/**
 * @param {!proto.GetMetadataRequest} request The
 *     request proto
 * @param {!Object<string, string>} metadata User defined
 *     call metadata
 * @return {!Promise<!proto.GetMetadataResponse>}
 *     The XHR Node Readable Stream
 */
proto.AconcaguaPromiseClient.prototype.getMetadata =
    function(request, metadata) {
  return new Promise((resolve, reject) => {
    this.delegateClient_.getMetadata(
      request, metadata, (error, response) => {
        error ? reject(error) : resolve(response);
      });
  });
};


/**
 * @const
 * @type {!grpc.web.AbstractClientBase.MethodInfo<
 *   !proto.GetObservationsRequest,
 *   !proto.GetObservationsResponse>}
 */
const methodInfo_Aconcagua_GetObservations = new grpc.web.AbstractClientBase.MethodInfo(
  proto.GetObservationsResponse,
  /** @param {!proto.GetObservationsRequest} request */
  function(request) {
    return request.serializeBinary();
  },
  proto.GetObservationsResponse.deserializeBinary
);


/**
 * @param {!proto.GetObservationsRequest} request The
 *     request proto
 * @param {!Object<string, string>} metadata User defined
 *     call metadata
 * @param {function(?grpc.web.Error, ?proto.GetObservationsResponse)}
 *     callback The callback function(error, response)
 * @return {!grpc.web.ClientReadableStream<!proto.GetObservationsResponse>|undefined}
 *     The XHR Node Readable Stream
 */
proto.AconcaguaClient.prototype.getObservations =
    function(request, metadata, callback) {
  return this.client_.rpcCall(this.hostname_ +
      '/Aconcagua/GetObservations',
      request,
      metadata,
      methodInfo_Aconcagua_GetObservations,
      callback);
};


/**
 * @param {!proto.GetObservationsRequest} request The
 *     request proto
 * @param {!Object<string, string>} metadata User defined
 *     call metadata
 * @return {!Promise<!proto.GetObservationsResponse>}
 *     The XHR Node Readable Stream
 */
proto.AconcaguaPromiseClient.prototype.getObservations =
    function(request, metadata) {
  return new Promise((resolve, reject) => {
    this.delegateClient_.getObservations(
      request, metadata, (error, response) => {
        error ? reject(error) : resolve(response);
      });
  });
};


module.exports = proto;

