using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeAid.API.Migrations
{
    public partial class MadeThreadsPropNullableInInterestModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserInterestModel_Interests_InterestId",
                table: "UserInterestModel");

            migrationBuilder.DropForeignKey(
                name: "FK_UserInterestModel_Users_UserId",
                table: "UserInterestModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserInterestModel",
                table: "UserInterestModel");

            migrationBuilder.RenameTable(
                name: "UserInterestModel",
                newName: "UserInterests");

            migrationBuilder.RenameIndex(
                name: "IX_UserInterestModel_InterestId",
                table: "UserInterests",
                newName: "IX_UserInterests_InterestId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserInterests",
                table: "UserInterests",
                columns: new[] { "UserId", "InterestId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserInterests_Interests_InterestId",
                table: "UserInterests",
                column: "InterestId",
                principalTable: "Interests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserInterests_Users_UserId",
                table: "UserInterests",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserInterests_Interests_InterestId",
                table: "UserInterests");

            migrationBuilder.DropForeignKey(
                name: "FK_UserInterests_Users_UserId",
                table: "UserInterests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserInterests",
                table: "UserInterests");

            migrationBuilder.RenameTable(
                name: "UserInterests",
                newName: "UserInterestModel");

            migrationBuilder.RenameIndex(
                name: "IX_UserInterests_InterestId",
                table: "UserInterestModel",
                newName: "IX_UserInterestModel_InterestId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserInterestModel",
                table: "UserInterestModel",
                columns: new[] { "UserId", "InterestId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserInterestModel_Interests_InterestId",
                table: "UserInterestModel",
                column: "InterestId",
                principalTable: "Interests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserInterestModel_Users_UserId",
                table: "UserInterestModel",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
