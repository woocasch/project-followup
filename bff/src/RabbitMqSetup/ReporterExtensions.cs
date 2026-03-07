namespace ProjectFollowUp.BFF.RabbitMqSetup;

public static class ReporterExtensions
{
    public static void Info(this IReporter reporter, string message)
    {
        reporter.Report(message, IReporter.ReportLevel.Info);
    }
    
    public static void Warning(this IReporter reporter, string message)
    {
        reporter.Report(message, IReporter.ReportLevel.Warning);
    }
    
    public static void Error(this IReporter reporter, string message)
    {
        reporter.Report(message, IReporter.ReportLevel.Error);
    }
}
