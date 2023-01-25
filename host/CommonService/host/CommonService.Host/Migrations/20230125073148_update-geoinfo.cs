using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CommonService.Migrations
{
    public partial class updategeoinfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Aera",
                table: "GeoInfos");

            migrationBuilder.RenameColumn(
                name: "Town",
                table: "GeoInfos",
                newName: "Area");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Area",
                table: "GeoInfos",
                newName: "Town");

            migrationBuilder.AddColumn<string>(
                name: "Aera",
                table: "GeoInfos",
                type: "text",
                nullable: true);
        }
    }
}
