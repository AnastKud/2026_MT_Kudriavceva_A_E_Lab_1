using System.Collections.ObjectModel;
using System.Globalization;
using Entities;
using Spectre.Console;

namespace Reports;

public class SizeImpactReport : IReport
{
    public void Show(Collection<ThreadSpeedMetric> data)
    {
        ((IReport)this).Show([.. data]);
    }

    void IReport.Show(List<ThreadSpeedMetric> data)
    {
        AnsiConsole.MarkupLine("[yellow]Анализ влияния размера матрицы (группировка по TestDescription)[/]");

        var sizes = data.GroupBy(m => m.TestDescription)
            .Select(g => new { Size = g.Key, AvgTime = g.Average(m => m.ParallelTimeMs) })
            .OrderBy(x => x.AvgTime)
            .Take(6);

        var table = new Table().Title("Влияние размера матрицы").Border(TableBorder.Rounded);
        table.AddColumns("Описание теста", "Среднее Parallel (мс)");
        foreach (var item in sizes)
        {
            table.AddRow(item.Size, item.AvgTime.ToString("F0", CultureInfo.InvariantCulture));
        }

        AnsiConsole.Write(table);
    }
}