docker build -t helloworld/envoy -f ./envoy.Dockerfile .
docker run -d -p 8080:8080  helloworld/envoy