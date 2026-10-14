using System.ComponentModel.DataAnnotations.Schema;

namespace ExportStatistics.Data.Entities;

public class ExportStatistic : BaseEntity
{
    [Column("state")]
    public string? State { get; set; }

    [Column("port")]
    public string? Port { get; set; }

    [Column("product_id")]
    public int ProductId { get; set; }

    [ForeignKey(nameof(ProductId))]
    public virtual ExportProduct? Product { get; set; }

    [Column("country_id")]
    public int CountryId { get; set; }

    [ForeignKey(nameof(CountryId))]
    public virtual Country? Country { get; set; }

    [Column("year")]
    public string Year { get; set; } = string.Empty;

    [Column("value")]
    public decimal Value { get; set; }

    [Column("quantity")]
    public decimal Quantity { get; set; }
}