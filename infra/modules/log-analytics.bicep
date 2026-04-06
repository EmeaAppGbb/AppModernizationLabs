@description('Name of the Log Analytics workspace')
param name string

@description('Azure region for the resource')
param location string

@description('Tags to apply to the resource')
param tags object = {}

@description('Log retention in days')
param retentionInDays int = 30

resource logAnalytics 'Microsoft.OperationalInsights/workspaces@2023-09-01' = {
  name: name
  location: location
  tags: tags
  properties: {
    sku: {
      name: 'PerGB2018'
    }
    retentionInDays: retentionInDays
  }
}

@description('The resource ID of the Log Analytics workspace')
output id string = logAnalytics.id

@description('The name of the Log Analytics workspace')
output name string = logAnalytics.name

@description('The customer ID (workspace ID) of the Log Analytics workspace')
output customerId string = logAnalytics.properties.customerId
