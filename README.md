# .NET Aspire MCP Server

A comprehensive Model Context Protocol (MCP) server that exposes .NET Aspire CLI commands as MCP tools. Built with the Microsoft ModelContextProtocol library and optimized for AOT compilation with minimal footprint.

## Features

### Core .NET Aspire Commands
- **Project Lifecycle**: Initialize, build, run, publish, and deploy .NET Aspire applications
- **Development Environment**: Start and manage the Aspire development experience with dashboard
- **Application Management**: Start, stop, list, and monitor running Aspire applications
- **Template Management**: List, install, and uninstall Aspire project templates
- **Deployment**: Generate manifests and deploy to various targets (Kubernetes, Docker Compose, etc.)

### Dynamic CLI Tool
- **DynamicAspire**: Execute any .NET Aspire CLI command with custom arguments
- **Future-Proof**: Supports any current or future Aspire CLI commands automatically
- **Flexible**: Working directory support and comprehensive error handling

### Advanced Features
- **AOT Compilation**: Optimized for ahead-of-time compilation with minimal binary size
- **Cross-Platform**: Supports Windows, Linux, and macOS
- **Single File**: Can be compiled to a single, self-contained executable
- **High Performance**: Minimal memory footprint and fast startup times

## Prerequisites

- .NET 9.0 SDK or later
- .NET Aspire workload: `dotnet workload install aspire`
- .NET Aspire CLI (included with the workload)

## Installation & Usage

### Basic Setup

1. **Clone and Build**:
   ```bash
   git clone <repository-url>
   cd AspireMcpServer
   dotnet build
   ```

2. **Run the Server**:
   ```bash
   cd AspireMcpServer
   dotnet run
   ```

### AOT Compilation

For production deployment with optimal performance:

```bash
# Windows x64
dotnet publish -c Release -r win-x64

# Linux x64
dotnet publish -c Release -r linux-x64

# macOS ARM64
dotnet publish -c Release -r osx-arm64
```

The resulting binary will be a single, self-contained executable optimized for size and performance.

## Available MCP Tools

### Core Aspire Commands

| Tool | Description | Key Parameters |
|------|-------------|----------------|
| `GetVersionAsync` | Gets installed .NET Aspire CLI version | None |
| `GetHelpAsync` | Displays Aspire CLI help information | `command` (optional) |
| `InitProjectAsync` | Initializes a new .NET Aspire project | `name`, `output`, `template` |
| `DevAsync` | Starts the Aspire development environment | `project`, `port`, `launchBrowser` |
| `BuildAsync` | Builds a .NET Aspire application | `project`, `configuration`, `framework` |
| `RunAsync` | Runs a .NET Aspire application | `project`, `configuration`, `launchBrowser` |
| `StopAsync` | Stops running .NET Aspire applications | `appId` |
| `ListAsync` | Lists running .NET Aspire applications | None |
| `PublishAsync` | Publishes a .NET Aspire application | `project`, `registry`, `tag`, `output` |
| `DeployAsync` | Deploys a .NET Aspire application | `manifest`, `environment`, `config` |
| `GenerateAsync` | Generates deployment manifests | `project`, `output`, `format` |
| `DashboardAsync` | Opens or manages the Aspire Dashboard | `url`, `port`, `launchBrowser` |
| `LogsAsync` | Views logs from Aspire applications | `service`, `follow`, `lines` |
| `StatusAsync` | Shows status of applications and services | `target`, `detailed` |
| `UpdateAsync` | Updates Aspire CLI and templates | `checkOnly` |

### Template Management

| Tool | Description | Key Parameters |
|------|-------------|----------------|
| `ListTemplatesAsync` | Lists available Aspire project templates | None |
| `InstallTemplateAsync` | Installs Aspire project templates | `templateSource` |
| `UninstallTemplateAsync` | Uninstalls Aspire project templates | `templateName` |

### Dynamic CLI Tool

| Tool | Description | Key Parameters |
|------|-------------|----------------|
| `DynamicAspireAsync` | Executes any Aspire CLI command | `command` (required), `workingDirectory` |

## Usage Examples

### Initializing a New Aspire Project

```json
{
  "tool": "InitProjectAsync",
  "arguments": {
    "name": "MyECommerceApp",
    "output": "./MyECommerceApp",
    "template": "aspire-starter",
    "workingDirectory": "C:\\Projects"
  }
}
```

### Starting Development Environment

```json
{
  "tool": "DevAsync",
  "arguments": {
    "project": "MyECommerceApp.AppHost",
    "launchBrowser": true,
    "port": 15000
  }
}
```

### Publishing for Production

```json
{
  "tool": "PublishAsync",
  "arguments": {
    "project": "MyECommerceApp.AppHost",
    "configuration": "Release",
    "registry": "myregistry.azurecr.io",
    "tag": "v1.0.0",
    "output": "./publish"
  }
}
```

