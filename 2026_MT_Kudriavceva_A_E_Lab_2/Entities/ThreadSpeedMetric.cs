namespace Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("ThreadSpeedMetrics")]
public class ThreadSpeedMetric : BaseEntity<int>
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int MetricId { get; set; }

    public override int Id => MetricId;

    [Required]
    [MaxLength(300)]
    public string TestDescription { get; set; } = string.Empty;

    [Required]
    public int LogicalCores { get; set; }

    [Required]
    public long SingleThreadTimeMs { get; set; }

    [Required]
    public long ParallelTimeMs { get; set; }

    [NotMapped]
    public decimal Efficiency =>
        ParallelTimeMs == 0 ? 0 : (decimal)SingleThreadTimeMs / ParallelTimeMs;

    [Required]
    public DateTime MeasuredAt { get; set; } = DateTime.UtcNow;

    [Required]
    [MaxLength(200)]
    public string CpuModel { get; set; } = string.Empty;

    [Required]
    public int RamGb { get; set; }

    [Required]
    [MaxLength(100)]
    public string Os { get; set; } = string.Empty;

    public override string ToLogString(string additionalInfo = "")
    {
        return base.ToLogString($"Test={TestDescription}, Eff={Efficiency:F2} {additionalInfo}");
    }
}