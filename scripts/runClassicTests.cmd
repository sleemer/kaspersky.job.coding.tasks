@echo off

echo -----------------------------
echo Running .NET 4.6 Unit tests
echo -----------------------------

call dotnet test "..\tests\CodingTasks.Tests\CodingTasks.Tests.csproj" --configuration Release --framework net46

if NOT %ERRORLEVEL% == 0 (
    echo .NET 4.6 Unit tests has failed
    goto end
)

echo -----------------------------
echo Running .NET 4.6 Stress tests
echo -----------------------------

call dotnet test "..\tests\CodingTasks.StressTests\CodingTasks.StressTests.csproj" --configuration Release --framework net46

if NOT %ERRORLEVEL% == 0 (
    echo .NET 4.6 Stress tests has failed
    goto end
)

echo -----------------------------
echo All tests has passed for .NET 4.6
echo -----------------------------

:end