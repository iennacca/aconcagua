/**
 *
 * Copyright 2018 Google LLC
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     https://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 */

const {GetVersionResponse} = require('./aconcagua_pb.js');
const {TimeseriesDataServiceClient} = require('./aconcagua_grpc_web_pb.js');
const {Empty} = require('./node_modules/google-protobuf/google/protobuf/empty_pb.js');

//var client = new TimeseriesDataServiceClient('http://' + window.location.hostname + ':8080', null, null);
var client = new TimeseriesDataServiceClient('http://localhost:50051', null, null);

// simple unary call
var request = new Empty();

client.getVersion(request, {}, (err, response) => {
console.log(response.getVersion());
});

