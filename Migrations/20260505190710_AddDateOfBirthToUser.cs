using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cleo.Migrations
{
    /// <inheritdoc />
    public partial class AddDateOfBirthToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "Users",
                type: "TEXT",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastActive",
                value: new DateTime(2026, 5, 5, 19, 7, 10, 56, DateTimeKind.Utc).AddTicks(169));

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 2,
                column: "LastActive",
                value: new DateTime(2026, 5, 5, 19, 7, 10, 56, DateTimeKind.Utc).AddTicks(172));

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 3,
                column: "LastActive",
                value: new DateTime(2026, 5, 5, 19, 7, 10, 56, DateTimeKind.Utc).AddTicks(174));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublishDate",
                value: new DateTime(2026, 5, 5, 19, 7, 10, 56, DateTimeKind.Utc).AddTicks(259));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 2,
                column: "PublishDate",
                value: new DateTime(2026, 5, 5, 19, 7, 10, 56, DateTimeKind.Utc).AddTicks(260));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 3,
                column: "PublishDate",
                value: new DateTime(2026, 5, 5, 19, 7, 10, 56, DateTimeKind.Utc).AddTicks(262));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 4,
                column: "PublishDate",
                value: new DateTime(2026, 5, 5, 19, 7, 10, 56, DateTimeKind.Utc).AddTicks(263));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastActive",
                value: new DateTime(2026, 5, 5, 18, 35, 21, 972, DateTimeKind.Utc).AddTicks(5268));

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 2,
                column: "LastActive",
                value: new DateTime(2026, 5, 5, 18, 35, 21, 972, DateTimeKind.Utc).AddTicks(5271));

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 3,
                column: "LastActive",
                value: new DateTime(2026, 5, 5, 18, 35, 21, 972, DateTimeKind.Utc).AddTicks(5272));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublishDate",
                value: new DateTime(2026, 5, 5, 18, 35, 21, 972, DateTimeKind.Utc).AddTicks(5355));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 2,
                column: "PublishDate",
                value: new DateTime(2026, 5, 5, 18, 35, 21, 972, DateTimeKind.Utc).AddTicks(5357));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 3,
                column: "PublishDate",
                value: new DateTime(2026, 5, 5, 18, 35, 21, 972, DateTimeKind.Utc).AddTicks(5358));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 4,
                column: "PublishDate",
                value: new DateTime(2026, 5, 5, 18, 35, 21, 972, DateTimeKind.Utc).AddTicks(5359));
        }
    }
}
