using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Model.Migrations
{
    public partial class updateentitiesDBcontext4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Warehouse_entities",
                table: "Warehouse_entities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Staff_entities",
                table: "Staff_entities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products_entities",
                table: "Products_entities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Produces_entities",
                table: "Produces_entities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Price_entities",
                table: "Price_entities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Importbill_entities",
                table: "Importbill_entities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Exportbill_entities",
                table: "Exportbill_entities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Detail_warehouse_entities",
                table: "Detail_warehouse_entities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Detail_importbill_entities",
                table: "Detail_importbill_entities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Detail_exportbill_entities",
                table: "Detail_exportbill_entities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Custommer_entities",
                table: "Custommer_entities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categorie_entities",
                table: "Categorie_entities");

            migrationBuilder.RenameTable(
                name: "Warehouse_entities",
                newName: "Warehouse");

            migrationBuilder.RenameTable(
                name: "Staff_entities",
                newName: "Staff");

            migrationBuilder.RenameTable(
                name: "Products_entities",
                newName: "Products");

            migrationBuilder.RenameTable(
                name: "Produces_entities",
                newName: "Produces");

            migrationBuilder.RenameTable(
                name: "Price_entities",
                newName: "Price");

            migrationBuilder.RenameTable(
                name: "Importbill_entities",
                newName: "Importbill");

            migrationBuilder.RenameTable(
                name: "Exportbill_entities",
                newName: "Exportbill");

            migrationBuilder.RenameTable(
                name: "Detail_warehouse_entities",
                newName: "Detail_warehouse");

            migrationBuilder.RenameTable(
                name: "Detail_importbill_entities",
                newName: "Detail_importbill");

            migrationBuilder.RenameTable(
                name: "Detail_exportbill_entities",
                newName: "Detail_exportbill");

            migrationBuilder.RenameTable(
                name: "Custommer_entities",
                newName: "Custommer");

            migrationBuilder.RenameTable(
                name: "Categorie_entities",
                newName: "Categorie");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Warehouse",
                table: "Warehouse",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Staff",
                table: "Staff",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Produces",
                table: "Produces",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Price",
                table: "Price",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Importbill",
                table: "Importbill",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Exportbill",
                table: "Exportbill",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Detail_warehouse",
                table: "Detail_warehouse",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Detail_importbill",
                table: "Detail_importbill",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Detail_exportbill",
                table: "Detail_exportbill",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Custommer",
                table: "Custommer",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categorie",
                table: "Categorie",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_customer = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActiveFlag = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Order_detail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_Order = table.Column<int>(type: "int", nullable: false),
                    Id_product = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    ActiveFlag = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order_detail", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Order_detail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Warehouse",
                table: "Warehouse");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Staff",
                table: "Staff");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Produces",
                table: "Produces");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Price",
                table: "Price");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Importbill",
                table: "Importbill");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Exportbill",
                table: "Exportbill");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Detail_warehouse",
                table: "Detail_warehouse");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Detail_importbill",
                table: "Detail_importbill");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Detail_exportbill",
                table: "Detail_exportbill");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Custommer",
                table: "Custommer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categorie",
                table: "Categorie");

            migrationBuilder.RenameTable(
                name: "Warehouse",
                newName: "Warehouse_entities");

            migrationBuilder.RenameTable(
                name: "Staff",
                newName: "Staff_entities");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "Products_entities");

            migrationBuilder.RenameTable(
                name: "Produces",
                newName: "Produces_entities");

            migrationBuilder.RenameTable(
                name: "Price",
                newName: "Price_entities");

            migrationBuilder.RenameTable(
                name: "Importbill",
                newName: "Importbill_entities");

            migrationBuilder.RenameTable(
                name: "Exportbill",
                newName: "Exportbill_entities");

            migrationBuilder.RenameTable(
                name: "Detail_warehouse",
                newName: "Detail_warehouse_entities");

            migrationBuilder.RenameTable(
                name: "Detail_importbill",
                newName: "Detail_importbill_entities");

            migrationBuilder.RenameTable(
                name: "Detail_exportbill",
                newName: "Detail_exportbill_entities");

            migrationBuilder.RenameTable(
                name: "Custommer",
                newName: "Custommer_entities");

            migrationBuilder.RenameTable(
                name: "Categorie",
                newName: "Categorie_entities");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Warehouse_entities",
                table: "Warehouse_entities",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Staff_entities",
                table: "Staff_entities",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products_entities",
                table: "Products_entities",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Produces_entities",
                table: "Produces_entities",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Price_entities",
                table: "Price_entities",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Importbill_entities",
                table: "Importbill_entities",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Exportbill_entities",
                table: "Exportbill_entities",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Detail_warehouse_entities",
                table: "Detail_warehouse_entities",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Detail_importbill_entities",
                table: "Detail_importbill_entities",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Detail_exportbill_entities",
                table: "Detail_exportbill_entities",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Custommer_entities",
                table: "Custommer_entities",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categorie_entities",
                table: "Categorie_entities",
                column: "Id");
        }
    }
}
