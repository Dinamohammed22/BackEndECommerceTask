using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KOG.ECommerce.Migrations
{
    /// <inheritdoc />
    public partial class CorrectAddProductModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Brand",
                schema: "Products",
                table: "Product");

            migrationBuilder.AddColumn<string>(
                name: "BrandId",
                schema: "Products",
                table: "Product",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BrandID",
                schema: "Brands",
                table: "Brand",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_BrandId",
                schema: "Products",
                table: "Product",
                column: "BrandId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Brand_BrandId",
                schema: "Products",
                table: "Product",
                column: "BrandId",
                principalSchema: "Brands",
                principalTable: "Brand",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Brand_Brand_BrandID",
                schema: "Brands",
                table: "Brand");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Brand_BrandId",
                schema: "Products",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_BrandId",
                schema: "Products",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Brand_BrandID",
                schema: "Brands",
                table: "Brand");

            migrationBuilder.DropColumn(
                name: "BrandId",
                schema: "Products",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "BrandID",
                schema: "Brands",
                table: "Brand");

            migrationBuilder.AddColumn<string>(
                name: "Brand",
                schema: "Products",
                table: "Product",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
