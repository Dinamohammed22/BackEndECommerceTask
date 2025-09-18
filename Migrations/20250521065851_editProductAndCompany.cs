using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KOG.ECommerce.Migrations
{
    /// <inheritdoc />
    public partial class editProductAndCompany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CompanyId",
                schema: "Products",
                table: "Product",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "fc94b7ec-3d64-4cc0-8152-5919a7847f50");

            migrationBuilder.CreateIndex(
                name: "IX_Product_CompanyId",
                schema: "Products",
                table: "Product",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Company_CompanyId",
                schema: "Products",
                table: "Product",
                column: "CompanyId",
                principalSchema: "Companies",
                principalTable: "Company",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Company_CompanyId",
                schema: "Products",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_CompanyId",
                schema: "Products",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                schema: "Products",
                table: "Product");
        }
    }
}
