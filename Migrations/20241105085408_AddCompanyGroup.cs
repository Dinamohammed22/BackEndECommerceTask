using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KOG.ECommerce.Migrations
{
    /// <inheritdoc />
    public partial class AddCompanyGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "CompanyGroups");

            migrationBuilder.AddColumn<string>(
                name: "CompanyGroupId",
                schema: "Companies",
                table: "Company",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CompanyGroup",
                schema: "CompanyGroups",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaxExempted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyGroup", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Company_CompanyGroupId",
                schema: "Companies",
                table: "Company",
                column: "CompanyGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Company_CompanyGroup_CompanyGroupId",
                schema: "Companies",
                table: "Company",
                column: "CompanyGroupId",
                principalSchema: "CompanyGroups",
                principalTable: "CompanyGroup",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Company_CompanyGroup_CompanyGroupId",
                schema: "Companies",
                table: "Company");

            migrationBuilder.DropTable(
                name: "CompanyGroup",
                schema: "CompanyGroups");

            migrationBuilder.DropIndex(
                name: "IX_Company_CompanyGroupId",
                schema: "Companies",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "CompanyGroupId",
                schema: "Companies",
                table: "Company");
        }
    }
}
