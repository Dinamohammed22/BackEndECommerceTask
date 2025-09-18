using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KOG.ECommerce.Migrations
{
    /// <inheritdoc />
    public partial class addOrderLiter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Weight",
                schema: "OrderItems",
                table: "OrderItem",
                newName: "Liter");

            migrationBuilder.RenameColumn(
                name: "ItemWeight",
                schema: "OrderItems",
                table: "OrderItem",
                newName: "ItemLiter");

            migrationBuilder.RenameColumn(
                name: "TotalWeight",
                schema: "Orders",
                table: "Order",
                newName: "TotalLiter");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Liter",
                schema: "OrderItems",
                table: "OrderItem",
                newName: "Weight");

            migrationBuilder.RenameColumn(
                name: "ItemLiter",
                schema: "OrderItems",
                table: "OrderItem",
                newName: "ItemWeight");

            migrationBuilder.RenameColumn(
                name: "TotalLiter",
                schema: "Orders",
                table: "Order",
                newName: "TotalWeight");
        }
    }
}
