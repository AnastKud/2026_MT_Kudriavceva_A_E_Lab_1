using Entities;

namespace Reports;

public interface IReport
{
    void Show(List<ThreadSpeedMetric> data);
}