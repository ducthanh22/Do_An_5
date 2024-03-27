using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Model.Migrations
{
    public partial class updateDBcontext3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Idsize",
                table: "Order_detail",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Idsize",
                table: "Detail_warehouse",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Idsize",
                table: "Detail_importbill",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Idsize",
                table: "Detail_exportbill",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Idsize",
                table: "Order_detail");

            migrationBuilder.DropColumn(
                name: "Idsize",
                table: "Detail_warehouse");

            migrationBuilder.DropColumn(
                name: "Idsize",
                table: "Detail_importbill");

            migrationBuilder.DropColumn(
                name: "Idsize",
                table: "Detail_exportbill");
        }
    }
}
