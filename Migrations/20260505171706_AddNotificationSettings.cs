using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cleo.Migrations
{
    /// <inheritdoc />
    public partial class AddNotificationSettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NotificationSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    PeriodApproachingEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    PeriodApproachingDays = table.Column<int>(type: "INTEGER", nullable: false),
                    OvulationWindowEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    NewCycleSummaryEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    DailyCheckInEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    DefaultReminderTime = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationSettings", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastActive",
                value: new DateTime(2026, 5, 5, 17, 17, 6, 197, DateTimeKind.Utc).AddTicks(904));

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 2,
                column: "LastActive",
                value: new DateTime(2026, 5, 5, 17, 17, 6, 197, DateTimeKind.Utc).AddTicks(909));

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 3,
                column: "LastActive",
                value: new DateTime(2026, 5, 5, 17, 17, 6, 197, DateTimeKind.Utc).AddTicks(910));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublishDate",
                value: new DateTime(2026, 5, 5, 17, 17, 6, 197, DateTimeKind.Utc).AddTicks(1044));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 2,
                column: "PublishDate",
                value: new DateTime(2026, 5, 5, 17, 17, 6, 197, DateTimeKind.Utc).AddTicks(1049));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 3,
                column: "PublishDate",
                value: new DateTime(2026, 5, 5, 17, 17, 6, 197, DateTimeKind.Utc).AddTicks(1051));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 4,
                column: "PublishDate",
                value: new DateTime(2026, 5, 5, 17, 17, 6, 197, DateTimeKind.Utc).AddTicks(1052));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotificationSettings");

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastActive",
                value: new DateTime(2026, 4, 6, 13, 47, 55, 333, DateTimeKind.Utc).AddTicks(5872));

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 2,
                column: "LastActive",
                value: new DateTime(2026, 4, 6, 13, 47, 55, 333, DateTimeKind.Utc).AddTicks(5877));

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 3,
                column: "LastActive",
                value: new DateTime(2026, 4, 6, 13, 47, 55, 333, DateTimeKind.Utc).AddTicks(5879));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublishDate",
                value: new DateTime(2026, 4, 6, 13, 47, 55, 333, DateTimeKind.Utc).AddTicks(5976));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 2,
                column: "PublishDate",
                value: new DateTime(2026, 4, 6, 13, 47, 55, 333, DateTimeKind.Utc).AddTicks(5979));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 3,
                column: "PublishDate",
                value: new DateTime(2026, 4, 6, 13, 47, 55, 333, DateTimeKind.Utc).AddTicks(5980));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 4,
                column: "PublishDate",
                value: new DateTime(2026, 4, 6, 13, 47, 55, 333, DateTimeKind.Utc).AddTicks(5982));
        }
    }
}
