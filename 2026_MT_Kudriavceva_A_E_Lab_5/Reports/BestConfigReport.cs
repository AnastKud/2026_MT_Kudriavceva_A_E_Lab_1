using System.Collections.ObjectModel;
using Entities;
using Spectre.Console;

namespace Reports;

public class BestConfigReport : IReport
{
    void IReport.Show(List<ThreadSpeedMetric> data)
    {
        ArgumentNullException.ThrowIfNull(data);
        var best = data.OrderBy(m => m.Efficiency).FirstOrDefault();
        if (best != null)
        {
            AnsiConsole.MarkupLine($"[bold green]Лучшая конфигурация:[/] {best.TestDescription} на {best.CpuModel}");
            AnsiConsole.MarkupLine($"Efficiency = [green]{best.Efficiency:F2}[/]");
        }
    }

    protected void Show(Collection<ThreadSpeedMetric> data)
    {
        ((IReport)this).Show([.. data]);
    }
}