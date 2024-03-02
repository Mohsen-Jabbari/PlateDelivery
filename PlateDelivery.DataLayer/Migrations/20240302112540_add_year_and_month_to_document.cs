using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlateDelivery.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class add_year_and_month_to_document : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Month",
                table: "Documents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "Documents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Documents_Month",
                table: "Documents",
                column: "Month");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_Year",
                table: "Documents",
                column: "Year");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Documents_Month",
                table: "Documents");

            migrationBuilder.DropIndex(
                name: "IX_Documents_Year",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "Month",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "Documents");
        }
    }
}
