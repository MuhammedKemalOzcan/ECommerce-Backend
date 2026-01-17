using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerceAPI.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig_17 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    OrderDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    SubTotal = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    TaxAmount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    ShippingCost = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    GrandTotal = table.Column<decimal>(type: "numeric", nullable: false),
                    ShippingAddress_Street = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ShippingAddress_City = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ShippingAddress_Country = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    ShippingAddress_ZipCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    BillingAddress_Street = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    BillingAddress_City = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    BillingAddress_Country = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    BillingAddress_ZipCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    PaymentInfo_PaymentId = table.Column<string>(type: "text", nullable: false),
                    PaymentInfo_PaymentType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    PaymentInfo_Installment = table.Column<int>(type: "integer", nullable: false),
                    PaymentInfo_CardAssociation = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    PaymentInfo_CardFamily = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    PaymentInfo_CardLastFourDigits = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Price = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItem_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItem_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_OrderId",
                table: "OrderItem",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_ProductId",
                table: "OrderItem",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItem");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
