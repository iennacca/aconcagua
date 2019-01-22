@rem [jc]: Compiling for grpc-web needs an additional executable 
@rem       pulled from https://github.com/grpc/grpc-web/releases. The Windows executable
@rem       from the release page is suffixed with version and OS information; 
@rem       (i.e., protoc-gen-grpc-web-1.0.3-windows-x86_64.exe); this needs to be renamed to 
@rem       protoc-gen-grpc-web.exe. I stuck it in the TOOLS_PATH for simplicity's sake. 
@rem [jc]: The np google-protobuf package does not have the precompiled JS versions
@rem       of the ff. google proto files:
@rem       - annotations.proto
@rem       - http.proto
@rem       As of this writing, I had to add 2 explicit protoc calls to compile these 2; both
@rem       end up built off helloworld's <main folder>\google\api. Left it there to preserve
@rem       the folder structure explicit from the import statements.
@rem [jc]: If all goes well, then npx webpack <client>.js should work.

@echo off

setlocal

set TOOLS_PATH=node_modules\grpc-tools\bin
set GOOGLEAPIS_DIR=..\..\googleapis

%TOOLS_PATH%\protoc.exe ^
	..\..\proto\aconcagua.proto  ^
	--js_out=import_style=commonjs:. ^
	--proto_path=%GOOGLEAPIS_DIR%;..\..\proto ^
	--grpc-web_out=import_style=commonjs,mode=grpcwebtext:.

%TOOLS_PATH%\protoc.exe ^
	%GOOGLEAPIS_DIR%\google\api\annotations.proto  ^
	--js_out=import_style=commonjs:. ^
	--proto_path=%GOOGLEAPIS_DIR%;. ^
	--grpc-web_out=import_style=commonjs,mode=grpcwebtext:.

%TOOLS_PATH%\protoc.exe ^
	%GOOGLEAPIS_DIR%\google\api\http.proto  ^
	--js_out=import_style=commonjs:. ^
	--proto_path=%GOOGLEAPIS_DIR%;. ^
	--grpc-web_out=import_style=commonjs,mode=grpcwebtext:.


endlocal
