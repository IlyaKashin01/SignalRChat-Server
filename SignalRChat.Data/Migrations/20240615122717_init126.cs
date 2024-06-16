using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SignalRChat.Data.Migrations
{
    /// <inheritdoc />
    public partial class init126 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ConnectionId",
                table: "person",
                newName: "email");

            migrationBuilder.AddColumn<string>(
                name: "Avatar",
                table: "person",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "person");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "person",
                newName: "ConnectionId");
        }
    }
}
