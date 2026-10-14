using ExportStatistics.Data.Models;

public static class PagingHelper
{
    private const int DefaultPageSize = 20;

    public static PagedResult<T> ToPagedResult<T>(
        this HttpRequest request,
        IEnumerable<T> items,
        int totalCount)
    {
        string topValue = request.Query["$top"].ToString();
        string skipValue = request.Query["$skip"].ToString();

        int pageSize = string.IsNullOrEmpty(topValue) ? DefaultPageSize : int.Parse(topValue);
        int skipCount = string.IsNullOrEmpty(skipValue) ? 0 : int.Parse(skipValue);
        int pageIndex = skipCount / Math.Max(pageSize, 1) + 1;

        return new PagedResult<T>
        {
            Items = items.ToList(),
            TotalCount = totalCount,
            PageSize = pageSize,
            PageIndex = pageIndex
        };
    }
}
