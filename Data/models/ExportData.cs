using ExportStatistics.Data.Entities;

namespace ExportStatistics.Data.Models;

public class ExportData
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? State { get; set; }
    public string? Port { get; set; }
    public int ProductId { get; set; }
    public int CountryId { get; set; }
    public string Year { get; set; } = string.Empty;
    public decimal Value { get; set; }
    public decimal Quantity { get; set; }
    public string CountryName { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public string HsCode { get; set; } = string.Empty;
    public int CategoryId { get; set; }
    public string ProductName { get; set; } = string.Empty;
}