#!/bin/bash

if [[ -n $(docker ps -q -f "name=^te-public-gateway$") ]];then
	docker stop te-public-gateway
    docker rm te-public-gateway
else
	echo "te-public-gateway not exist"
fi

docker images | grep public-gateway | awk '{print $3}' | xargs docker rmi -f

docker build -t public-gateway:latest .

docker tag public-gateway 192.168.110.101:90/travel-enjoyment/public-gateway:latest

docker push 192.168.110.101:90/travel-enjoyment/public-gateway:latest

docker run -d --name te-public-gateway -p 59600:59600 public-gateway