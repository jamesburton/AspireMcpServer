using AspireMcpServer.Core;
using Xunit;

namespace AspireMcpServer.Tests;

public class AspireExecutorTests
{
    [Fact]
    public async Task ExecuteAsync_WithInvalidCommand_ReturnsNonZeroExitCode()
    {
        // Arrange
        var invalidCommand = "invalid-command-that-does-not-exist";

        // Act
        var result = await AspireExecutor.ExecuteAsync(invalidCommand);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.NotEqual(0, result.ExitCode);
        Assert.NotEmpty(result.StandardError);
        Assert.Contains("aspire", result.Command);
    }

    [Fact]
    public async Task ExecuteAsync_WithWorkingDirectory_SetsCorrectWorkingDirectory()
    {
        // Arrange
        var workingDirectory = "C:\\temp";
        var command = "--help";

        // Act
        var result = await AspireExecutor.ExecuteAsync(command, workingDirectory);

        // Assert
        Assert.Equal(workingDirectory, result.WorkingDirectory);
        Assert.Contains("aspire", result.Command);
    }

    [Fact]
    public async Task ExecuteAsync_WithNullWorkingDirectory_UsesCurrentDirectory()
    {
        // Arrange
        var command = "--help";

        // Act
        var result = await AspireExecutor.ExecuteAsync(command, null);

        // Assert
        Assert.Equal(Environment.CurrentDirectory, result.WorkingDirectory);
    }

    [Fact]
    public async Task IsAvailableAsync_ReturnsBoolean()
    {
        // Act
        var isAvailable = await AspireExecutor.IsAvailableAsync();

        // Assert
        Assert.IsType<bool>(isAvailable);
        // Note: We don't assert true/false because it depends on whether Aspire is installed
    }

    [Fact]
    public void CommandResult_ToString_IncludesAllProperties()
    {
        // Arrange
        var result = new CommandResult
        {
            ExitCode = 1,
            StandardOutput = "Test output",
            StandardError = "Test error",
            ExecutionTime = TimeSpan.FromMilliseconds(100),
            Command = "aspire --help",
            WorkingDirectory = "C:\\test"
        };

        // Act
        var toString = result.ToString();

        // Assert
        Assert.Contains("aspire --help", toString);
        Assert.Contains("C:\\test", toString);
        Assert.Contains("Exit Code: 1", toString);
        Assert.Contains("100.00ms", toString);
        Assert.Contains("Test output", toString);
        Assert.Contains("Test error", toString);
    }

    [Fact]
    public void CommandResult_IsSuccess_ReturnsCorrectValue()
    {
        // Arrange & Act
        var successResult = new CommandResult { ExitCode = 0 };
        var failureResult = new CommandResult { ExitCode = 1 };

        // Assert
        Assert.True(successResult.IsSuccess);
        Assert.False(failureResult.IsSuccess);
    }
}
