@description('Name of the Container Apps Environment')
param name string

@description('Azure region for the resource')
param location string

@description('Tags to apply to the resource')
param tags object = {}

@description('Resource ID of the Log Analytics workspace')
param logAnalyticsWorkspaceId string

@description('Customer ID (workspace ID) of the Log Analytics workspace')
param logAnalyticsCustomerId string

resource logAnalytics 'Microsoft.OperationalInsights/workspaces@2023-09-01' existing = {
  name: last(split(logAnalyticsWorkspaceId, '/'))
}

resource containerAppEnv 'Microsoft.App/managedEnvironments@2024-03-01' = {
  name: name
  location: location
  tags: tags
  properties: {
    appLogsConfiguration: {
      destination: 'log-analytics'
      logAnalyticsConfiguration: {
        customerId: logAnalyticsCustomerId
        sharedKey: logAnalytics.listKeys().primarySharedKey
      }
    }
  }
}

@description('The resource ID of the Container Apps Environment')
output id string = containerAppEnv.id

@description('The name of the Container Apps Environment')
output name string = containerAppEnv.name

@description('The default domain of the Container Apps Environment')
output defaultDomain string = containerAppEnv.properties.defaultDomain
