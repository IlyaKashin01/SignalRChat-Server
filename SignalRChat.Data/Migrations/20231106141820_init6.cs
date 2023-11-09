using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SignalRChat.Data.Migrations
{
    /// <inheritdoc />
    public partial class init6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_private_message_person_PersonId",
                table: "private_message");

            migrationBuilder.DropForeignKey(
                name: "FK_private_message_person_PersonId1",
                table: "private_message");

            migrationBuilder.DropPrimaryKey(
                name: "PK_private_message",
                table: "private_message");

            migrationBuilder.RenameTable(
                name: "private_message",
                newName: "personal_message");

            migrationBuilder.RenameIndex(
                name: "IX_private_message_PersonId1",
                table: "personal_message",
                newName: "IX_personal_message_PersonId1");

            migrationBuilder.RenameIndex(
                name: "IX_private_message_PersonId",
                table: "personal_message",
                newName: "IX_personal_message_PersonId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_personal_message",
                table: "personal_message",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_personal_message_person_PersonId",
                table: "personal_message",
                column: "PersonId",
                principalTable: "person",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_personal_message_person_PersonId1",
                table: "personal_message",
                column: "PersonId1",
                principalTable: "person",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_personal_message_person_PersonId",
                table: "personal_message");

            migrationBuilder.DropForeignKey(
                name: "FK_personal_message_person_PersonId1",
                table: "personal_message");

            migrationBuilder.DropPrimaryKey(
                name: "PK_personal_message",
                table: "personal_message");

            migrationBuilder.RenameTable(
                name: "personal_message",
                newName: "private_message");

            migrationBuilder.RenameIndex(
                name: "IX_personal_message_PersonId1",
                table: "private_message",
                newName: "IX_private_message_PersonId1");

            migrationBuilder.RenameIndex(
                name: "IX_personal_message_PersonId",
                table: "private_message",
                newName: "IX_private_message_PersonId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_private_message",
                table: "private_message",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_private_message_person_PersonId",
                table: "private_message",
                column: "PersonId",
                principalTable: "person",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_private_message_person_PersonId1",
                table: "private_message",
                column: "PersonId1",
                principalTable: "person",
                principalColumn: "Id");
        }
    }
}
