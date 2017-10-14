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
echo Running .NET 4.6 Stress tests
echo -----------------------------

call dotnet test "CodingTasks.BlockingQueue.StressTests\CodingTasks.BlockingQueue.StressTests.csproj" --configuration Release --framework net46

if NOT %ERRORLEVEL% == 0 (
    echo .NET 4.6 Stress tests has failed
    goto end
)

echo -----------------------------
echo All tests has passed for .NET 4.6
echo -----------------------------

:end