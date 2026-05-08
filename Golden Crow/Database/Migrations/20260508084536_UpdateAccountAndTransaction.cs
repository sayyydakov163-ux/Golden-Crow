using Golden_Crow.Models;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Golden_Crow.Database.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAccountAndTransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@$"
                insert into accounts (user_id, currency, balance)
                select u.id, '{Currency.EUR}', 0
                from users u;
");

            migrationBuilder.Sql(@$"
                insert into accounts (user_id, currency, balance)
                select u.id, '{Currency.GBP}', 0
                from users u;
");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
