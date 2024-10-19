using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace book.Migrations
{
    /// <inheritdoc />
    public partial class Auth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1b50ae4a-6b9e-4747-afeb-193850deb422", "832d2c76-4713-4baf-b4f3-c2fe04eb16d8", "User", "user" },
                    { "7f89a022-ad9b-4999-bdb0-fc6cba46f85c", "6944cd4b-4bd7-43c2-b5f5-2699681f786a", "Admin", "admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1b50ae4a-6b9e-4747-afeb-193850deb422");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7f89a022-ad9b-4999-bdb0-fc6cba46f85c");
        }
    }
}
