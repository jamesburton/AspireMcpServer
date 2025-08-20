# .NET Aspire MCP Server

A comprehensive Model Context Protocol (MCP) server that exposes .NET Aspire CLI commands as MCP tools. This allows AI assistants and other MCP clients to interact with .NET Aspire applications and services through a standardized interface.

## Features

### Core .NET Aspire Commands
- **Project Management**: Initialize, build, run, and publish .NET Aspire applications
- **Development Environment**: Start and manage the Aspire development experience
- **Application Lifecycle**: Deploy, start, stop, and monitor Aspire applications
- **Dashboard Operations**: Open and manage the .NET Aspire Dashboard
- **Template Management**: List, install, and uninstall Aspire project templates

### Dynamic Aspire CLI Tool
- **DynamicAspire**: Execute any .NET Aspire CLI command with custom arguments
- **Flexible Execution**: Support for any current or future Aspire CLI commands
- **Working Directory Support**: Execute commands in specific directories

### Monitoring & Management
- **Application Status**: Check status of running applications and services
- **Log Viewing**: Access and follow logs from Aspire applications
- **Deployment Management**: Generate and deploy manifests for various targets

## Prerequisites

- .NET 9.0 SDK or later
- .NET Aspire workload: `dotnet workload install aspire`
- .NET Aspire CLI (typically installed with the workload)

## Installation & Usage

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

## Available MCP Tools

### Core Aspire Commands

| Tool | Description | Required Parameters |
|------|-------------|-------------------|
| `aspire_version` | Gets installed .NET Aspire CLI version | None |
| `aspire_help` | Displays .NET Aspire CLI help information | None |
| `aspire_init` | Initializes a new .NET Aspire project | None |
| `aspire_dev` | Starts the .NET Aspire development environment | None |
| `aspire_build` | Builds a .NET Aspire application | None |
| `aspire_run` | Runs a .NET Aspire application | None |
| `aspire_stop` | Stops running .NET Aspire applications | None |
| `aspire_list` | Lists running .NET Aspire applications | None |
| `aspire_publish` | Publishes a .NET Aspire application | None |
| `aspire_deploy` | Deploys a .NET Aspire application | None |
| `aspire_generate` | Generates deployment manifests | None |
| `aspire_dashboard` | Opens or manages the .NET Aspire Dashboard | None |
| `aspire_logs` | Views logs from .NET Aspire applications | None |
| `aspire_status` | Shows status of applications and services | None |
| `aspire_update` | Updates .NET Aspire CLI and templates | None |

### Template Management Commands

| Tool | Description | Required Parameters |
|------|-------------|-------------------|
| `aspire_template_list` | Lists available .NET Aspire project templates | None |
| `aspire_template_install` | Installs .NET Aspire project templates | `templateSource` |
| `aspire_template_uninstall` | Uninstalls .NET Aspire project templates | `templateName` |

### Dynamic CLI Tool

| Tool | Description | Required Parameters |
|------|-------------|-------------------|
| `DynamicAspire` | Executes any .NET Aspire CLI command | `command` |

## Examples

### Initializing a New Aspire Project
```json
{
  "tool": "aspire_init",
  "parameters": {
    "name": "MyAspireApp",
    "output": "./MyAspireApp",
    "template": "aspire-starter",
    "workingDirectory": "/path/to/projects"
  }
}
```

### Starting the Development Environment
```json
{
  "tool": "aspire_dev",
  "parameters": {
    "project": "MyAspireApp.AppHost",
    "launchBrowser": true,
    "port": 15000
  }
}
```

### Publishing an Aspire Application
```json
{
  "tool": "aspire_publish",
  "parameters": {
    "project": "MyAspireApp.AppHost",
    "configuration": "Release",
    "output": "./publish",
    "registry": "myregistry.azurecr.io",
    "tag": "v1.0.0"
  }
}
```

