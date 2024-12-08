using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyCrm.Migrations
{
    /// <inheritdoc />
    public partial class Added_Lead_Address_Relationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "AddressId",
                table: "AppLeads",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.CreateIndex(
                name: "IX_AppLeads_AddressId",
                table: "AppLeads",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppLeads_AppAddresses_AddressId",
                table: "AppLeads",
                column: "AddressId",
                principalTable: "AppAddresses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppLeads_AppAddresses_AddressId",
                table: "AppLeads");

            migrationBuilder.DropIndex(
                name: "IX_AppLeads_AddressId",
                table: "AppLeads");

            migrationBuilder.AlterColumn<int>(
                name: "AddressId",
                table: "AppLeads",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);
        }
    }
}
