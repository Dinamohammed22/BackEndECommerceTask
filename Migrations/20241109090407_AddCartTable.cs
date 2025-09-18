using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KOG.ECommerce.Migrations
{
    /// <inheritdoc />
    public partial class AddCartTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.EnsureSchema(
                name: "Carts");


            migrationBuilder.AddColumn<string>(
                name: "CartId",
                schema: "Products",
                table: "Product",
                type: "nvarchar(450)",
                nullable: true);



            migrationBuilder.CreateTable(
                name: "Cart",
                schema: "Carts",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cart", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Cart_Client_ID",
                        column: x => x.ID,
                        principalSchema: "Clients",
                        principalTable: "Client",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_CartId",
                schema: "Products",
                table: "Product",
                column: "CartId");


            migrationBuilder.AddForeignKey(
                name: "FK_Product_Cart_CartId",
                schema: "Products",
                table: "Product",
                column: "CartId",
                principalSchema: "Carts",
                principalTable: "Cart",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
           
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Cart_CartId",
                schema: "Products",
                table: "Product");

            migrationBuilder.DropTable(
                name: "Cart",
                schema: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Product_CartId",
                schema: "Products",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "CartId",
                schema: "Products",
                table: "Product");

 
          
        }
    }
}
