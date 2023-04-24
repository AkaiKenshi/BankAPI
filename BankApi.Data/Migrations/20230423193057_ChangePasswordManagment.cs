using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangePasswordManagment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Customers");

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordHash",
                table: "Customers",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSalt",
                table: "Customers",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "Customers");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Customers",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
