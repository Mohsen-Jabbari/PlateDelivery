using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlateDelivery.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class add_ratio_to_service_codeing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Ratio",
                table: "ServiceCodings",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ratio",
                table: "ServiceCodings");
        }
    }
}
