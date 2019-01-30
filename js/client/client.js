console.log('Start aconcagua check');

const {GetVersionReply} = require('./aconcagua_pb.js');
const {TimeseriesDataServiceClient} = require('./aconcagua_grpc_web_pb.js');

var client = new TimeseriesDataServiceClient('http://localhost:50050', null, null);
var request = new proto.google.protobuf.Empty();

client.getVersion(request, {}, (err, response) => {
  console.log(response.getVersion());
});

console.log('End aconcagua check');


$('button1').on("click", function () {
  let url = new URL('https://localhost:44397/api/testdata');
  observations = document.querySelector('#observations').value;
  database = document.querySelector('#database').value;
  let searchParams = new URLSearchParams({
      "database": database,
      "observations": observations
  });
  url.search = searchParams;

  fetch(url)
      .then(function (response) {
          return response.json();
      })
      .then(loadData);
});

