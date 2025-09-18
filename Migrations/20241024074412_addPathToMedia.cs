﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KOG.ECommerce.Migrations
{
    /// <inheritdoc />
    public partial class addPathToMedia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Path",
                schema: "Medias",
                table: "Media",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Path",
                schema: "Medias",
                table: "Media");
        }
    }
}
