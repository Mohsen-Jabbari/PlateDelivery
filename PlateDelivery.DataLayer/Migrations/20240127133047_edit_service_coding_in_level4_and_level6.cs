using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlateDelivery.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class edit_service_coding_in_level4_and_level6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ServiceCodings_CodeLevel4",
                table: "ServiceCodings");

            migrationBuilder.DropIndex(
                name: "IX_ServiceCodings_CodeLevel6",
                table: "ServiceCodings");

            migrationBuilder.AlterColumn<string>(
                name: "CodeLevel6",
                table: "ServiceCodings",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "CodeLevel4",
                table: "ServiceCodings",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CodeLevel6",
                table: "ServiceCodings",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CodeLevel4",
                table: "ServiceCodings",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceCodings_CodeLevel4",
                table: "ServiceCodings",
                column: "CodeLevel4");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceCodings_CodeLevel6",
                table: "ServiceCodings",
                column: "CodeLevel6");
        }
    }
}
