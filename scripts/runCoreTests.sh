#!/bin/bash
echo Running Core 2.0 Unit tests
echo
dotnet test ../tests/CodingTasks.Tests/CodingTasks.Tests.csproj --configuration Release --framework netcoreapp2.0
echo
echo -----------------------------
echo
echo Running Core 2.0 Stress tests
echo
dotnet test ../tests/CodingTasks.StressTests/CodingTasks.StressTests.csproj --configuration Release --framework netcoreapp2.0