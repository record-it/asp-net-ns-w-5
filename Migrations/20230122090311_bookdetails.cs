using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wykład_4.Migrations
{
    public partial class bookdetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookDetailsSet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    BookId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookDetailsSet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    EditionYear = table.Column<int>(type: "INTEGER", nullable: false),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "BookDetailsSet",
                columns: new[] { "Id", "BookId", "Description" },
                values: new object[] { 1, 1, "Super" });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Created", "EditionYear", "Title" },
                values: new object[] { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2020, "ASP.NET" });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Created", "EditionYear", "Title" },
                values: new object[] { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2022, "C#" });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Created", "EditionYear", "Title" },
                values: new object[] { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2021, "Java" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookDetailsSet");

            migrationBuilder.DropTable(
                name: "Books");
        }
    }
}
