#!/bin/bash

if [[ -n $(podman ps -q -f "name=^te-storage-service$") ]];then
	podman stop te-storage-service
    podman rm te-storage-service
else
	echo "te-storage-service not exist"
fi

CON=`podman image ls 'storage-service' | wc -l` 
if [ $CON -eq 2 ] 
then
podman images | grep storage-service | awk '{print $3}' | xargs podman rmi -f
fi

podman build -t storage-service:latest .

podman tag storage-service registry.cn-hangzhou.aliyuncs.com/rours/storage-service:latest

podman push registry.cn-hangzhou.aliyuncs.com/rours/storage-service:latest

podman run -d --name te-storage-service -p 54400:54400 storage-service