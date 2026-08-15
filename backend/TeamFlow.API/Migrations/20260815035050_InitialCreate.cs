using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TeamFlow.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    TaskId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    AssigneeName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Priority = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    DueDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.TaskId);
                });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "TaskId", "AssigneeName", "CreatedAt", "DueDate", "Priority", "Status", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "Sarah Connor", new DateTime(2026, 8, 13, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 8, 17, 0, 0, 0, 0, DateTimeKind.Utc), "High", "In Progress", "Design homepage wireframes & layout", new DateTime(2026, 8, 14, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, "John Doe", new DateTime(2026, 8, 13, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 8, 19, 0, 0, 0, 0, DateTimeKind.Utc), "High", "To Do", "Implement task controller REST API", new DateTime(2026, 8, 13, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 3, "Alex Rivera", new DateTime(2026, 8, 12, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 8, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Medium", "Done", "Create PostgreSQL database schema & migrations", new DateTime(2026, 8, 14, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 4, "Emily Chen", new DateTime(2026, 8, 14, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 8, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Medium", "To Do", "Test Swagger API endpoints & frontend integration", new DateTime(2026, 8, 14, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 5, "Michael Scott", new DateTime(2026, 8, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 8, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Low", "To Do", "Prepare SE3090 project documentation & presentation", new DateTime(2026, 8, 15, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_AssigneeName",
                table: "Tasks",
                column: "AssigneeName");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_Status",
                table: "Tasks",
                column: "Status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");
        }
    }
}
