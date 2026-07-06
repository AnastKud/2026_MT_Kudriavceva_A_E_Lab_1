using Entities;
using Reports;
using Repositories;
using Spectre.Console;

namespace Lab5;

public class AnalyticsService(IUnitOfWork uow)
{
    private readonly IUnitOfWork uow = uow;

    public static void ShowReport(string choice, System.Collections.ObjectModel.ReadOnlyCollection<ThreadSpeedMetric> data)
    {
        ArgumentNullException.ThrowIfNull(data);
        ArgumentNullException.ThrowIfNull(choice);

        if (data.Count == 0)
        {
            AnsiConsole.MarkupLine("[red]Нет данных в базе.[/]");
            return;
        }

        var dataList = data.ToList();

        switch (choice[0])
        {
            case '1': new TopMethodsReport().Show(dataList); break;
            case '2': new ParallelEffectReport().Show(dataList); break;
            case '3': new AnomaliesReport().Show(dataList); break;
            case '4': new EnvironmentReport().Show(dataList); break;
            case '5': ((IReport)new BestConfigReport()).Show(dataList); break;
            case '6': ((IReport)new SizeImpactReport()).Show(dataList); break;
        }
    }

    public async Task<List<ThreadSpeedMetric>> GetAllMetricsAsync()
    {
        try
        {
            return
                [.. await this.uow.Metrics.GetAllAsync().ConfigureAwait(false)];
        }
        catch (Exception ex) when (ex.Message.Contains("no such table", StringComparison.OrdinalIgnoreCase))
        {
            AnsiConsole.MarkupLine("[yellow]Таблица ThreadSpeedMetrics ещё не создана. Запусти тесты из Lab 3.[/]");
            return
                [];
        }
    }
}