using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ProfilePhotoUrl",
                schema: "clients",
                table: "Clients",
                type: "character varying",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying");

            migrationBuilder.AlterColumn<string>(
                name: "MobileNumber",
                schema: "clients",
                table: "Clients",
                type: "character varying",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying",
                oldMaxLength: 20);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ProfilePhotoUrl",
                schema: "clients",
                table: "Clients",
                type: "character varying",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MobileNumber",
                schema: "clients",
                table: "Clients",
                type: "character varying",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying",
                oldMaxLength: 20,
                oldNullable: true);
        }
    }
}
