using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace book.Migrations
{
    /// <inheritdoc />
    public partial class M10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buy_AspNetUsers_UserId",
                table: "Buy");

            migrationBuilder.DropForeignKey(
                name: "FK_Buy_books_BookId",
                table: "Buy");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Buy",
                table: "Buy");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "828c00d0-9936-4898-89ab-3ba55f01486e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ce1bb028-ae3a-4911-a17a-f2e630827af7");

            migrationBuilder.RenameTable(
                name: "Buy",
                newName: "buys");

            migrationBuilder.RenameIndex(
                name: "IX_Buy_BookId",
                table: "buys",
                newName: "IX_buys_BookId");

            migrationBuilder.AddColumn<int>(
                name: "price",
                table: "books",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "buys",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_buys",
                table: "buys",
                columns: new[] { "UserId", "BookId" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "08d7052a-6723-439a-9015-a74a20b822c0", "acb2a0cb-c616-4813-9f98-4fe2f609ff1a", "User", "user" },
                    { "831b280a-d285-4826-82d9-790e3126b2b1", "d3469ee7-bc17-4c68-8822-3a44f62b9fa2", "Admin", "admin" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_buys_AspNetUsers_UserId",
                table: "buys",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_buys_books_BookId",
                table: "buys",
                column: "BookId",
                principalTable: "books",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_buys_AspNetUsers_UserId",
                table: "buys");

            migrationBuilder.DropForeignKey(
                name: "FK_buys_books_BookId",
                table: "buys");

            migrationBuilder.DropPrimaryKey(
                name: "PK_buys",
                table: "buys");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "08d7052a-6723-439a-9015-a74a20b822c0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "831b280a-d285-4826-82d9-790e3126b2b1");

            migrationBuilder.DropColumn(
                name: "price",
                table: "books");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "buys");

            migrationBuilder.RenameTable(
                name: "buys",
                newName: "Buy");

            migrationBuilder.RenameIndex(
                name: "IX_buys_BookId",
                table: "Buy",
                newName: "IX_Buy_BookId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Buy",
                table: "Buy",
                columns: new[] { "UserId", "BookId" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "828c00d0-9936-4898-89ab-3ba55f01486e", "84db53f4-ef47-4d3f-bd33-9d2702ca8a6c", "Admin", "admin" },
                    { "ce1bb028-ae3a-4911-a17a-f2e630827af7", "68dbbe85-fbbe-4cea-95d6-452aadadf232", "User", "user" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Buy_AspNetUsers_UserId",
                table: "Buy",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Buy_books_BookId",
                table: "Buy",
                column: "BookId",
                principalTable: "books",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
