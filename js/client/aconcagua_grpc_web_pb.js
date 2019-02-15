/**
 * @fileoverview gRPC-Web generated client stub for aconcagua.proto
 * @enhanceable
 * @public
 */

// GENERATED CODE -- DO NOT EDIT!



const grpc = {};
grpc.web = require('grpc-web');


var google_api_annotations_pb = require('./google/api/annotations_pb.js')

var google_protobuf_empty_pb = require('google-protobuf/google/protobuf/empty_pb.js')
const proto = {};
proto.aconcagua = {};
proto.aconcagua.proto = require('./aconcagua_pb.js');

/**
 * @param {string} hostname
 * @param {?Object} credentials
 * @param {?Object} options
 * @constructor
 * @struct
 * @final
 */
proto.aconcagua.proto.TimeseriesDataServiceClient =
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
proto.aconcagua.proto.TimeseriesDataServicePromiseClient =
    function(hostname, credentials, options) {
  if (!options) options = {};
  options['format'] = 'text';

  /**
   * @private @const {!proto.aconcagua.proto.TimeseriesDataServiceClient} The delegate callback based client
   */
  this.delegateClient_ = new proto.aconcagua.proto.TimeseriesDataServiceClient(
      hostname, credentials, options);

};


/**
 * @const
 * @type {!grpc.web.AbstractClientBase.MethodInfo<
 *   !proto.google.protobuf.Empty,
 *   !proto.aconcagua.proto.GetVersionResponse>}
 */
const methodInfo_TimeseriesDataService_GetVersion = new grpc.web.AbstractClientBase.MethodInfo(
  proto.aconcagua.proto.GetVersionResponse,
  /** @param {!proto.google.protobuf.Empty} request */
  function(request) {
    return request.serializeBinary();
  },
  proto.aconcagua.proto.GetVersionResponse.deserializeBinary
);


/**
 * @param {!proto.google.protobuf.Empty} request The
 *     request proto
 * @param {!Object<string, string>} metadata User defined
 *     call metadata
 * @param {function(?grpc.web.Error, ?proto.aconcagua.proto.GetVersionResponse)}
 *     callback The callback function(error, response)
 * @return {!grpc.web.ClientReadableStream<!proto.aconcagua.proto.GetVersionResponse>|undefined}
 *     The XHR Node Readable Stream
 */
proto.aconcagua.proto.TimeseriesDataServiceClient.prototype.getVersion =
    function(request, metadata, callback) {
  return this.client_.rpcCall(this.hostname_ +
      '/aconcagua.proto.TimeseriesDataService/GetVersion',
      request,
      metadata,
      methodInfo_TimeseriesDataService_GetVersion,
      callback);
};


/**
 * @param {!proto.google.protobuf.Empty} request The
 *     request proto
 * @param {!Object<string, string>} metadata User defined
 *     call metadata
 * @return {!Promise<!proto.aconcagua.proto.GetVersionResponse>}
 *     The XHR Node Readable Stream
 */
proto.aconcagua.proto.TimeseriesDataServicePromiseClient.prototype.getVersion =
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
 *   !proto.aconcagua.proto.GetSeriesKeysRequest,
 *   !proto.aconcagua.proto.GetSeriesKeysResponse>}
 */
const methodInfo_TimeseriesDataService_GetSeriesKeys = new grpc.web.AbstractClientBase.MethodInfo(
  proto.aconcagua.proto.GetSeriesKeysResponse,
  /** @param {!proto.aconcagua.proto.GetSeriesKeysRequest} request */
  function(request) {
    return request.serializeBinary();
  },
  proto.aconcagua.proto.GetSeriesKeysResponse.deserializeBinary
);


/**
 * @param {!proto.aconcagua.proto.GetSeriesKeysRequest} request The
 *     request proto
 * @param {!Object<string, string>} metadata User defined
 *     call metadata
 * @param {function(?grpc.web.Error, ?proto.aconcagua.proto.GetSeriesKeysResponse)}
 *     callback The callback function(error, response)
 * @return {!grpc.web.ClientReadableStream<!proto.aconcagua.proto.GetSeriesKeysResponse>|undefined}
 *     The XHR Node Readable Stream
 */
proto.aconcagua.proto.TimeseriesDataServiceClient.prototype.getSeriesKeys =
    function(request, metadata, callback) {
  return this.client_.rpcCall(this.hostname_ +
      '/aconcagua.proto.TimeseriesDataService/GetSeriesKeys',
      request,
      metadata,
      methodInfo_TimeseriesDataService_GetSeriesKeys,
      callback);
};


