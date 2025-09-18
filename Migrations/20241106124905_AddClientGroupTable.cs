using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KOG.ECommerce.Migrations
{
    /// <inheritdoc />
    public partial class AddClientGroupTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.EnsureSchema(
                name: "ClientGroup");

           

            migrationBuilder.AddColumn<string>(
                name: "ClientGroupId",
                schema: "Clients",
                table: "Client",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                schema: "Clients",
                table: "Client",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ClientGroup",
                schema: "ClientGroup",
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
                    table.PrimaryKey("PK_ClientGroup", x => x.ID);
                });

          
            migrationBuilder.CreateIndex(
                name: "IX_Client_ClientGroupId",
                schema: "Clients",
                table: "Client",
                column: "ClientGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Client_ClientGroup_ClientGroupId",
                schema: "Clients",
                table: "Client",
                column: "ClientGroupId",
                principalSchema: "ClientGroup",
                principalTable: "ClientGroup",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Client_ClientGroup_ClientGroupId",
                schema: "Clients",
                table: "Client");

            migrationBuilder.DropTable(
                name: "ClientGroup",
                schema: "ClientGroup");

            migrationBuilder.DropIndex(
                name: "IX_Client_ClientGroupId",
                schema: "Clients",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "ClientGroupId",
                schema: "Clients",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "Email",
                schema: "Clients",
                table: "Client");

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
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaxExempted = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
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
    }
}
