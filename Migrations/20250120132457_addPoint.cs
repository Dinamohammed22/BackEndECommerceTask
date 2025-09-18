using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KOG.ECommerce.Migrations
{
    /// <inheritdoc />
    public partial class addPoint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActivePoint",
                schema: "Products",
                table: "Product",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfPoints",
                schema: "Products",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ItemPoint",
                schema: "OrderItems",
                table: "OrderItem",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Point",
                schema: "OrderItems",
                table: "OrderItem",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CancellationDate",
                schema: "Orders",
                table: "Order",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CompletedDate",
                schema: "Orders",
                table: "Order",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ConfirmationDate",
                schema: "Orders",
                table: "Order",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeliveryDate",
                schema: "Orders",
                table: "Order",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InProcessDate",
                schema: "Orders",
                table: "Order",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TotalNumberOfPoints",
                schema: "Orders",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "AmountOfMoney",
                schema: "Companies",
                table: "Company",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfPoints",
                schema: "Companies",
                table: "Company",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfPoints",
                schema: "Clients",
                table: "Client",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Wallet",
                schema: "Clients",
                table: "Client",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActivePoint",
                schema: "Products",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "NumberOfPoints",
                schema: "Products",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ItemPoint",
                schema: "OrderItems",
                table: "OrderItem");

            migrationBuilder.DropColumn(
                name: "Point",
                schema: "OrderItems",
                table: "OrderItem");

            migrationBuilder.DropColumn(
                name: "CancellationDate",
                schema: "Orders",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "CompletedDate",
                schema: "Orders",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "ConfirmationDate",
                schema: "Orders",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "DeliveryDate",
                schema: "Orders",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "InProcessDate",
                schema: "Orders",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "TotalNumberOfPoints",
                schema: "Orders",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "AmountOfMoney",
                schema: "Companies",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "NumberOfPoints",
                schema: "Companies",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "NumberOfPoints",
                schema: "Clients",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "Wallet",
                schema: "Clients",
                table: "Client");
        }
    }
}
