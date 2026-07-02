namespace Factories;

using Entities;

public interface IDataFactory
{
    Project CreateProject(string name, string folderPath);
    PipelineStepExecution CreateStep(Project project, string stepName, bool isSuccess, long durationMs);
    IssueLog CreateIssue(PipelineStepExecution execution, string severity, string? code, string message);
    ThreadSpeedMetric CreateMetric(
        string testDescription,
        int logicalCores,
        long singleThreadTimeMs,
        long parallelTimeMs,
        string cpuModel,
        int ramGb,
        string os);
}