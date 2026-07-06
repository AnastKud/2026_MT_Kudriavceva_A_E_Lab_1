namespace Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("IssueLogs")]
public class IssueLog : BaseEntity<int>
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int LogId { get; set; }

    public override int Id => this.LogId;

    [Required]
    public int ExecutionId { get; set; }

    [Required]
    public DateTime LoggedAt { get; set; } = DateTime.UtcNow;

    [Required]
    [MaxLength(20)]
    public string Severity { get; set; } = "Error";

    [MaxLength(50)]
    public string? Code { get; set; }

    [Required]
    public string Message { get; set; } = string.Empty;

    [ForeignKey(nameof(ExecutionId))]
    public virtual PipelineStepExecution Execution { get; set; } = null!;

    public override string ToLogString(string additionalInfo = "")
    {
        return base.ToLogString($"[{this.Severity}] {this.Code}: {this.Message} {additionalInfo}");
    }
}