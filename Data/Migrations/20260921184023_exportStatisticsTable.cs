using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ExportStatistics.Data.Migrations
{
    /// <inheritdoc />
    public partial class exportStatisticsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExportStatistics",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    state = table.Column<string>(type: "text", nullable: true),
                    port = table.Column<string>(type: "text", nullable: true),
                    product_id = table.Column<int>(type: "integer", nullable: false),
                    country_id = table.Column<int>(type: "integer", nullable: false),
                    year = table.Column<string>(type: "text", nullable: false),
                    value = table.Column<decimal>(type: "numeric", nullable: false),
                    quantity = table.Column<decimal>(type: "numeric", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExportStatistics", x => x.id);
                    table.ForeignKey(
                        name: "FK_ExportStatistics_Countries_country_id",
                        column: x => x.country_id,
                        principalTable: "Countries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExportStatistics_ExportProducts_product_id",
                        column: x => x.product_id,
                        principalTable: "ExportProducts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExportProducts_hscode",
                table: "ExportProducts",
                column: "hscode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExportStatistics_country_id",
                table: "ExportStatistics",
                column: "country_id");

            migrationBuilder.CreateIndex(
                name: "IX_ExportStatistics_product_id",
                table: "ExportStatistics",
                column: "product_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExportStatistics");

            migrationBuilder.DropIndex(
                name: "IX_ExportProducts_hscode",
                table: "ExportProducts");
        }
    }
}
