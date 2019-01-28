# aconcagua
Testbed for RPC technologies [grpc.io]

This project was originally conceived as a proof of concept for utilizing RPC strategies in general; currently, this POC highlights gRPC, from Google's grpc.io initiative. 

## Background
gRPC serves as a language-independent interface layer to facilitate fast remote process communication via HTTP/2. Basically, from a service definition ([aconcagua.proto](proto/aconcagua.proto)), a type-safe server and client module are created in the developer's language of choice. Currently the project establishes this by building out an RPC server/client pair in C#. We also created an analogous client in Python (aconagua.clientserver), which was created using the same service definition; the Python server implementation has not been built out, however.


## Running the C# server and client
Compile aconcagua.proto by running generate_protos.bat in [aconcagua.common](csharp/aconcagua.common). Two class files will be built (Aconcagua.cs and AconcaguaGrpc.cs) in [aconcagua.common](csharp/aconcagua.common), which are then consumed within the 2 console projects called aconcagua.client and aconcagua.server, respectively.

Once the proto is compiled, compile all C# projects, which will render 2 console executables in particular, called aconcagua.client.exe and aconcagua.server.exe. From a DOS terminal command line, run aconcagua.server.exe, which will start listening for requests. Running aconcagua.client.exe on a separate DOS terminal will launch 2 RPC calls called GetMetadata() and GetObservations(); hopefully if all goes well, the client will spit out some data on each call. You will likely need to change the file path for the database according to your own settings.

## Running the Python client
Make sure the C# server is running, then from the Python/aconcagua.clientserver project, open an interactive window or terminal under the included virtual environment, and run the script in aconcagua.client.py. This should emulate the C# client and should call the C# server using the same RPC calls. As with the C# client, you will likely need to change the file path as well.

## Running the JavaScript client
[Under construction. AS of this writing, JS client is now being called from index.html, where a solitary call to GetVersion() is initiated from main.js].

Constructing the Javascript client took some doing as there were a couple of major differences from the initial C# and Python constructions (not to mention I'm not well-versed in Javascript). 

First off, the grpc-web Javascript client is substantially different from the first two client implementations above as this demonstrates JS calls from a browser running HTTP to a gRPC server running HTTP/2. Basically, grpc-web's HelloWorld sample accommodates this by running an [Envoy](https://www.envoyproxy.io/) proxy between the JS client and the gRPC server. The proxy effectively translates the HTTP/1 calls into HTTP/2 calls (check it out [here](https://blog.envoyproxy.io/envoy-and-grpc-web-a-fresh-new-alternative-to-rest-6504ce7eb880)).To follow the example, it's best to have a good grasp of grpc, then grpc-web, and a passing acquaintance with Envoy (and a dalliance with Docker as the Envoy proxy runs in a Docker container) to understand its idiosyncrasies.

Second, it seemed that there were differences in how the import statement worked within the proto file when being compiled by the C#/Python compilers as opposed to the way the grpc-web version did; it seemed that the js version was looking for the precompiled js versions of the imported google protobuf files where Google's predefined proto files (I'm importing annotations.proto and empty.proto, with http.proto being a dependency import).

I had to do this by 1) cloning/compiling/runninng grpc-web's HelloWorld sample, then 2) cloning that whole folder onto another that references my own proto definitions (and dealing with my proto's own idiosyncrasies).

###  Step 1. Cloning/compiling/running HelloWorld (js/helloworld)
The first step to understanding how proto files are compiled started from cloning grpc-web's [HelloWorld](https://github.com/grpc/grpc-web/tree/master/net/grpc/gateway/examples/helloworld) sample, then slowly adding the import files to client and client-node.js. It became quickly apparent that the proto folder dependency structure I followed for making the C# and Python clients didn't work for creating the JS client.

 Once you have that, run their HelloWorld sample on your own; I had to copy it to my own folder and had to deal with a number of issues if you're following their readme. I documented these in this [readme](js/helloworld/README.md).

### Step 2. Copying my (revised) HelloWorld folder into client (js/client)
After that, it's a matter of minor changes: had to make the client ping the Envoy proxy at 50050 (both the C# and Python clients ping the server directly so they use 50051). This necessitated a change to envoy.yaml to listen to port 50050 for incoming client requests, then emit at port 50051 which is where the csharp server is listening in on.
