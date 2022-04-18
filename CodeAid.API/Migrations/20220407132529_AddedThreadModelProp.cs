using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeAid.API.Migrations
{
    public partial class AddedThreadModelProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Threads",
                newName: "QuestionTitle");

            migrationBuilder.AddColumn<string>(
                name: "Question",
                table: "Threads",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "PostDate",
                table: "Messages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Question",
                table: "Threads");

            migrationBuilder.DropColumn(
                name: "PostDate",
                table: "Messages");

            migrationBuilder.RenameColumn(
                name: "QuestionTitle",
                table: "Threads",
                newName: "Name");
        }
    }
}
