using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KOG.ECommerce.Migrations
{
    /// <inheritdoc />
    public partial class editBrandModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Brand_Brand_BrandID",
                schema: "Brands",
                table: "Brand");

            migrationBuilder.DropIndex(
                name: "IX_Brand_BrandID",
                schema: "Brands",
                table: "Brand");

            migrationBuilder.DropColumn(
                name: "BrandID",
                schema: "Brands",
                table: "Brand");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "Brands",
                table: "Brand",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Tags",
                schema: "Brands",
                table: "Brand",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "Brands",
                table: "Brand");

            migrationBuilder.DropColumn(
                name: "Tags",
                schema: "Brands",
                table: "Brand");

            migrationBuilder.AddColumn<string>(
                name: "BrandID",
                schema: "Brands",
                table: "Brand",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Brand_BrandID",
                schema: "Brands",
                table: "Brand",
                column: "BrandID");

            migrationBuilder.AddForeignKey(
                name: "FK_Brand_Brand_BrandID",
                schema: "Brands",
                table: "Brand",
                column: "BrandID",
                principalSchema: "Brands",
                principalTable: "Brand",
                principalColumn: "ID");
        }
    }
}
