using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiComparison.EfCore.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Generic_Id_Add : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DishIngredient_Dish_DishIngredientsDishId",
                table: "DishIngredient");

            migrationBuilder.DropForeignKey(
                name: "FK_DishIngredient_Ingredient_DishIngredientsIngredientId",
                table: "DishIngredient");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "User");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Ingredient");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Dish");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Account");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "User",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "IngredientId",
                table: "Ingredient",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "DishIngredientsIngredientId",
                table: "DishIngredient",
                newName: "DishIngredientsId1");

            migrationBuilder.RenameColumn(
                name: "DishIngredientsDishId",
                table: "DishIngredient",
                newName: "DishIngredientsId");

            migrationBuilder.RenameIndex(
                name: "IX_DishIngredient_DishIngredientsIngredientId",
                table: "DishIngredient",
                newName: "IX_DishIngredient_DishIngredientsId1");

            migrationBuilder.RenameColumn(
                name: "DishId",
                table: "Dish",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "AddressId",
                table: "Address",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "Account",
                newName: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DishIngredient_Dish_DishIngredientsId",
                table: "DishIngredient",
                column: "DishIngredientsId",
                principalTable: "Dish",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DishIngredient_Ingredient_DishIngredientsId1",
                table: "DishIngredient",
                column: "DishIngredientsId1",
                principalTable: "Ingredient",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DishIngredient_Dish_DishIngredientsId",
                table: "DishIngredient");

            migrationBuilder.DropForeignKey(
                name: "FK_DishIngredient_Ingredient_DishIngredientsId1",
                table: "DishIngredient");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "User",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Ingredient",
                newName: "IngredientId");

            migrationBuilder.RenameColumn(
                name: "DishIngredientsId1",
                table: "DishIngredient",
                newName: "DishIngredientsIngredientId");

            migrationBuilder.RenameColumn(
                name: "DishIngredientsId",
                table: "DishIngredient",
                newName: "DishIngredientsDishId");

            migrationBuilder.RenameIndex(
                name: "IX_DishIngredient_DishIngredientsId1",
                table: "DishIngredient",
                newName: "IX_DishIngredient_DishIngredientsIngredientId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Dish",
                newName: "DishId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Address",
                newName: "AddressId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Account",
                newName: "AccountId");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "User",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Ingredient",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Dish",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Address",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Account",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_DishIngredient_Dish_DishIngredientsDishId",
                table: "DishIngredient",
                column: "DishIngredientsDishId",
                principalTable: "Dish",
                principalColumn: "DishId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DishIngredient_Ingredient_DishIngredientsIngredientId",
                table: "DishIngredient",
                column: "DishIngredientsIngredientId",
                principalTable: "Ingredient",
                principalColumn: "IngredientId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
