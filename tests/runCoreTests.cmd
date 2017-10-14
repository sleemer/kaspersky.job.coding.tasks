@echo off

echo -----------------------------
echo Running dotnet restore
echo -----------------------------

call dotnet restore "CodingTasks.BlockingQueue.StressTests\CodingTasks.BlockingQueue.StressTests.csproj"

if NOT %ERRORLEVEL% == 0 (
    echo Dotnet restore has failed
    goto end
)

echo -----------------------------
echo Running Core 2.0 Stress tests
echo -----------------------------

call dotnet test "CodingTasks.BlockingQueue.StressTests\CodingTasks.BlockingQueue.StressTests.csproj" --configuration Release --framework netcoreapp2.0

if NOT %ERRORLEVEL% == 0 (
    echo CORE 2.0 Stress tests has failed
    goto end
)

echo -----------------------------
echo All tests has passed for Core
echo -----------------------------

:end