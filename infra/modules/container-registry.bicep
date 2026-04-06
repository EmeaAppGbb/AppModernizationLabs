@description('Name of the Azure Container Registry')
param name string

@description('Azure region for the resource')
param location string

@description('Tags to apply to the resource')
param tags object = {}

@description('SKU for the Container Registry')
@allowed(['Basic', 'Standard', 'Premium'])
param sku string = 'Basic'

resource containerRegistry 'Microsoft.ContainerRegistry/registries@2023-11-01-preview' = {
  name: name
  location: location
  tags: tags
  sku: {
    name: sku
  }
  properties: {
    adminUserEnabled: true
    publicNetworkAccess: 'Enabled'
  }
}

@description('The login server URL for the Container Registry')
output loginServer string = containerRegistry.properties.loginServer

@description('The name of the Container Registry')
output name string = containerRegistry.name

@description('The resource ID of the Container Registry')
output id string = containerRegistry.id
