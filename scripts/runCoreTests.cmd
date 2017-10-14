@echo off

echo -----------------------------
echo Running Core 2.0 Unit tests
echo -----------------------------

call dotnet test "..\tests\CodingTasks.Algorithm.Tests\CodingTasks.Algorithm.Tests.csproj" --configuration Release --framework netcoreapp2

if NOT %ERRORLEVEL% == 0 (
    echo Core 2.0 Unit tests has failed
    goto end
)

echo -----------------------------
echo Running Core 2.0 Stress tests
echo -----------------------------

call dotnet test "..\tests\CodingTasks.BlockingQueue.StressTests\CodingTasks.BlockingQueue.StressTests.csproj" --configuration Release --framework netcoreapp2.0

if NOT %ERRORLEVEL% == 0 (
    echo CORE 2.0 Stress tests has failed
    goto end
)

echo -----------------------------
echo All tests has passed for Core
echo -----------------------------

:end