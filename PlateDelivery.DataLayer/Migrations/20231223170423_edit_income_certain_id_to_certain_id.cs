using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlateDelivery.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class edit_income_certain_id_to_certain_id : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IncomeCertainId",
                table: "ServiceCodings");

            migrationBuilder.AddColumn<long>(
                name: "CertainId",
                table: "ServiceCodings",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CertainId",
                table: "ServiceCodings");

            migrationBuilder.AddColumn<int>(
                name: "IncomeCertainId",
                table: "ServiceCodings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
