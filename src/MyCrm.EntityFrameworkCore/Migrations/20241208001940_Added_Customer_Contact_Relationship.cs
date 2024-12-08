using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyCrm.Migrations
{
    /// <inheritdoc />
    public partial class Added_Customer_Contact_Relationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ContactId",
                table: "AppCustomers",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.CreateIndex(
                name: "IX_AppCustomers_ContactId",
                table: "AppCustomers",
                column: "ContactId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppCustomers_AppContacts_ContactId",
                table: "AppCustomers",
                column: "ContactId",
                principalTable: "AppContacts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppCustomers_AppContacts_ContactId",
                table: "AppCustomers");

            migrationBuilder.DropIndex(
                name: "IX_AppCustomers_ContactId",
                table: "AppCustomers");

            migrationBuilder.AlterColumn<int>(
                name: "ContactId",
                table: "AppCustomers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);
        }
    }
}
