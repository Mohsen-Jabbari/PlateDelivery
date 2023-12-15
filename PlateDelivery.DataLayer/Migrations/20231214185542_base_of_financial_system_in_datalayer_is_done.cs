using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlateDelivery.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class base_of_financial_system_in_datalayer_is_done : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Batches");

            migrationBuilder.DropTable(
                name: "Counters");

            migrationBuilder.DropTable(
                name: "Stores");

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Iban = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BankCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BankName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Certains",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CertainName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CertainCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certains", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Provinces",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProvinceName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SubProvince = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProvinceCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provinces", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceCodings",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ServiceFullName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ServiceCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CodeLevel4 = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CodeLevel6 = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceCodings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TopYarTmps",
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
                    ProvinceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubProvince = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProvinceCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CertainCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CodeLever4 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CodeLever5 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CodeLever6 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaxAmount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IncomeAmount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopYarTmps", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_BankCode",
                table: "Accounts",
                column: "BankCode");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_BankName",
                table: "Accounts",
                column: "BankName");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_Iban",
                table: "Accounts",
                column: "Iban");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_Id",
                table: "Accounts",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Certains_CertainCode",
                table: "Certains",
                column: "CertainCode");

            migrationBuilder.CreateIndex(
                name: "IX_Certains_CertainName",
                table: "Certains",
                column: "CertainName");

            migrationBuilder.CreateIndex(
                name: "IX_Certains_Id",
                table: "Certains",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Provinces_Id",
                table: "Provinces",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Provinces_ProvinceCode",
                table: "Provinces",
                column: "ProvinceCode");

            migrationBuilder.CreateIndex(
                name: "IX_Provinces_ProvinceName",
                table: "Provinces",
                column: "ProvinceName");

            migrationBuilder.CreateIndex(
                name: "IX_Provinces_SubProvince",
                table: "Provinces",
                column: "SubProvince");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceCodings_CodeLevel4",
                table: "ServiceCodings",
                column: "CodeLevel4");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceCodings_CodeLevel6",
                table: "ServiceCodings",
                column: "CodeLevel6");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceCodings_Id",
                table: "ServiceCodings",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceCodings_ServiceCode",
                table: "ServiceCodings",
                column: "ServiceCode");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceCodings_ServiceFullName",
                table: "ServiceCodings",
                column: "ServiceFullName");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceCodings_ServiceName",
                table: "ServiceCodings",
                column: "ServiceName");

            migrationBuilder.CreateIndex(
                name: "IX_TopYarTmps_Id",
                table: "TopYarTmps",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TopYarTmps_RetrivalRef",
                table: "TopYarTmps",
                column: "RetrivalRef");

            migrationBuilder.CreateIndex(
                name: "IX_TopYarTmps_ServiceCode",
                table: "TopYarTmps",
                column: "ServiceCode");

            migrationBuilder.CreateIndex(
                name: "IX_TopYarTmps_Terminal",
                table: "TopYarTmps",
                column: "Terminal");

            migrationBuilder.CreateIndex(
                name: "IX_TopYarTmps_TrackingNo",
                table: "TopYarTmps",
                column: "TrackingNo");

            migrationBuilder.CreateIndex(
                name: "IX_TopYarTmps_TransactionDate",
                table: "TopYarTmps",
                column: "TransactionDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Certains");

            migrationBuilder.DropTable(
                name: "Provinces");

            migrationBuilder.DropTable(
                name: "ServiceCodings");

            migrationBuilder.DropTable(
                name: "TopYarTmps");

            migrationBuilder.CreateTable(
                name: "Batches",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AgencyId = table.Column<long>(type: "bigint", nullable: false),
                    AgencyPlaceId = table.Column<long>(type: "bigint", nullable: false),
                    ChassisTrim = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ContractOwnerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OwnerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequestDate = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PlateCount = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Counters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stores",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OperationType = table.Column<bool>(type: "bit", nullable: false),
                    PlateNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlateText = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stores", x => x.Id);
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
                name: "IX_Stores_Id",
                table: "Stores",
                column: "Id",
                unique: true);
        }
    }
}
