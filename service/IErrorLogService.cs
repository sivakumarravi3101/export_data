namespace ExportStatistics.service;

using ExportStatistics.Data.Request;

public interface IErrorLogService
{
    public Task InsertErrorLogAsync(CreateErrorLogRequest error);
}