using Entities;
using Spectre.Console;

namespace Reports;

public class ParallelEffectReport : IReport
{
    public void Show(List<ThreadSpeedMetric> data)
    {
        var avg = data
            .Where(m => m.SingleThreadTimeMs > 0)
            .Average(m => (decimal)m.SingleThreadTimeMs / m.ParallelTimeMs);

        AnsiConsole.MarkupLine($"[bold]Средний коэффициент ускорения параллелизма:[/] [green]{avg:F2}x[/]");
    }
}