using Microsoft.Extensions.Logging;

namespace TodoList.Service.Implementations;

public class LoggerService: ILogger
{
    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        Console.WriteLine($"""
                           
                               logLevel = {logLevel}
                               eventId = {eventId}
                               state = {state}
                               exception = {exception}
                                   
                           """);
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return true;
    }

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        return null;
    }
}