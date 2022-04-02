using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MxWork.ElsaWf.WebSqlite.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WorkflowDefinitionVersions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    VersionId = table.Column<string>(type: "TEXT", nullable: true),
                    DefinitionId = table.Column<string>(type: "TEXT", nullable: true),
                    Version = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Variables = table.Column<string>(type: "TEXT", nullable: true),
                    IsSingleton = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsDisabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsPublished = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsLatest = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkflowDefinitionVersions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkflowInstances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    InstanceId = table.Column<string>(type: "TEXT", nullable: true),
                    DefinitionId = table.Column<string>(type: "TEXT", nullable: true),
                    Version = table.Column<int>(type: "INTEGER", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false),
                    CorrelationId = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    StartedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    FinishedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    FaultedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    AbortedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Scope = table.Column<string>(type: "TEXT", nullable: true),
                    Input = table.Column<string>(type: "TEXT", nullable: true),
                    ExecutionLog = table.Column<string>(type: "TEXT", nullable: true),
                    Fault = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkflowInstances", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ActivityDefinitions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ActivityId = table.Column<string>(type: "TEXT", nullable: true),
                    WorkflowDefinitionVersionId = table.Column<int>(type: "INTEGER", nullable: true),
                    Type = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    DisplayName = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Left = table.Column<int>(type: "INTEGER", nullable: false),
                    Top = table.Column<int>(type: "INTEGER", nullable: false),
                    State = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityDefinitions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActivityDefinitions_WorkflowDefinitionVersions_WorkflowDefinitionVersionId",
                        column: x => x.WorkflowDefinitionVersionId,
                        principalTable: "WorkflowDefinitionVersions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ConnectionDefinitions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    WorkflowDefinitionVersionId = table.Column<int>(type: "INTEGER", nullable: true),
                    SourceActivityId = table.Column<string>(type: "TEXT", nullable: true),
                    DestinationActivityId = table.Column<string>(type: "TEXT", nullable: true),
                    Outcome = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConnectionDefinitions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConnectionDefinitions_WorkflowDefinitionVersions_WorkflowDefinitionVersionId",
                        column: x => x.WorkflowDefinitionVersionId,
                        principalTable: "WorkflowDefinitionVersions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ActivityInstances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ActivityId = table.Column<string>(type: "TEXT", nullable: true),
                    WorkflowInstanceId = table.Column<int>(type: "INTEGER", nullable: true),
                    Type = table.Column<string>(type: "TEXT", nullable: true),
                    State = table.Column<string>(type: "TEXT", nullable: true),
                    Output = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityInstances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActivityInstances_WorkflowInstances_WorkflowInstanceId",
                        column: x => x.WorkflowInstanceId,
                        principalTable: "WorkflowInstances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BlockingActivities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    WorkflowInstanceId = table.Column<int>(type: "INTEGER", nullable: true),
                    ActivityId = table.Column<string>(type: "TEXT", nullable: true),
                    ActivityType = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlockingActivities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlockingActivities_WorkflowInstances_WorkflowInstanceId",
                        column: x => x.WorkflowInstanceId,
                        principalTable: "WorkflowInstances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActivityDefinitions_WorkflowDefinitionVersionId",
                table: "ActivityDefinitions",
                column: "WorkflowDefinitionVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityInstances_WorkflowInstanceId",
                table: "ActivityInstances",
                column: "WorkflowInstanceId");

            migrationBuilder.CreateIndex(
                name: "IX_BlockingActivities_WorkflowInstanceId",
                table: "BlockingActivities",
                column: "WorkflowInstanceId");

            migrationBuilder.CreateIndex(
                name: "IX_ConnectionDefinitions_WorkflowDefinitionVersionId",
                table: "ConnectionDefinitions",
                column: "WorkflowDefinitionVersionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityDefinitions");

            migrationBuilder.DropTable(
                name: "ActivityInstances");

            migrationBuilder.DropTable(
                name: "BlockingActivities");

            migrationBuilder.DropTable(
                name: "ConnectionDefinitions");

            migrationBuilder.DropTable(
                name: "WorkflowInstances");

            migrationBuilder.DropTable(
                name: "WorkflowDefinitionVersions");
        }
    }
}
