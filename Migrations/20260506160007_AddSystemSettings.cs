using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cleo.Migrations
{
    /// <inheritdoc />
    public partial class AddSystemSettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SystemSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SiteName = table.Column<string>(type: "TEXT", nullable: false),
                    SupportEmail = table.Column<string>(type: "TEXT", nullable: false),
                    ContactNo = table.Column<string>(type: "TEXT", nullable: false),
                    AllowNewRegistrations = table.Column<bool>(type: "INTEGER", nullable: false),
                    SessionTimeout = table.Column<bool>(type: "INTEGER", nullable: false),
                    DefaultCycleLength = table.Column<int>(type: "INTEGER", nullable: false),
                    EnableDiscussionDisplay = table.Column<bool>(type: "INTEGER", nullable: false),
                    ShowTimerDashboard = table.Column<bool>(type: "INTEGER", nullable: false),
                    ShowCycleSummaryCard = table.Column<bool>(type: "INTEGER", nullable: false),
                    RestrictAdminAccess = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemSettings", x => x.Id);
                });

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

            migrationBuilder.InsertData(
                table: "SystemSettings",
                columns: new[] { "Id", "AllowNewRegistrations", "ContactNo", "DefaultCycleLength", "EnableDiscussionDisplay", "RestrictAdminAccess", "SessionTimeout", "ShowCycleSummaryCard", "ShowTimerDashboard", "SiteName", "SupportEmail" },
                values: new object[] { 1, true, "+91 1234567890", 28, true, false, true, false, false, "Cleo", "support@cleo.com" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SystemSettings");

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastActive",
                value: new DateTime(2026, 5, 5, 19, 17, 26, 531, DateTimeKind.Utc).AddTicks(6757));

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 2,
                column: "LastActive",
                value: new DateTime(2026, 5, 5, 19, 17, 26, 531, DateTimeKind.Utc).AddTicks(6798));

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 3,
                column: "LastActive",
                value: new DateTime(2026, 5, 5, 19, 17, 26, 531, DateTimeKind.Utc).AddTicks(6800));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublishDate",
                value: new DateTime(2026, 5, 5, 19, 17, 26, 531, DateTimeKind.Utc).AddTicks(6887));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 2,
                column: "PublishDate",
                value: new DateTime(2026, 5, 5, 19, 17, 26, 531, DateTimeKind.Utc).AddTicks(6890));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 3,
                column: "PublishDate",
                value: new DateTime(2026, 5, 5, 19, 17, 26, 531, DateTimeKind.Utc).AddTicks(6891));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 4,
                column: "PublishDate",
                value: new DateTime(2026, 5, 5, 19, 17, 26, 531, DateTimeKind.Utc).AddTicks(6892));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 5,
                column: "PublishDate",
                value: new DateTime(2026, 5, 5, 19, 17, 26, 531, DateTimeKind.Utc).AddTicks(6893));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 6,
                column: "PublishDate",
                value: new DateTime(2026, 5, 5, 19, 17, 26, 531, DateTimeKind.Utc).AddTicks(6894));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 7,
                column: "PublishDate",
                value: new DateTime(2026, 5, 5, 19, 17, 26, 531, DateTimeKind.Utc).AddTicks(6895));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 8,
                column: "PublishDate",
                value: new DateTime(2026, 5, 5, 19, 17, 26, 531, DateTimeKind.Utc).AddTicks(6896));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 9,
                column: "PublishDate",
                value: new DateTime(2026, 5, 5, 19, 17, 26, 531, DateTimeKind.Utc).AddTicks(6896));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 10,
                column: "PublishDate",
                value: new DateTime(2026, 5, 5, 19, 17, 26, 531, DateTimeKind.Utc).AddTicks(6897));
        }
    }
}
