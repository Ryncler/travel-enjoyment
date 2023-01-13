#!/bin/bash

if [[ -n $(podman ps -q -f "name=^te-base-service$") ]];then
	podman stop te-base-service
    podman rm te-base-service
else
	echo "te-base-service not exist"
fi

CON=`podman image ls 'base-service' | wc -l` 
if [ $CON -eq 2 ] 
then
podman images | grep base-service | awk '{print $3}' | xargs podman rmi -f
fi

podman build -t base-service:latest .

podman tag base-service registry.cn-hangzhou.aliyuncs.com/rours/base-service:latest

podman push registry.cn-hangzhou.aliyuncs.com/rours/base-service:latest

podman run -d --name te-base-service -p 54300:54300 base-service