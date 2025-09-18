using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KOG.ECommerce.Migrations
{
    /// <inheritdoc />
    public partial class ModuleFeatures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ModuleFeatures");

            //migrationBuilder.EnsureSchema(
            //    name: "RoleFeatures");

            //migrationBuilder.AddColumn<int>(
            //    name: "VerifyStatus",
            //    schema: "Clients",
            //    table: "Client",
            //    type: "int",
            //    nullable: false,
            //    defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ModuleFeature",
                schema: "ModuleFeatures",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ModuleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Features = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModuleFeature", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ModuleFeature_Module_ModuleId",
                        column: x => x.ModuleId,
                        principalSchema: "Modules",
                        principalTable: "Module",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            //migrationBuilder.CreateTable(
            //    name: "RoleFeature",
            //    schema: "RoleFeatures",
            //    columns: table => new
            //    {
            //        ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //        RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //        Features = table.Column<int>(type: "int", nullable: false),
            //        CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Deleted = table.Column<bool>(type: "bit", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_RoleFeature", x => x.ID);
            //        table.ForeignKey(
            //            name: "FK_RoleFeature_Role_RoleId",
            //            column: x => x.RoleId,
            //            principalSchema: "Roles",
            //            principalTable: "Role",
            //            principalColumn: "ID",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            migrationBuilder.CreateIndex(
                name: "IX_ModuleFeature_ModuleId_Features",
                schema: "ModuleFeatures",
                table: "ModuleFeature",
                columns: new[] { "ModuleId", "Features" },
                unique: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_RoleFeature_RoleId_Features",
            //    schema: "RoleFeatures",
            //    table: "RoleFeature",
            //    columns: new[] { "RoleId", "Features" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ModuleFeature",
                schema: "ModuleFeatures");

            //migrationBuilder.DropTable(
            //    name: "RoleFeature",
            //    schema: "RoleFeatures");

            //migrationBuilder.DropColumn(
            //    name: "VerifyStatus",
            //    schema: "Clients",
            //    table: "Client");
        }
    }
}
