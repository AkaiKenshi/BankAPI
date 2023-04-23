using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedSequence : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "account_seq",
                minValue: 1L);

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Accounts",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                defaultValueSql: "lpad(nextval('account_seq')::VARCHAR(10), 10, '0')",
                oldClrType: typeof(string),
                oldType: "character varying(10)",
                oldMaxLength: 10);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Accounts",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(10)",
                oldMaxLength: 10,
                oldDefaultValueSql: "lpad(nextval('account_seq')::VARCHAR(10), 10, '0')");

            migrationBuilder.DropSequence(
                   name: "account_seq");
        }
    }
}
