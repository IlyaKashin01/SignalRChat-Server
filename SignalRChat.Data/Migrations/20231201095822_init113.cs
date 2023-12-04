using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SignalRChat.Data.Migrations
{
    /// <inheritdoc />
    public partial class init113 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_personal_message_person_PersonId",
                table: "personal_message");

            migrationBuilder.DropIndex(
                name: "IX_personal_message_PersonId",
                table: "personal_message");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "personal_message");

            migrationBuilder.CreateIndex(
                name: "IX_personal_message_recipient_id",
                table: "personal_message",
                column: "recipient_id");

            migrationBuilder.AddForeignKey(
                name: "FK_personal_message_person_recipient_id",
                table: "personal_message",
                column: "recipient_id",
                principalTable: "person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_personal_message_person_recipient_id",
                table: "personal_message");

            migrationBuilder.DropIndex(
                name: "IX_personal_message_recipient_id",
                table: "personal_message");

            migrationBuilder.AddColumn<int>(
                name: "PersonId",
                table: "personal_message",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_personal_message_PersonId",
                table: "personal_message",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_personal_message_person_PersonId",
                table: "personal_message",
                column: "PersonId",
                principalTable: "person",
                principalColumn: "Id");
        }
    }
}
