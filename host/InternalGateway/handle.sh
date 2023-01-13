#!/bin/bash

if [[ -n $(podman ps -q -f "name=^te-internal-gateway$") ]];then
	podman stop te-internal-gateway
    podman rm te-internal-gateway
else
	echo "te-internal-gateway not exist"
fi

CON=`podman image ls 'internal-gateway' | wc -l` 
if [ $CON -eq 2 ] 
then
podman images | grep internal-gateway | awk '{print $3}' | xargs podman rmi -f
fi

podman build -t internal-gateway:latest .

podman tag internal-gateway registry.cn-hangzhou.aliyuncs.com/rours/internal-gateway:latest

podman push registry.cn-hangzhou.aliyuncs.com/rours/internal-gateway:latest

podman run -d --name te-internal-gateway -p 59600:59600 internal-gateway