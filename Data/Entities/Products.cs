using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ExportStatistics.Data.Entities;

public class ExportProduct : BaseEntity
{
    [Column("name")]
    public required string Name { get; set; }
    [Column("hscode")]
    public required string HsCode { get; set; }
    [Column("category_id")]
    public int CategoryId { get; set; }
    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }

    [ForeignKey("CategoryId")]
    public virtual Category? Category { get; set; }
}