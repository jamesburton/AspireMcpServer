# AspireMcpServer Usage Examples

This document provides practical examples of how to use the AspireMcpServer with various .NET Aspire CLI commands.

## Prerequisites

Before using these examples, ensure you have:

1. .NET 9.0 SDK installed
2. .NET Aspire workload installed: `dotnet workload install aspire`
3. AspireMcpServer running and connected to your MCP client

## Basic Usage Examples

### 1. Check Aspire CLI Version

```json
{
  "tool": "aspire_version",
  "parameters": {}
}
```

### 2. Get Help Information

```json
{
  "tool": "aspire_help",
  "parameters": {
    "command": "init"
  }
}
```

### 3. Initialize a New Aspire Project

```json
{
  "tool": "aspire_init",
  "parameters": {
    "name": "MyECommerceApp",
    "output": "./MyECommerceApp",
    "template": "aspire-starter",
    "workingDirectory": "C:\\Projects"
  }
}
```

## Development Workflow Examples

### 4. Start Development Environment

```json
{
  "tool": "aspire_dev",
  "parameters": {
    "project": "MyECommerceApp.AppHost",
    "launchBrowser": true,
    "port": 15000,
    "workingDirectory": "C:\\Projects\\MyECommerceApp"
  }
}
```

### 5. Build the Application

```json
{
  "tool": "aspire_build",
  "parameters": {
    "project": "MyECommerceApp.AppHost",
    "configuration": "Release",
    "framework": "net9.0",
    "workingDirectory": "C:\\Projects\\MyECommerceApp"
  }
}
```

### 6. Run the Application

```json
{
  "tool": "aspire_run",
  "parameters": {
    "project": "MyECommerceApp.AppHost",
    "configuration": "Debug",
    "launchBrowser": true,
    "workingDirectory": "C:\\Projects\\MyECommerceApp"
  }
}
```

## Monitoring and Debugging Examples

### 7. View Application Status

```json
{
  "tool": "aspire_status",
  "parameters": {
    "target": "webapi",
    "detailed": true,
    "workingDirectory": "C:\\Projects\\MyECommerceApp"
  }
}
```

### 8. View Application Logs

```json
{
  "tool": "aspire_logs",
  "parameters": {
    "service": "webapi",
    "follow": true,
    "lines": 50,
    "workingDirectory": "C:\\Projects\\MyECommerceApp"
  }
}
```

### 9. List Running Applications

```json
{
  "tool": "aspire_list",
  "parameters": {
    "workingDirectory": "C:\\Projects\\MyECommerceApp"
  }
}
```

### 10. Open Dashboard

```json
{
  "tool": "aspire_dashboard",
  "parameters": {
    "port": 18888,
    "launchBrowser": true,
    "workingDirectory": "C:\\Projects\\MyECommerceApp"
  }
}
```

## Deployment Examples

### 11. Generate Kubernetes Manifests

```json
{
  "tool": "aspire_generate",
  "parameters": {
    "project": "MyECommerceApp.AppHost",
    "output": "./k8s-manifests",
    "format": "k8s",
    "workingDirectory": "C:\\Projects\\MyECommerceApp"
  }
}
```

### 12. Generate Docker Compose Files

```json
{
  "tool": "aspire_generate",
  "parameters": {
    "project": "MyECommerceApp.AppHost",
    "output": "./docker-compose",
    "format": "docker-compose",
    "workingDirectory": "C:\\Projects\\MyECommerceApp"
  }
}
```

### 13. Publish Application

```json
{
  "tool": "aspire_publish",
  "parameters": {
    "project": "MyECommerceApp.AppHost",
    "configuration": "Release",
    "output": "./publish",
    "registry": "myregistry.azurecr.io",
    "tag": "v1.0.0",
    "workingDirectory": "C:\\Projects\\MyECommerceApp"
  }
}
```

### 14. Deploy Application

```json
{
  "tool": "aspire_deploy",
  "parameters": {
    "manifest": "./aspire-manifest.json",
    "environment": "production",
    "config": "environment=prod;replicas=3",
    "workingDirectory": "C:\\Projects\\MyECommerceApp"
  }
}
```

## Template Management Examples

### 15. List Available Templates

```json
{
  "tool": "aspire_template_list",
  "parameters": {
    "workingDirectory": "C:\\Projects"
  }
}
```

### 16. Install Custom Template

```json
{
  "tool": "aspire_template_install",
  "parameters": {
    "templateSource": "Microsoft.AspNetCore.Templates::8.0.0",
    "workingDirectory": "C:\\Projects"
  }
}
```

