using System.Globalization;
using Entities;
using Spectre.Console;

namespace Reports;

public class TopMethodsReport : IReport
{
    public void Show(List<ThreadSpeedMetric> data)
    {
        var top = data
            .Where(m => m.TestDescription.Contains("2000", StringComparison.Ordinal))
            .OrderBy(m => m.ParallelTimeMs)
            .Take(3)
            .ToList();

        var table = new Table().Title("Топ-3 самых быстрых методов (2000×2000)").Border(TableBorder.Rounded);
        table.AddColumns("Описание", "CPU", "Parallel (мс)", "Efficiency");
        foreach (var m in top)
        {
            table.AddRow(
                m.TestDescription,
                m.CpuModel,
                m.ParallelTimeMs.ToString(CultureInfo.InvariantCulture),
                $"[green]{m.Efficiency:F2}[/]");
        }

        AnsiConsole.Write(table);
    }
}