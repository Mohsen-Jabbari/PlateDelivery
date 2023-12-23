using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlateDelivery.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class add_certain_to_service_coding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IncomeCertainId",
                table: "ServiceCodings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IncomeCertainId",
                table: "ServiceCodings");
        }
    }
}
