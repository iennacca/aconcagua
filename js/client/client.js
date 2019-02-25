console.log('[INIT] client');
const { TimeseriesDataServiceClient } = require('./aconcagua_grpc_web_pb.js');

var client = new TimeseriesDataServiceClient('http://localhost:50450', null, null);
exports.client = client;
