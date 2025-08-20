# AOT Compilation Guide for AspireMcpServer

This guide explains how to compile the AspireMcpServer with ahead-of-time (AOT) compilation for optimal performance and deployment.

## What is AOT Compilation?

AOT (Ahead-of-Time) compilation transforms .NET applications into native code during the build process rather than at runtime. This provides several benefits:

- **Faster startup**: No JIT compilation overhead
- **Smaller memory footprint**: Reduced runtime requirements
- **Single-file deployment**: All dependencies bundled into one executable
- **Better performance**: Optimized native code
- **Reduced dependencies**: No need for .NET runtime on target machine

## Prerequisites

- .NET 9.0 SDK or later
- Platform-specific requirements:
  - **Windows**: Visual Studio 2022 17.8+ or Build Tools
  - **Linux**: GCC or Clang toolchain
  - **macOS**: Xcode command line tools

## Building with AOT

### Option 1: Using the Build Script (Recommended)

#### Windows (PowerShell)
```powershell
# Build for current platform
.\build-aot.ps1

# Build for specific platform
.\build-aot.ps1 -Runtime win-x64
.\build-aot.ps1 -Runtime linux-x64
.\build-aot.ps1 -Runtime osx-arm64

# Build for all platforms
.\build-aot.ps1 -Runtime all

# Release configuration (default)
.\build-aot.ps1 -Configuration Release
```

#### Windows (Batch)
```cmd
# Simple build for Windows x64
.\build-aot.bat
```

### Option 2: Manual dotnet publish

```bash
# Windows x64
dotnet publish AspireMcpServer/AspireMcpServer.csproj \
  -c Release \
  -r win-x64 \
  --self-contained \
  /p:PublishAot=true \
  /p:PublishSingleFile=true \
  /p:PublishTrimmed=true

# Linux x64
dotnet publish AspireMcpServer/AspireMcpServer.csproj \
  -c Release \
  -r linux-x64 \
  --self-contained \
  /p:PublishAot=true \
  /p:PublishSingleFile=true \
  /p:PublishTrimmed=true

# macOS ARM64
dotnet publish AspireMcpServer/AspireMcpServer.csproj \
  -c Release \
  -r osx-arm64 \
  --self-contained \
  /p:PublishAot=true \
  /p:PublishSingleFile=true \
  /p:PublishTrimmed=true
```

## AOT-Specific Configuration

The project includes several AOT optimizations in the `.csproj` file:

### Core AOT Settings
```xml
<PublishAot>true</PublishAot>
<PublishSingleFile>true</PublishSingleFile>
<PublishTrimmed>true</PublishTrimmed>
<SelfContained>true</SelfContained>
```

### Trimming Configuration
```xml
<TrimMode>full</TrimMode>
<EnableTrimAnalyzer>true</EnableTrimAnalyzer>
<SuppressTrimAnalysisWarnings>false</SuppressTrimAnalysisWarnings>
```

### Size Optimizations
```xml
<OptimizationPreference>Size</OptimizationPreference>
<IlcOptimizationPreference>Size</IlcOptimizationPreference>
<IlcFoldIdenticalMethodBodies>true</IlcFoldIdenticalMethodBodies>
```

### Runtime Optimizations
```xml
<InvariantGlobalization>true</InvariantGlobalization>
<UseSystemResourceKeys>true</UseSystemResourceKeys>
<DebuggerSupport>false</DebuggerSupport>
<EventSourceSupport>false</EventSourceSupport>
```

## Trimming Preservation

The `ILLink.Descriptors.xml` file ensures that MCP-required types are preserved during trimming:

```xml
<linker>
  <!-- Preserve MCP tool classes -->
  <assembly fullname="AspireMcpServer" preserve="all">
    <type fullname="AspireMcpServer.Services.AspireCommands" preserve="all" />
    <type fullname="AspireMcpServer.Core.AspireExecutor" preserve="all" />
  </assembly>
  
  <!-- Preserve ModelContextProtocol types -->
  <assembly fullname="ModelContextProtocol">
    <namespace fullname="ModelContextProtocol.Server" preserve="all" />
  </assembly>
</linker>
```

## Output Locations

After building, find your executables at:

```
AspireMcpServer/bin/Release/net9.0/{runtime}/publish/
```

Where `{runtime}` is:
- `win-x64` for Windows 64-bit
- `linux-x64` for Linux 64-bit
- `osx-x64` for macOS Intel
- `osx-arm64` for macOS Apple Silicon

## Performance Characteristics

### Binary Sizes (Typical)
- **Windows**: ~25-35 MB
- **Linux**: ~20-30 MB
- **macOS**: ~25-35 MB

### Startup Performance
- **Cold start**: ~50-100ms
- **Memory usage**: ~15-25 MB
- **JIT overhead**: Eliminated

### Runtime Performance
- **CLI execution**: No noticeable overhead
- **MCP response time**: Sub-millisecond
- **Process creation**: ~2-5ms per command

## Troubleshooting AOT Issues

### Common Build Errors

#### 1. Trim Analysis Warnings
```
warning IL2026: Using member 'Type.GetMethod' which has 'RequiresUnreferencedCodeAttribute'
```

**Solution**: Add types to `ILLink.Descriptors.xml` or use `[UnconditionalSuppressMessage]` attribute.

#### 2. Missing Runtime Dependencies
```
error : Could not find required native dependencies
```

