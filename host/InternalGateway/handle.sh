#!/bin/bash

if [[ -n $(docker ps -q -f "name=^te-internal-gateway$") ]];then
	docker stop te-internal-gateway
    docker rm te-internal-gateway
else
	echo "te-internal-gateway not exist"
fi

CON=`docker image ls 'internal-gateway' | wc -l` 
if [ $CON -eq 2 ] 
then
docker images | grep internal-gateway | awk '{print $3}' | xargs docker rmi -f
fi

docker build -t internal-gateway:latest .

docker tag internal-gateway 192.168.110.101:90/travel-enjoyment/internal-gateway:latest

docker push 192.168.110.101:90/travel-enjoyment/internal-gateway:latest

docker run -d --name te-internal-gateway -p 59600:59600 internal-gateway