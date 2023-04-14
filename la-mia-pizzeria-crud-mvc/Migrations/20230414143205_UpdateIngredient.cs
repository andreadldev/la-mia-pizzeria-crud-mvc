using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace la_mia_pizzeria_crud_mvc.Migrations
{
    /// <inheritdoc />
    public partial class UpdateIngredient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pizzas_Ingredients_IngredientId",
                table: "Pizzas");

            migrationBuilder.DropIndex(
                name: "IX_Pizzas_IngredientId",
                table: "Pizzas");

            migrationBuilder.DropColumn(
                name: "IngredientId",
                table: "Pizzas");

            migrationBuilder.CreateTable(
                name: "IngredientPizza",
                columns: table => new
                {
                    IngredientsId = table.Column<long>(type: "bigint", nullable: false),
                    PizzasId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientPizza", x => new { x.IngredientsId, x.PizzasId });
                    table.ForeignKey(
                        name: "FK_IngredientPizza_Ingredients_IngredientsId",
                        column: x => x.IngredientsId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngredientPizza_Pizzas_PizzasId",
                        column: x => x.PizzasId,
                        principalTable: "Pizzas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IngredientPizza_PizzasId",
                table: "IngredientPizza",
                column: "PizzasId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IngredientPizza");

            migrationBuilder.AddColumn<long>(
                name: "IngredientId",
                table: "Pizzas",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pizzas_IngredientId",
                table: "Pizzas",
                column: "IngredientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pizzas_Ingredients_IngredientId",
                table: "Pizzas",
                column: "IngredientId",
                principalTable: "Ingredients",
                principalColumn: "Id");
        }
    }
}
