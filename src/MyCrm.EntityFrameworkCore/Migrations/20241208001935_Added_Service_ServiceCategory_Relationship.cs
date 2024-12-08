using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyCrm.Migrations
{
    /// <inheritdoc />
    public partial class Added_Service_ServiceCategory_Relationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ServiceCategoryId",
                table: "AppServices",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.CreateIndex(
                name: "IX_AppServices_ServiceCategoryId",
                table: "AppServices",
                column: "ServiceCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppServices_AppServiceCategories_ServiceCategoryId",
                table: "AppServices",
                column: "ServiceCategoryId",
                principalTable: "AppServiceCategories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppServices_AppServiceCategories_ServiceCategoryId",
                table: "AppServices");

            migrationBuilder.DropIndex(
                name: "IX_AppServices_ServiceCategoryId",
                table: "AppServices");

            migrationBuilder.AlterColumn<int>(
                name: "ServiceCategoryId",
                table: "AppServices",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);
        }
    }
}
