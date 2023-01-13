#!/bin/bash

if [[ -n $(podman ps -q -f "name=^te-public-gateway$") ]];then
	podman stop te-public-gateway
    podman rm te-public-gateway
else
	echo "te-public-gateway not exist"
fi

CON=`podman image ls 'public-gateway' | wc -l` 
if [ $CON -eq 2 ] 
then
podman images | grep public-gateway | awk '{print $3}' | xargs podman rmi -f
fi

podman build -t public-gateway:latest .

podman tag public-gateway registry.cn-hangzhou.aliyuncs.com/rours/public-gateway:latest

podman push registry.cn-hangzhou.aliyuncs.com/rours/public-gateway:latest

podman run -d --name te-public-gateway -p 59500:59500 public-gateway