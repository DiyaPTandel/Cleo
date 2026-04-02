using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cleo.Migrations
{
    /// <inheritdoc />
    public partial class AddAITips : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AITip",
                table: "SymptomLogs",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AITip",
                table: "MoodNotes",
                type: "TEXT",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastActive",
                value: new DateTime(2026, 4, 2, 12, 20, 14, 57, DateTimeKind.Utc).AddTicks(9417));

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 2,
                column: "LastActive",
                value: new DateTime(2026, 4, 2, 12, 20, 14, 57, DateTimeKind.Utc).AddTicks(9422));

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 3,
                column: "LastActive",
                value: new DateTime(2026, 4, 2, 12, 20, 14, 57, DateTimeKind.Utc).AddTicks(9424));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublishDate",
                value: new DateTime(2026, 4, 2, 12, 20, 14, 57, DateTimeKind.Utc).AddTicks(9539));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 2,
                column: "PublishDate",
                value: new DateTime(2026, 4, 2, 12, 20, 14, 57, DateTimeKind.Utc).AddTicks(9543));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 3,
                column: "PublishDate",
                value: new DateTime(2026, 4, 2, 12, 20, 14, 57, DateTimeKind.Utc).AddTicks(9545));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 4,
                column: "PublishDate",
                value: new DateTime(2026, 4, 2, 12, 20, 14, 57, DateTimeKind.Utc).AddTicks(9546));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AITip",
                table: "SymptomLogs");

            migrationBuilder.DropColumn(
                name: "AITip",
                table: "MoodNotes");

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastActive",
                value: new DateTime(2026, 4, 1, 17, 25, 38, 522, DateTimeKind.Utc).AddTicks(9313));

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 2,
                column: "LastActive",
                value: new DateTime(2026, 4, 1, 17, 25, 38, 522, DateTimeKind.Utc).AddTicks(9318));

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 3,
                column: "LastActive",
                value: new DateTime(2026, 4, 1, 17, 25, 38, 522, DateTimeKind.Utc).AddTicks(9319));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublishDate",
                value: new DateTime(2026, 4, 1, 17, 25, 38, 522, DateTimeKind.Utc).AddTicks(9425));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 2,
                column: "PublishDate",
                value: new DateTime(2026, 4, 1, 17, 25, 38, 522, DateTimeKind.Utc).AddTicks(9427));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 3,
                column: "PublishDate",
                value: new DateTime(2026, 4, 1, 17, 25, 38, 522, DateTimeKind.Utc).AddTicks(9428));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 4,
                column: "PublishDate",
                value: new DateTime(2026, 4, 1, 17, 25, 38, 522, DateTimeKind.Utc).AddTicks(9429));
        }
    }
}