### 17. Uninstall Template

```json
{
  "tool": "aspire_template_uninstall",
  "parameters": {
    "templateName": "Microsoft.AspNetCore.Templates",
    "workingDirectory": "C:\\Projects"
  }
}
```

## Dynamic CLI Examples

### 18. Using Dynamic Aspire Tool

The `DynamicAspire` tool allows you to execute any Aspire CLI command, including future commands that may not be explicitly implemented:

```json
{
  "tool": "DynamicAspire",
  "parameters": {
    "command": "dev --project MyApp.AppHost --port 15001 --no-launch-browser",
    "workingDirectory": "C:\\Projects\\MyApp"
  }
}
```

### 19. Complex Deploy Command

```json
{
  "tool": "DynamicAspire",
  "parameters": {
    "command": "deploy --manifest ./manifests/prod-manifest.json --environment production --config connectionString='Server=prod-db;Database=MyApp' --config replicas=5",
    "workingDirectory": "C:\\Projects\\MyECommerceApp"
  }
}
```

### 20. Advanced Generate Command

```json
{
  "tool": "DynamicAspire",
  "parameters": {
    "command": "generate --project MyApp.AppHost --output ./deployment --format helm --values ./helm-values.yaml",
    "workingDirectory": "C:\\Projects\\MyApp"
  }
}
```

## Application Lifecycle Management

### 21. Stop Running Application

```json
{
  "tool": "aspire_stop",
  "parameters": {
    "appId": "MyECommerceApp",
    "workingDirectory": "C:\\Projects\\MyECommerceApp"
  }
}
```

### 22. Update Aspire Tools

```json
{
  "tool": "aspire_update",
  "parameters": {
    "checkOnly": false,
    "workingDirectory": "C:\\Projects"
  }
}
```

### 23. Check for Updates Only

```json
{
  "tool": "aspire_update",
  "parameters": {
    "checkOnly": true,
    "workingDirectory": "C:\\Projects"
  }
}
```

## Real-World Scenarios

### Scenario 1: Setting up a new microservices project

1. Initialize the project:
```json
{
  "tool": "aspire_init",
  "parameters": {
    "name": "ECommercePlatform",
    "output": "./ECommercePlatform",
    "workingDirectory": "C:\\Projects"
  }
}
```

2. Start development environment:
```json
{
  "tool": "aspire_dev",
  "parameters": {
    "project": "ECommercePlatform.AppHost",
    "workingDirectory": "C:\\Projects\\ECommercePlatform"
  }
}
```

3. Monitor the applications:
```json
{
  "tool": "aspire_dashboard",
  "parameters": {
    "workingDirectory": "C:\\Projects\\ECommercePlatform"
  }
}
```

### Scenario 2: Deploying to production

1. Build for production:
```json
{
  "tool": "aspire_build",
  "parameters": {
    "project": "ECommercePlatform.AppHost",
    "configuration": "Release",
    "workingDirectory": "C:\\Projects\\ECommercePlatform"
  }
}
```

2. Generate deployment manifests:
```json
{
  "tool": "aspire_generate",
  "parameters": {
    "project": "ECommercePlatform.AppHost",
    "output": "./k8s-deployment",
    "format": "k8s",
    "workingDirectory": "C:\\Projects\\ECommercePlatform"
  }
}
```

3. Publish container images:
```json
{
  "tool": "aspire_publish",
  "parameters": {
    "project": "ECommercePlatform.AppHost",
    "configuration": "Release",
    "registry": "ecommerce.azurecr.io",
    "tag": "latest",
    "workingDirectory": "C:\\Projects\\ECommercePlatform"
  }
}
```

4. Deploy to production:
```json
{
  "tool": "aspire_deploy",
  "parameters": {
    "manifest": "./k8s-deployment/aspire-manifest.json",
    "environment": "production",
    "workingDirectory": "C:\\Projects\\ECommercePlatform"
  }
}
```

## Tips and Best Practices

1. **Always specify workingDirectory**: This ensures commands run in the correct context.

2. **Use meaningful names**: When initializing projects, use descriptive names that reflect the application's purpose.

3. **Monitor applications**: Use the dashboard and logs frequently during development to catch issues early.

4. **Environment-specific configs**: Use different configurations for development, staging, and production deployments.

5. **Version your containers**: Always tag your container images with specific versions for production deployments.

6. **Backup manifests**: Keep generated deployment manifests in version control for reproducible deployments.

These examples demonstrate the comprehensive capabilities of the AspireMcpServer for managing .NET Aspire applications throughout their entire lifecycle.
