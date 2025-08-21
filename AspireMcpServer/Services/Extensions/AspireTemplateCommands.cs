using System.ComponentModel;
using AspireMcpServer.Core;
using ModelContextProtocol.Server;

namespace AspireMcpServer.Services.Extensions;

/// <summary>
/// Provides enhanced .NET Aspire template and extension management commands
/// </summary>
[McpServerToolType]
public static class AspireTemplateCommands
{
    [McpServerTool, Description("Creates a custom Aspire project template from an existing project")]
    public static async Task<string> CreateCustomTemplateAsync(
        [Description("Path to source project to create template from")] string sourcePath,
        [Description("Template name")] string templateName,
        [Description("Template description")] string? description = null,
        [Description("Template author")] string? author = null,
        [Description("Template tags (comma-separated)")] string? tags = null,
        [Description("Output directory for template package")] string? output = null,
        [Description("Working directory")] string? workingDirectory = null)
    {
        var args = $"template create --source \"{sourcePath}\" --name \"{templateName}\"";
        
        if (!string.IsNullOrEmpty(description))
            args += $" --description \"{description}\"";
        if (!string.IsNullOrEmpty(author))
            args += $" --author \"{author}\"";
        if (!string.IsNullOrEmpty(tags))
            args += $" --tags \"{tags}\"";
        if (!string.IsNullOrEmpty(output))
            args += $" --output \"{output}\"";

        var result = await AspireExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpServerTool, Description("Packages an Aspire template for distribution")]
    public static async Task<string> PackageTemplateAsync(
        [Description("Path to template directory")] string templatePath,
        [Description("Output directory for package")] string? output = null,
        [Description("Package version")] string? version = null,
        [Description("Include symbols")] bool includeSymbols = false,
        [Description("Working directory")] string? workingDirectory = null)
    {
        var args = $"template pack \"{templatePath}\"";
        
        if (!string.IsNullOrEmpty(output))
            args += $" --output \"{output}\"";
        if (!string.IsNullOrEmpty(version))
            args += $" --version {version}";
        if (includeSymbols)
            args += " --include-symbols";

        var result = await AspireExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpServerTool, Description("Validates an Aspire template structure and configuration")]
    public static async Task<string> ValidateTemplateAsync(
        [Description("Path to template directory")] string templatePath,
        [Description("Validate dependencies")] bool validateDependencies = true,
        [Description("Check template metadata")] bool checkMetadata = true,
        [Description("Working directory")] string? workingDirectory = null)
    {
        var args = $"template validate \"{templatePath}\"";
        
        if (validateDependencies)
            args += " --validate-dependencies";
        if (checkMetadata)
            args += " --check-metadata";

        var result = await AspireExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpServerTool, Description("Lists all available Aspire template sources")]
    public static async Task<string> ListTemplateSourcesAsync(
        [Description("Include remote sources")] bool includeRemote = true,
        [Description("Show detailed information")] bool detailed = false,
        [Description("Working directory")] string? workingDirectory = null)
    {
        var args = "template source list";
        
        if (includeRemote)
            args += " --include-remote";
        if (detailed)
            args += " --detailed";

        var result = await AspireExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpServerTool, Description("Adds a new Aspire template source")]
    public static async Task<string> AddTemplateSourceAsync(
        [Description("Source name")] string name,
        [Description("Source URL or path")] string source,
        [Description("Source priority")] int? priority = null,
        [Description("Authentication token")] string? token = null,
        [Description("Working directory")] string? workingDirectory = null)
    {
        var args = $"template source add \"{name}\" \"{source}\"";
        
        if (priority.HasValue)
            args += $" --priority {priority}";
        if (!string.IsNullOrEmpty(token))
            args += $" --token \"{token}\"";

        var result = await AspireExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpServerTool, Description("Removes an Aspire template source")]
    public static async Task<string> RemoveTemplateSourceAsync(
        [Description("Source name")] string name,
        [Description("Working directory")] string? workingDirectory = null)
    {
        var args = $"template source remove \"{name}\"";
        var result = await AspireExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpServerTool, Description("Updates all Aspire template sources")]
    public static async Task<string> UpdateTemplateSourcesAsync(
        [Description("Force update even if cache is fresh")] bool force = false,
        [Description("Update timeout in seconds")] int? timeout = null,
        [Description("Working directory")] string? workingDirectory = null)
    {
        var args = "template source update";
        
        if (force)
            args += " --force";
        if (timeout.HasValue)
            args += $" --timeout {timeout}";

        var result = await AspireExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpServerTool, Description("Searches for Aspire templates in all configured sources")]
    public static async Task<string> SearchTemplatesAsync(
        [Description("Search query")] string query,
        [Description("Maximum number of results")] int? maxResults = null,
        [Description("Include prerelease templates")] bool includePrerelease = false,
        [Description("Filter by tags (comma-separated)")] string? tags = null,
        [Description("Working directory")] string? workingDirectory = null)
    {
        var args = $"template search \"{query}\"";
        
        if (maxResults.HasValue)
            args += $" --max-results {maxResults}";
        if (includePrerelease)
            args += " --include-prerelease";
        if (!string.IsNullOrEmpty(tags))
            args += $" --tags \"{tags}\"";

        var result = await AspireExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }

    [McpServerTool, Description("Gets detailed information about a specific Aspire template")]
    public static async Task<string> GetTemplateInfoAsync(
        [Description("Template name or ID")] string templateName,
        [Description("Template version")] string? version = null,
        [Description("Include usage examples")] bool includeExamples = false,
        [Description("Working directory")] string? workingDirectory = null)
    {
        var args = $"template info \"{templateName}\"";
        
        if (!string.IsNullOrEmpty(version))
            args += $" --version {version}";
        if (includeExamples)
            args += " --include-examples";

        var result = await AspireExecutor.ExecuteAsync(args, workingDirectory);
        return result.ToString();
    }
}