/**
 * @param {!proto.aconcagua.proto.GetSeriesKeysRequest} request The
 *     request proto
 * @param {!Object<string, string>} metadata User defined
 *     call metadata
 * @return {!Promise<!proto.aconcagua.proto.GetSeriesKeysResponse>}
 *     The XHR Node Readable Stream
 */
proto.aconcagua.proto.TimeseriesDataServicePromiseClient.prototype.getSeriesKeys =
    function(request, metadata) {
  return new Promise((resolve, reject) => {
    this.delegateClient_.getSeriesKeys(
      request, metadata, (error, response) => {
        error ? reject(error) : resolve(response);
      });
  });
};


/**
 * @const
 * @type {!grpc.web.AbstractClientBase.MethodInfo<
 *   !proto.aconcagua.proto.GetMetadataRequest,
 *   !proto.aconcagua.proto.GetMetadataResponse>}
 */
const methodInfo_TimeseriesDataService_GetMetadata = new grpc.web.AbstractClientBase.MethodInfo(
  proto.aconcagua.proto.GetMetadataResponse,
  /** @param {!proto.aconcagua.proto.GetMetadataRequest} request */
  function(request) {
    return request.serializeBinary();
  },
  proto.aconcagua.proto.GetMetadataResponse.deserializeBinary
);


/**
 * @param {!proto.aconcagua.proto.GetMetadataRequest} request The
 *     request proto
 * @param {!Object<string, string>} metadata User defined
 *     call metadata
 * @param {function(?grpc.web.Error, ?proto.aconcagua.proto.GetMetadataResponse)}
 *     callback The callback function(error, response)
 * @return {!grpc.web.ClientReadableStream<!proto.aconcagua.proto.GetMetadataResponse>|undefined}
 *     The XHR Node Readable Stream
 */
proto.aconcagua.proto.TimeseriesDataServiceClient.prototype.getMetadata =
    function(request, metadata, callback) {
  return this.client_.rpcCall(this.hostname_ +
      '/aconcagua.proto.TimeseriesDataService/GetMetadata',
      request,
      metadata,
      methodInfo_TimeseriesDataService_GetMetadata,
      callback);
};


/**
 * @param {!proto.aconcagua.proto.GetMetadataRequest} request The
 *     request proto
 * @param {!Object<string, string>} metadata User defined
 *     call metadata
 * @return {!Promise<!proto.aconcagua.proto.GetMetadataResponse>}
 *     The XHR Node Readable Stream
 */
proto.aconcagua.proto.TimeseriesDataServicePromiseClient.prototype.getMetadata =
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
 *   !proto.aconcagua.proto.GetObservationsRequest,
 *   !proto.aconcagua.proto.GetObservationsResponse>}
 */
const methodInfo_TimeseriesDataService_GetObservations = new grpc.web.AbstractClientBase.MethodInfo(
  proto.aconcagua.proto.GetObservationsResponse,
  /** @param {!proto.aconcagua.proto.GetObservationsRequest} request */
  function(request) {
    return request.serializeBinary();
  },
  proto.aconcagua.proto.GetObservationsResponse.deserializeBinary
);


/**
 * @param {!proto.aconcagua.proto.GetObservationsRequest} request The
 *     request proto
 * @param {!Object<string, string>} metadata User defined
 *     call metadata
 * @param {function(?grpc.web.Error, ?proto.aconcagua.proto.GetObservationsResponse)}
 *     callback The callback function(error, response)
 * @return {!grpc.web.ClientReadableStream<!proto.aconcagua.proto.GetObservationsResponse>|undefined}
 *     The XHR Node Readable Stream
 */
proto.aconcagua.proto.TimeseriesDataServiceClient.prototype.getObservations =
    function(request, metadata, callback) {
  return this.client_.rpcCall(this.hostname_ +
      '/aconcagua.proto.TimeseriesDataService/GetObservations',
      request,
      metadata,
      methodInfo_TimeseriesDataService_GetObservations,
      callback);
};


/**
 * @param {!proto.aconcagua.proto.GetObservationsRequest} request The
 *     request proto
 * @param {!Object<string, string>} metadata User defined
 *     call metadata
 * @return {!Promise<!proto.aconcagua.proto.GetObservationsResponse>}
 *     The XHR Node Readable Stream
 */
proto.aconcagua.proto.TimeseriesDataServicePromiseClient.prototype.getObservations =
    function(request, metadata) {
  return new Promise((resolve, reject) => {
    this.delegateClient_.getObservations(
      request, metadata, (error, response) => {
        error ? reject(error) : resolve(response);
      });
  });
};


module.exports = proto.aconcagua.proto;

