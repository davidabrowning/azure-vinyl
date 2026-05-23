## Update API manual steps
Eventually this can be added to CI/CD.

### Rebuild image
docker build -t azurevinyl-api -f src/AzureVinyl.Api/Dockerfile .

### Tag
docker tag azurevinyl-api containerregistryazurevinyl.azurecr.io/azurevinyl-api:latest

### Login docker to azure container registry
az acr login --name containerregistryazurevinyl

### Push to azure container registry
docker push containerregistryazurevinyl.azurecr.io/azurevinyl-api:latest

### Force revision
az containerapp update --name container-app-azure-vinyl-api --resource-group resource-group-azure-vinyl-dev --image containerregistryazurevinyl.azurecr.io/azurevinyl-api:latest

### Restart container app
Do this manually in Azure web portal.