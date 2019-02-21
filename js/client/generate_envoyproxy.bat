docker build -t aconcagua/envoy -f ./envoy.Dockerfile .
docker run -d -p 50450:50450 aconcagua/envoy