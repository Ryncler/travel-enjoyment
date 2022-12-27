#!/bin/bash

if [[ -n $(docker ps -q -f "name=^te-auth-service$") ]];then
	docker stop te-auth-service
    docker rm te-auth-service
else
	echo "te-auth-service not exist"
fi

CON=`docker image ls 'auth-service' | wc -l` 
if [ $CON -eq 2 ] 
then
docker images | grep auth-service | awk '{print $3}' | xargs docker rmi -f
fi

docker build -t auth-service:latest .

docker tag auth-service 192.168.110.101:90/travel-enjoyment/auth-service:latest

docker push 192.168.110.101:90/travel-enjoyment/auth-service:latest

docker run -d --name te-auth-service -p 53300:53300 auth-service