using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyCrm.Migrations
{
    /// <inheritdoc />
    public partial class Added_Contact_Reward_Relationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "RewardId",
                table: "AppContacts",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.CreateIndex(
                name: "IX_AppContacts_RewardId",
                table: "AppContacts",
                column: "RewardId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppContacts_AppRewards_RewardId",
                table: "AppContacts",
                column: "RewardId",
                principalTable: "AppRewards",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppContacts_AppRewards_RewardId",
                table: "AppContacts");

            migrationBuilder.DropIndex(
                name: "IX_AppContacts_RewardId",
                table: "AppContacts");

            migrationBuilder.AlterColumn<int>(
                name: "RewardId",
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
