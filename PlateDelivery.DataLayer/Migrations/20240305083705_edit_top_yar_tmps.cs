using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlateDelivery.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class edit_top_yar_tmps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CertainCode",
                table: "TopYarTmps");

            migrationBuilder.DropColumn(
                name: "CodeLevel4",
                table: "TopYarTmps");

            migrationBuilder.DropColumn(
                name: "CodeLevel5",
                table: "TopYarTmps");

            migrationBuilder.DropColumn(
                name: "CodeLevel6",
                table: "TopYarTmps");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "TopYarTmps");

            migrationBuilder.DropColumn(
                name: "IncomeAmount",
                table: "TopYarTmps");

            migrationBuilder.DropColumn(
                name: "TaxAmount",
                table: "TopYarTmps");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CertainCode",
                table: "TopYarTmps",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodeLevel4",
                table: "TopYarTmps",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodeLevel5",
                table: "TopYarTmps",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodeLevel6",
                table: "TopYarTmps",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "TopYarTmps",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IncomeAmount",
                table: "TopYarTmps",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TaxAmount",
                table: "TopYarTmps",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
