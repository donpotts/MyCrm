using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyCrm.Migrations
{
    /// <inheritdoc />
    public partial class Added_Vendor_Service_Relationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ServiceId",
                table: "AppVendors",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.CreateIndex(
                name: "IX_AppVendors_ServiceId",
                table: "AppVendors",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppVendors_AppServices_ServiceId",
                table: "AppVendors",
                column: "ServiceId",
                principalTable: "AppServices",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppVendors_AppServices_ServiceId",
                table: "AppVendors");

            migrationBuilder.DropIndex(
                name: "IX_AppVendors_ServiceId",
                table: "AppVendors");

            migrationBuilder.AlterColumn<int>(
                name: "ServiceId",
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
