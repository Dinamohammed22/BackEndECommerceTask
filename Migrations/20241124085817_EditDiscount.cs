using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KOG.ECommerce.Migrations
{
    /// <inheritdoc />
    public partial class EditDiscount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Percentage",
                schema: "Discounts",
                table: "Discount",
                newName: "ReceiptAmount");

            migrationBuilder.AddColumn<double>(
                name: "Amount",
                schema: "Discounts",
                table: "Discount",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "DiscountCategory",
                schema: "Discounts",
                table: "Discount",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DiscountType",
                schema: "Discounts",
                table: "Discount",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "Discounts",
                table: "Discount",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                schema: "Discounts",
                table: "Discount");

            migrationBuilder.DropColumn(
                name: "DiscountCategory",
                schema: "Discounts",
                table: "Discount");

            migrationBuilder.DropColumn(
                name: "DiscountType",
                schema: "Discounts",
                table: "Discount");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "Discounts",
                table: "Discount");

            migrationBuilder.RenameColumn(
                name: "ReceiptAmount",
                schema: "Discounts",
                table: "Discount",
                newName: "Percentage");
        }
    }
}
