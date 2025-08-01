using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddManyToManyRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "ProductEntries",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "UnitPrice",
                table: "ProductEntries",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "VendorProductId",
                table: "ProductEntries",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Category = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VendorProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VendorId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    VendorProductCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LeadTimeDays = table.Column<int>(type: "int", nullable: false),
                    MinOrderQuantity = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    IsPreferredVendor = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VendorProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VendorProducts_Vendors_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductEntries_ProductId",
                table: "ProductEntries",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductEntries_VendorProductId",
                table: "ProductEntries",
                column: "VendorProductId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorProducts_ProductId",
                table: "VendorProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorProducts_VendorId_ProductId",
                table: "VendorProducts",
                columns: new[] { "VendorId", "ProductId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductEntries_Products_ProductId",
                table: "ProductEntries",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductEntries_VendorProducts_VendorProductId",
                table: "ProductEntries",
                column: "VendorProductId",
                principalTable: "VendorProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductEntries_Products_ProductId",
                table: "ProductEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductEntries_VendorProducts_VendorProductId",
                table: "ProductEntries");

            migrationBuilder.DropTable(
                name: "VendorProducts");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropIndex(
                name: "IX_ProductEntries_ProductId",
                table: "ProductEntries");

            migrationBuilder.DropIndex(
                name: "IX_ProductEntries_VendorProductId",
                table: "ProductEntries");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ProductEntries");

            migrationBuilder.DropColumn(
                name: "UnitPrice",
                table: "ProductEntries");

            migrationBuilder.DropColumn(
                name: "VendorProductId",
                table: "ProductEntries");
        }
    }
}
