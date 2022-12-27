using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BaseService.Migrations
{
    public partial class CreateUserExtesion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
               name: "RynclerUserExtensions",
               columns: table => new
               {
                   UserId = table.Column<Guid>(type: "uuid", nullable: false),
                   Avatar = table.Column<string>(type: "text", nullable: true),
                   Profile = table.Column<string>(type: "text", nullable: true),
                   IsPushCommentMessage = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                   IsPushPrivateMessage = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                   CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                   CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                   LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                   LastModifierId = table.Column<Guid>(type: "uuid", nullable: true)
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_RynclerUserExtensions", x => x.UserId);
               });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RynclerUserExtensions");
        }
    }
}
