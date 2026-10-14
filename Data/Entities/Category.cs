using System.ComponentModel.DataAnnotations.Schema;

namespace ExportStatistics.Data.Entities;

public class Category : BaseEntity
{
    [Column("name")]
    public string Name { get; set; } = string.Empty;
}