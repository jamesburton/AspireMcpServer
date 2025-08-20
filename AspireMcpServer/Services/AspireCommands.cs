using System.ComponentModel;
using AspireMcpServer.Core;
using ModelContextProtocol.Server;

namespace AspireMcpServer.Services;

/// <summary>
/// Provides .NET Aspire CLI commands as MCP tools
/// </summary>
[McpServerToolType]
public static class AspireCommands
{
    [McpServerTool, Description("Gets the installed .NET Aspire CLI version")]
    public static async Task<string> GetVersionAsync()
    {
        var result = await AspireExecutor.ExecuteAsync("--version");
        return result.ToString();
    }

    [McpServerTool, Description("Displays .NET Aspire CLI help information")]
    public static async Task<string> GetHelpAsync(
        [Description("Specific command to get help for")] string? command = null)
    {
        var args = "help";
        if (!string.IsNullOrEmpty(command))
            args += $" {command}";

        var result = await AspireExecutor.ExecuteAsync(args);
        return result.ToString();
    }

    [McpServerTool, Description("Initializes a new .NET Aspire project")]
    public static async Task<string> InitProjectAsync(
        [Description("Project name")] string? name = null,
        [Description("Output directory")] string? output = null,
        [Description("Template to use")] string? template = null,
        [Description("Working directory")] string? workingDirectory = null)
    {
        var args = "init";
        
        if (!string.IsNullOrEmpty(name))
            args += $" --name {name}";
        if (!string.IsNullOrEmpty(output))
            args += $" --output {output}";
        if (!string.IsNullOrEmpty(template))
            args += $" --template {template}";

        var result = await AspireExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpServerTool, Description("Starts the .NET Aspire development environment")]
    public static async Task<string> DevAsync(
        [Description("Path to the AppHost project")] string? project = null,
        [Description("Launch browser")] bool launchBrowser = true,
        [Description("Port for the dashboard")] int? port = null,
        [Description("Working directory")] string? workingDirectory = null)
    {
        var args = "dev";
        
        if (!string.IsNullOrEmpty(project))
            args += $" --project {project}";
        if (!launchBrowser)
            args += " --no-launch-browser";
        if (port.HasValue)
            args += $" --port {port}";

        var result = await AspireExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpServerTool, Description("Builds a .NET Aspire application")]
    public static async Task<string> BuildAsync(
        [Description("Path to the AppHost project")] string? project = null,
        [Description("Build configuration (Debug/Release)")] string? configuration = null,
        [Description("Target framework")] string? framework = null,
        [Description("Working directory")] string? workingDirectory = null)
    {
        var args = "build";
        
        if (!string.IsNullOrEmpty(project))
            args += $" --project {project}";
        if (!string.IsNullOrEmpty(configuration))
            args += $" --configuration {configuration}";
        if (!string.IsNullOrEmpty(framework))
            args += $" --framework {framework}";

        var result = await AspireExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpServerTool, Description("Runs a .NET Aspire application")]
    public static async Task<string> RunAsync(
        [Description("Path to the AppHost project")] string? project = null,
        [Description("Build configuration (Debug/Release)")] string? configuration = null,
        [Description("Target framework")] string? framework = null,
        [Description("Launch browser")] bool launchBrowser = true,
        [Description("Working directory")] string? workingDirectory = null)
    {
        var args = "run";
        
        if (!string.IsNullOrEmpty(project))
            args += $" --project {project}";
        if (!string.IsNullOrEmpty(configuration))
            args += $" --configuration {configuration}";
        if (!string.IsNullOrEmpty(framework))
            args += $" --framework {framework}";
        if (!launchBrowser)
            args += " --no-launch-browser";

        var result = await AspireExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpServerTool, Description("Stops running .NET Aspire applications")]
    public static async Task<string> StopAsync(
        [Description("Application ID to stop")] string? appId = null,
        [Description("Working directory")] string? workingDirectory = null)
    {
        var args = "stop";
        
        if (!string.IsNullOrEmpty(appId))
            args += $" {appId}";

        var result = await AspireExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpServerTool, Description("Lists running .NET Aspire applications")]
    public static async Task<string> ListAsync(
        [Description("Working directory")] string? workingDirectory = null)
    {
        var result = await AspireExecutor.ExecuteAsync("list", workingDirectory);
        return result.ToString();
    }

