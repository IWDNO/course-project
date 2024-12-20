using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComputerStore.Migrations
{
    /// <inheritdoc />
    public partial class AddWriteOffEntities2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WriteOffItems_Products_productId",
                table: "WriteOffItems");

            migrationBuilder.RenameColumn(
                name: "productId",
                table: "WriteOffItems",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_WriteOffItems_productId",
                table: "WriteOffItems",
                newName: "IX_WriteOffItems_ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_WriteOffItems_Products_ProductId",
                table: "WriteOffItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WriteOffItems_Products_ProductId",
                table: "WriteOffItems");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "WriteOffItems",
                newName: "productId");

            migrationBuilder.RenameIndex(
                name: "IX_WriteOffItems_ProductId",
                table: "WriteOffItems",
                newName: "IX_WriteOffItems_productId");

            migrationBuilder.AddForeignKey(
                name: "FK_WriteOffItems_Products_productId",
                table: "WriteOffItems",
                column: "productId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
