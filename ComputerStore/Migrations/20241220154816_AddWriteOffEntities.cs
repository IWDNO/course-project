using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComputerStore.Migrations
{
    /// <inheritdoc />
    public partial class AddWriteOffEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WriteOffs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Reason = table.Column<string>(type: "text", nullable: false),
                    WriteOffDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ManagerId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WriteOffs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WriteOffs_AspNetUsers_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WriteOffItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    productId = table.Column<Guid>(type: "uuid", nullable: false),
                    WriteOffId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WriteOffItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WriteOffItems_Products_productId",
                        column: x => x.productId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WriteOffItems_WriteOffs_WriteOffId",
                        column: x => x.WriteOffId,
                        principalTable: "WriteOffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WriteOffItems_productId",
                table: "WriteOffItems",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_WriteOffItems_WriteOffId",
                table: "WriteOffItems",
                column: "WriteOffId");

            migrationBuilder.CreateIndex(
                name: "IX_WriteOffs_ManagerId",
                table: "WriteOffs",
                column: "ManagerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WriteOffItems");

            migrationBuilder.DropTable(
                name: "WriteOffs");
        }
    }
}
