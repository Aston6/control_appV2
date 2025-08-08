using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyApp2.Migrations
{
    /// <inheritdoc />
    public partial class AddLastLoginTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastLoginTime",
                table: "Users",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastLoginTime",
                table: "Users");
        }
    }
}
