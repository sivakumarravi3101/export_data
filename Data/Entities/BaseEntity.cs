using System.ComponentModel.DataAnnotations.Schema;

namespace ExportStatistics.Data.Entities;

public class BaseEntity
{
    [Column("id")]
    public int Id { get; set; }
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
    [Column("is_deleted")]
    public bool IsDeleted { get; set; } = false;

}