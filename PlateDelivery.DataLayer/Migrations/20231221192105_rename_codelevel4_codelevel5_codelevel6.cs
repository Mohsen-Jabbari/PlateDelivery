using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlateDelivery.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class rename_codelevel4_codelevel5_codelevel6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CodeLever6",
                table: "TopYarTmps",
                newName: "CodeLevel6");

            migrationBuilder.RenameColumn(
                name: "CodeLever5",
                table: "TopYarTmps",
                newName: "CodeLevel5");

            migrationBuilder.RenameColumn(
                name: "CodeLever4",
                table: "TopYarTmps",
                newName: "CodeLevel4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CodeLevel6",
                table: "TopYarTmps",
                newName: "CodeLever6");

            migrationBuilder.RenameColumn(
                name: "CodeLevel5",
                table: "TopYarTmps",
                newName: "CodeLever5");

            migrationBuilder.RenameColumn(
                name: "CodeLevel4",
                table: "TopYarTmps",
                newName: "CodeLever4");
        }
    }
}
