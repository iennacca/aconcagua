
setlocal
set GOOGLEAPIS_DIR=..\..\googleapis

python ^
    -m grpc_tools.protoc ^
	-I..\..\proto ^
	--python_out=. ^
	--grpc_python_out=. ^
	--proto_path=%GOOGLEAPIS_DIR% ^
	..\..\proto\aconcagua.proto

endlocal