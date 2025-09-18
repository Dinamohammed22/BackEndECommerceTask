using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KOG.ECommerce.Migrations
{
    /// <inheritdoc />
    public partial class UniqueRoleFeature : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RoleFeature_RoleId",
                schema: "RoleFeatures",
                table: "RoleFeature");

            migrationBuilder.CreateIndex(
                name: "IX_RoleFeature_RoleId_Features",
                schema: "RoleFeatures",
                table: "RoleFeature",
                columns: new[] { "RoleId", "Features" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RoleFeature_RoleId_Features",
                schema: "RoleFeatures",
                table: "RoleFeature");

            migrationBuilder.CreateIndex(
                name: "IX_RoleFeature_RoleId",
                schema: "RoleFeatures",
                table: "RoleFeature",
                column: "RoleId");
        }
    }
}
