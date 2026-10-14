using System.Text.RegularExpressions;
using Community.OData.Linq;
using System.Globalization;

public static class ODataQueryExtensions
{
    public static ODataQuery<T> ApplyODataQuery<T>(
            this ODataQuery<T> oDataQuery,
            HttpRequest request,
            bool skipPagination = false)
    {
        const int DefaultPageSize = 20;
        const int MaxPageSize = 100;
        const int MaxSkip = 10000;
        var query = request.Query;

        // ---------- Validate pagination inputs ----------

        int ParseInt(string key, int defaultValue)
        {
            if (!query.ContainsKey(key))
                return defaultValue;

            var value = query[key].ToString();

            if (!int.TryParse(
                    value,
                    NumberStyles.None,
                    CultureInfo.InvariantCulture,
                    out int result))
            {
                throw new BadHttpRequestException(
                    $"'{key}' must be a valid non-negative integer.",
                    StatusCodes.Status400BadRequest);
            }

            return result;
        }

        int top = ParseInt("$top", DefaultPageSize);
        int pageSize = ParseInt("pageSize", DefaultPageSize);
        int skip = ParseInt("$skip", 0);
        int pageNumber = ParseInt("pageNumber", 1);

        if (top < 1 || top > MaxPageSize)
        {
            throw new BadHttpRequestException(
                $"'$top' must be between 1 and {MaxPageSize}.",
                StatusCodes.Status400BadRequest);
        }

        if (pageSize < 1 || pageSize > MaxPageSize)
        {
            throw new BadHttpRequestException(
                $"'pageSize' must be between 1 and {MaxPageSize}.",
                StatusCodes.Status400BadRequest);
        }

        if (skip < 0 || skip > MaxSkip)
        {
            throw new BadHttpRequestException(
                $"'$skip' must be between 0 and {MaxSkip}.",
                StatusCodes.Status400BadRequest);
        }

        if (pageNumber < 1)
        {
            throw new BadHttpRequestException(
                "'pageNumber' must be 1 or greater.",
                StatusCodes.Status400BadRequest);
        }

        // ---------- $filter ----------
        if (query.ContainsKey("$filter"))
        {
            var filter = query["$filter"].ToString();

            // Normalize common string operations to case-insensitive
            // eq: Name eq 'ABC' -> tolower(Name) eq 'abc'
            filter = Regex.Replace(
                filter,
                @"(\w+)\s+eq\s+'([^']+)'",
                m => $"tolower({m.Groups[1].Value}) eq '{m.Groups[2].Value.ToLower()}'",
                RegexOptions.IgnoreCase
            );

            // contains/startswith/endswith
            filter = Regex.Replace(
                filter,
                @"(contains|startswith|endswith)\((\w+),\s*'([^']+)'\)",
                m => $"{m.Groups[1].Value}(tolower({m.Groups[2].Value}),'{m.Groups[3].Value.ToLower()}')",
                RegexOptions.IgnoreCase
            );

            oDataQuery = oDataQuery.Filter(filter);
        }

        // ---------- $orderby ----------
        if (query.ContainsKey("$orderby"))
        {
            oDataQuery = oDataQuery.OrderBy(query["$orderby"].ToString());
        }

        // ---------- $top / $skip ----------
        // Skip pagination if requested (useful for counting filtered results)
        if (skipPagination)
            return oDataQuery;

        // ---------- Determine pagination ----------

        int pageSizeToUse =
            query.ContainsKey("$top") ? top :
            query.ContainsKey("pageSize") ? pageSize :
            DefaultPageSize;

        int skipToUse;

        if (query.ContainsKey("$skip"))
        {
            skipToUse = skip;
        }
        else if (query.ContainsKey("pageNumber"))
        {
            long calculatedSkip =
                ((long)pageNumber - 1) * pageSizeToUse;

            if (calculatedSkip > MaxSkip)
            {
                throw new BadHttpRequestException(
                    $"Calculated pagination offset cannot exceed {MaxSkip}.",
                    StatusCodes.Status400BadRequest);
            }

            skipToUse = (int)calculatedSkip;
        }
        else
        {
            skipToUse = 0;
        }

        oDataQuery = oDataQuery.TopSkip(
            pageSizeToUse.ToString(CultureInfo.InvariantCulture),
            skipToUse.ToString(CultureInfo.InvariantCulture));


        return oDataQuery;
    }
}