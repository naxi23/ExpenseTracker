using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpenseTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class change_structure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_SubCategories_SubCategoryID",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Categories_CategoryID",
                table: "Expenses");

            migrationBuilder.DropIndex(
                name: "IX_Categories_SubCategoryID",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "SubCategoryID",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "CategoryID",
                table: "Expenses",
                newName: "SubCategoryID");

            migrationBuilder.RenameIndex(
                name: "IX_Expenses_CategoryID",
                table: "Expenses",
                newName: "IX_Expenses_SubCategoryID");

            migrationBuilder.AddColumn<int>(
                name: "CategoryID",
                table: "SubCategories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SubCategories_CategoryID",
                table: "SubCategories",
                column: "CategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_SubCategories_SubCategoryID",
                table: "Expenses",
                column: "SubCategoryID",
                principalTable: "SubCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubCategories_Categories_CategoryID",
                table: "SubCategories",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_SubCategories_SubCategoryID",
                table: "Expenses");

            migrationBuilder.DropForeignKey(
                name: "FK_SubCategories_Categories_CategoryID",
                table: "SubCategories");

            migrationBuilder.DropIndex(
                name: "IX_SubCategories_CategoryID",
                table: "SubCategories");

            migrationBuilder.DropColumn(
                name: "CategoryID",
                table: "SubCategories");

            migrationBuilder.RenameColumn(
                name: "SubCategoryID",
                table: "Expenses",
                newName: "CategoryID");

            migrationBuilder.RenameIndex(
                name: "IX_Expenses_SubCategoryID",
                table: "Expenses",
                newName: "IX_Expenses_CategoryID");

            migrationBuilder.AddColumn<int>(
                name: "SubCategoryID",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_SubCategoryID",
                table: "Categories",
                column: "SubCategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_SubCategories_SubCategoryID",
                table: "Categories",
                column: "SubCategoryID",
                principalTable: "SubCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Categories_CategoryID",
                table: "Expenses",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
