namespace Data;

using Entities;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<PipelineStepExecution> PipelineStepExecutions => Set<PipelineStepExecution>();
    public DbSet<IssueLog> IssueLogs => Set<IssueLog>();
    public DbSet<ThreadSpeedMetric> ThreadSpeedMetrics => Set<ThreadSpeedMetric>();

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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