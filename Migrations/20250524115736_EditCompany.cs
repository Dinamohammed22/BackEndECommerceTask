using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KOG.ECommerce.Migrations
{
    /// <inheritdoc />
    public partial class EditCompany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfPoints",
                schema: "Companies",
                table: "Company");

            migrationBuilder.RenameColumn(
                name: "AmountOfMoney",
                schema: "Companies",
                table: "Company",
                newName: "Longitude");

            migrationBuilder.AddColumn<string>(
                name: "CompanyCode",
                schema: "Companies",
                table: "Company",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreditLimit",
                schema: "Companies",
                table: "Company",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                schema: "Companies",
                table: "Company",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyCode",
                schema: "Companies",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "CreditLimit",
                schema: "Companies",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "Latitude",
                schema: "Companies",
                table: "Company");

            migrationBuilder.RenameColumn(
                name: "Longitude",
                schema: "Companies",
                table: "Company",
                newName: "AmountOfMoney");

            migrationBuilder.AddColumn<int>(
                name: "NumberOfPoints",
                schema: "Companies",
                table: "Company",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
