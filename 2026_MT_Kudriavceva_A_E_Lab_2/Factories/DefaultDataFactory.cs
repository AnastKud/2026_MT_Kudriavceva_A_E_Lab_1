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
            CreatedAt = DateTime.UtcNow
        };
    }

    public PipelineStepExecution CreateStep(Project project, string stepName, bool isSuccess, long durationMs)
    {
        return new PipelineStepExecution
        {
            ProjectId = project.ProjectId,
            StepName = stepName,
            StartedAt = DateTime.UtcNow,
            DurationMs = durationMs,
            IsSuccess = isSuccess,
            CreatedAt = DateTime.UtcNow
        };
    }

    public IssueLog CreateIssue(PipelineStepExecution execution, string severity, string? code, string message)
    {
        return new IssueLog
        {
            ExecutionId = execution.ExecutionId,
            LoggedAt = DateTime.UtcNow,
            Severity = severity,
            Code = code,
            Message = message,
            CreatedAt = DateTime.UtcNow
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
        return new ThreadSpeedMetric
        {
            TestDescription = testDescription,
            LogicalCores = logicalCores,
            SingleThreadTimeMs = singleThreadTimeMs,
            ParallelTimeMs = parallelTimeMs,
            CpuModel = cpuModel,
            RamGb = ramGb,
            Os = os,
            MeasuredAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow
        };
    }
}