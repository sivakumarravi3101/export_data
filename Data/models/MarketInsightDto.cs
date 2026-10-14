namespace ExportStatistics.Data.Models;

public class MarketInsightDto
{
    public string? CountryName { get; set; }
    public string? TrendSignal { get; set; }
    public string? GrowthRate { get; set; } // Nullable for new markets
    public decimal CurrentPriceMT { get; set; }
    public decimal AvgPrice5Yr { get; set; }
    public string? PriceTrend { get; set; }
    public decimal MarketShare { get; set; }
    public decimal PricePerMT { get; set; }
    public string? PeakYear { get; set; }
    public string? RiskLevel { get; set; } // Derived from Volatility
}