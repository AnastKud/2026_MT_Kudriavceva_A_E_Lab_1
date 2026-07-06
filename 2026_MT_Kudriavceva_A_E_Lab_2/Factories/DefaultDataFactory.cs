namespace Factories;

using Entities;

public class DefaultDataFactory : IDataFactory
{
    public Project CreateProject(string name, string folderPath)
    {
        return new Project
        {
            Name = name,
            FolderPath = folderPath,
            CreatedAt = DateTime.UtcNow,
        };
    }

    public PipelineStepExecution CreateStep(Project project, string stepName, bool isSuccess, long durationMs)
    {
        ArgumentNullException.ThrowIfNull(project);

        return new PipelineStepExecution
        {
            ProjectId = project.ProjectId,
            StepName = stepName,
            StartedAt = DateTime.UtcNow,
            DurationMs = durationMs,
            IsSuccess = isSuccess,
            CreatedAt = DateTime.UtcNow,
        };
    }

    public IssueLog CreateIssue(PipelineStepExecution execution, string severity, string? code, string message)
    {
        ArgumentNullException.ThrowIfNull(execution);

        return new IssueLog
        {
            ExecutionId = execution.ExecutionId,
            LoggedAt = DateTime.UtcNow,
            Severity = severity,
            Code = code,
            Message = message,
            CreatedAt = DateTime.UtcNow,
        };
    }

    public ThreadSpeedMetric CreateMetric(
        string testDescription,
        int logicalCores,
        long singleThreadTimeMs,
        long parallelTimeMs,
        string cpuModel,
        int ramGb,
        string os)
    {
        var metric = new ThreadSpeedMetric
        {
            TestDescription = testDescription,
            CpuModel = cpuModel,
            Os = os,
            CreatedAt = DateTime.UtcNow,
        };

        typeof(ThreadSpeedMetric).GetProperty(nameof(ThreadSpeedMetric.LogicalCores))?
            .SetValue(metric, logicalCores);
        typeof(ThreadSpeedMetric).GetProperty(nameof(ThreadSpeedMetric.SingleThreadTimeMs))?
            .SetValue(metric, singleThreadTimeMs);
        typeof(ThreadSpeedMetric).GetProperty(nameof(ThreadSpeedMetric.ParallelTimeMs))?
            .SetValue(metric, parallelTimeMs);
        typeof(ThreadSpeedMetric).GetProperty(nameof(ThreadSpeedMetric.RamGb))?
            .SetValue(metric, ramGb);

        return metric;
    }
}