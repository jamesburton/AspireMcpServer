using AspireMcpServer.Core;
using MCPSharp;

namespace AspireMcpServer.Services;

/// <summary>
/// Provides dynamic .NET Aspire CLI commands as MCP tools
/// </summary>
public class DynamicAspire
{
    [McpTool("DynamicAspire", "Executes any .NET Aspire CLI command with custom arguments")]
    public static async Task<string> ExecuteCommandAsync(
        [McpParameter(true, "The complete Aspire command arguments (everything after 'aspire')")] string command,
        [McpParameter(false, "Working directory (Default value: null)")] string? workingDirectory = null)
    {
        try
        {
            var result = await AspireExecutor.ExecuteAsync(command, workingDirectory);
            return result.ToString();
        }
        catch (Exception ex)
        {
            return $"Error executing Aspire command: {ex.Message}";
        }
    }
}
