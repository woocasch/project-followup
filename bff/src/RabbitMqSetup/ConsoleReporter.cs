namespace ProjectFollowUp.BFF.RabbitMqSetup;

public sealed class ConsoleReporter : IReporter
{
    public void Report(string message, IReporter.ReportLevel level)
    {
        var originalColor = Console.ForegroundColor;
        Console.ForegroundColor = GetColorForLevel(level);
        var prefix = GetPrefixForLevel(level);
        Console.WriteLine("{0} - {1}", prefix, message);
        Console.ForegroundColor = originalColor;
    }

    private static ConsoleColor GetColorForLevel(IReporter.ReportLevel level) => level switch
    {
        IReporter.ReportLevel.Info => ConsoleColor.Green,
        IReporter.ReportLevel.Warning => ConsoleColor.Yellow,
        IReporter.ReportLevel.Error => ConsoleColor.Red,
        _ => ConsoleColor.White,
    };

    private static string GetPrefixForLevel(IReporter.ReportLevel level) => level switch
    {
        IReporter.ReportLevel.Info => "[INFO] ",
        IReporter.ReportLevel.Warning => "[WARNING] ",
        IReporter.ReportLevel.Error => "[ERROR] ",
        _ => string.Empty,
    };
}
