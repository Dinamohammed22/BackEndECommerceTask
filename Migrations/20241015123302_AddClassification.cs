using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KOG.ECommerce.Migrations
{
    /// <inheritdoc />
    public partial class AddClassification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Classifications");

            migrationBuilder.AddColumn<string>(
                name: "Activity",
                schema: "Companies",
                table: "Company",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CityId",
                schema: "Companies",
                table: "Company",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ClassificationId",
                schema: "Companies",
                table: "Company",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GovernorateId",
                schema: "Companies",
                table: "Company",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ManagerMobile",
                schema: "Companies",
                table: "Company",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ManagerName",
                schema: "Companies",
                table: "Company",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Mobile",
                schema: "Companies",
                table: "Company",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NID",
                schema: "Companies",
                table: "Company",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TaxCardID",
                schema: "Companies",
                table: "Company",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TaxRegistryNumber",
                schema: "Companies",
                table: "Company",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Classification",
                schema: "Classifications",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classification", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Company_CityId",
                schema: "Companies",
                table: "Company",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Company_ClassificationId",
                schema: "Companies",
                table: "Company",
                column: "ClassificationId");

            migrationBuilder.CreateIndex(
                name: "IX_Company_GovernorateId",
                schema: "Companies",
                table: "Company",
                column: "GovernorateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Company_City_CityId",
                schema: "Companies",
                table: "Company",
                column: "CityId",
                principalSchema: "Cities",
                principalTable: "City",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Company_Classification_ClassificationId",
                schema: "Companies",
                table: "Company",
                column: "ClassificationId",
                principalSchema: "Classifications",
                principalTable: "Classification",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Company_Governorate_GovernorateId",
                schema: "Companies",
                table: "Company",
                column: "GovernorateId",
                principalSchema: "Governorates",
                principalTable: "Governorate",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Company_City_CityId",
                schema: "Companies",
                table: "Company");

            migrationBuilder.DropForeignKey(
                name: "FK_Company_Classification_ClassificationId",
                schema: "Companies",
                table: "Company");

            migrationBuilder.DropForeignKey(
                name: "FK_Company_Governorate_GovernorateId",
                schema: "Companies",
                table: "Company");

            migrationBuilder.DropTable(
                name: "Classification",
                schema: "Classifications");

            migrationBuilder.DropIndex(
                name: "IX_Company_CityId",
                schema: "Companies",
                table: "Company");

            migrationBuilder.DropIndex(
                name: "IX_Company_ClassificationId",
                schema: "Companies",
                table: "Company");

            migrationBuilder.DropIndex(
                name: "IX_Company_GovernorateId",
                schema: "Companies",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "Activity",
                schema: "Companies",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "CityId",
                schema: "Companies",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "ClassificationId",
                schema: "Companies",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "GovernorateId",
                schema: "Companies",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "ManagerMobile",
                schema: "Companies",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "ManagerName",
                schema: "Companies",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "Mobile",
                schema: "Companies",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "NID",
                schema: "Companies",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "TaxCardID",
                schema: "Companies",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "TaxRegistryNumber",
                schema: "Companies",
                table: "Company");
        }
    }
}
