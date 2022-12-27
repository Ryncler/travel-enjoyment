#!/bin/bash

if [[ -n $(docker ps -q -f "name=^te-base-service$") ]];then
	docker stop te-base-service
    docker rm te-base-service
else
	echo "te-base-service not exist"
fi

CON=`docker image ls 'base-service' | wc -l` 
if [ $CON -eq 2 ] 
then
docker images | grep base-service | awk '{print $3}' | xargs docker rmi -f
fi

docker build -t base-service:latest .

docker tag base-service 192.168.110.101:90/travel-enjoyment/base-service:latest

docker push 192.168.110.101:90/travel-enjoyment/base-service:latest

docker run -d --name te-base-service -p 54300:54300 base-service