using Entities;

namespace Repositories;

public interface IUnitOfWork : IDisposable
{
    IRepository<Project, int> Projects { get; }

    IRepository<PipelineStepExecution, int> PipelineSteps { get; }

    IRepository<IssueLog, int> IssueLogs { get; }

    IRepository<ThreadSpeedMetric, int> Metrics { get; }

    Task SaveChangesAsync();
}