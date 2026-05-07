using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cleo.Migrations
{
    /// <inheritdoc />
    public partial class AddTotalMinutesSpentToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "TotalMinutesSpent",
                table: "Users",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastActive",
                value: new DateTime(2026, 5, 6, 18, 24, 34, 296, DateTimeKind.Utc).AddTicks(8510));

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 2,
                column: "LastActive",
                value: new DateTime(2026, 5, 6, 18, 24, 34, 296, DateTimeKind.Utc).AddTicks(8517));

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 3,
                column: "LastActive",
                value: new DateTime(2026, 5, 6, 18, 24, 34, 296, DateTimeKind.Utc).AddTicks(8518));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublishDate",
                value: new DateTime(2026, 5, 6, 18, 24, 34, 296, DateTimeKind.Utc).AddTicks(8651));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 2,
                column: "PublishDate",
                value: new DateTime(2026, 5, 6, 18, 24, 34, 296, DateTimeKind.Utc).AddTicks(8657));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 3,
                column: "PublishDate",
                value: new DateTime(2026, 5, 6, 18, 24, 34, 296, DateTimeKind.Utc).AddTicks(8658));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 4,
                column: "PublishDate",
                value: new DateTime(2026, 5, 6, 18, 24, 34, 296, DateTimeKind.Utc).AddTicks(8659));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 5,
                column: "PublishDate",
                value: new DateTime(2026, 5, 6, 18, 24, 34, 296, DateTimeKind.Utc).AddTicks(8660));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 6,
                column: "PublishDate",
                value: new DateTime(2026, 5, 6, 18, 24, 34, 296, DateTimeKind.Utc).AddTicks(8661));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 7,
                column: "PublishDate",
                value: new DateTime(2026, 5, 6, 18, 24, 34, 296, DateTimeKind.Utc).AddTicks(8662));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 8,
                column: "PublishDate",
                value: new DateTime(2026, 5, 6, 18, 24, 34, 296, DateTimeKind.Utc).AddTicks(8662));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 9,
                column: "PublishDate",
                value: new DateTime(2026, 5, 6, 18, 24, 34, 296, DateTimeKind.Utc).AddTicks(8663));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 10,
                column: "PublishDate",
                value: new DateTime(2026, 5, 6, 18, 24, 34, 296, DateTimeKind.Utc).AddTicks(8664));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalMinutesSpent",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastActive",
                value: new DateTime(2026, 5, 6, 16, 45, 27, 657, DateTimeKind.Utc).AddTicks(1292));

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 2,
                column: "LastActive",
                value: new DateTime(2026, 5, 6, 16, 45, 27, 657, DateTimeKind.Utc).AddTicks(1296));

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 3,
                column: "LastActive",
                value: new DateTime(2026, 5, 6, 16, 45, 27, 657, DateTimeKind.Utc).AddTicks(1297));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublishDate",
                value: new DateTime(2026, 5, 6, 16, 45, 27, 657, DateTimeKind.Utc).AddTicks(1399));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 2,
                column: "PublishDate",
                value: new DateTime(2026, 5, 6, 16, 45, 27, 657, DateTimeKind.Utc).AddTicks(1401));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 3,
                column: "PublishDate",
                value: new DateTime(2026, 5, 6, 16, 45, 27, 657, DateTimeKind.Utc).AddTicks(1403));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 4,
                column: "PublishDate",
                value: new DateTime(2026, 5, 6, 16, 45, 27, 657, DateTimeKind.Utc).AddTicks(1403));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 5,
                column: "PublishDate",
                value: new DateTime(2026, 5, 6, 16, 45, 27, 657, DateTimeKind.Utc).AddTicks(1404));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 6,
                column: "PublishDate",
                value: new DateTime(2026, 5, 6, 16, 45, 27, 657, DateTimeKind.Utc).AddTicks(1405));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 7,
                column: "PublishDate",
                value: new DateTime(2026, 5, 6, 16, 45, 27, 657, DateTimeKind.Utc).AddTicks(1406));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 8,
                column: "PublishDate",
                value: new DateTime(2026, 5, 6, 16, 45, 27, 657, DateTimeKind.Utc).AddTicks(1411));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 9,
                column: "PublishDate",
                value: new DateTime(2026, 5, 6, 16, 45, 27, 657, DateTimeKind.Utc).AddTicks(1411));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 10,
                column: "PublishDate",
                value: new DateTime(2026, 5, 6, 16, 45, 27, 657, DateTimeKind.Utc).AddTicks(1412));
        }
    }
}
