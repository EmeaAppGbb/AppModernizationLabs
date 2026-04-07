@description('Name of the Container App')
param name string

@description('Azure region for the resource')
param location string

@description('Tags to apply to the resource')
param tags object = {}

@description('Resource ID of the Container Apps Environment')
param containerAppEnvId string

@description('Docker image name including registry (e.g., myacr.azurecr.io/app)')
param imageName string

@description('Tag of the Docker image to deploy')
param imageTag string = 'latest'

@description('ACR login server URL')
param containerRegistryLoginServer string

@description('Name of the Azure Container Registry')
param containerRegistryName string

@description('Application Insights connection string')
@secure()
param appInsightsConnectionString string

@description('Application Insights App ID for API queries')
param appInsightsAppId string

@description('YouTube Data API key (optional)')
param youtubeApiKey string = ''

@description('YouTube Channel ID (optional)')
param youtubeChannelId string = ''

@description('Whether to use ACR image (false = use placeholder for initial provision)')
param useAcrImage bool = false
param minReplicas int = 0

@description('Maximum number of replicas')
param maxReplicas int = 3

@description('CPU cores allocated to each replica')
param cpu string = '0.5'

@description('Memory allocated to each replica')
param memory string = '1Gi'

@description('External ingress port')
param targetPort int = 8080

resource containerRegistry 'Microsoft.ContainerRegistry/registries@2023-11-01-preview' existing = {
  name: containerRegistryName
}

var acrImage = '${containerRegistryLoginServer}/${imageName}:${imageTag}'
var placeholderImage = 'mcr.microsoft.com/azuredocs/containerapps-helloworld:latest'
var containerImage = useAcrImage ? acrImage : placeholderImage

resource containerApp 'Microsoft.App/containerApps@2024-03-01' = {
  name: name
  location: location
  tags: tags
  identity: {
    type: 'SystemAssigned'
  }
  properties: {
    managedEnvironmentId: containerAppEnvId
    configuration: {
      ingress: {
        external: true
        targetPort: targetPort
        transport: 'auto'
        allowInsecure: false
      }
      registries: [
        {
          server: containerRegistryLoginServer
          username: containerRegistry.listCredentials().username
          passwordSecretRef: 'acr-password'
        }
      ]
      secrets: [
        {
          name: 'acr-password'
          value: containerRegistry.listCredentials().passwords[0].value
        }
        {
          name: 'app-insights-connection-string'
          value: appInsightsConnectionString
        }
      ]
    }
    template: {
      containers: [
        {
          name: 'dashboard'
          image: containerImage
          resources: {
            cpu: json(cpu)
            memory: memory
          }
          env: [
            {
              name: 'AppInsights__ConnectionString'
              secretRef: 'app-insights-connection-string'
            }
            {
              name: 'AppInsights__AppId'
              value: appInsightsAppId
            }
            {
              name: 'AppInsights__UseManagedIdentity'
              value: 'true'
            }
            {
              name: 'APPLICATIONINSIGHTS_CONNECTION_STRING'
              secretRef: 'app-insights-connection-string'
            }
            {
              name: 'YouTube__ApiKey'
              value: youtubeApiKey
            }
            {
              name: 'YouTube__ChannelId'
              value: youtubeChannelId
            }
          ]
        }
      ]
      scale: {
        minReplicas: minReplicas
        maxReplicas: maxReplicas
        rules: [
          {
            name: 'http-rule'
            http: {
              metadata: {
                concurrentRequests: '50'
              }
            }
          }
        ]
      }
    }
  }
}

@description('The FQDN of the Container App')
output fqdn string = containerApp.properties.configuration.ingress.fqdn

@description('The URL of the Container App')
output url string = 'https://${containerApp.properties.configuration.ingress.fqdn}'

@description('The name of the Container App')
output name string = containerApp.name

@description('The resource ID of the Container App')
output id string = containerApp.id

@description('The principal ID of the system-assigned Managed Identity')
output principalId string = containerApp.identity.principalId
