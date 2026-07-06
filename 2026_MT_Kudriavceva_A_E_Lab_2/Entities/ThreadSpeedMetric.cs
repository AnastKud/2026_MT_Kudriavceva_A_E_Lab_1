namespace Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("ThreadSpeedMetrics")]
public class ThreadSpeedMetric : BaseEntity<int>
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int MetricId { get; set; }

    public override int Id => this.MetricId;

    [Required]
    [MaxLength(300)]
    public string TestDescription { get; set; } = string.Empty;

    [Required]
    public int LogicalCores { get; }

    [Required]
    public long SingleThreadTimeMs { get; }

    [Required]
    public long ParallelTimeMs { get; }

    [NotMapped]
    public decimal Efficiency =>
        this.ParallelTimeMs == 0 ? 0 : (decimal)this.SingleThreadTimeMs / this.ParallelTimeMs;

    [Required]
    public DateTime MeasuredAt { get; } = DateTime.UtcNow;

    [Required]
    [MaxLength(200)]
    public string CpuModel { get; set; } = string.Empty;

    [Required]
    public int RamGb { get; }

    [Required]
    [MaxLength(100)]
    public string Os { get; set; } = string.Empty;

    public override string ToLogString(string additionalInfo = "")
    {
        return base.ToLogString($"Test={this.TestDescription}, Eff={this.Efficiency:F2} {additionalInfo}");
    }
}