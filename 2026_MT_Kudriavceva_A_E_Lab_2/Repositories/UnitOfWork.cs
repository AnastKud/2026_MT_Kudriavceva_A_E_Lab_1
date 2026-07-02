namespace Repositories;

using Data;
using Entities;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public IRepository<Project, int> Projects { get; }
    public IRepository<PipelineStepExecution, int> PipelineSteps { get; }
    public IRepository<IssueLog, int> IssueLogs { get; }
    public IRepository<ThreadSpeedMetric, int> Metrics { get; }

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        Projects = new GenericRepository<Project, int>(context);
        PipelineSteps = new GenericRepository<PipelineStepExecution, int>(context);
        IssueLogs = new GenericRepository<IssueLog, int>(context);
        Metrics = new GenericRepository<ThreadSpeedMetric, int>(context);
    }

    public async Task SaveChangesAsync() => await _context.SaveChangesAsync();

    public void Dispose() => _context.Dispose();
}