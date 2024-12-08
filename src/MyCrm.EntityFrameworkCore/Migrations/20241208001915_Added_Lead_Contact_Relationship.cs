using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyCrm.Migrations
{
    /// <inheritdoc />
    public partial class Added_Lead_Contact_Relationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ContactId",
                table: "AppLeads",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.CreateIndex(
                name: "IX_AppLeads_ContactId",
                table: "AppLeads",
                column: "ContactId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppLeads_AppContacts_ContactId",
                table: "AppLeads",
                column: "ContactId",
                principalTable: "AppContacts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppLeads_AppContacts_ContactId",
                table: "AppLeads");

            migrationBuilder.DropIndex(
                name: "IX_AppLeads_ContactId",
                table: "AppLeads");

            migrationBuilder.AlterColumn<int>(
                name: "ContactId",
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
