using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExportStatistics.Data.Migrations
{
    /// <inheritdoc />
    public partial class marketInsightsTablechangesaddnewfields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<decimal>(
                name: "last_year_price_per_mt",
                table: "market_insight",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "price_trend",
                table: "market_insight",
                type: "text",
                nullable: true);
            migrationBuilder.DropColumn(
               name: "last_year_price_per_mt",
               table: "market_insight");

            migrationBuilder.DropColumn(
                name: "price_trend",
                table: "market_insight");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<decimal>(
                name: "last_year_price_per_mt",
                table: "market_insight",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "price_trend",
                table: "market_insight",
                type: "text",
                nullable: true);
            migrationBuilder.DropColumn(
                name: "last_year_price_per_mt",
                table: "market_insight");

            migrationBuilder.DropColumn(
                name: "price_trend",
                table: "market_insight");
        }
    }
}
