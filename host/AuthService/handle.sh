#!/bin/bash

if [[ -n $(podman ps -q -f "name=^te-auth-service$") ]];then
	podman stop te-auth-service
    podman rm te-auth-service
else
	echo "te-auth-service not exist"
fi

CON=`podman image ls 'auth-service' | wc -l` 
if [ $CON -eq 2 ] 
then
podman images | grep auth-service | awk '{print $3}' | xargs podman rmi -f
fi

podman build -t auth-service:latest .

podman tag auth-service registry.cn-hangzhou.aliyuncs.com/rours/auth-service:latest

podman push registry.cn-hangzhou.aliyuncs.com/rours/auth-service:latest

podman run -d --name te-auth-service -p 53300:53300 auth-service