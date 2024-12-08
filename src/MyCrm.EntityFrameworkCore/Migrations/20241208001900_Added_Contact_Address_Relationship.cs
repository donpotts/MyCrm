using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyCrm.Migrations
{
    /// <inheritdoc />
    public partial class Added_Contact_Address_Relationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "AddressId",
                table: "AppContacts",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.CreateIndex(
                name: "IX_AppContacts_AddressId",
                table: "AppContacts",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppContacts_AppAddresses_AddressId",
                table: "AppContacts",
                column: "AddressId",
                principalTable: "AppAddresses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppContacts_AppAddresses_AddressId",
                table: "AppContacts");

            migrationBuilder.DropIndex(
                name: "IX_AppContacts_AddressId",
                table: "AppContacts");

            migrationBuilder.AlterColumn<int>(
                name: "AddressId",
                table: "AppContacts",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);
        }
    }
}
