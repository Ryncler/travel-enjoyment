using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SightsService.Migrations
{
    public partial class addsightsTag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SightsTags",
                columns: table => new
                {
                    SightsId = table.Column<Guid>(type: "uuid", nullable: false),
                    TagId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SightsTags", x => new { x.SightsId, x.TagId });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SightsTags");
        }
    }
}
