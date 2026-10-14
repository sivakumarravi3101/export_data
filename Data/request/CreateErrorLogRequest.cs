namespace ExportStatistics.Data.Request;

public class CreateErrorLogRequest
{
    public string? ControllerName { get; set; }
    public string? ActionName { get; set; }
    public int StatusCode { get; set; }
    public string? ExceptionType { get; set; }
    public string? Message { get; set; }
    public string? TraceId { get; set; }
    public string? StackTrace { get; set; }
}