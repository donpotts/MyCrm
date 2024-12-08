using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyCrm.Migrations
{
    /// <inheritdoc />
    public partial class Added_Product_ProductCategory_Relationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ProductCategoryId",
                table: "AppProducts",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.CreateIndex(
                name: "IX_AppProducts_ProductCategoryId",
                table: "AppProducts",
                column: "ProductCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppProducts_AppProductCategories_ProductCategoryId",
                table: "AppProducts",
                column: "ProductCategoryId",
                principalTable: "AppProductCategories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppProducts_AppProductCategories_ProductCategoryId",
                table: "AppProducts");

            migrationBuilder.DropIndex(
                name: "IX_AppProducts_ProductCategoryId",
                table: "AppProducts");

            migrationBuilder.AlterColumn<int>(
                name: "ProductCategoryId",
                table: "AppProducts",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);
        }
    }
}
