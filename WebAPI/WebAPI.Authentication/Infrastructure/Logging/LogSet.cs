using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using static WebAPI.Authentication.Infrastructure.Logging.LogTheme;

namespace WebAPI.Authentication.Infrastructure.Logging
{
  public static class LogSet
  {
    /// <summary>
    /// The setup of logging settings by default.
    /// </summary>
    public static void CustomThemeSetup()
    {
      Log.Logger = new LoggerConfiguration()
        .WriteTo.Console(theme: Signal, outputTemplate: OutputTemplate)
        .CreateLogger();
    }

    public static void MinimumLevelSetup()
    {
      Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Debug()
        .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
        // Эта строка исключает логирование SQL запросов на уровне ниже Warning
        .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", LogEventLevel.Warning)
        .Enrich.FromLogContext()
        .WriteTo.Console(theme: AnsiConsoleTheme.Literate)
        .CreateLogger();
    }


    // public static void MinimumLevelSetup()
    // {
    //   var myTheme = new AnsiConsoleTheme(new Dictionary<ConsoleThemeStyle, string>
    //   {
    //     [ConsoleThemeStyle.Text] = "\x1b[38;5;0253m",
    //     [ConsoleThemeStyle.SecondaryText] = "\x1b[38;5;0246m",
    //     [ConsoleThemeStyle.TertiaryText] = "\x1b[38;5;0242m",
    //     [ConsoleThemeStyle.Invalid] = "\x1b[38;5;0196m",
    //     [ConsoleThemeStyle.Null] = "\x1b[38;5;0022m",
    //     [ConsoleThemeStyle.Name] = "\x1b[38;5;0216m",
    //     [ConsoleThemeStyle.String] = "\x1b[38;5;0215m",
    //     [ConsoleThemeStyle.Number] = "\x1b[38;5;151m",
    //     [ConsoleThemeStyle.Boolean] = "\x1b[38;5;0038m",
    //     [ConsoleThemeStyle.Scalar] = "\x1b[38;5;0079m",
    //     [ConsoleThemeStyle.LevelVerbose] = "\x1b[37m",
    //     [ConsoleThemeStyle.LevelDebug] = "\x1b[37m",
    //     [ConsoleThemeStyle.LevelInformation] = "\x1b[32m",
    //     [ConsoleThemeStyle.LevelWarning] = "\x1b[33m", // Желтый цвет для предупреждений
    //     [ConsoleThemeStyle.LevelError] = "\x1b[31m",
    //     [ConsoleThemeStyle.LevelFatal] = "\x1b[31m",
    //   });
    //
    //   Log.Logger = new LoggerConfiguration()
    //     .MinimumLevel.Debug()
    //     .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    //     .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", LogEventLevel.Warning)
    //     .Enrich.FromLogContext()
    //     .WriteTo.Console(theme: myTheme) // Используйте вашу тему здесь
    //     .CreateLogger();
    // }
  }
}
