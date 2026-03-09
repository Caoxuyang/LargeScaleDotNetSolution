using LargeScaleSolution.LoggingLib.Services;
using LargeScaleSolution.LoggingLib.Configuration;
using Xunit;

namespace LargeScaleSolution.LoggingLib.Tests;

public class Log4NetLoggerServiceTests
{
    [Fact]
    public void CreateLogger_WithType_ReturnsLoggerService()
    {
        var logger = LoggerFactory.CreateLogger<Log4NetLoggerServiceTests>();

        Assert.NotNull(logger);
    }

    [Fact]
    public void CreateLogger_WithName_ReturnsLoggerService()
    {
        var logger = LoggerFactory.CreateLogger("TestLogger");

        Assert.NotNull(logger);
    }

    [Fact]
    public void Info_DoesNotThrow()
    {
        var logger = new Log4NetLoggerService("TestLogger");

        var exception = Record.Exception(() => logger.Info("Test message"));

        Assert.Null(exception);
    }

    [Fact]
    public void Error_WithException_DoesNotThrow()
    {
        var logger = new Log4NetLoggerService("TestLogger");

        var exception = Record.Exception(() =>
            logger.Error("Error message", new InvalidOperationException("Test")));

        Assert.Null(exception);
    }
}
