
@echo off

setlocal

set TOOLS_PATH=node_modules\grpc-tools\bin
set GOOGLEAPIS_DIR=..\..\googleapis

%TOOLS_PATH%\protoc.exe ^
	.\helloworld.proto  ^
	--js_out=import_style=commonjs:. ^
	--proto_path=%GOOGLEAPIS_DIR%;. ^
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
