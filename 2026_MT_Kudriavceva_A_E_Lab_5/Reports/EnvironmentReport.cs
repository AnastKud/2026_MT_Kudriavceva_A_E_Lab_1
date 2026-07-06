using System.Globalization;
using Entities;
using Spectre.Console;

namespace Reports;

public class EnvironmentReport : IReport
{
    public void Show(List<ThreadSpeedMetric> data)
    {
        var groups = data.GroupBy(m => m.CpuModel)
            .Select(g => new
            {
                Cpu = g.Key,
                AvgTime = g.Average(m => m.ParallelTimeMs),
                Count = g.Count(),
            })
            .OrderBy(x => x.AvgTime);

        var table = new Table().Title("Сравнение окружений").Border(TableBorder.Rounded);
        table.AddColumns("Процессор", "Среднее Parallel (мс)", "Кол-во тестов");
        foreach (var g in groups)
        {
            table.AddRow(
                g.Cpu,
                g.AvgTime.ToString("F0", CultureInfo.InvariantCulture),
                g.Count.ToString(CultureInfo.InvariantCulture));
        }

        AnsiConsole.Write(table);
    }
}