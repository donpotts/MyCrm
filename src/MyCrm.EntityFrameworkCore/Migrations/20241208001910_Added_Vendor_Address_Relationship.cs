using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyCrm.Migrations
{
    /// <inheritdoc />
    public partial class Added_Vendor_Address_Relationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "AddressId",
                table: "AppVendors",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.CreateIndex(
                name: "IX_AppVendors_AddressId",
                table: "AppVendors",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppVendors_AppAddresses_AddressId",
                table: "AppVendors",
                column: "AddressId",
                principalTable: "AppAddresses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppVendors_AppAddresses_AddressId",
                table: "AppVendors");

            migrationBuilder.DropIndex(
                name: "IX_AppVendors_AddressId",
                table: "AppVendors");

            migrationBuilder.AlterColumn<int>(
                name: "AddressId",
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