### Using Dynamic CLI Tool

```json
{
  "tool": "DynamicAspireAsync",
  "arguments": {
    "command": "deploy --manifest ./aspire-manifest.json --environment production",
    "workingDirectory": "./MyECommerceApp"
  }
}
```

### Generating Kubernetes Manifests

```json
{
  "tool": "GenerateAsync",
  "arguments": {
    "project": "MyECommerceApp.AppHost",
    "output": "./k8s-manifests",
    "format": "k8s"
  }
}
```

## Architecture

The project follows modern .NET practices with AOT optimization:

- **Core/AspireExecutor**: Process execution engine for Aspire CLI commands
- **Services/AspireCommands**: Comprehensive MCP tool implementations
- **Program.cs**: Minimal hosting setup with MCP server registration
- **ILLink.Descriptors.xml**: AOT compatibility configuration
- **AOT Optimizations**: Size and performance optimizations for production

## Key Features

### Development Experience
- Easy project initialization and template management
- Integrated development environment with hot reload
- Real-time dashboard for monitoring and debugging
- Comprehensive logging and status monitoring

### Deployment & Operations
- Multi-target manifest generation (Kubernetes, Docker Compose, etc.)
- Container registry integration with versioning
- Environment-specific deployments
- Application lifecycle management

### Performance & Deployment
- AOT compilation for optimal performance
- Single-file deployment ready
- Minimal memory footprint (typically < 50MB)
- Fast startup times (< 100ms)
- Cross-platform compatibility

### Dynamic CLI Integration
- Execute any Aspire CLI command through `DynamicAspireAsync`
- Future-proof design supporting new Aspire features
- Comprehensive error handling and reporting
- Working directory support for complex scenarios

## AOT Compilation Benefits

- **Size**: Reduced application size (typically 20-50MB vs 200MB+ for regular .NET apps)
- **Performance**: Faster startup and improved runtime performance
- **Deployment**: Single executable with all dependencies included
- **Security**: Reduced attack surface with trimmed dependencies

## Configuration for MCP Clients

### Claude Desktop Configuration

Add to your `claude_desktop_config.json`:

```json
{
  "mcpServers": {
    "aspire": {
      "command": "C:\\path\\to\\AspireMcpServer.exe",
      "env": {
        "ASPIRE_ALLOW_UNSECURED_TRANSPORT": "true"
      }
    }
  }
}
```

### Development Configuration

For development with `dotnet run`:

```json
{
  "mcpServers": {
    "aspire": {
      "command": "dotnet",
      "args": [
        "run",
        "--project",
        "C:\\Development\\MCP\\AspireMcpServer\\AspireMcpServer"
      ],
      "cwd": "C:\\Development\\MCP\\AspireMcpServer",
      "env": {
        "ASPIRE_ALLOW_UNSECURED_TRANSPORT": "true"
      }
    }
  }
}
```

## Error Handling

- **CLI Validation**: Checks Aspire CLI availability on startup
- **Command Validation**: Validates command arguments before execution
- **Process Management**: Robust process execution with timeout handling
- **Error Reporting**: Detailed error messages with context and suggestions
- **Graceful Degradation**: Continues operation even if some commands fail

## Development Workflow Integration

### Local Development
1. Use `InitProjectAsync` to create new Aspire projects
2. Use `DevAsync` to start the development environment
3. Use `DashboardAsync` to monitor applications
4. Use `LogsAsync` to debug issues

### Build & Test
1. Use `BuildAsync` to compile applications
2. Use `RunAsync` to test locally
3. Use `StatusAsync` to verify application health

### Deployment
1. Use `GenerateAsync` to create deployment manifests
2. Use `PublishAsync` to build and push container images
3. Use `DeployAsync` to deploy to target environments
4. Use `StatusAsync` and `LogsAsync` to monitor deployments

## Troubleshooting

### Common Issues

**Aspire CLI not found:**
```bash
# Install the Aspire workload
dotnet workload install aspire

# Verify installation
aspire --version
```

**AOT Compilation Issues:**
- Ensure all dependencies support AOT
- Check ILLink.Descriptors.xml for missing type preservation
- Use `--verbosity detailed` for detailed trimming warnings

**Performance Issues:**
- Use Release configuration for production builds
- Enable AOT compilation for optimal performance
- Monitor memory usage with built-in diagnostics

## Contributing

1. Fork the repository
2. Create a feature branch
3. Follow the existing code patterns
4. Test with both regular and AOT builds
5. Submit a pull request

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Related Projects

- [DotNetMcpServer2](../DotNetMcpServer2/) - MCP server for .NET CLI commands
- [.NET Aspire Documentation](https://learn.microsoft.com/en-us/dotnet/aspire/)
- [ModelContextProtocol](https://github.com/microsoft/mcp-dotnet) - .NET implementation of Model Context Protocol
