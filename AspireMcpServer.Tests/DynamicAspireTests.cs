using AspireMcpServer.Services;
using Xunit;

namespace AspireMcpServer.Tests;

public class DynamicAspireTests
{
    [Fact]
    public async Task ExecuteCommandAsync_WithValidCommand_ReturnsResult()
    {
        // Arrange
        var command = "--help";

        // Act
        var result = await DynamicAspire.ExecuteCommandAsync(command);

        // Assert
        Assert.NotNull(result);
        Assert.Contains("aspire", result);
    }

    [Fact]
    public async Task ExecuteCommandAsync_WithWorkingDirectory_ReturnsResult()
    {
        // Arrange
        var command = "--help";
        var workingDirectory = "C:\\temp";

        // Act
        var result = await DynamicAspire.ExecuteCommandAsync(command, workingDirectory);

        // Assert
        Assert.NotNull(result);
        Assert.Contains("aspire", result);
        Assert.Contains(workingDirectory, result);
    }

    [Fact]
    public async Task ExecuteCommandAsync_WithInvalidCommand_ReturnsErrorResult()
    {
        // Arrange
        var command = "invalid-command-xyz";

        // Act
        var result = await DynamicAspire.ExecuteCommandAsync(command);

        // Assert
        Assert.NotNull(result);
        // The result should contain error information
        Assert.True(result.Contains("Exit Code:") && !result.Contains("Exit Code: 0"));
    }

    [Fact]
    public async Task ExecuteCommandAsync_WithEmptyCommand_ReturnsResult()
    {
        // Arrange
        var command = "";

        // Act
        var result = await DynamicAspire.ExecuteCommandAsync(command);

        // Assert
        Assert.NotNull(result);
        Assert.Contains("aspire", result);
    }
}
