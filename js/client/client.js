console.log('Start client.js');

const {GetVersionReply} = require('./aconcagua_pb.js');
const {TimeseriesDataServiceClient} = require('./aconcagua_grpc_web_pb.js');

var client = new TimeseriesDataServiceClient('http://localhost:50050', null, null);

var request = new proto.google.protobuf.Empty();

client.getVersion(request, {}, (err, response) => {
  console.log(response.getVersion());
});