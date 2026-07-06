using Data;
using Factories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Repositories;

namespace Lab2;

internal class Program
{
    private static async Task Main(string[] args)
    {
        ArgumentNullException.ThrowIfNull(args);
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlite(configuration.GetConnectionString("Default"))
            .Options;

        await using var context = new ApplicationDbContext(options);
        await context.Database.MigrateAsync().ConfigureAwait(false);

        using var unitOfWork = new UnitOfWork(context);
        var factory = new DefaultDataFactory();

        Console.WriteLine("Начало тестирования репозиториев...\n");

        var project = factory.CreateProject("DemoApplication", @"C:\Projects\DemoApp");
        await unitOfWork.Projects.AddAsync(project).ConfigureAwait(false);
        Console.WriteLine($" Проект создан: {project.Name} (ID: {project.ProjectId})");

        var buildStep = factory.CreateStep(project, "dotnet build", true, 1840);
        await unitOfWork.PipelineSteps.AddAsync(buildStep).ConfigureAwait(false);
        Console.WriteLine($" Этап сборки сохранён (ID: {buildStep.ExecutionId})");

        var error = factory.CreateIssue(buildStep, "Error", "CS0246", "The type or namespace name 'XXX' could not be found");
        var warning = factory.CreateIssue(buildStep, "Warning", null, "Variable is assigned but never used");

        await unitOfWork.IssueLogs.AddAsync(error).ConfigureAwait(false);
        await unitOfWork.IssueLogs.AddAsync(warning).ConfigureAwait(false);
        Console.WriteLine($" Добавлено {buildStep.IssueLogs.Count} логов");

        var metric = factory.CreateMetric(
            "Matrix Multiplication 2000x2000",
            16,
            1250,
            340,
            "Intel Core i7-12700H",
            32,
            "Windows 11");

        await unitOfWork.Metrics.AddAsync(metric).ConfigureAwait(false);
        Console.WriteLine($"Метрика производительности сохранена. Efficiency: {metric.Efficiency:F2}");

        Console.WriteLine("\nВсё успешно сохранено в базу!");

        Console.ReadKey();
    }
}