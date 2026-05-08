using Golden_Crow.Models;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Golden_Crow.Database.Migrations
{
    /// <inheritdoc />
    public partial class multicurrency_accounts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "currency",
                table: "accounts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "USD");


 
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "currency",
                table: "accounts");
        }
    }
}
