using System.ComponentModel.DataAnnotations.Schema;

namespace ExportStatistics.Data.Entities;

[Table("market_insight")]
public class MarketInsight : BaseEntity
{
    [Column("product_id")]
    public int ProductId { get; set; }
    [Column("country_id")]
    public int CountryId { get; set; }
    [Column("cagr_percent")]
    public decimal? CagrPercentage { get; set; }
    [Column("trend_signal")]
    public string? TrendSignal { get; set; } = string.Empty;
    [Column("avg_price_per_mt")]
    public decimal? AveragePriceMT { get; set; }
    [Column("volatility_score")]
    public decimal? VolatilityScore { get; set; }
    [Column("peak_year")]
    public string? PeakYear { get; set; }
    [Column("last_year_volume_mt")]
    public decimal? LastYearVolumeMt { get; set; }
    [Column("market_share_percent")]
    public decimal? MarketShare { get; set; }
    [Column("last_year_price_per_mt")]
    public decimal? LastYearPricePerMt { get; set; }

    [Column("price_trend")]
    public string? PriceTrend { get; set; }
    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey(nameof(ProductId))]
    public virtual ExportProduct? Product { get; set; }
    [ForeignKey(nameof(CountryId))]
    public virtual Country? Country { get; set; }
}