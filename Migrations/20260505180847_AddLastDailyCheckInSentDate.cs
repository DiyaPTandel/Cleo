using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cleo.Migrations
{
    /// <inheritdoc />
    public partial class AddLastDailyCheckInSentDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastDailyCheckInSentDate",
                table: "NotificationSettings",
                type: "TEXT",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastActive",
                value: new DateTime(2026, 5, 5, 18, 8, 47, 397, DateTimeKind.Utc).AddTicks(8152));

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 2,
                column: "LastActive",
                value: new DateTime(2026, 5, 5, 18, 8, 47, 397, DateTimeKind.Utc).AddTicks(8157));

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 3,
                column: "LastActive",
                value: new DateTime(2026, 5, 5, 18, 8, 47, 397, DateTimeKind.Utc).AddTicks(8158));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublishDate",
                value: new DateTime(2026, 5, 5, 18, 8, 47, 397, DateTimeKind.Utc).AddTicks(8274));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 2,
                column: "PublishDate",
                value: new DateTime(2026, 5, 5, 18, 8, 47, 397, DateTimeKind.Utc).AddTicks(8278));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 3,
                column: "PublishDate",
                value: new DateTime(2026, 5, 5, 18, 8, 47, 397, DateTimeKind.Utc).AddTicks(8279));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 4,
                column: "PublishDate",
                value: new DateTime(2026, 5, 5, 18, 8, 47, 397, DateTimeKind.Utc).AddTicks(8280));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastDailyCheckInSentDate",
                table: "NotificationSettings");

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
    }
}
