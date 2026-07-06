using System.Globalization;
using Entities;
using Spectre.Console;

namespace Reports;

public class AnomaliesReport : IReport
{
    public void Show(List<ThreadSpeedMetric> data)
    {
        var anomalies = data
            .Where(m => m.ParallelTimeMs > m.SingleThreadTimeMs * 1.1m)
            .ToList();

        if (anomalies.Count == 0)
        {
            AnsiConsole.MarkupLine("[green]Аномалий (parallel медленнее sequential) не обнаружено.[/]");
            return;
        }

        var table = new Table().Title("Аномалии параллелизма").Border(TableBorder.Rounded);
        table.AddColumns("Описание", "Single (мс)", "Parallel (мс)", "Overhead");
        foreach (var m in anomalies)
        {
            table.AddRow(
                m.TestDescription,
                m.SingleThreadTimeMs.ToString(CultureInfo.InvariantCulture),
                m.ParallelTimeMs.ToString(CultureInfo.InvariantCulture),
                (m.ParallelTimeMs - m.SingleThreadTimeMs).ToString(CultureInfo.InvariantCulture));
        }

        AnsiConsole.Write(table);
    }
}