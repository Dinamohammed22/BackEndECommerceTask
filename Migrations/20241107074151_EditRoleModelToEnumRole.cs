using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KOG.ECommerce.Migrations
{
    /// <inheritdoc />
    public partial class EditRoleModelToEnumRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoleFeature_Role_RoleId",
                schema: "RoleFeatures",
                table: "RoleFeature");

            migrationBuilder.DropForeignKey(
                name: "FK_RoleModule_Role_RoleId",
                schema: "RoleModules",
                table: "RoleModule");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Role_RoleId",
                schema: "Users",
                table: "User");

            migrationBuilder.DropTable(
                name: "Role",
                schema: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_User_RoleId",
                schema: "Users",
                table: "User");

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                schema: "Users",
                table: "User",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                schema: "RoleModules",
                table: "RoleModule",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                schema: "RoleFeatures",
                table: "RoleFeature",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Roles");

            migrationBuilder.AlterColumn<string>(
                name: "RoleId",
                schema: "Users",
                table: "User",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "RoleId",
                schema: "RoleModules",
                table: "RoleModule",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "RoleId",
                schema: "RoleFeatures",
                table: "RoleFeature",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "Role",
                schema: "Roles",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleId",
                schema: "Users",
                table: "User",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoleFeature_Role_RoleId",
                schema: "RoleFeatures",
                table: "RoleFeature",
                column: "RoleId",
                principalSchema: "Roles",
                principalTable: "Role",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoleModule_Role_RoleId",
                schema: "RoleModules",
                table: "RoleModule",
                column: "RoleId",
                principalSchema: "Roles",
                principalTable: "Role",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Role_RoleId",
                schema: "Users",
                table: "User",
                column: "RoleId",
                principalSchema: "Roles",
                principalTable: "Role",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
