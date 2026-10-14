using ExportStatistics.Data;
using ExportStatistics.Data.Entities;
using ExportStatistics.Data.Request;
using ExportStatistics.service;


public class ErrorLogService : IErrorLogService
{
    private readonly EfDbContext _dbContext;
    private readonly ILogger<ErrorLogService> _logger;
    public ErrorLogService(EfDbContext dbContext, ILogger<ErrorLogService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task InsertErrorLogAsync(CreateErrorLogRequest error)
    {

        try
        {
            var errorLog = new ErrorLog
            {
                CreatedAt = DateTime.UtcNow,
                ActionName = error.ActionName,
                ControllerName = error.ControllerName,
                StackTrace = error.StackTrace,
                StatusCode = error.StatusCode,
                TraceId = error.TraceId,
                ExceptionType = error.ExceptionType,
                Message = error.Message
            };
            await _dbContext.ErrorLogs.AddAsync(errorLog);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to save error log to the database.");
        }

    }
}