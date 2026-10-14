using System.ComponentModel.DataAnnotations.Schema;

namespace ExportStatistics.Data.Entities
{
    public class ErrorLog : BaseEntity
    {
        [Column("controller_name")]
        public string? ControllerName { get; set; }
        [Column("action_name")]
        public string? ActionName { get; set; }
        [Column("status_code")]
        public int? StatusCode { get; set; }
        [Column("exception_type")]
        public string? ExceptionType { get; set; }
        [Column("message")]
        public string? Message { get; set; }
        [Column("trace_id")]
        public string? TraceId { get; set; }
        [Column("stack_trace")]
        public string? StackTrace { get; set; }
    }
}