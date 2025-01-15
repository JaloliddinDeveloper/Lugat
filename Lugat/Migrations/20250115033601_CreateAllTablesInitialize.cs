﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lugat.Migrations
{
    /// <inheritdoc />
    public partial class CreateAllTablesInitialize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Star = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bolimlar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    SectionPicture = table.Column<string>(type: "TEXT", nullable: false),
                    Star = table.Column<int>(type: "INTEGER", nullable: false),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bolimlar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bolimlar_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Words",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    English = table.Column<string>(type: "TEXT", nullable: false),
                    EnglishTrans = table.Column<string>(type: "TEXT", nullable: false),
                    Uzbek = table.Column<string>(type: "TEXT", nullable: false),
                    WordPicture = table.Column<string>(type: "TEXT", nullable: false),
                    ExampleEng = table.Column<string>(type: "TEXT", nullable: false),
                    ExampleUz = table.Column<string>(type: "TEXT", nullable: false),
                    BolimId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Words", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Words_Bolimlar_BolimId",
                        column: x => x.BolimId,
                        principalTable: "Bolimlar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bolimlar_CategoryId",
                table: "Bolimlar",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Words_BolimId",
                table: "Words",
                column: "BolimId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Words");

            migrationBuilder.DropTable(
                name: "Bolimlar");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