    [McpServerTool, Description("Publishes a .NET Aspire application")]
    public static async Task<string> PublishAsync(
        [Description("Path to the AppHost project")] string? project = null,
        [Description("Build configuration (Debug/Release)")] string? configuration = null,
        [Description("Target framework")] string? framework = null,
        [Description("Output directory")] string? output = null,
        [Description("Container registry")] string? registry = null,
        [Description("Container image tag")] string? tag = null,
        [Description("Working directory")] string? workingDirectory = null)
    {
        var args = "publish";
        
        if (!string.IsNullOrEmpty(project))
            args += $" --project {project}";
        if (!string.IsNullOrEmpty(configuration))
            args += $" --configuration {configuration}";
        if (!string.IsNullOrEmpty(framework))
            args += $" --framework {framework}";
        if (!string.IsNullOrEmpty(output))
            args += $" --output {output}";
        if (!string.IsNullOrEmpty(registry))
            args += $" --registry {registry}";
        if (!string.IsNullOrEmpty(tag))
            args += $" --tag {tag}";

        var result = await AspireExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpServerTool, Description("Deploys a .NET Aspire application")]
    public static async Task<string> DeployAsync(
        [Description("Path to the manifest file")] string? manifest = null,
        [Description("Target environment")] string? environment = null,
        [Description("Configuration values")] string? config = null,
        [Description("Working directory")] string? workingDirectory = null)
    {
        var args = "deploy";
        
        if (!string.IsNullOrEmpty(manifest))
            args += $" --manifest {manifest}";
        if (!string.IsNullOrEmpty(environment))
            args += $" --environment {environment}";
        if (!string.IsNullOrEmpty(config))
            args += $" --config {config}";

        var result = await AspireExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpServerTool, Description("Generates deployment manifests for .NET Aspire applications")]
    public static async Task<string> GenerateAsync(
        [Description("Path to the AppHost project")] string? project = null,
        [Description("Output directory for manifests")] string? output = null,
        [Description("Target format (k8s, docker-compose, etc.)")] string? format = null,
        [Description("Working directory")] string? workingDirectory = null)
    {
        var args = "generate";
        
        if (!string.IsNullOrEmpty(project))
            args += $" --project {project}";
        if (!string.IsNullOrEmpty(output))
            args += $" --output {output}";
        if (!string.IsNullOrEmpty(format))
            args += $" --format {format}";

        var result = await AspireExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpServerTool, Description("Opens or manages the .NET Aspire Dashboard")]
    public static async Task<string> DashboardAsync(
        [Description("Dashboard URL or connection string")] string? url = null,
        [Description("Port for the dashboard")] int? port = null,
        [Description("Launch browser")] bool launchBrowser = true,
        [Description("Working directory")] string? workingDirectory = null)
    {
        var args = "dashboard";
        
        if (!string.IsNullOrEmpty(url))
            args += $" --url {url}";
        if (port.HasValue)
            args += $" --port {port}";
        if (!launchBrowser)
            args += " --no-launch-browser";

        var result = await AspireExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpServerTool, Description("Lists available .NET Aspire project templates")]
    public static async Task<string> ListTemplatesAsync(
        [Description("Working directory")] string? workingDirectory = null)
    {
        var result = await AspireExecutor.ExecuteAsync("template list", workingDirectory);
        return result.ToString();
    }

    [McpServerTool, Description("Installs .NET Aspire project templates")]
    public static async Task<string> InstallTemplateAsync(
        [Description("Template package or path")] string templateSource,
        [Description("Working directory")] string? workingDirectory = null)
    {
        var args = $"template install {templateSource}";
        var result = await AspireExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpServerTool, Description("Uninstalls .NET Aspire project templates")]
    public static async Task<string> UninstallTemplateAsync(
        [Description("Template package name")] string templateName,
        [Description("Working directory")] string? workingDirectory = null)
    {
        var args = $"template uninstall {templateName}";
        var result = await AspireExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpServerTool, Description("Views logs from .NET Aspire applications")]
    public static async Task<string> LogsAsync(
        [Description("Application or service name")] string? service = null,
        [Description("Follow log output")] bool follow = false,
        [Description("Number of log lines to show")] int? lines = null,
        [Description("Working directory")] string? workingDirectory = null)
    {
        var args = "logs";
        
        if (!string.IsNullOrEmpty(service))
            args += $" {service}";
        if (follow)
            args += " --follow";
        if (lines.HasValue)
            args += $" --tail {lines}";

        var result = await AspireExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpServerTool, Description("Shows status of .NET Aspire applications and services")]
    public static async Task<string> StatusAsync(
        [Description("Application or service name")] string? target = null,
        [Description("Show detailed status")] bool detailed = false,
        [Description("Working directory")] string? workingDirectory = null)
    {
        var args = "status";
        
        if (!string.IsNullOrEmpty(target))
            args += $" {target}";
        if (detailed)
            args += " --detailed";

        var result = await AspireExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpServerTool, Description("Updates .NET Aspire CLI and templates")]
    public static async Task<string> UpdateAsync(
        [Description("Check for updates only")] bool checkOnly = false,
        [Description("Working directory")] string? workingDirectory = null)
    {
        var args = "update";
        
        if (checkOnly)
            args += " --check";

        var result = await AspireExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpServerTool, Description("Executes any .NET Aspire CLI command with custom arguments")]
    public static async Task<string> DynamicAspireAsync(
        [Description("The complete Aspire command arguments (everything after 'aspire')")] string command,
        [Description("Working directory")] string? workingDirectory = null)
    {
        if (string.IsNullOrWhiteSpace(command))
            throw new ArgumentException("Command cannot be null or empty", nameof(command));

        var result = await AspireExecutor.ExecuteAsync(command.Trim(), workingDirectory);
        return result.ToString();
    }
}
