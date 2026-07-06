using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Repositories;
using Spectre.Console;

namespace Lab5;

internal class Program
{
    private static async Task Main()
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlite(config.GetConnectionString("Default"))
            .Options;

        await using var context = new ApplicationDbContext(options);
        await context.Database.MigrateAsync().ConfigureAwait(false);

        using var uow = new UnitOfWork(context);
        var analytics = new AnalyticsService(uow);

        AnsiConsole.MarkupLine("[bold green]=== Анализ производительности матриц ===[/]\n");

        while (true)
        {
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Выберите отчет:")
                    .PageSize(10)
                    .AddChoices(
                        "1. Топ-3 самых быстрых методов (2000×2000)",
                        "2. Эффект параллелизма",
                        "3. Аномалии параллелизма",
                        "4. Сравнение окружений",
                        "5. Лучшие конфигурации",
                        "6. Влияние размера матрицы",
                        "7. Выход"));

            if (choice.Contains("Выход", StringComparison.Ordinal))
            {
                break;
            }

            var metrics = await analytics.GetAllMetricsAsync().ConfigureAwait(false);

            await AnsiConsole.Status()
                .Spinner(Spinner.Known.Dots)
                .StartAsync("Обработка...", _ => Task.CompletedTask).ConfigureAwait(false);

            AnalyticsService.ShowReport(choice, metrics.AsReadOnly());

            AnsiConsole.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }
    }
}