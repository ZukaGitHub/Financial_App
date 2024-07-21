using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations
{
    /// <inheritdoc />
    public partial class Cascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Clients_ClientId",
                schema: "misc",
                table: "Addresses");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Clients_ClientId",
                schema: "misc",
                table: "Addresses",
                column: "ClientId",
                principalSchema: "clients",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Clients_ClientId",
                schema: "misc",
                table: "Addresses");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Clients_ClientId",
                schema: "misc",
                table: "Addresses",
                column: "ClientId",
                principalSchema: "clients",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
