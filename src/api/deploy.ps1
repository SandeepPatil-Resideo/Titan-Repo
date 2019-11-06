$ErrorActionPreference="Stop"

helm init
docker build -t "c:\titan\titan-address-api/c:\titan\titan-address-api/titanaddressapi" .
docker push c:\titan\titan-address-api/c:\titan\titan-address-api/titanaddressapi
kubectl apply -f ".\ops\env\et\secrets_logging.yaml"
helm install .\ops\helm\titanaddressapi-stack -n "titanaddressapi-stack" -f ".\ops\env\et\values.yaml"