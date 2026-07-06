namespace Repositories;

using Data;
using Entities;

public class UnitOfWork(ApplicationDbContext context)
    : IUnitOfWork, IDisposable
{
    private readonly ApplicationDbContext context = context;
    private bool disposed;

    public IRepository<Project, int> Projects { get; } = new GenericRepository<Project, int>(context);

    public IRepository<PipelineStepExecution, int> PipelineSteps { get; } = new GenericRepository<PipelineStepExecution, int>(context);

    public IRepository<IssueLog, int> IssueLogs { get; } = new GenericRepository<IssueLog, int>(context);

    public IRepository<ThreadSpeedMetric, int> Metrics { get; } = new GenericRepository<ThreadSpeedMetric, int>(context);

    public async Task SaveChangesAsync() => await this.context.SaveChangesAsync(false).ConfigureAwait(false);

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                this.context.Dispose();
            }

            this.disposed = true;
        }
    }
}