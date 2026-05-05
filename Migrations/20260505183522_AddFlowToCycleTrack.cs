using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cleo.Migrations
{
    /// <inheritdoc />
    public partial class AddFlowToCycleTrack : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WaitlistEntries");

            migrationBuilder.AddColumn<string>(
                name: "Flow",
                table: "CycleTracks",
                type: "TEXT",
                nullable: true);

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
                columns: new[] { "Email", "LastActive", "Name" },
                values: new object[] { "diya@cleo.app", new DateTime(2026, 5, 5, 18, 35, 21, 972, DateTimeKind.Utc).AddTicks(5271), "Diya" });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Flow",
                table: "CycleTracks");

            migrationBuilder.CreateTable(
                name: "WaitlistEntries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    SignupDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WaitlistEntries", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastActive",
                value: new DateTime(2026, 4, 7, 5, 20, 13, 509, DateTimeKind.Utc).AddTicks(9656));

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Email", "LastActive", "Name" },
                values: new object[] { "ava@cleo.app", new DateTime(2026, 4, 7, 5, 20, 13, 509, DateTimeKind.Utc).AddTicks(9660), "Ava" });

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 3,
                column: "LastActive",
                value: new DateTime(2026, 4, 7, 5, 20, 13, 509, DateTimeKind.Utc).AddTicks(9662));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublishDate",
                value: new DateTime(2026, 4, 7, 5, 20, 13, 509, DateTimeKind.Utc).AddTicks(9780));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 2,
                column: "PublishDate",
                value: new DateTime(2026, 4, 7, 5, 20, 13, 509, DateTimeKind.Utc).AddTicks(9784));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 3,
                column: "PublishDate",
                value: new DateTime(2026, 4, 7, 5, 20, 13, 509, DateTimeKind.Utc).AddTicks(9786));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 4,
                column: "PublishDate",
                value: new DateTime(2026, 4, 7, 5, 20, 13, 509, DateTimeKind.Utc).AddTicks(9788));
        }
    }
}
