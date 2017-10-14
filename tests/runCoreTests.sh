echo Running Core 2.0 Unit tests
echo
dotnet test CodingTasks.Algorithm.Tests/CodingTasks.Algorithm.Tests.csproj --configuration Release --framework netcoreapp2.0
echo
echo -----------------------------
echo
echo Running Core 2.0 Stress tests
echo
dotnet test CodingTasks.BlockingQueue.StressTests/CodingTasks.BlockingQueue.StressTests.csproj --configuration Release --framework netcoreapp2.0