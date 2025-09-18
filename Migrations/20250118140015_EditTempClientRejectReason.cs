using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KOG.ECommerce.Migrations
{
    /// <inheritdoc />
    public partial class EditTempClientRejectReason : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RejectReason",
                schema: "TempClients",
                table: "TempClient",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RejectReason",
                schema: "TempClients",
                table: "TempClient");
        }
    }
}
