@echo off
echo Building AspireMcpServer with AOT...
dotnet publish AspireMcpServer\AspireMcpServer.csproj -c Release -r win-x64 --self-contained
echo Build complete. Check AspireMcpServer\bin\Release\net9.0\win-x64\publish\ for output.
pause
