using System.ComponentModel;
using AspireMcpServer.Core;
using ModelContextProtocol.Server;

namespace AspireMcpServer.Services.Extensions;

/// <summary>
/// Provides .NET Aspire DevOps and CI/CD integration commands
/// </summary>
[McpServerToolType]
public static class AspireDevOpsCommands
{
    [McpServerTool, Description("Generates GitHub Actions workflow for Aspire application")]
    public static async Task<string> GenerateGitHubActionsWorkflowAsync(
        [Description("Workflow name")] string workflowName,
        [Description("Trigger events (push,pull_request,schedule)")] string triggers,
        [Description("Target environment (dev,staging,prod)")] string? targetEnvironment = null,
        [Description("Build configuration")] string? buildConfiguration = null,
        [Description("Container registry")] string? containerRegistry = null,
        [Description("Deployment target (azure,aws,gcp,k8s)")] string? deploymentTarget = null,
        [Description("Include security scanning")] bool includeSecurity = true,
        [Description("Include performance testing")] bool includePerformanceTesting = false,
        [Description("Output file path")] string? outputPath = null,
        [Description("Working directory")] string? workingDirectory = null)
    {
        var args = $"devops generate github-actions --name \"{workflowName}\" --triggers \"{triggers}\"";
        
        if (!string.IsNullOrEmpty(targetEnvironment))
            args += $" --environment {targetEnvironment}";
        if (!string.IsNullOrEmpty(buildConfiguration))
            args += $" --build-config {buildConfiguration}";
        if (!string.IsNullOrEmpty(containerRegistry))
            args += $" --registry \"{containerRegistry}\"";
        if (!string.IsNullOrEmpty(deploymentTarget))
            args += $" --deploy-target {deploymentTarget}";
        if (includeSecurity)
            args += " --include-security";
        if (includePerformanceTesting)
            args += " --include-perf-tests";
        if (!string.IsNullOrEmpty(outputPath))
            args += $" --output \"{outputPath}\"";

        var result = await AspireExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpServerTool, Description("Generates Azure DevOps pipeline for Aspire application")]
    public static async Task<string> GenerateAzureDevOpsPipelineAsync(
        [Description("Pipeline name")] string pipelineName,
        [Description("Azure subscription ID")] string? subscriptionId = null,
        [Description("Resource group")] string? resourceGroup = null,
        [Description("Service connection name")] string? serviceConnection = null,
        [Description("Build agent pool")] string? agentPool = null,
        [Description("Variable groups")] string? variableGroups = null,
        [Description("Deployment stages (comma-separated)")] string? deploymentStages = null,
        [Description("Include approval gates")] bool includeApprovals = true,
        [Description("Output file path")] string? outputPath = null,
        [Description("Working directory")] string? workingDirectory = null)
    {
        var args = $"devops generate azure-devops --name \"{pipelineName}\"";
        
        if (!string.IsNullOrEmpty(subscriptionId))
            args += $" --subscription {subscriptionId}";
        if (!string.IsNullOrEmpty(resourceGroup))
            args += $" --resource-group \"{resourceGroup}\"";
        if (!string.IsNullOrEmpty(serviceConnection))
            args += $" --service-connection \"{serviceConnection}\"";
        if (!string.IsNullOrEmpty(agentPool))
            args += $" --agent-pool \"{agentPool}\"";
        if (!string.IsNullOrEmpty(variableGroups))
            args += $" --variable-groups \"{variableGroups}\"";
        if (!string.IsNullOrEmpty(deploymentStages))
            args += $" --stages \"{deploymentStages}\"";
        if (includeApprovals)
            args += " --include-approvals";
        if (!string.IsNullOrEmpty(outputPath))
            args += $" --output \"{outputPath}\"";

        var result = await AspireExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpServerTool, Description("Generates GitLab CI/CD pipeline for Aspire application")]
    public static async Task<string> GenerateGitLabPipelineAsync(
        [Description("Pipeline name")] string pipelineName,
        [Description("GitLab stages (comma-separated)")] string? stages = null,
        [Description("Container registry URL")] string? registryUrl = null,
        [Description("Deployment environments")] string? environments = null,
        [Description("Runner tags")] string? runnerTags = null,
        [Description("Include code quality checks")] bool includeCodeQuality = true,
        [Description("Include dependency scanning")] bool includeDependencyScanning = true,
        [Description("Output file path")] string? outputPath = null,
        [Description("Working directory")] string? workingDirectory = null)
    {
        var args = $"devops generate gitlab-ci --name \"{pipelineName}\"";
        
        if (!string.IsNullOrEmpty(stages))
            args += $" --stages \"{stages}\"";
        if (!string.IsNullOrEmpty(registryUrl))
            args += $" --registry \"{registryUrl}\"";
        if (!string.IsNullOrEmpty(environments))
            args += $" --environments \"{environments}\"";
        if (!string.IsNullOrEmpty(runnerTags))
            args += $" --runner-tags \"{runnerTags}\"";
        if (includeCodeQuality)
            args += " --include-code-quality";
        if (includeDependencyScanning)
            args += " --include-dependency-scanning";
        if (!string.IsNullOrEmpty(outputPath))
            args += $" --output \"{outputPath}\"";

        var result = await AspireExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpServerTool, Description("Configures automated testing pipeline for Aspire application")]
    public static async Task<string> ConfigureTestingPipelineAsync(
        [Description("Test suite name")] string testSuiteName,
        [Description("Test types (unit,integration,e2e,performance,security)")] string testTypes,
        [Description("Test framework (xunit,nunit,mstest)")] string? testFramework = null,
        [Description("Test environment configuration")] string? testEnvironment = null,
        [Description("Coverage threshold percentage")] int? coverageThreshold = null,
        [Description("Performance benchmarks")] string? performanceBenchmarks = null,
        [Description("Test data configuration")] string? testDataConfig = null,
        [Description("Working directory")] string? workingDirectory = null)
    {
        var args = $"testing configure --suite \"{testSuiteName}\" --types \"{testTypes}\"";
        
        if (!string.IsNullOrEmpty(testFramework))
            args += $" --framework {testFramework}";
        if (!string.IsNullOrEmpty(testEnvironment))
            args += $" --environment \"{testEnvironment}\"";
        if (coverageThreshold.HasValue)
            args += $" --coverage-threshold {coverageThreshold}";
        if (!string.IsNullOrEmpty(performanceBenchmarks))
            args += $" --performance-benchmarks \"{performanceBenchmarks}\"";
        if (!string.IsNullOrEmpty(testDataConfig))
            args += $" --test-data \"{testDataConfig}\"";

        var result = await AspireExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpServerTool, Description("Sets up infrastructure as code for Aspire application")]
    public static async Task<string> SetupInfrastructureAsCodeAsync(
        [Description("IaC tool (terraform,bicep,arm,pulumi,cdk)")] string iacTool,
        [Description("Cloud provider (azure,aws,gcp)")] string cloudProvider,
        [Description("Infrastructure template name")] string templateName,
        [Description("Resource naming convention")] string? namingConvention = null,
        [Description("Environment configuration")] string? environmentConfig = null,
        [Description("State management configuration")] string? stateConfig = null,
        [Description("Include monitoring resources")] bool includeMonitoring = true,
        [Description("Include security resources")] bool includeSecurity = true,
        [Description("Output directory")] string? outputDirectory = null,
        [Description("Working directory")] string? workingDirectory = null)
    {
        var args = $"iac setup --tool {iacTool} --provider {cloudProvider} --template \"{templateName}\"";
        
        if (!string.IsNullOrEmpty(namingConvention))
            args += $" --naming-convention \"{namingConvention}\"";
        if (!string.IsNullOrEmpty(environmentConfig))
            args += $" --environment-config \"{environmentConfig}\"";
        if (!string.IsNullOrEmpty(stateConfig))
            args += $" --state-config \"{stateConfig}\"";
        if (includeMonitoring)
            args += " --include-monitoring";
        if (includeSecurity)
            args += " --include-security";
        if (!string.IsNullOrEmpty(outputDirectory))
            args += $" --output \"{outputDirectory}\"";

        var result = await AspireExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpServerTool, Description("Configures deployment strategies for Aspire application")]
    public static async Task<string> ConfigureDeploymentStrategyAsync(
        [Description("Strategy type (blue-green,canary,rolling,recreate)")] string strategyType,
        [Description("Strategy name")] string strategyName,
        [Description("Health check configuration")] string? healthCheckConfig = null,
        [Description("Rollback criteria")] string? rollbackCriteria = null,
        [Description("Traffic splitting configuration")] string? trafficSplitting = null,
        [Description("Deployment timeout in minutes")] int? deploymentTimeout = null,
        [Description("Post-deployment validation")] string? postDeploymentValidation = null,
        [Description("Working directory")] string? workingDirectory = null)
    {
        var args = $"deployment-strategy configure --type {strategyType} --name \"{strategyName}\"";
        
        if (!string.IsNullOrEmpty(healthCheckConfig))
            args += $" --health-check \"{healthCheckConfig}\"";
        if (!string.IsNullOrEmpty(rollbackCriteria))
            args += $" --rollback-criteria \"{rollbackCriteria}\"";
        if (!string.IsNullOrEmpty(trafficSplitting))
            args += $" --traffic-splitting \"{trafficSplitting}\"";
        if (deploymentTimeout.HasValue)
            args += $" --timeout {deploymentTimeout}";
        if (!string.IsNullOrEmpty(postDeploymentValidation))
            args += $" --post-deployment-validation \"{postDeploymentValidation}\"";

        var result = await AspireExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpServerTool, Description("Sets up observability and monitoring for Aspire application")]
    public static async Task<string> SetupObservabilityAsync(
        [Description("Observability stack (prometheus,grafana,jaeger,elk,datadog,newrelic)")] string observabilityStack,
        [Description("Metrics configuration")] string? metricsConfig = null,
        [Description("Logging configuration")] string? loggingConfig = null,
        [Description("Tracing configuration")] string? tracingConfig = null,
        [Description("Dashboard configuration")] string? dashboardConfig = null,
        [Description("Alert rules configuration")] string? alertRules = null,
        [Description("Retention policies")] string? retentionPolicies = null,
        [Description("Working directory")] string? workingDirectory = null)
    {
        var args = $"observability setup --stack {observabilityStack}";
        
        if (!string.IsNullOrEmpty(metricsConfig))
            args += $" --metrics \"{metricsConfig}\"";
        if (!string.IsNullOrEmpty(loggingConfig))
            args += $" --logging \"{loggingConfig}\"";
        if (!string.IsNullOrEmpty(tracingConfig))
            args += $" --tracing \"{tracingConfig}\"";
        if (!string.IsNullOrEmpty(dashboardConfig))
            args += $" --dashboard \"{dashboardConfig}\"";
        if (!string.IsNullOrEmpty(alertRules))
            args += $" --alerts \"{alertRules}\"";
        if (!string.IsNullOrEmpty(retentionPolicies))
            args += $" --retention \"{retentionPolicies}\"";

        var result = await AspireExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpServerTool, Description("Configures secrets management for Aspire application")]
    public static async Task<string> ConfigureSecretsManagementAsync(
        [Description("Secrets provider (azure-keyvault,aws-secrets,gcp-secrets,kubernetes,hashicorp-vault)")] string secretsProvider,
        [Description("Secret categories (database,api-keys,certificates,tokens)")] string? secretCategories = null,
        [Description("Rotation policy")] string? rotationPolicy = null,
        [Description("Access policies")] string? accessPolicies = null,
        [Description("Encryption configuration")] string? encryptionConfig = null,
        [Description("Audit configuration")] string? auditConfig = null,
        [Description("Working directory")] string? workingDirectory = null)
    {
        var args = $"secrets configure --provider {secretsProvider}";
        
        if (!string.IsNullOrEmpty(secretCategories))
            args += $" --categories \"{secretCategories}\"";
        if (!string.IsNullOrEmpty(rotationPolicy))
            args += $" --rotation-policy \"{rotationPolicy}\"";
        if (!string.IsNullOrEmpty(accessPolicies))
            args += $" --access-policies \"{accessPolicies}\"";
        if (!string.IsNullOrEmpty(encryptionConfig))
            args += $" --encryption \"{encryptionConfig}\"";
        if (!string.IsNullOrEmpty(auditConfig))
            args += $" --audit \"{auditConfig}\"";

        var result = await AspireExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpServerTool, Description("Generates compliance and governance reports for Aspire application")]
    public static async Task<string> GenerateComplianceReportAsync(
        [Description("Compliance framework (soc2,iso27001,hipaa,gdpr,pci-dss)")] string complianceFramework,
        [Description("Report scope (security,data-protection,operational)")] string reportScope,
        [Description("Assessment period")] string? assessmentPeriod = null,
        [Description("Include remediation recommendations")] bool includeRemediation = true,
        [Description("Output format (pdf,html,json,csv)")] string? outputFormat = null,
        [Description("Output file path")] string? outputPath = null,
        [Description("Working directory")] string? workingDirectory = null)
    {
        var args = $"compliance report --framework {complianceFramework} --scope {reportScope}";
        
        if (!string.IsNullOrEmpty(assessmentPeriod))
            args += $" --period \"{assessmentPeriod}\"";
        if (includeRemediation)
            args += " --include-remediation";
        if (!string.IsNullOrEmpty(outputFormat))
            args += $" --format {outputFormat}";
        if (!string.IsNullOrEmpty(outputPath))
            args += $" --output \"{outputPath}\"";

        var result = await AspireExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpServerTool, Description("Configures disaster recovery for Aspire application")]
    public static async Task<string> ConfigureDisasterRecoveryAsync(
        [Description("DR strategy (backup-restore,pilot-light,warm-standby,multi-site)")] string drStrategy,
        [Description("Recovery time objective (RTO) in minutes")] int rto,
        [Description("Recovery point objective (RPO) in minutes")] int rpo,
        [Description("Primary region")] string primaryRegion,
        [Description("Secondary region")] string secondaryRegion,
        [Description("Backup configuration")] string? backupConfig = null,
        [Description("Replication configuration")] string? replicationConfig = null,
        [Description("Failover testing schedule")] string? testingSchedule = null,
        [Description("Working directory")] string? workingDirectory = null)
    {
        var args = $"disaster-recovery configure --strategy {drStrategy} --rto {rto} --rpo {rpo} --primary-region {primaryRegion} --secondary-region {secondaryRegion}";
        
        if (!string.IsNullOrEmpty(backupConfig))
            args += $" --backup \"{backupConfig}\"";
        if (!string.IsNullOrEmpty(replicationConfig))
            args += $" --replication \"{replicationConfig}\"";
        if (!string.IsNullOrEmpty(testingSchedule))
            args += $" --testing-schedule \"{testingSchedule}\"";

        var result = await AspireExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }
}
