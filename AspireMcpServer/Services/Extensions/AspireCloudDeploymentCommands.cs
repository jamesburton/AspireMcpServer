using System.ComponentModel;
using AspireMcpServer.Core;
using ModelContextProtocol.Server;

namespace AspireMcpServer.Services.Extensions;

/// <summary>
/// Provides .NET Aspire cloud deployment target commands
/// </summary>
[McpServerToolType]
public static class AspireCloudDeploymentCommands
{
    [McpServerTool, Description("Deploys Aspire application to Azure Container Apps")]
    public static async Task<string> DeployToAzureContainerAppsAsync(
        [Description("Azure subscription ID")] string subscriptionId,
        [Description("Resource group name")] string resourceGroup,
        [Description("Container Apps environment name")] string environmentName,
        [Description("Application name")] string appName,
        [Description("Azure region")] string? region = null,
        [Description("Container registry URL")] string? registryUrl = null,
        [Description("Environment variables (key=value,key2=value2)")] string? environmentVariables = null,
        [Description("Minimum replicas")] int? minReplicas = null,
        [Description("Maximum replicas")] int? maxReplicas = null,
        [Description("Working directory")] string? workingDirectory = null)
    {
        var args = $"deploy azure-container-apps --subscription {subscriptionId} --resource-group \"{resourceGroup}\" --environment \"{environmentName}\" --app-name \"{appName}\"";
        
        if (!string.IsNullOrEmpty(region))
            args += $" --region {region}";
        if (!string.IsNullOrEmpty(registryUrl))
            args += $" --registry \"{registryUrl}\"";
        if (!string.IsNullOrEmpty(environmentVariables))
            args += $" --env \"{environmentVariables}\"";
        if (minReplicas.HasValue)
            args += $" --min-replicas {minReplicas}";
        if (maxReplicas.HasValue)
            args += $" --max-replicas {maxReplicas}";

        var result = await AspireExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpServerTool, Description("Deploys Aspire application to AWS ECS")]
    public static async Task<string> DeployToAwsEcsAsync(
        [Description("AWS region")] string region,
        [Description("ECS cluster name")] string clusterName,
        [Description("Service name")] string serviceName,
        [Description("Task definition family")] string taskDefinitionFamily,
        [Description("VPC ID")] string? vpcId = null,
        [Description("Subnet IDs (comma-separated)")] string? subnetIds = null,
        [Description("Security group IDs (comma-separated)")] string? securityGroupIds = null,
        [Description("Load balancer target group ARN")] string? targetGroupArn = null,
        [Description("Desired count")] int? desiredCount = null,
        [Description("Working directory")] string? workingDirectory = null)
    {
        var args = $"deploy aws-ecs --region {region} --cluster \"{clusterName}\" --service \"{serviceName}\" --task-definition \"{taskDefinitionFamily}\"";
        
        if (!string.IsNullOrEmpty(vpcId))
            args += $" --vpc-id {vpcId}";
        if (!string.IsNullOrEmpty(subnetIds))
            args += $" --subnet-ids \"{subnetIds}\"";
        if (!string.IsNullOrEmpty(securityGroupIds))
            args += $" --security-groups \"{securityGroupIds}\"";
        if (!string.IsNullOrEmpty(targetGroupArn))
            args += $" --target-group \"{targetGroupArn}\"";
        if (desiredCount.HasValue)
            args += $" --desired-count {desiredCount}";

        var result = await AspireExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpServerTool, Description("Deploys Aspire application to Google Cloud Run")]
    public static async Task<string> DeployToGoogleCloudRunAsync(
        [Description("Google Cloud project ID")] string projectId,
        [Description("Cloud Run region")] string region,
        [Description("Service name")] string serviceName,
        [Description("Container image URL")] string imageUrl,
        [Description("Service account email")] string? serviceAccount = null,
        [Description("Memory limit (e.g., 512Mi, 2Gi)")] string? memory = null,
        [Description("CPU limit (e.g., 1, 2)")] string? cpu = null,
        [Description("Maximum concurrent requests")] int? maxConcurrency = null,
        [Description("Minimum instances")] int? minInstances = null,
        [Description("Maximum instances")] int? maxInstances = null,
        [Description("Working directory")] string? workingDirectory = null)
    {
        var args = $"deploy google-cloud-run --project {projectId} --region {region} --service \"{serviceName}\" --image \"{imageUrl}\"";
        
        if (!string.IsNullOrEmpty(serviceAccount))
            args += $" --service-account \"{serviceAccount}\"";
        if (!string.IsNullOrEmpty(memory))
            args += $" --memory {memory}";
        if (!string.IsNullOrEmpty(cpu))
            args += $" --cpu {cpu}";
        if (maxConcurrency.HasValue)
            args += $" --max-concurrency {maxConcurrency}";
        if (minInstances.HasValue)
            args += $" --min-instances {minInstances}";
        if (maxInstances.HasValue)
            args += $" --max-instances {maxInstances}";

        var result = await AspireExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpServerTool, Description("Configures custom cloud deployment target")]
    public static async Task<string> ConfigureCustomCloudTargetAsync(
        [Description("Target name")] string targetName,
        [Description("Cloud provider (azure,aws,gcp,custom)")] string provider,
        [Description("Deployment endpoint URL")] string endpoint,
        [Description("Authentication method (oauth,apikey,certificate)")] string authMethod,
        [Description("Authentication credentials")] string? credentials = null,
        [Description("Default region")] string? defaultRegion = null,
        [Description("Configuration file path")] string? configFile = null,
        [Description("Working directory")] string? workingDirectory = null)
    {
        var args = $"cloud-target configure \"{targetName}\" --provider {provider} --endpoint \"{endpoint}\" --auth-method {authMethod}";
        
        if (!string.IsNullOrEmpty(credentials))
            args += $" --credentials \"{credentials}\"";
        if (!string.IsNullOrEmpty(defaultRegion))
            args += $" --default-region {defaultRegion}";
        if (!string.IsNullOrEmpty(configFile))
            args += $" --config-file \"{configFile}\"";

        var result = await AspireExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpServerTool, Description("Lists available cloud deployment targets")]
    public static async Task<string> ListCloudTargetsAsync(
        [Description("Filter by provider (azure,aws,gcp,custom)")] string? provider = null,
        [Description("Show detailed information")] bool detailed = false,
        [Description("Include health status")] bool includeHealth = false,
        [Description("Working directory")] string? workingDirectory = null)
    {
        var args = "cloud-target list";
        
        if (!string.IsNullOrEmpty(provider))
            args += $" --provider {provider}";
        if (detailed)
            args += " --detailed";
        if (includeHealth)
            args += " --include-health";

        var result = await AspireExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpServerTool, Description("Tests connection to a cloud deployment target")]
    public static async Task<string> TestCloudTargetConnectionAsync(
        [Description("Target name")] string targetName,
        [Description("Connection timeout in seconds")] int? timeout = null,
        [Description("Validate credentials")] bool validateCredentials = true,
        [Description("Check resource permissions")] bool checkPermissions = false,
        [Description("Working directory")] string? workingDirectory = null)
    {
        var args = $"cloud-target test \"{targetName}\"";
        
        if (timeout.HasValue)
            args += $" --timeout {timeout}";
        if (validateCredentials)
            args += " --validate-credentials";
        if (checkPermissions)
            args += " --check-permissions";

        var result = await AspireExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpServerTool, Description("Manages cloud deployment environments")]
    public static async Task<string> ManageCloudEnvironmentAsync(
        [Description("Action (create,update,delete,list)")] string action,
        [Description("Environment name")] string? environmentName = null,
        [Description("Target cloud provider")] string? targetProvider = null,
        [Description("Environment configuration file")] string? configFile = null,
        [Description("Environment variables (key=value,key2=value2)")] string? environmentVariables = null,
        [Description("Environment tier (dev,staging,prod)")] string? tier = null,
        [Description("Working directory")] string? workingDirectory = null)
    {
        var args = $"cloud-environment {action}";
        
        if (!string.IsNullOrEmpty(environmentName))
            args += $" \"{environmentName}\"";
        if (!string.IsNullOrEmpty(targetProvider))
            args += $" --provider {targetProvider}";
        if (!string.IsNullOrEmpty(configFile))
            args += $" --config-file \"{configFile}\"";
        if (!string.IsNullOrEmpty(environmentVariables))
            args += $" --env \"{environmentVariables}\"";
        if (!string.IsNullOrEmpty(tier))
            args += $" --tier {tier}";

        var result = await AspireExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpServerTool, Description("Manages cloud resource scaling policies")]
    public static async Task<string> ManageCloudScalingAsync(
        [Description("Target name")] string targetName,
        [Description("Resource name")] string resourceName,
        [Description("Scaling policy (manual,auto,scheduled)")] string scalingPolicy,
        [Description("Minimum instances")] int? minInstances = null,
        [Description("Maximum instances")] int? maxInstances = null,
        [Description("CPU target percentage")] int? cpuTargetPercentage = null,
        [Description("Memory target percentage")] int? memoryTargetPercentage = null,
        [Description("Schedule expression (for scheduled scaling)")] string? scheduleExpression = null,
        [Description("Working directory")] string? workingDirectory = null)
    {
        var args = $"cloud-scaling configure \"{targetName}\" \"{resourceName}\" --policy {scalingPolicy}";
        
        if (minInstances.HasValue)
            args += $" --min-instances {minInstances}";
        if (maxInstances.HasValue)
            args += $" --max-instances {maxInstances}";
        if (cpuTargetPercentage.HasValue)
            args += $" --cpu-target {cpuTargetPercentage}";
        if (memoryTargetPercentage.HasValue)
            args += $" --memory-target {memoryTargetPercentage}";
        if (!string.IsNullOrEmpty(scheduleExpression))
            args += $" --schedule \"{scheduleExpression}\"";

        var result = await AspireExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpServerTool, Description("Manages multi-cloud deployment strategies")]
    public static async Task<string> ManageMultiCloudDeploymentAsync(
        [Description("Strategy name")] string strategyName,
        [Description("Primary cloud provider")] string primaryProvider,
        [Description("Secondary cloud providers (comma-separated)")] string? secondaryProviders = null,
        [Description("Traffic distribution (primary:secondary percentages)")] string? trafficDistribution = null,
        [Description("Failover policy (automatic,manual)")] string? failoverPolicy = null,
        [Description("Health check configuration")] string? healthCheckConfig = null,
        [Description("Working directory")] string? workingDirectory = null)
    {
        var args = $"multi-cloud strategy configure \"{strategyName}\" --primary {primaryProvider}";
        
        if (!string.IsNullOrEmpty(secondaryProviders))
            args += $" --secondary \"{secondaryProviders}\"";
        if (!string.IsNullOrEmpty(trafficDistribution))
            args += $" --traffic-distribution \"{trafficDistribution}\"";
        if (!string.IsNullOrEmpty(failoverPolicy))
            args += $" --failover-policy {failoverPolicy}";
        if (!string.IsNullOrEmpty(healthCheckConfig))
            args += $" --health-check \"{healthCheckConfig}\"";

        var result = await AspireExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }
}
