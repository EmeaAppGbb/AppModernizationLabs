targetScope = 'resourceGroup'

// ---------------------------------------------------------------------------
// Parameters
// ---------------------------------------------------------------------------

@minLength(1)
@maxLength(64)
@description('Name of the environment used for resource naming')
param environmentName string

@description('Azure region for all resources (defaults to resource group location)')
param location string = resourceGroup().location

@description('Docker image tag for the dashboard container')
param dashboardImageTag string = 'latest'

@description('Application Insights API key (create once via CLI, pass as env var)')
@secure()
param appInsightsApiKey string = ''

@description('YouTube Data API key (optional — for video metrics)')
param youtubeApiKey string = ''

@description('YouTube Channel ID (optional — for channel metrics)')
param youtubeChannelId string = ''

// ---------------------------------------------------------------------------
// Variables
// ---------------------------------------------------------------------------

var abbrs = loadJsonContent('./abbreviations.json')
var resourceToken = toLower(uniqueString(resourceGroup().id, environmentName, location))
var envName = toLower(environmentName)
// Container App names must be ≤32 chars, lowercase, alphanumeric + hyphens
// Format: ca{env}-{token} → 2 + env + 1 + 13 = env must be ≤16
var shortEnvName = length(envName) > 16 ? substring(envName, 0, 16) : envName
var tags = {
  'azd-env-name': environmentName
  project: 'appmodlabs-dashboard'
}

// ---------------------------------------------------------------------------
// Modules
// ---------------------------------------------------------------------------

module logAnalytics 'modules/log-analytics.bicep' = {
  name: 'log-analytics'
  params: {
    name: '${abbrs.operationalInsightsWorkspaces}${envName}-${resourceToken}'
    location: location
    tags: tags
  }
}

module appInsights 'modules/app-insights.bicep' = {
  name: 'app-insights'
  params: {
    name: '${abbrs.insightsComponents}${envName}-${resourceToken}'
    location: location
    tags: tags
    logAnalyticsWorkspaceId: logAnalytics.outputs.id
  }
}

module containerRegistry 'modules/container-registry.bicep' = {
  name: 'container-registry'
  params: {
    name: '${abbrs.containerRegistryRegistries}${replace(envName, '-', '')}${resourceToken}'
    location: location
    tags: tags
  }
}

module containerAppEnv 'modules/container-app-env.bicep' = {
  name: 'container-app-env'
  params: {
    name: '${abbrs.appManagedEnvironments}${shortEnvName}-${resourceToken}'
    location: location
    tags: tags
    logAnalyticsWorkspaceId: logAnalytics.outputs.id
    logAnalyticsCustomerId: logAnalytics.outputs.customerId
  }
}

module containerApp 'modules/container-app.bicep' = {
  name: 'container-app'
  params: {
    name: '${abbrs.appContainerApps}${shortEnvName}-${resourceToken}'
    location: location
    tags: union(tags, { 'azd-service-name': 'dashboard' })
    containerAppEnvId: containerAppEnv.outputs.id
    imageName: 'dashboard'
    imageTag: dashboardImageTag
    containerRegistryLoginServer: containerRegistry.outputs.loginServer
    containerRegistryName: containerRegistry.outputs.name
    appInsightsConnectionString: appInsights.outputs.connectionString
    appInsightsAppId: appInsights.outputs.appId
    appInsightsApiKey: appInsightsApiKey
    youtubeApiKey: youtubeApiKey
    youtubeChannelId: youtubeChannelId
  }
}

// ---------------------------------------------------------------------------
// Outputs
// ---------------------------------------------------------------------------

@description('Application Insights connection string for configuring the gallery website JS SDK')
output APP_INSIGHTS_CONNECTION_STRING string = appInsights.outputs.connectionString

@description('Application Insights App ID for the dashboard API queries')
output APP_INSIGHTS_APP_ID string = appInsights.outputs.appId

@description('The FQDN URL of the deployed dashboard Container App')
output DASHBOARD_URL string = containerApp.outputs.url

@description('The Azure Container Registry login server endpoint')
output AZURE_CONTAINER_REGISTRY_ENDPOINT string = containerRegistry.outputs.loginServer
