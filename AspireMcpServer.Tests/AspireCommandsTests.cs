using AspireMcpServer.Services;
using Xunit;

namespace AspireMcpServer.Tests;

public class AspireCommandsTests
{
    [Fact]
    public async Task GetVersionAsync_ReturnsVersionInfo()
    {
        // Act
        var result = await AspireCommands.GetVersionAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Contains("aspire", result);
        Assert.Contains("--version", result);
    }

    [Fact]
    public async Task GetHelpAsync_WithoutCommand_ReturnsHelpInfo()
    {
        // Act
        var result = await AspireCommands.GetHelpAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Contains("aspire", result);
        Assert.Contains("help", result);
    }

    [Fact]
    public async Task GetHelpAsync_WithSpecificCommand_ReturnsCommandHelp()
    {
        // Arrange
        var command = "init";

        // Act
        var result = await AspireCommands.GetHelpAsync(command);

        // Assert
        Assert.NotNull(result);
        Assert.Contains("aspire", result);
        Assert.Contains("help init", result);
    }

    [Fact]
    public async Task InitProjectAsync_WithParameters_ConstructsCorrectCommand()
    {
        // Arrange
        var name = "TestProject";
        var output = "./test";
        var template = "aspire-starter";
        var workingDirectory = "C:\\temp";

        // Act
        var result = await AspireCommands.InitProjectAsync(name, output, template, workingDirectory);

        // Assert
        Assert.NotNull(result);
        Assert.Contains("aspire init", result);
        Assert.Contains($"--name {name}", result);
        Assert.Contains($"--output {output}", result);
        Assert.Contains($"--template {template}", result);
        Assert.Contains(workingDirectory, result);
    }

    [Fact]
    public async Task DevAsync_WithParameters_ConstructsCorrectCommand()
    {
        // Arrange
        var project = "MyApp.AppHost";
        var port = 15000;
        var workingDirectory = "C:\\projects";

        // Act
        var result = await AspireCommands.DevAsync(project, true, port, workingDirectory);

        // Assert
        Assert.NotNull(result);
        Assert.Contains("aspire dev", result);
        Assert.Contains($"--project {project}", result);
        Assert.Contains($"--port {port}", result);
        Assert.Contains(workingDirectory, result);
    }

    [Fact]
    public async Task DevAsync_WithNoBrowser_IncludesNoBrowserFlag()
    {
        // Act
        var result = await AspireCommands.DevAsync(launchBrowser: false);

        // Assert
        Assert.NotNull(result);
        Assert.Contains("aspire dev", result);
        Assert.Contains("--no-launch-browser", result);
    }

    [Fact]
    public async Task BuildAsync_WithAllParameters_ConstructsCorrectCommand()
    {
        // Arrange
        var project = "MyApp.AppHost";
        var configuration = "Release";
        var framework = "net9.0";
        var workingDirectory = "C:\\projects";

        // Act
        var result = await AspireCommands.BuildAsync(project, configuration, framework, workingDirectory);

        // Assert
        Assert.NotNull(result);
        Assert.Contains("aspire build", result);
        Assert.Contains($"--project {project}", result);
        Assert.Contains($"--configuration {configuration}", result);
        Assert.Contains($"--framework {framework}", result);
        Assert.Contains(workingDirectory, result);
    }

    [Fact]
    public async Task PublishAsync_WithRegistryAndTag_ConstructsCorrectCommand()
    {
        // Arrange
        var registry = "myregistry.azurecr.io";
        var tag = "v1.0.0";

        // Act
        var result = await AspireCommands.PublishAsync(registry: registry, tag: tag);

        // Assert
        Assert.NotNull(result);
        Assert.Contains("aspire publish", result);
        Assert.Contains($"--registry {registry}", result);
        Assert.Contains($"--tag {tag}", result);
    }

    [Fact]
    public async Task LogsAsync_WithFollowAndLines_ConstructsCorrectCommand()
    {
        // Arrange
        var service = "webapi";
        var lines = 100;

        // Act
        var result = await AspireCommands.LogsAsync(service, true, lines);

        // Assert
        Assert.NotNull(result);
        Assert.Contains("aspire logs", result);
        Assert.Contains(service, result);
        Assert.Contains("--follow", result);
        Assert.Contains($"--tail {lines}", result);
    }

    [Fact]
    public async Task InstallTemplateAsync_WithTemplateSource_ConstructsCorrectCommand()
    {
        // Arrange
        var templateSource = "Microsoft.AspNetCore.Templates";

        // Act
        var result = await AspireCommands.InstallTemplateAsync(templateSource);

        // Assert
        Assert.NotNull(result);
        Assert.Contains("aspire template install", result);
        Assert.Contains(templateSource, result);
    }

    [Fact]
    public async Task UpdateAsync_WithCheckOnly_IncludesCheckFlag()
    {
        // Act
        var result = await AspireCommands.UpdateAsync(checkOnly: true);

        // Assert
        Assert.NotNull(result);
        Assert.Contains("aspire update", result);
        Assert.Contains("--check", result);
    }
}
