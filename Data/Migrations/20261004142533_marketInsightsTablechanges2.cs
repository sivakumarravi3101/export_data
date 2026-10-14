using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExportStatistics.Data.Migrations
{
    /// <inheritdoc />
    public partial class marketInsightsTablechanges2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateIndex(
                name: "IX_market_insight_country_id",
                table: "market_insight",
                column: "country_id");

            migrationBuilder.CreateIndex(
                name: "IX_market_insight_product_id",
                table: "market_insight",
                column: "product_id");
            migrationBuilder.DropIndex(
                name: "IX_market_insight_country_id",
                table: "market_insight");

            migrationBuilder.DropIndex(
                name: "IX_market_insight_product_id",
                table: "market_insight");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateIndex(
                name: "IX_market_insight_country_id",
                table: "market_insight",
                column: "country_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_market_insight_product_id",
                table: "market_insight",
                column: "product_id",
                unique: true);
            migrationBuilder.DropIndex(
                name: "IX_market_insight_country_id",
                table: "market_insight");

            migrationBuilder.DropIndex(
                name: "IX_market_insight_product_id",
                table: "market_insight");

        }
    }
}
