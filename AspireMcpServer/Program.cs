using AspireMcpServer.Core;
using AspireMcpServer.Services;
using MCPSharp;

var quiet = !args.Contains("--debug");

try
{
    // Verify aspire CLI is available
    if (!quiet) Console.WriteLine("Checking .NET Aspire CLI availability...");
    var isAvailable = await AspireExecutor.IsAvailableAsync();
    if (!isAvailable)
    {
        Console.WriteLine("ERROR: .NET Aspire CLI is not available or not in PATH.");
        Console.WriteLine("Please ensure .NET Aspire is installed and accessible.");
        Console.WriteLine("Install with: dotnet workload install aspire");
        Environment.Exit(1);
    }

    // Get and display Aspire version
    var versionResult = await AspireExecutor.ExecuteAsync("--version");
    if (versionResult.IsSuccess && !quiet)
    {
        Console.WriteLine($".NET Aspire CLI Version: {versionResult.StandardOutput}");
    }

    // Register all command services with MCP Server
    if (!quiet) Console.WriteLine("Registering .NET Aspire commands...");
    MCPServer.Register<AspireCommands>();
    
    if (!quiet) Console.WriteLine("Registering Dynamic Aspire CLI tool...");
    MCPServer.Register<DynamicAspire>();

    // Start the MCP server
    if (!quiet) Console.WriteLine("Starting .NET Aspire MCP Server...");
    await MCPServer.StartAsync("AspireMcpServer", "1.0.0");

    Console.WriteLine();
    Console.WriteLine("=== .NET Aspire MCP Server Started Successfully ===");
    Console.WriteLine("Available command categories:");
    Console.WriteLine("  • Core .NET Aspire CLI commands (init, dev, run, build, publish, deploy)");
    Console.WriteLine("  • Application management (start, stop, list, status, logs)");
    Console.WriteLine("  • Template management (list, install, uninstall)");
    Console.WriteLine("  • Dashboard operations");
    Console.WriteLine("  • Dynamic Aspire CLI execution");
    Console.WriteLine();
    Console.WriteLine("Press Enter to stop the server...");
    Console.ReadLine();
}
catch (Exception ex)
{
    Console.WriteLine($"ERROR: Failed to start .NET Aspire MCP Server: {ex.Message}");
    Environment.Exit(1);
}
