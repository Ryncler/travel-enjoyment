#!/bin/bash

if [[ -n $(podman ps -q -f "name=^te-common-service$") ]];then
	podman stop te-common-service
    podman rm te-common-service
else
	echo "te-common-service not exist"
fi

CON=`podman image ls 'common-service' | wc -l` 
if [ $CON -eq 2 ] 
then
podman images | grep common-service | awk '{print $3}' | xargs podman rmi -f
fi

podman build -t common-service:latest .

podman tag common-service registry.cn-hangzhou.aliyuncs.com/rours/common-service:latest

podman push registry.cn-hangzhou.aliyuncs.com/rours/common-service:latest

podman run -d --name te-common-service -p 54600:54600 common-service