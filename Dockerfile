# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0-alpine AS build
WORKDIR /src

# Copy project files
COPY ["AspireMcpServer/AspireMcpServer.csproj", "AspireMcpServer/"]

# Restore dependencies
RUN dotnet restore "AspireMcpServer/AspireMcpServer.csproj"

# Copy source code
COPY . .

# Build and publish with AOT
WORKDIR "/src/AspireMcpServer"
RUN dotnet publish "AspireMcpServer.csproj" \
    --configuration Release \
    --runtime linux-musl-x64 \
    --self-contained true \
    --output /app/publish \
    /p:PublishAot=true \
    /p:PublishSingleFile=true \
    /p:PublishTrimmed=true \
    /p:DebuggerSupport=false \
    /p:EnableUnsafeUTF7Encoding=false \
    /p:InvariantGlobalization=true \
    /p:OptimizationPreference=Size

# Runtime stage
FROM mcr.microsoft.com/dotnet/runtime-deps:9.0-alpine AS runtime

# Install required packages for .NET Aspire CLI
RUN apk add --no-cache \
    icu-libs \
    icu-data-full \
    tzdata \
    ca-certificates \
    # Required for .NET Aspire CLI operations
    docker-cli \
    kubectl \
    && rm -rf /var/cache/apk/*

# Create non-root user
RUN addgroup -g 1000 aspire && \
    adduser -D -s /bin/sh -u 1000 -G aspire aspire

WORKDIR /app

# Copy published application
COPY --from=build /app/publish/AspireMcpServer .

# Set permissions
RUN chmod +x AspireMcpServer && \
    chown -R aspire:aspire /app

# Switch to non-root user
USER aspire

# Health check
HEALTHCHECK --interval=30s --timeout=10s --start-period=5s --retries=3 \
    CMD ./AspireMcpServer --help > /dev/null || exit 1

# Expose MCP server port (default: stdio, but can be configured for TCP)
EXPOSE 3000

# Labels
LABEL org.opencontainers.image.title="AspireMcpServer" \
      org.opencontainers.image.description="Comprehensive .NET Aspire MCP Server with 70+ enterprise tools" \
      org.opencontainers.image.vendor="AspireMcpServer" \
      org.opencontainers.image.licenses="MIT" \
      org.opencontainers.image.source="https://github.com/username/AspireMcpServer" \
      org.opencontainers.image.documentation="https://github.com/username/AspireMcpServer/blob/main/README.md"

# Default entrypoint
ENTRYPOINT ["./AspireMcpServer"]
CMD ["--help"]
