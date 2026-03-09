using LargeScaleSolution.Abstractions;

namespace LargeScaleSolution.LoggingLib.Configuration;

public static class LoggerFactory
{
    public static ILoggerService CreateLogger<T>()
    {
        return new Services.Log4NetLoggerService(typeof(T));
    }

    public static ILoggerService CreateLogger(string name)
    {
        return new Services.Log4NetLoggerService(name);
    }
}
