using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BethanysPieShop.Migrations
{
    /// <inheritdoc />
    public partial class ShoppingCartAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "shoppingCartItems",
                columns: table => new
                {
                    ShoppingCartItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PieId = table.Column<int>(type: "int", nullable: true),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    ShoppingCartId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shoppingCartItems", x => x.ShoppingCartItemId);
                    table.ForeignKey(
                        name: "FK_shoppingCartItems_Pies_PieId",
                        column: x => x.PieId,
                        principalTable: "Pies",
                        principalColumn: "PieId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_shoppingCartItems_PieId",
                table: "shoppingCartItems",
                column: "PieId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "shoppingCartItems");
        }
    }
}
