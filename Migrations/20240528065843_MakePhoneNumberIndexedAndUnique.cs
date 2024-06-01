using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PSchool.Backend.Migrations
{
    /// <inheritdoc />
    public partial class MakePhoneNumberIndexedAndUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Parents_UserId",
                table: "Parents");

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "Id",
                keyValue: "3f22e88c-1e89-45cf-b2f0-abf1424d760d");

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "Id",
                keyValue: "6d39bf43-c1dd-436a-ba2b-e1d43b5e2a0d");

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "310d8fe2-0d20-4154-8c29-7db17543772d", "1", "Admin", "ADMIN" },
                    { "844c96bf-b484-40d4-b3ed-40f59e0e0b88", "2", "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Parents_UserId",
                table: "Parents",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Parents_UserId",
                table: "Parents");

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "Id",
                keyValue: "310d8fe2-0d20-4154-8c29-7db17543772d");

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "Id",
                keyValue: "844c96bf-b484-40d4-b3ed-40f59e0e0b88");

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3f22e88c-1e89-45cf-b2f0-abf1424d760d", "2", "User", "USER" },
                    { "6d39bf43-c1dd-436a-ba2b-e1d43b5e2a0d", "1", "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Parents_UserId",
                table: "Parents",
                column: "UserId",
                unique: true);
        }
    }
}
