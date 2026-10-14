using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ExportStatistics.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueCategoryName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExportProducts_Categoties_category_id",
                table: "ExportProducts");

            migrationBuilder.DropTable(
                name: "Categoties");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "ExportProducts",
                newName: "Id");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_name",
                table: "Categories",
                column: "name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ExportProducts_Categories_category_id",
                table: "ExportProducts",
                column: "category_id",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExportProducts_Categories_category_id",
                table: "ExportProducts");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ExportProducts",
                newName: "id");

            migrationBuilder.CreateTable(
                name: "Categoties",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoties", x => x.id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ExportProducts_Categoties_category_id",
                table: "ExportProducts",
                column: "category_id",
                principalTable: "Categoties",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
