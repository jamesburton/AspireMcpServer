# Use the official .NET 9 runtime image as base
FROM mcr.microsoft.com/dotnet/runtime:9.0-alpine AS base
WORKDIR /app

# Use the official .NET 9 SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:9.0-alpine AS build
WORKDIR /src

# Copy project file and restore dependencies
COPY AspireMcpServer/AspireMcpServer.csproj AspireMcpServer/
RUN dotnet restore AspireMcpServer/AspireMcpServer.csproj

# Copy source code and build
COPY . .
WORKDIR /src/AspireMcpServer
RUN dotnet publish AspireMcpServer.csproj -c Release -o /app/publish \
    --runtime linux-x64 \
    --self-contained true \
    /p:PublishAot=true \
    /p:PublishSingleFile=true \
    /p:PublishTrimmed=true

# Final stage
FROM mcr.microsoft.com/dotnet/runtime-deps:9.0-alpine AS final
WORKDIR /app

# Install Aspire CLI (this would need to be updated when Aspire CLI supports Linux containers)
# For now, this serves as a placeholder for when container support is available
RUN apk add --no-cache icu-libs

# Copy the published application
COPY --from=build /app/publish .

# Set the entry point
ENTRYPOINT ["./AspireMcpServer"]
