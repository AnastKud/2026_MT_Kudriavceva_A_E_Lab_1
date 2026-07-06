namespace Data;

using Entities;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : DbContext(options)
{
    public DbSet<Project> Projects => this.Set<Project>();

    public DbSet<PipelineStepExecution> PipelineStepExecutions => this.Set<PipelineStepExecution>();

    public DbSet<IssueLog> IssueLogs => this.Set<IssueLog>();

    public DbSet<ThreadSpeedMetric> ThreadSpeedMetrics => this.Set<ThreadSpeedMetric>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        modelBuilder.Entity<PipelineStepExecution>()
            .HasOne(e => e.Project)
            .WithMany(p => p.PipelineStepExecutions)
            .HasForeignKey(e => e.ProjectId);

        modelBuilder.Entity<IssueLog>()
            .HasOne(i => i.Execution)
            .WithMany(e => e.IssueLogs)
            .HasForeignKey(i => i.ExecutionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}