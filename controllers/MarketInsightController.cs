using AutoMapper;
using AutoMapper.QueryableExtensions;
using Community.OData.Linq;
using ExportStatistics.Data;
using ExportStatistics.Data.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;

[EnableRateLimiting("PublicReadApi")]
[ApiController]
[Route("api/market-insight")]
public class MarketInsightApicontroller : ControllerBase
{
    private readonly EfDbContext _dbContext;
    private readonly IMapper _mapper;

    public MarketInsightApicontroller(EfDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    /// <summary>
    /// Get export demand by year and products
    /// </summary>
    /// <param name="hscode"></param>
    /// <returns></returns>
    [HttpGet("export-demand/{hscode}")]
    public async Task<IActionResult> GetExportDemand(string hscode)
    {
        // Clean the input (remove dots if user types 22.07.10)
        var cleanHsCode = hscode.Replace(".", "");

        var product = await _dbContext.ExportProducts
            .FirstOrDefaultAsync(p => p.HsCode == cleanHsCode);

        if (product == null) return NotFound(new { error = "HS Code not found." });

        var insights = await _dbContext.MarketInsights
            .Include(m => m.Country)
            .Where(m => m.ProductId == product.Id && m.LastYearVolumeMt > 0 && m.AveragePriceMT > 0)
            .OrderByDescending(m => m.MarketShare)
            .Select(m => new MarketInsightDto
            {
                CountryName = m.Country.Name,
                TrendSignal = m.TrendSignal,
                GrowthRate = m.CagrPercentage.HasValue ? m.CagrPercentage.Value.ToString("0.00") + "%" : "New Market",
                MarketShare = m.MarketShare ?? 0,
                CurrentPriceMT = m.LastYearPricePerMt ?? 0,
                AvgPrice5Yr = m.AveragePriceMT ?? 0,
                PriceTrend = m.PriceTrend,
                PeakYear = m.PeakYear,
                RiskLevel = m.VolatilityScore > 50 ? "High" : (m.VolatilityScore > 20 ? "Medium" : "Low")
            })
            .ToListAsync();

        var history = await _dbContext.ExportStatistics
            .Where(es => es.ProductId == product.Id && es.Year != "2026-27")
            .GroupBy(es => es.Year)
            .Select(g => new { Year = g.Key, ValueMillionUSD = Math.Round(g.Sum(x => x.Value), 2) })
            .OrderBy(g => g.Year)
            .ToListAsync();

        return Ok(new
        {
            productName = product.Name,
            hsCode = product.HsCode,
            insights = insights,
            globalHistory = history,
            metadata = new
            {
                source = "APEDA (Agricultural and Processed Food Products Export Development Authority, India)",
                data_quality = "Official Government Trade Statistics",
                reporting_lag = "6-12 Months (Typical for international trade records)",
                last_full_year_analyzed = "2025-26",
                disclaimer = "This analysis is for strategic guidance only. Historical trends do not guarantee future performance. Market conditions, government policies, and global logistics may impact actual trade outcomes. Users are advised to verify critical data with current port-level records.",
                methodology = "Growth (CAGR) and Trends are calculated using a 5-year rolling window of finalized financial years."
            }
        });
    }
    /// <summary>
    /// Get Exported products 
    /// </summary>
    /// <returns></returns>
    [HttpGet("products")]
    public async Task<IActionResult> GetExportProducts()
    {
        var queryable = _dbContext.ExportStatistics
        .Where(ep => !ep.IsDeleted)
        .ProjectTo<ExportData>(_mapper.ConfigurationProvider)
        .OrderByDescending(ep => ep.CreatedAt);

        var oDataQuery = queryable.OData().ApplyODataQuery(HttpContext.Request);
        var filteredQuery = queryable.OData()
        .ApplyODataQuery(HttpContext.Request, skipPagination: true)
        .ToOriginalQuery();

        var totalCount = await filteredQuery.CountAsync();
        var items = await oDataQuery.ToOriginalQuery().ToListAsync();
        var pagedResults = HttpContext.Request.ToPagedResult(items, totalCount);

        return Ok(pagedResults);
    }
    [HttpGet("countries")]
    public async Task<IActionResult> GetCountries()
    {
        var countries = await _dbContext.Countries.Where(c => c.IsDeleted == false)
        .OrderBy(c => c.Name).Select(c => c.Name).ToListAsync();
        return Ok(countries);
    }
    [HttpGet("categories")]
    public async Task<IActionResult> GetCatogries()
    {
        var categories = await _dbContext.Categories.Where(c => c.IsDeleted == false)
        .OrderBy(c => c.Name).Select(c => c.Name).ToListAsync();
        return Ok(categories);
    }
}