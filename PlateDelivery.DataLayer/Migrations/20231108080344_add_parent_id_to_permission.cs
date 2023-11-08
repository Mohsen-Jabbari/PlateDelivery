using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlateDelivery.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class add_parent_id_to_permission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ParentId",
                table: "Permission",
                type: "bigint",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Permission");
        }
    }
}
