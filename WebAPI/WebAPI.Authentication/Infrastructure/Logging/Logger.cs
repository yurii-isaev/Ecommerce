using System;
using System.Globalization;
using System.IO;

namespace WebAPI.Authentication.Infrastructure.Logging;

public class Logger
{
  private StreamWriter _writer;

  public Logger(string logFilePath)
  {
    _writer = new StreamWriter(logFilePath, true);
  }

  public void Log(string message)
  {
    _writer.WriteLine($"{DateTime.Now.ToString(CultureInfo.CurrentCulture)}: {message}");
  }

  public void Flush()
  {
    _writer.Flush();
  }
  

  public static void Fatal(Exception exception)
  {
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(exception);
    Console.ResetColor();
  }

  public static void Fatal(string exception)
  {
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(exception);
    Console.ResetColor();
  }
}