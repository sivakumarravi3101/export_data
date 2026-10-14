namespace ExportStatistics.Data.Models;

public class PagedResult<T>
{
    public IList<T> Items { get; set; } = [];
    public int TotalCount { get; set; }
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
}