using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace book.Migrations
{
    /// <inheritdoc />
    public partial class M8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "8d454ec9-1b6c-4a24-80e5-377e490d65af", "7e19206a-82c5-4ab5-a5a1-3451ec939d47", "Admin", "admin" },
                    { "c55e9bd2-8195-4f03-beb4-8921b0e1ab07", "79f0c7a0-12c7-4f28-987c-5460ccff8e30", "User", "user" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8d454ec9-1b6c-4a24-80e5-377e490d65af");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c55e9bd2-8195-4f03-beb4-8921b0e1ab07");
        }
    }
}
