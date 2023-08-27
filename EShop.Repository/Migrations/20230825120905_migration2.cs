using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.Repository.Migrations
{
    public partial class migration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ConcertInShoppingCarts",
                table: "ConcertInShoppingCarts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ConcertInOrders",
                table: "ConcertInOrders");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ConcertInShoppingCarts",
                table: "ConcertInShoppingCarts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ConcertInOrders",
                table: "ConcertInOrders",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ConcertInShoppingCarts_ConcertId",
                table: "ConcertInShoppingCarts",
                column: "ConcertId");

            migrationBuilder.CreateIndex(
                name: "IX_ConcertInOrders_ConcertId",
                table: "ConcertInOrders",
                column: "ConcertId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ConcertInShoppingCarts",
                table: "ConcertInShoppingCarts");

            migrationBuilder.DropIndex(
                name: "IX_ConcertInShoppingCarts_ConcertId",
                table: "ConcertInShoppingCarts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ConcertInOrders",
                table: "ConcertInOrders");

            migrationBuilder.DropIndex(
                name: "IX_ConcertInOrders_ConcertId",
                table: "ConcertInOrders");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ConcertInShoppingCarts",
                table: "ConcertInShoppingCarts",
                columns: new[] { "ConcertId", "ShoppingCartId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ConcertInOrders",
                table: "ConcertInOrders",
                columns: new[] { "ConcertId", "OrderId" });
        }
    }
}
