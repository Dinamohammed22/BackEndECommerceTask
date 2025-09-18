using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KOG.ECommerce.Migrations
{
    /// <inheritdoc />
    public partial class userStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VerifyStatus",
                schema: "Clients",
                table: "Client");

            migrationBuilder.AddColumn<int>(
                name: "VerifyStatus",
                schema: "Users",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VerifyStatus",
                schema: "Users",
                table: "User");

            migrationBuilder.AddColumn<int>(
                name: "VerifyStatus",
                schema: "Clients",
                table: "Client",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
