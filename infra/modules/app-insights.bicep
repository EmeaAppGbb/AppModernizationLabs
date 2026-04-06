@description('Name of the Application Insights instance')
param name string

@description('Azure region for the resource')
param location string

@description('Tags to apply to the resource')
param tags object = {}

@description('Resource ID of the Log Analytics workspace to connect to')
param logAnalyticsWorkspaceId string

resource appInsights 'Microsoft.Insights/components@2020-02-02' = {
  name: name
  location: location
  tags: tags
  kind: 'web'
  properties: {
    Application_Type: 'web'
    WorkspaceResourceId: logAnalyticsWorkspaceId
    IngestionMode: 'LogAnalytics'
    publicNetworkAccessForIngestion: 'Enabled'
    publicNetworkAccessForQuery: 'Enabled'
  }
}

resource apiKey 'Microsoft.Insights/components/ApiKeys@2015-05-01' = {
  parent: appInsights
  name: 'dashboard-read-key'
  properties: {
    linkedReadProperties: [
      '${appInsights.id}/api'
    ]
    linkedWriteProperties: []
  }
}

@description('The connection string for Application Insights')
output connectionString string = appInsights.properties.ConnectionString

@description('The instrumentation key for Application Insights')
output instrumentationKey string = appInsights.properties.InstrumentationKey

@description('The App ID for Application Insights (used for API queries)')
output appId string = appInsights.properties.AppId

@description('The API key for Application Insights REST API queries')
output apiKey string = apiKey.properties.apiKey

@description('The resource ID of the Application Insights instance')
output id string = appInsights.id

@description('The name of the Application Insights instance')
output name string = appInsights.name
