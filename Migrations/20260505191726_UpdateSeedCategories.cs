using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace cleo.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeedCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                columns: new[] { "Category", "Content", "PublishDate" },
                values: new object[] { "Quick-Nutrition", "During your period, your body loses iron. Focus on leafy greens, lentils, and lean meats to maintain energy levels.", new DateTime(2026, 5, 5, 19, 17, 26, 531, DateTimeKind.Utc).AddTicks(6887) });

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Content", "PublishDate" },
                values: new object[] { "Gentle yoga poses like Child's Pose and Cat-Cow can help relax pelvic muscles and reduce menstrual cramping.", new DateTime(2026, 5, 5, 19, 17, 26, 531, DateTimeKind.Utc).AddTicks(6890) });

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Category", "Content", "PublishDate" },
                values: new object[] { "Wellness", "The Luteinizing Hormone (LH) surge triggers ovulation. Tracking this can help you identify your most fertile days.", new DateTime(2026, 5, 5, 19, 17, 26, 531, DateTimeKind.Utc).AddTicks(6891) });

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Category", "Content", "PublishDate" },
                values: new object[] { "PMS-Relief", "Reduce salt intake and stay hydrated to combat hormonal water retention during the luteal phase.", new DateTime(2026, 5, 5, 19, 17, 26, 531, DateTimeKind.Utc).AddTicks(6892) });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "Category", "Content", "PublishDate", "Status", "Title", "Views" },
                values: new object[,]
                {
                    { 5, "Quick-Nutrition", "Magnesium-rich foods like dark chocolate and pumpkin seeds can help stabilize mood swings and reduce anxiety.", new DateTime(2026, 5, 5, 19, 17, 26, 531, DateTimeKind.Utc).AddTicks(6893), "Published", "Magnesium for Mood", 620 },
                    { 6, "Exercise", "A 20-minute brisk walk increases blood flow and releases endorphins, which can alleviate low-level period pain.", new DateTime(2026, 5, 5, 19, 17, 26, 531, DateTimeKind.Utc).AddTicks(6894), "Published", "The Power of Walking", 750 },
                    { 7, "Wellness", "Estrogen rises during this phase, often leading to increased energy and improved cognitive focus. It's a great time for new projects.", new DateTime(2026, 5, 5, 19, 17, 26, 531, DateTimeKind.Utc).AddTicks(6895), "Published", "The Follicular Phase", 1100 },
                    { 8, "Hygiene", "Your body temperature changes throughout your cycle, which can affect sleep. Keep your room cool during the luteal phase.", new DateTime(2026, 5, 5, 19, 17, 26, 531, DateTimeKind.Utc).AddTicks(6896), "Published", "Sleep Hygiene & Cycles", 430 },
                    { 9, "Quick-Nutrition", "Raspberry leaf and ginger teas are known for their ability to soothe the uterus and reduce inflammation.", new DateTime(2026, 5, 5, 19, 17, 26, 531, DateTimeKind.Utc).AddTicks(6896), "Published", "Herbal Tea Benefits", 510 },
                    { 10, "Exercise", "You might feel strongest during your follicular phase. High-intensity training is often most effective during this time.", new DateTime(2026, 5, 5, 19, 17, 26, 531, DateTimeKind.Utc).AddTicks(6897), "Published", "Strength Training Timing", 890 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 10);

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
                columns: new[] { "Category", "Content", "PublishDate" },
                values: new object[] { "Nutrition", "Detailed analysis...", new DateTime(2026, 5, 5, 19, 7, 10, 56, DateTimeKind.Utc).AddTicks(259) });

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Content", "PublishDate" },
                values: new object[] { "Detailed analysis...", new DateTime(2026, 5, 5, 19, 7, 10, 56, DateTimeKind.Utc).AddTicks(260) });

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Category", "Content", "PublishDate" },
                values: new object[] { "Science", "Detailed analysis...", new DateTime(2026, 5, 5, 19, 7, 10, 56, DateTimeKind.Utc).AddTicks(262) });

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Category", "Content", "PublishDate" },
                values: new object[] { "Health", "Detailed analysis...", new DateTime(2026, 5, 5, 19, 7, 10, 56, DateTimeKind.Utc).AddTicks(263) });
        }
    }
}
