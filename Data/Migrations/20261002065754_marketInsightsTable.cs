using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ExportStatistics.Data.Migrations
{
    /// <inheritdoc />
    public partial class marketInsightsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "market_insight",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    product_id = table.Column<int>(type: "integer", nullable: false),
                    country_id = table.Column<int>(type: "integer", nullable: false),
                    cagr_percent = table.Column<decimal>(type: "numeric", nullable: false),
                    trend_signal = table.Column<string>(type: "text", nullable: false),
                    avg_price_per_mt = table.Column<decimal>(type: "numeric", nullable: false),
                    volatility_score = table.Column<decimal>(type: "numeric", nullable: false),
                    peak_year = table.Column<string>(type: "text", nullable: true),
                    last_year_volume_mt = table.Column<decimal>(type: "numeric", nullable: false),
                    market_share_percent = table.Column<decimal>(type: "numeric", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_market_insight", x => x.id);
                    table.ForeignKey(
                        name: "FK_market_insight_Countries_country_id",
                        column: x => x.country_id,
                        principalTable: "Countries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_market_insight_ExportProducts_product_id",
                        column: x => x.product_id,
                        principalTable: "ExportProducts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_market_insight_country_id",
                table: "market_insight",
                column: "country_id");

            migrationBuilder.CreateIndex(
                name: "IX_market_insight_product_id",
                table: "market_insight",
                column: "product_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "market_insight");
        }
    }
}
