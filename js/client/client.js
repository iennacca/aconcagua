console.log('Start client.js');

const {GetVersionReply} = require('./aconcagua_pb.js');
const {AconcaguaClient} = require('./aconcagua_grpc_web_pb.js');

var client = new AconcaguaClient('http://localhost:50051', null, null);

var request = new proto.google.protobuf.Empty();

client.getVersion(request, {}, (err, response) => {
  console.log(response.getVersion());
});