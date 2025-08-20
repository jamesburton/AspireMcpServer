using AspireMcpServer.Core;
using MCPSharp;

namespace AspireMcpServer.Services;

/// <summary>
/// Provides .NET Aspire CLI commands as MCP tools
/// </summary>
public class AspireCommands
{
    [McpTool("aspire_version", "Gets the installed .NET Aspire CLI version")]
    public static async Task<string> GetVersionAsync()
    {
        var result = await AspireExecutor.ExecuteAsync("--version");
        return result.ToString();
    }

    [McpTool("aspire_help", "Displays .NET Aspire CLI help information")]
    public static async Task<string> GetHelpAsync(
        [McpParameter(false, "Specific command to get help for")] string? command = null)
    {
        var args = "help";
        if (!string.IsNullOrEmpty(command))
            args += $" {command}";

        var result = await AspireExecutor.ExecuteAsync(args);
        return result.ToString();
    }

    [McpTool("aspire_init", "Initializes a new .NET Aspire project")]
    public static async Task<string> InitProjectAsync(
        [McpParameter(false, "Project name")] string? name = null,
        [McpParameter(false, "Output directory")] string? output = null,
        [McpParameter(false, "Template to use")] string? template = null,
        [McpParameter(false, "Working directory")] string? workingDirectory = null)
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

    [McpTool("aspire_dev", "Starts the .NET Aspire development environment")]
    public static async Task<string> DevAsync(
        [McpParameter(false, "Path to the AppHost project")] string? project = null,
        [McpParameter(false, "Launch browser")] bool launchBrowser = true,
        [McpParameter(false, "Port for the dashboard")] int? port = null,
        [McpParameter(false, "Working directory")] string? workingDirectory = null)
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

    [McpTool("aspire_build", "Builds an .NET Aspire application")]
    public static async Task<string> BuildAsync(
        [McpParameter(false, "Path to the AppHost project")] string? project = null,
        [McpParameter(false, "Build configuration (Debug/Release)")] string? configuration = null,
        [McpParameter(false, "Target framework")] string? framework = null,
        [McpParameter(false, "Working directory")] string? workingDirectory = null)
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

    [McpTool("aspire_run", "Runs an .NET Aspire application")]
    public static async Task<string> RunAsync(
        [McpParameter(false, "Path to the AppHost project")] string? project = null,
        [McpParameter(false, "Build configuration (Debug/Release)")] string? configuration = null,
        [McpParameter(false, "Target framework")] string? framework = null,
        [McpParameter(false, "Launch browser")] bool launchBrowser = true,
        [McpParameter(false, "Working directory")] string? workingDirectory = null)
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

    [McpTool("aspire_stop", "Stops running .NET Aspire applications")]
    public static async Task<string> StopAsync(
        [McpParameter(false, "Application ID to stop")] string? appId = null,
        [McpParameter(false, "Working directory")] string? workingDirectory = null)
    {
        var args = "stop";
        
        if (!string.IsNullOrEmpty(appId))
            args += $" {appId}";

        var result = await AspireExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpTool("aspire_list", "Lists running .NET Aspire applications")]
    public static async Task<string> ListAsync(
        [McpParameter(false, "Working directory")] string? workingDirectory = null)
    {
        var result = await AspireExecutor.ExecuteAsync("list", workingDirectory);
        return result.ToString();
    }

    [McpTool("aspire_publish", "Publishes a .NET Aspire application")]
    public static async Task<string> PublishAsync(
        [McpParameter(false, "Path to the AppHost project")] string? project = null,
        [McpParameter(false, "Build configuration (Debug/Release)")] string? configuration = null,
        [McpParameter(false, "Target framework")] string? framework = null,
        [McpParameter(false, "Output directory")] string? output = null,
        [McpParameter(false, "Container registry")] string? registry = null,
        [McpParameter(false, "Container image tag")] string? tag = null,
        [McpParameter(false, "Working directory")] string? workingDirectory = null)
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

    [McpTool("aspire_deploy", "Deploys a .NET Aspire application")]
    public static async Task<string> DeployAsync(
        [McpParameter(false, "Path to the manifest file")] string? manifest = null,
        [McpParameter(false, "Target environment")] string? environment = null,
        [McpParameter(false, "Configuration values")] string? config = null,
        [McpParameter(false, "Working directory")] string? workingDirectory = null)
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

    [McpTool("aspire_generate", "Generates deployment manifests for .NET Aspire applications")]
    public static async Task<string> GenerateAsync(
        [McpParameter(false, "Path to the AppHost project")] string? project = null,
        [McpParameter(false, "Output directory for manifests")] string? output = null,
        [McpParameter(false, "Target format (k8s, docker-compose, etc.)")] string? format = null,
        [McpParameter(false, "Working directory")] string? workingDirectory = null)
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

    [McpTool("aspire_dashboard", "Opens or manages the .NET Aspire Dashboard")]
    public static async Task<string> DashboardAsync(
        [McpParameter(false, "Dashboard URL or connection string")] string? url = null,
        [McpParameter(false, "Port for the dashboard")] int? port = null,
        [McpParameter(false, "Launch browser")] bool launchBrowser = true,
        [McpParameter(false, "Working directory")] string? workingDirectory = null)
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

    [McpTool("aspire_template_list", "Lists available .NET Aspire project templates")]
    public static async Task<string> ListTemplatesAsync(
        [McpParameter(false, "Working directory")] string? workingDirectory = null)
    {
        var result = await AspireExecutor.ExecuteAsync("template list", workingDirectory);
        return result.ToString();
    }

    [McpTool("aspire_template_install", "Installs .NET Aspire project templates")]
    public static async Task<string> InstallTemplateAsync(
        [McpParameter(true, "Template package or path")] string templateSource,
        [McpParameter(false, "Working directory")] string? workingDirectory = null)
    {
        var args = $"template install {templateSource}";
        var result = await AspireExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpTool("aspire_template_uninstall", "Uninstalls .NET Aspire project templates")]
    public static async Task<string> UninstallTemplateAsync(
        [McpParameter(true, "Template package name")] string templateName,
        [McpParameter(false, "Working directory")] string? workingDirectory = null)
    {
        var args = $"template uninstall {templateName}";
        var result = await AspireExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpTool("aspire_logs", "Views logs from .NET Aspire applications")]
    public static async Task<string> LogsAsync(
        [McpParameter(false, "Application or service name")] string? service = null,
        [McpParameter(false, "Follow log output")] bool follow = false,
        [McpParameter(false, "Number of log lines to show")] int? lines = null,
        [McpParameter(false, "Working directory")] string? workingDirectory = null)
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

    [McpTool("aspire_status", "Shows status of .NET Aspire applications and services")]
    public static async Task<string> StatusAsync(
        [McpParameter(false, "Application or service name")] string? target = null,
        [McpParameter(false, "Show detailed status")] bool detailed = false,
        [McpParameter(false, "Working directory")] string? workingDirectory = null)
    {
        var args = "status";
        
        if (!string.IsNullOrEmpty(target))
            args += $" {target}";
        if (detailed)
            args += " --detailed";

        var result = await AspireExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpTool("aspire_update", "Updates .NET Aspire CLI and templates")]
    public static async Task<string> UpdateAsync(
        [McpParameter(false, "Check for updates only")] bool checkOnly = false,
        [McpParameter(false, "Working directory")] string? workingDirectory = null)
    {
        var args = "update";
        
        if (checkOnly)
            args += " --check";

        var result = await AspireExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }
}
