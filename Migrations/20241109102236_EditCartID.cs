using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KOG.ECommerce.Migrations
{
    /// <inheritdoc />
    public partial class EditCartID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Client_User_UserId",
                schema: "Clients",
                table: "Client");

            migrationBuilder.DropIndex(
                name: "IX_Client_UserId",
                schema: "Clients",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "Clients",
                table: "Client");

            migrationBuilder.AddForeignKey(
                name: "FK_Client_User_ID",
                schema: "Clients",
                table: "Client",
                column: "ID",
                principalSchema: "Users",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Client_User_ID",
                schema: "Clients",
                table: "Client");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                schema: "Clients",
                table: "Client",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Client_UserId",
                schema: "Clients",
                table: "Client",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Client_User_UserId",
                schema: "Clients",
                table: "Client",
                column: "UserId",
                principalSchema: "Users",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
