using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlateDelivery.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class create_document_entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RetrivalRef = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TrackingNo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TransactionDate = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TransactionTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FinancialDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Iban = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrincipalAmount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CardNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Terminal = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    InstallationPlace = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServiceCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ServiceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProvinceName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubProvince = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProvinceCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CertainCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodeLevel4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodeLevel5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodeLevel6 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Debt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Credit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Documents_Id",
                table: "Documents",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Documents_RetrivalRef",
                table: "Documents",
                column: "RetrivalRef");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_ServiceCode",
                table: "Documents",
                column: "ServiceCode");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_Terminal",
                table: "Documents",
                column: "Terminal");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_TrackingNo",
                table: "Documents",
                column: "TrackingNo");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_TransactionDate",
                table: "Documents",
                column: "TransactionDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Documents");
        }
    }
}
