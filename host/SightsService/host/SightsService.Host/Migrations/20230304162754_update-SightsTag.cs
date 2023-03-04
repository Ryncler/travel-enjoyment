using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SightsService.Migrations
{
    public partial class updateSightsTag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SightsTags",
                table: "SightsTags");

            migrationBuilder.DropColumn(
                name: "TagId",
                table: "SightsTags");

            migrationBuilder.AddColumn<string>(
                name: "TagName",
                table: "SightsTags",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SightsTags",
                table: "SightsTags",
                columns: new[] { "SightsId", "TagName" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SightsTags",
                table: "SightsTags");

            migrationBuilder.DropColumn(
                name: "TagName",
                table: "SightsTags");

            migrationBuilder.AddColumn<Guid>(
                name: "TagId",
                table: "SightsTags",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_SightsTags",
                table: "SightsTags",
                columns: new[] { "SightsId", "TagId" });
        }
    }
}
