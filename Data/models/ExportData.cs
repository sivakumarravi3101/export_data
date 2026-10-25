using ExportStatistics.Data.Entities;

namespace ExportStatistics.Data.Models;

public class ExportData
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int CountryId { get; set; }
    public int CategoryId { get; set; }
    public string HsCode { get; set; } = string.Empty;
    public string Year { get; set; } = string.Empty;
    public decimal ProductValue { get; set; }
    public decimal Quantity { get; set; }
    public string CountryName { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
    public string? State { get; set; }
    public string? Port { get; set; }
    public DateTime CreatedAt { get; set; }
}