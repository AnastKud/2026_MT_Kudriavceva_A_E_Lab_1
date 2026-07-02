using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _2026_MT_Kudriavceva_A_E_Lab_2.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    FolderPath = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.ProjectId);
                });

            migrationBuilder.CreateTable(
                name: "ThreadSpeedMetrics",
                columns: table => new
                {
                    MetricId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TestDescription = table.Column<string>(type: "TEXT", maxLength: 300, nullable: false),
                    LogicalCores = table.Column<int>(type: "INTEGER", nullable: false),
                    SingleThreadTimeMs = table.Column<long>(type: "INTEGER", nullable: false),
                    ParallelTimeMs = table.Column<long>(type: "INTEGER", nullable: false),
                    MeasuredAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CpuModel = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    RamGb = table.Column<int>(type: "INTEGER", nullable: false),
                    Os = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThreadSpeedMetrics", x => x.MetricId);
                });

            migrationBuilder.CreateTable(
                name: "PipelineStepExecutions",
                columns: table => new
                {
                    ExecutionId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProjectId = table.Column<int>(type: "INTEGER", nullable: false),
                    StepName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    StartedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DurationMs = table.Column<long>(type: "INTEGER", nullable: false),
                    IsSuccess = table.Column<bool>(type: "INTEGER", nullable: false),
                    TotalErrors = table.Column<int>(type: "INTEGER", nullable: false),
                    TotalWarnings = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PipelineStepExecutions", x => x.ExecutionId);
                    table.ForeignKey(
                        name: "FK_PipelineStepExecutions_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IssueLogs",
                columns: table => new
                {
                    LogId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ExecutionId = table.Column<int>(type: "INTEGER", nullable: false),
                    LoggedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Severity = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Code = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Message = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueLogs", x => x.LogId);
                    table.ForeignKey(
                        name: "FK_IssueLogs_PipelineStepExecutions_ExecutionId",
                        column: x => x.ExecutionId,
                        principalTable: "PipelineStepExecutions",
                        principalColumn: "ExecutionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IssueLogs_ExecutionId",
                table: "IssueLogs",
                column: "ExecutionId");

            migrationBuilder.CreateIndex(
                name: "IX_PipelineStepExecutions_ProjectId",
                table: "PipelineStepExecutions",
                column: "ProjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IssueLogs");

            migrationBuilder.DropTable(
                name: "ThreadSpeedMetrics");

            migrationBuilder.DropTable(
                name: "PipelineStepExecutions");

            migrationBuilder.DropTable(
                name: "Projects");
        }
    }
}
