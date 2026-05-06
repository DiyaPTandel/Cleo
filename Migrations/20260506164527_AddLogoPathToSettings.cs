using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cleo.Migrations
{
    /// <inheritdoc />
    public partial class AddLogoPathToSettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LogoPath",
                table: "SystemSettings",
                type: "TEXT",
                nullable: true);

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

            migrationBuilder.UpdateData(
                table: "SystemSettings",
                keyColumn: "Id",
                keyValue: 1,
                column: "LogoPath",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LogoPath",
                table: "SystemSettings");

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastActive",
                value: new DateTime(2026, 5, 6, 16, 0, 7, 421, DateTimeKind.Utc).AddTicks(9733));

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 2,
                column: "LastActive",
                value: new DateTime(2026, 5, 6, 16, 0, 7, 421, DateTimeKind.Utc).AddTicks(9736));

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 3,
                column: "LastActive",
                value: new DateTime(2026, 5, 6, 16, 0, 7, 421, DateTimeKind.Utc).AddTicks(9737));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublishDate",
                value: new DateTime(2026, 5, 6, 16, 0, 7, 421, DateTimeKind.Utc).AddTicks(9851));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 2,
                column: "PublishDate",
                value: new DateTime(2026, 5, 6, 16, 0, 7, 421, DateTimeKind.Utc).AddTicks(9854));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 3,
                column: "PublishDate",
                value: new DateTime(2026, 5, 6, 16, 0, 7, 421, DateTimeKind.Utc).AddTicks(9855));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 4,
                column: "PublishDate",
                value: new DateTime(2026, 5, 6, 16, 0, 7, 421, DateTimeKind.Utc).AddTicks(9856));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 5,
                column: "PublishDate",
                value: new DateTime(2026, 5, 6, 16, 0, 7, 421, DateTimeKind.Utc).AddTicks(9857));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 6,
                column: "PublishDate",
                value: new DateTime(2026, 5, 6, 16, 0, 7, 421, DateTimeKind.Utc).AddTicks(9858));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 7,
                column: "PublishDate",
                value: new DateTime(2026, 5, 6, 16, 0, 7, 421, DateTimeKind.Utc).AddTicks(9859));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 8,
                column: "PublishDate",
                value: new DateTime(2026, 5, 6, 16, 0, 7, 421, DateTimeKind.Utc).AddTicks(9860));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 9,
                column: "PublishDate",
                value: new DateTime(2026, 5, 6, 16, 0, 7, 421, DateTimeKind.Utc).AddTicks(9861));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 10,
                column: "PublishDate",
                value: new DateTime(2026, 5, 6, 16, 0, 7, 421, DateTimeKind.Utc).AddTicks(9862));
        }
    }
}
