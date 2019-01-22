# Addenda: gRPC-Web Hello World Guide

This serves as an addendum to the [gRPC-Web Hello World Guide](https://github.com/grpc/grpc-web/tree/master/net/grpc/gateway/examples/helloworld) and is intended to help me remember what the heck I had to do to get 
the gRPC Hello World sample running. In particular, I hit 2 major issues:

 - protoc compilation with Google predefined proto files in google/api and google/protobuf 
 were problematic;
 - Envoy proxy in Docker issues because of a couple of internal changes.

 Further notes concerning these follow.

## Protoc compilation

### Issue 1: grpc-gen-grpc-web 
Compiling for grpc-web needs an additional executable pulled from
[grpc-web](https://github.com/grpc/grpc-web/releases). The Windows executable
from the release page is suffixed with version and OS information; 
(i.e., protoc-gen-grpc-web-1.0.3-windows-x86_64.exe); this needs to be renamed to 
protoc-gen-grpc-web.exe. In generate_proto.bat, I stuck it in the TOOLS_PATH for simplicity's sake. 

### Issue 2: Google proto compilation
The issue was that the js compiler either compiled the imported proto files in some unknown
folder or it didn't have precompiled versions in the first place. I'm still trying to figure 
this out. As a workaround, I had to force-compile (in generate_protos.bat) the ff. Google-created 
proto files that got written in the google/api and google/protobuf folders:

- annotations.proto
- http.proto

Note that empty.proto was referenced as well, but I think since it was in the google-protobuf npm package it
compiled without errors.

## Envoy proxy in Docker

AS of this writing, [this](https://github.com/grpc/grpc-web/issues/436) solved my Envoy 400/500 server
 issue:
 - envoy.yaml: had to use address: host.docker.internal instead of address: localhost under the clusters field,
 - running the envoy container should be done using the command

```sh
$ docker run -d -p 8080:8080 helloworld/envoy
```

  as the old README had specified the network=host option which is now removed as per the instructions. 

