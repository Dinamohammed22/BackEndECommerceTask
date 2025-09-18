using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KOG.ECommerce.Migrations
{
    /// <inheritdoc />
    public partial class AddCompanyGovernorate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "CompanyGovernorates");

            migrationBuilder.CreateTable(
                name: "CompanyGovernorate",
                schema: "CompanyGovernorates",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GovernorateId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CompanyId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyGovernorate", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CompanyGovernorate_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "Companies",
                        principalTable: "Company",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyGovernorate_Governorate_GovernorateId",
                        column: x => x.GovernorateId,
                        principalSchema: "Governorates",
                        principalTable: "Governorate",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyGovernorate_CompanyId",
                schema: "CompanyGovernorates",
                table: "CompanyGovernorate",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyGovernorate_GovernorateId",
                schema: "CompanyGovernorates",
                table: "CompanyGovernorate",
                column: "GovernorateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyGovernorate",
                schema: "CompanyGovernorates");
        }
    }
}
