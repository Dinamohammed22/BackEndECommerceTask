using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KOG.ECommerce.Migrations
{
    /// <inheritdoc />
    public partial class ShippingAddressModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Client_City_CityId",
                schema: "Clients",
                table: "Client");

            migrationBuilder.DropForeignKey(
                name: "FK_Client_Governorate_GovernorateId",
                schema: "Clients",
                table: "Client");

            migrationBuilder.DropIndex(
                name: "IX_Client_CityId",
                schema: "Clients",
                table: "Client");

            migrationBuilder.DropIndex(
                name: "IX_Client_GovernorateId",
                schema: "Clients",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "CityId",
                schema: "Clients",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "GovernorateId",
                schema: "Clients",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "Landmark",
                schema: "Clients",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "Latitude",
                schema: "Clients",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "Longitude",
                schema: "Clients",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "Street",
                schema: "Clients",
                table: "Client");

            migrationBuilder.EnsureSchema(
                name: "ShippingAddresses");

            migrationBuilder.CreateTable(
                name: "ShippingAddress",
                schema: "ShippingAddresses",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GovernorateId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CityId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Landmark = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    ClientId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsDefualt = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingAddress", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ShippingAddress_City_CityId",
                        column: x => x.CityId,
                        principalSchema: "Cities",
                        principalTable: "City",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ShippingAddress_Client_ClientId",
                        column: x => x.ClientId,
                        principalSchema: "Clients",
                        principalTable: "Client",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ShippingAddress_Governorate_GovernorateId",
                        column: x => x.GovernorateId,
                        principalSchema: "Governorates",
                        principalTable: "Governorate",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShippingAddress_CityId",
                schema: "ShippingAddresses",
                table: "ShippingAddress",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingAddress_ClientId",
                schema: "ShippingAddresses",
                table: "ShippingAddress",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingAddress_GovernorateId",
                schema: "ShippingAddresses",
                table: "ShippingAddress",
                column: "GovernorateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShippingAddress",
                schema: "ShippingAddresses");

            migrationBuilder.AddColumn<string>(
                name: "CityId",
                schema: "Clients",
                table: "Client",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GovernorateId",
                schema: "Clients",
                table: "Client",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Landmark",
                schema: "Clients",
                table: "Client",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                schema: "Clients",
                table: "Client",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                schema: "Clients",
                table: "Client",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Street",
                schema: "Clients",
                table: "Client",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Client_CityId",
                schema: "Clients",
                table: "Client",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Client_GovernorateId",
                schema: "Clients",
                table: "Client",
                column: "GovernorateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Client_City_CityId",
                schema: "Clients",
                table: "Client",
                column: "CityId",
                principalSchema: "Cities",
                principalTable: "City",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Client_Governorate_GovernorateId",
                schema: "Clients",
                table: "Client",
                column: "GovernorateId",
                principalSchema: "Governorates",
                principalTable: "Governorate",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
