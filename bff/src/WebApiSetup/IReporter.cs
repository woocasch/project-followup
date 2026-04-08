namespace ProjectFollowUp.BFF.WebApiSetup;

public interface IReporter
{
    public enum ReportLevel
    {
        Info,
        Warning,
        Error
    }

    void Report(string message, ReportLevel level);
}