### Using Dynamic CLI Tool
```json
{
  "tool": "DynamicAspire",
  "parameters": {
    "command": "deploy --manifest ./aspire-manifest.json --environment production",
    "workingDirectory": "./MyAspireApp"
  }
}
```

### Viewing Application Logs
```json
{
  "tool": "aspire_logs",
  "parameters": {
    "service": "webapi",
    "follow": true,
    "lines": 100
  }
}
```

### Generating Kubernetes Manifests
```json
{
  "tool": "aspire_generate",
  "parameters": {
    "project": "MyAspireApp.AppHost",
    "output": "./k8s-manifests",
    "format": "k8s"
  }
}
```

## Architecture

The project follows the same clean architecture pattern as the DotNetMcpServer:

- **Core/AspireExecutor**: Handles process execution and command result management for Aspire CLI
- **Services/**: Contains MCP tool implementations organized by functionality
  - `AspireCommands`: Comprehensive .NET Aspire CLI operations
  - `DynamicAspire`: Dynamic CLI command execution tool
- **Program.cs**: Main entry point with server initialization and registration

## Key Features

### Comprehensive Aspire Support
- Full coverage of .NET Aspire CLI commands
- Support for all major Aspire operations (init, dev, build, run, publish, deploy)
- Template management for custom Aspire project templates
- Application lifecycle management

### Development Experience
- Easy startup of Aspire development environment
- Dashboard integration for monitoring and debugging
- Log viewing and application status monitoring
- Hot reload and development-time features

### Deployment & Operations
- Manifest generation for multiple deployment targets (Kubernetes, Docker Compose, etc.)
- Container registry integration
- Environment-specific deployments
- Application monitoring and management

### Dynamic CLI Integration
- Execute any Aspire CLI command through the `DynamicAspire` tool
- Future-proof design that supports new Aspire features automatically
- Flexible parameter passing and working directory support

## Security Considerations

- All commands are executed in the context of the running user
- Working directory can be specified to limit scope
- No shell injection vulnerabilities - all parameters are properly escaped
- Commands run with the same permissions as the MCP server process
- Container registry credentials should be managed securely

## Error Handling

- Comprehensive error capture from both stdout and stderr
- Execution time tracking for performance monitoring
- Graceful handling of missing Aspire CLI or workload
- Detailed error reporting with context information
- Proper validation of Aspire CLI availability on startup

## Development Workflow Integration

### Local Development
1. Use `aspire_init` to create new Aspire projects
2. Use `aspire_dev` to start the development environment
3. Use `aspire_dashboard` to monitor applications
4. Use `aspire_logs` to debug issues

### Build & Test
1. Use `aspire_build` to compile applications
2. Use `aspire_run` to test locally
3. Use `aspire_status` to verify application health

### Deployment
1. Use `aspire_generate` to create deployment manifests
2. Use `aspire_publish` to build and push container images
3. Use `aspire_deploy` to deploy to target environments
4. Use `aspire_status` and `aspire_logs` to monitor deployments

## Troubleshooting

### Common Issues

**Aspire CLI not found:**
```bash
# Install the Aspire workload
dotnet workload install aspire

# Verify installation
aspire --version
```

**Project initialization fails:**
- Ensure you have the latest .NET SDK
- Check that Aspire templates are installed
- Verify working directory permissions

**Development environment won't start:**
- Check port availability (default is usually 15000+)
- Ensure Docker is running (if using containerized services)
- Verify project file integrity

### Debug Mode
Run the server with `--debug` flag for verbose output:
```bash
dotnet run -- --debug
```

## Contributing

1. Fork the repository
2. Create a feature branch
3. Add tests for new functionality
4. Ensure all tests pass
5. Submit a pull request

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Related Projects

- [DotNetMcpServer](../DotNetMcpServer/) - MCP server for .NET CLI commands
- [.NET Aspire Documentation](https://learn.microsoft.com/en-us/dotnet/aspire/)
- [MCPSharp](https://github.com/microsoft/MCPSharp) - .NET implementation of Model Context Protocol
