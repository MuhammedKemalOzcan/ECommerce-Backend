using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerceAPI.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig_6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Products_ProductId",
                table: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Files_ProductId",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Files");

            migrationBuilder.CreateTable(
                name: "ProductProductGallery",
                columns: table => new
                {
                    ProductGalleriesId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductProductGallery", x => new { x.ProductGalleriesId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_ProductProductGallery_Files_ProductGalleriesId",
                        column: x => x.ProductGalleriesId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductProductGallery_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductProductGallery_ProductId",
                table: "ProductProductGallery",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductProductGallery");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "Files",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Files_ProductId",
                table: "Files",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Products_ProductId",
                table: "Files",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
