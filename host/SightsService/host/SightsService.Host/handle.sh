#!/bin/bash

if [[ -n $(podman ps -q -f "name=^te-sights-service$") ]];then
	podman stop te-sights-service
    podman rm te-sights-service
else
	echo "te-sights-service not exist"
fi

CON=`podman image ls 'sights-service' | wc -l` 
if [ $CON -eq 2 ] 
then
podman images | grep sights-service | awk '{print $3}' | xargs podman rmi -f
fi

podman build -t sights-service:latest .

podman tag sights-service registry.cn-hangzhou.aliyuncs.com/rours/sights-service:latest

podman push registry.cn-hangzhou.aliyuncs.com/rours/sights-service:latest

podman run -d --name te-sights-service -p 54300:54300 sights-service