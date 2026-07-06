namespace Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("PipelineStepExecutions")]
public class PipelineStepExecution : BaseEntity<int>
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ExecutionId { get; set; }

    public override int Id => this.ExecutionId;

    [Required]
    public int ProjectId { get; set; }

    [Required]
    [MaxLength(100)]
    public string StepName { get; set; } = string.Empty;

    [Required]
    public DateTime StartedAt { get; set; }

    [Required]
    public long DurationMs { get; set; }

    [Required]
    public bool IsSuccess { get; set; }

    public int TotalErrors { get; set; }

    public int TotalWarnings { get; set; }

    [ForeignKey(nameof(ProjectId))]
    public virtual Project Project { get; set; } = null!;

    public virtual ICollection<IssueLog> IssueLogs { get; } =
        [];

    public override string ToLogString(string additionalInfo = "")
    {
        return base.ToLogString($"Project={this.Project?.Name}, Step={this.StepName}, Success={this.IsSuccess} {additionalInfo}");
    }
}