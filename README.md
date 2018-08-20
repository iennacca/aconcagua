# aconcagua
Testbed for RPC technologies [grpc.io]

This project was originally conceived as a proof of concept for utilizing RPC strategies in general; currently, this POC highlights gRPC, from Google's grpc.io initiative. 

### Checking the solution
gRPC serves as a language-independent interface layer to facilitate fast remote process communication via HTTP/2 Basically, from a service definition (aconcagua.proto), a type-safe server and client module are created in the developer's language of choice. Currently the project establishes this by building out an RPC server/client pair in C#; the 2 class files are Aconcagua.cs and AconcaguaGrpc.cs, both located in aconcagua.common, which are then consumed within the 2 console projects called aconcagua.client and aconcagua.server, respectively. An analogous client in Python (aconagua.clientserver) also exists, which was created using the same service definition; the Python server client has not been built.

### Running the C# server and client
Compile all C# projects, which will render 2 console executables in particular, called aconcagua.client.exe and aconcagua.server.exe. From a DOS terminal command line, run aconcagua.server.exe, which will start listening for requests. Running aconcagua.client.exe on a separate DOS terminal will launch 2 RPC calls called GetMetadata() and GetObservations(); hopefully if all goes well, the client will spit out some data on each call. 

### Running the Python client
Make sure the C# server is running, then from the Python/aconcagua.clientserver project run the script in aconcagua.client.py. This should emulate the C# client and should call the C# server using the same RPC calls.
