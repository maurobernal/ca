#!/bin/bash
image="maurobernal/ca"
id=$(date +'%Y%m%d%H%M')
imagename=$image:$id
echo "========== Build image:$imagename=========="
docker.exe build -t $imagename .
#echo "========== Creating container image:$imagename=========="
#docker.exe run  -p8443:443 -p8080:80 -d $imagename
docker.exe push $imagename
#echo "========== Deploy image:$imagename=========="
kubectl.exe set image deployment/deploy-caapi container-caapi=$imagename -n maurobernal