**Solution**: Install platform-specific build tools:
- **Windows**: Visual Studio Build Tools
- **Linux**: `sudo apt install build-essential` (Ubuntu/Debian)
- **macOS**: `xcode-select --install`

#### 3. Serialization Issues
```
System.InvalidOperationException: No JSON type information
```

**Solution**: Add types to `AppJsonSerializerContext`:
```csharp
[JsonSerializable(typeof(YourType))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{
}
```

#### 4. Reflection Errors
```
System.InvalidOperationException: Cannot create instance of type
```

**Solution**: Preserve the type in `ILLink.Descriptors.xml`:
```xml
<type fullname="YourNamespace.YourType" preserve="all" />
```

### Build Optimization Tips

#### 1. Reduce Binary Size
- Enable invariant globalization: `<InvariantGlobalization>true</InvariantGlobalization>`
- Disable unused features: `<DebuggerSupport>false</DebuggerSupport>`
- Use size optimization: `<OptimizationPreference>Size</OptimizationPreference>`

#### 2. Improve Build Performance
- Use incremental builds: Keep intermediate files between builds
- Parallelize builds: Use `/m` flag with MSBuild
- Cache native dependencies: Reuse native compilation artifacts

#### 3. Debug AOT Issues
- Enable verbose logging: `dotnet publish -v detailed`
- Analyze trim warnings: Review all IL2XXX warnings
- Test with sample data: Verify all code paths work with AOT

## Deployment Strategies

### Standalone Deployment
The AOT-compiled executable is completely self-contained:

```bash
# Copy the single executable
cp AspireMcpServer.exe /target/directory/

# No runtime installation required
./AspireMcpServer.exe
```

### Container Deployment
Use the provided Dockerfile for containerized deployment:

```bash
# Build container image
docker build -t aspire-mcp-server .

# Run container
docker run -d aspire-mcp-server
```

### CI/CD Integration
Example GitHub Actions workflow:

```yaml
- name: Publish AOT
  run: |
    dotnet publish AspireMcpServer/AspireMcpServer.csproj \
      -c Release \
      -r linux-x64 \
      --self-contained \
      /p:PublishAot=true

- name: Upload Artifacts
  uses: actions/upload-artifact@v3
  with:
    name: aspire-mcp-server-linux-x64
    path: AspireMcpServer/bin/Release/net9.0/linux-x64/publish/
```

## Testing AOT Builds

### Functional Testing
```bash
# Test basic functionality
./AspireMcpServer.exe --version

# Test MCP protocol (requires MCP client)
echo '{"jsonrpc":"2.0","method":"tools/list","id":1}' | ./AspireMcpServer.exe
```

### Performance Testing
```bash
# Measure startup time
time ./AspireMcpServer.exe --help

# Monitor memory usage
dotnet-counters monitor --process-id $(pgrep AspireMcpServer)
```

### Compatibility Testing
Test on target platforms:
- Windows Server 2019+
- Ubuntu 20.04+
- macOS 11+

## Best Practices

### Development
1. **Test AOT builds regularly**: Don't wait until deployment
2. **Use trim-safe patterns**: Avoid dynamic code generation
3. **Minimize reflection**: Use compile-time alternatives when possible
4. **Profile performance**: Measure actual improvements

### Deployment
1. **Verify target compatibility**: Check OS and architecture requirements
2. **Test in production-like environments**: Validate performance characteristics
3. **Monitor resource usage**: Track memory and startup time
4. **Plan rollback strategy**: Keep non-AOT builds available

### Maintenance
1. **Update dependencies carefully**: Verify AOT compatibility
2. **Review trim warnings**: Address new warnings promptly
3. **Benchmark regularly**: Monitor performance regressions
4. **Document known issues**: Track platform-specific limitations

## Advanced Configuration

### Custom ILC Options
Add custom native code generation options:

```xml
<PropertyGroup>
  <IlcOptimizationPreference>Speed</IlcOptimizationPreference>
  <IlcGenerateStackTraceData>false</IlcGenerateStackTraceData>
  <IlcFoldIdenticalMethodBodies>true</IlcFoldIdenticalMethodBodies>
</PropertyGroup>
```

### Platform-Specific Optimizations
```xml
<PropertyGroup Condition="'$(RuntimeIdentifier)' == 'win-x64'">
  <IlcOptimizationPreference>Speed</IlcOptimizationPreference>
</PropertyGroup>

<PropertyGroup Condition="'$(RuntimeIdentifier)' == 'linux-x64'">
  <IlcOptimizationPreference>Size</IlcOptimizationPreference>
</PropertyGroup>
```

### JSON Serialization Context
Extend the JSON context for new types:

```csharp
[JsonSerializable(typeof(string))]
[JsonSerializable(typeof(int))]
[JsonSerializable(typeof(bool))]
[JsonSerializable(typeof(CommandResult))]
[JsonSerializable(typeof(Dictionary<string, object>))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{
}
```

## Conclusion

AOT compilation provides significant benefits for the AspireMcpServer:
- Faster startup times
- Reduced memory footprint  
- Simplified deployment
- Better performance

The project is fully configured for AOT compilation with appropriate optimizations and trimming preservation. Use the provided build scripts for the best experience, and follow the troubleshooting guide when issues arise.

For production deployments, always test AOT builds thoroughly on your target platforms to ensure compatibility and optimal performance.
