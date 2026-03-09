using log4net;
using log4net.Config;
using LargeScaleSolution.Abstractions;

namespace LargeScaleSolution.LoggingLib.Services;

public sealed class Log4NetLoggerService : ILoggerService
{
    private readonly ILog _log;

    public Log4NetLoggerService(Type type)
    {
        _log = LogManager.GetLogger(type);
    }

    public Log4NetLoggerService(string name)
    {
        _log = LogManager.GetLogger(name);
    }

    public static void Configure()
    {
        var logRepository = LogManager.GetRepository(System.Reflection.Assembly.GetEntryAssembly()!);
        XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
    }

    public void Info(string message)
    {
        _log.Info(message);
    }

    public void Warn(string message)
    {
        _log.Warn(message);
    }

    public void Error(string message, Exception? ex = null)
    {
        if (ex != null)
            _log.Error(message, ex);
        else
            _log.Error(message);
    }

    public void Debug(string message)
    {
        _log.Debug(message);
    }
}
