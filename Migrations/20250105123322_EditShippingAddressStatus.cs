using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KOG.ECommerce.Migrations
{
    /// <inheritdoc />
    public partial class EditShippingAddressStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "status",
                schema: "ShippingAddresses",
                table: "ShippingAddress",
                newName: "Status");

            migrationBuilder.AddColumn<string>(
                name: "BuildingData",
                schema: "ShippingAddresses",
                table: "ShippingAddress",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BuildingData",
                schema: "ShippingAddresses",
                table: "ShippingAddress");

            migrationBuilder.RenameColumn(
                name: "Status",
                schema: "ShippingAddresses",
                table: "ShippingAddress",
                newName: "status");
        }
    }
}
