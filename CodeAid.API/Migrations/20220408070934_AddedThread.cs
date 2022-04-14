using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeAid.API.Migrations
{
    public partial class AddedThread : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ThreadDate",
                table: "Threads",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThreadDate",
                table: "Threads");
        }
    }
}
