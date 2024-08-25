using Microsoft.Extensions.Logging;
using TodoList.Service.Interfaces;

namespace TodoList.Service.Implementations;

public class TestService(ILogger logger) : ITestService
{
    public void DoSomething()
    {
        logger.Log(LogLevel.Information, "DoSomething");
    }
}