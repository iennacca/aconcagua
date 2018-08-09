@rem Copyright 2016 gRPC authors.
@rem
@rem Licensed under the Apache License, Version 2.0 (the "License");
@rem you may not use this file except in compliance with the License.
@rem You may obtain a copy of the License at
@rem
@rem     http://www.apache.org/licenses/LICENSE-2.0
@rem
@rem Unless required by applicable law or agreed to in writing, software
@rem distributed under the License is distributed on an "AS IS" BASIS,
@rem WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
@rem See the License for the specific language governing permissions and
@rem limitations under the License.

@rem Generate the C# code for .proto files

echo off
setlocal

@rem enter this directory
cd /d %~dp0

set TOOLS_PATH=..\packages\Grpc.Tools.1.13.0\tools\windows_x86
set GOOGLEAPIS_DIR=..\..\googleapis

%TOOLS_PATH%\protoc.exe ^
	-I ..\..\proto ^
	--csharp_out=. ^
	--grpc_out=. ^
	--proto_path=%GOOGLEAPIS_DIR% ^
	--plugin=protoc-gen-grpc=%TOOLS_PATH%\grpc_csharp_plugin.exe ^
	..\..\proto\aconcagua.proto 

%TOOLS_PATH%\protoc.exe ^
	-I ..\..\proto ^
	--csharp_out=. ^
	--grpc-gateway_out=logtostderr=true:. ^
	--proto_path=%GOOGLEAPIS_DIR% ^
	..\..\proto\aconcagua.proto 
endlocal


@rem [jc]: grpc-gateway build for REST API stubs
@rem protoc -I. -I$(cygpath -w /usr/local/include) -I${GOPATH}/src -I${GOPATH}/src/github.com/grpc-ecosystem/grpc-gateway/third_party/googleapis --go_out=plugins=grpc:. ./path/to/your_service.proto
@rem protoc -I. -I$(cygpath -w /usr/local/include) -I${GOPATH}/src -I${GOPATH}/src/github.com/grpc-ecosystem/grpc-gateway/third_party/googleapis --grpc-gateway_out=logtostderr=true:. ./path/to/your_service.proto
@rem protoc -I. -I$(cygpath -w /usr/local/include) -I${GOPATH}/src -I${GOPATH}/src/github.com/grpc-ecosystem/grpc-gateway/third_party/googleapis --swagger_out=logtostderr=true:. ./path/to/your_service.proto

@rem [jc]: grpc-gateway : change db_home on nsswitch.conf to run cygwin build correctly
@rem # /etc/nsswitch.conf
@rem #
@rem #    This file is read once by the first process in a Cygwin process tree.
@rem #    To pick up changes, restart all Cygwin processes.  For a description
@rem #    see https://cygwin.com/cygwin-ug-net/ntsec.html#ntsec-mapping-nsswitch
@rem #
@rem # Defaults:
@rem # passwd:   files db
@rem # group:    files db
@rem # db_enum:  cache builtin
@rem db_home:  windows
@rem # db_shell: /bin/bash
@rem # db_gecos: <empty>
