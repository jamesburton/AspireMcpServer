using System.ComponentModel;
using AspireMcpServer.Core;
using ModelContextProtocol.Server;

namespace AspireMcpServer.Services.Extensions;

/// <summary>
/// Provides .NET Aspire resource monitoring and health check commands
/// </summary>
[McpServerToolType]
public static class AspireMonitoringCommands
{
    [McpServerTool, Description("Gets comprehensive health status of all Aspire resources")]
    public static async Task<string> GetHealthStatusAsync(
        [Description("Target resource name (optional, checks all if not specified)")] string? resourceName = null,
        [Description("Include dependency health")] bool includeDependencies = true,
        [Description("Health check timeout in seconds")] int? timeout = null,
        [Description("Working directory")] string? workingDirectory = null)
    {
        var args = "health check";
        
        if (!string.IsNullOrEmpty(resourceName))
            args += $" --resource \"{resourceName}\"";
        if (includeDependencies)
            args += " --include-dependencies";
        if (timeout.HasValue)
            args += $" --timeout {timeout}";

        var result = await AspireExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpServerTool, Description("Monitors resource metrics in real-time")]
    public static async Task<string> MonitorResourceMetricsAsync(
        [Description("Target resource name")] string resourceName,
        [Description("Metrics to monitor (cpu,memory,network,disk)")] string? metrics = null,
        [Description("Monitoring duration in seconds")] int? duration = null,
        [Description("Sampling interval in seconds")] int? interval = null,
        [Description("Export format (json,csv,prometheus)")] string? format = null,
        [Description("Working directory")] string? workingDirectory = null)
    {
        var args = $"monitor metrics \"{resourceName}\"";
        
        if (!string.IsNullOrEmpty(metrics))
            args += $" --metrics {metrics}";
        if (duration.HasValue)
            args += $" --duration {duration}";
        if (interval.HasValue)
            args += $" --interval {interval}";
        if (!string.IsNullOrEmpty(format))
            args += $" --format {format}";

        var result = await AspireExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpServerTool, Description("Gets resource utilization statistics")]
    public static async Task<string> GetResourceUtilizationAsync(
        [Description("Target resource name (optional, gets all if not specified)")] string? resourceName = null,
        [Description("Time range (1h,24h,7d,30d)")] string? timeRange = null,
        [Description("Include historical data")] bool includeHistory = false,
        [Description("Working directory")] string? workingDirectory = null)
    {
        var args = "resource utilization";
        
        if (!string.IsNullOrEmpty(resourceName))
            args += $" --resource \"{resourceName}\"";
        if (!string.IsNullOrEmpty(timeRange))
            args += $" --time-range {timeRange}";
        if (includeHistory)
            args += " --include-history";

        var result = await AspireExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpServerTool, Description("Sets up monitoring alerts for resources")]
    public static async Task<string> SetupMonitoringAlertsAsync(
        [Description("Target resource name")] string resourceName,
        [Description("Alert rules configuration file")] string? rulesFile = null,
        [Description("CPU threshold percentage")] int? cpuThreshold = null,
        [Description("Memory threshold percentage")] int? memoryThreshold = null,
        [Description("Disk threshold percentage")] int? diskThreshold = null,
        [Description("Network threshold in Mbps")] int? networkThreshold = null,
        [Description("Alert webhook URL")] string? webhookUrl = null,
        [Description("Working directory")] string? workingDirectory = null)
    {
        var args = $"monitor alerts setup \"{resourceName}\"";
        
        if (!string.IsNullOrEmpty(rulesFile))
            args += $" --rules-file \"{rulesFile}\"";
        if (cpuThreshold.HasValue)
            args += $" --cpu-threshold {cpuThreshold}";
        if (memoryThreshold.HasValue)
            args += $" --memory-threshold {memoryThreshold}";
        if (diskThreshold.HasValue)
            args += $" --disk-threshold {diskThreshold}";
        if (networkThreshold.HasValue)
            args += $" --network-threshold {networkThreshold}";
        if (!string.IsNullOrEmpty(webhookUrl))
            args += $" --webhook \"{webhookUrl}\"";

        var result = await AspireExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpServerTool, Description("Gets active monitoring alerts")]
    public static async Task<string> GetActiveAlertsAsync(
        [Description("Alert severity (info,warning,error,critical)")] string? severity = null,
        [Description("Resource filter")] string? resourceFilter = null,
        [Description("Include resolved alerts")] bool includeResolved = false,
        [Description("Working directory")] string? workingDirectory = null)
    {
        var args = "monitor alerts list";
        
        if (!string.IsNullOrEmpty(severity))
            args += $" --severity {severity}";
        if (!string.IsNullOrEmpty(resourceFilter))
            args += $" --resource \"{resourceFilter}\"";
        if (includeResolved)
            args += " --include-resolved";

        var result = await AspireExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpServerTool, Description("Performs resource dependency analysis")]
    public static async Task<string> AnalyzeDependenciesAsync(
        [Description("Target resource name")] string resourceName,
        [Description("Analysis depth")] int? depth = null,
        [Description("Include external dependencies")] bool includeExternal = true,
        [Description("Generate dependency graph")] bool generateGraph = false,
        [Description("Output format (text,json,mermaid)")] string? format = null,
        [Description("Working directory")] string? workingDirectory = null)
    {
        var args = $"analyze dependencies \"{resourceName}\"";
        
        if (depth.HasValue)
            args += $" --depth {depth}";
        if (includeExternal)
            args += " --include-external";
        if (generateGraph)
            args += " --generate-graph";
        if (!string.IsNullOrEmpty(format))
            args += $" --format {format}";

        var result = await AspireExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpServerTool, Description("Performs security scan on Aspire resources")]
    public static async Task<string> SecurityScanAsync(
        [Description("Target resource name (optional, scans all if not specified)")] string? resourceName = null,
        [Description("Scan type (vulnerabilities,configuration,secrets,compliance)")] string? scanType = null,
        [Description("Severity threshold (low,medium,high,critical)")] string? severityThreshold = null,
        [Description("Generate security report")] bool generateReport = false,
        [Description("Report format (json,html,pdf)")] string? reportFormat = null,
        [Description("Working directory")] string? workingDirectory = null)
    {
        var args = "security scan";
        
        if (!string.IsNullOrEmpty(resourceName))
            args += $" --resource \"{resourceName}\"";
        if (!string.IsNullOrEmpty(scanType))
            args += $" --scan-type {scanType}";
        if (!string.IsNullOrEmpty(severityThreshold))
            args += $" --severity {severityThreshold}";
        if (generateReport)
            args += " --generate-report";
        if (!string.IsNullOrEmpty(reportFormat))
            args += $" --report-format {reportFormat}";

        var result = await AspireExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpServerTool, Description("Generates resource performance report")]
    public static async Task<string> GeneratePerformanceReportAsync(
        [Description("Report time range (1h,24h,7d,30d)")] string timeRange,
        [Description("Resource filter (optional, includes all if not specified)")] string? resourceFilter = null,
        [Description("Report format (html,pdf,json,csv)")] string? format = null,
        [Description("Include recommendations")] bool includeRecommendations = true,
        [Description("Output file path")] string? outputPath = null,
        [Description("Working directory")] string? workingDirectory = null)
    {
        var args = $"report performance --time-range {timeRange}";
        
        if (!string.IsNullOrEmpty(resourceFilter))
            args += $" --resource \"{resourceFilter}\"";
        if (!string.IsNullOrEmpty(format))
            args += $" --format {format}";
        if (includeRecommendations)
            args += " --include-recommendations";
        if (!string.IsNullOrEmpty(outputPath))
            args += $" --output \"{outputPath}\"";

        var result = await AspireExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpServerTool, Description("Configures resource scaling policies")]
    public static async Task<string> ConfigureAutoScalingAsync(
        [Description("Target resource name")] string resourceName,
        [Description("Minimum instances")] int? minInstances = null,
        [Description("Maximum instances")] int? maxInstances = null,
        [Description("CPU scale threshold percentage")] int? cpuThreshold = null,
        [Description("Memory scale threshold percentage")] int? memoryThreshold = null,
        [Description("Scale up cooldown in seconds")] int? scaleUpCooldown = null,
        [Description("Scale down cooldown in seconds")] int? scaleDownCooldown = null,
        [Description("Working directory")] string? workingDirectory = null)
    {
        var args = $"scaling configure \"{resourceName}\"";
        
        if (minInstances.HasValue)
            args += $" --min-instances {minInstances}";
        if (maxInstances.HasValue)
            args += $" --max-instances {maxInstances}";
        if (cpuThreshold.HasValue)
            args += $" --cpu-threshold {cpuThreshold}";
        if (memoryThreshold.HasValue)
            args += $" --memory-threshold {memoryThreshold}";
        if (scaleUpCooldown.HasValue)
            args += $" --scale-up-cooldown {scaleUpCooldown}";
        if (scaleDownCooldown.HasValue)
            args += $" --scale-down-cooldown {scaleDownCooldown}";

        var result = await AspireExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }
}
