using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlateDelivery.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Batches",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContractOwnerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ChassisTrim = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AgencyId = table.Column<long>(type: "bigint", nullable: false),
                    OwnerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AgencyPlaceId = table.Column<long>(type: "bigint", nullable: false),
                    RequestDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Batches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Counters",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlateCount = table.Column<long>(type: "bigint", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Counters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Oks",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContractOwnerId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlateNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ChassisNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InvoiceNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequestDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlateStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlaceOfRemoval = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfRemoval = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DetachDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChassisTrim = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Oks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Representations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RepresentationCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValidityDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrokerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrokerCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrokerTell = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrokerMobile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Extra = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrokerCodeR = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Representations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stores",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlateText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlateNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OperationType = table.Column<bool>(type: "bit", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Batches_ChassisTrim",
                table: "Batches",
                column: "ChassisTrim");

            migrationBuilder.CreateIndex(
                name: "IX_Batches_ContractOwnerId",
                table: "Batches",
                column: "ContractOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Batches_Id",
                table: "Batches",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Counters_Id",
                table: "Counters",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Oks_ChassisTrim",
                table: "Oks",
                column: "ChassisTrim");

            migrationBuilder.CreateIndex(
                name: "IX_Oks_Id",
                table: "Oks",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Oks_PlateNumber",
                table: "Oks",
                column: "PlateNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Representations_Id",
                table: "Representations",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stores_Id",
                table: "Stores",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Id",
                table: "Users",
                column: "Id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Batches");

            migrationBuilder.DropTable(
                name: "Counters");

            migrationBuilder.DropTable(
                name: "Oks");

            migrationBuilder.DropTable(
                name: "Representations");

            migrationBuilder.DropTable(
                name: "Stores");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
