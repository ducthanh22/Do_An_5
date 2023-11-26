using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Model.Migrations
{
    public partial class updateentitiesDBcontext5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdImportbillId",
                table: "Detail_importbill",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdExportbill",
                table: "Detail_exportbill",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdImportbillId",
                table: "Detail_importbill");

            migrationBuilder.DropColumn(
                name: "IdExportbill",
                table: "Detail_exportbill");
        }
    }
}
