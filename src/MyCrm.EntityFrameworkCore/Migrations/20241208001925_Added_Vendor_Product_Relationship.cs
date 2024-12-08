using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyCrm.Migrations
{
    /// <inheritdoc />
    public partial class Added_Vendor_Product_Relationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "AppVendors",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.CreateIndex(
                name: "IX_AppVendors_ProductId",
                table: "AppVendors",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppVendors_AppProducts_ProductId",
                table: "AppVendors",
                column: "ProductId",
                principalTable: "AppProducts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppVendors_AppProducts_ProductId",
                table: "AppVendors");

            migrationBuilder.DropIndex(
                name: "IX_AppVendors_ProductId",
                table: "AppVendors");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "AppVendors",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);
        }
    }
}
