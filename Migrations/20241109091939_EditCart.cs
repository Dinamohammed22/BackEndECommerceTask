using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KOG.ECommerce.Migrations
{
    /// <inheritdoc />
    public partial class EditCart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                schema: "Carts",
                table: "Cart",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                schema: "Carts",
                table: "Cart");
        }
    }
}
