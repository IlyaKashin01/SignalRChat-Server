using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SignalRChat.Data.Migrations
{
    /// <inheritdoc />
    public partial class init5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "private_dialog");

            migrationBuilder.RenameColumn(
                name: "to_person_id",
                table: "private_message",
                newName: "sender_id");

            migrationBuilder.RenameColumn(
                name: "from_person_id",
                table: "private_message",
                newName: "recipient_id");

            migrationBuilder.AddColumn<int>(
                name: "PersonId",
                table: "private_message",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PersonId1",
                table: "private_message",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_private_message_PersonId",
                table: "private_message",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_private_message_PersonId1",
                table: "private_message",
                column: "PersonId1");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_private_message_person_PersonId",
                table: "private_message");

            migrationBuilder.DropForeignKey(
                name: "FK_private_message_person_PersonId1",
                table: "private_message");

            migrationBuilder.DropIndex(
                name: "IX_private_message_PersonId",
                table: "private_message");

            migrationBuilder.DropIndex(
                name: "IX_private_message_PersonId1",
                table: "private_message");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "private_message");

            migrationBuilder.DropColumn(
                name: "PersonId1",
                table: "private_message");

            migrationBuilder.RenameColumn(
                name: "sender_id",
                table: "private_message",
                newName: "to_person_id");

            migrationBuilder.RenameColumn(
                name: "recipient_id",
                table: "private_message",
                newName: "from_person_id");

            migrationBuilder.CreateTable(
                name: "private_dialog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    dialog_is_opened_person_id = table.Column<int>(type: "integer", nullable: false),
                    PrivateMessageId = table.Column<int>(type: "integer", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    delete_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_private_dialog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_private_dialog_person_dialog_is_opened_person_id",
                        column: x => x.dialog_is_opened_person_id,
                        principalTable: "person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_private_dialog_private_message_PrivateMessageId",
                        column: x => x.PrivateMessageId,
                        principalTable: "private_message",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_private_dialog_dialog_is_opened_person_id",
                table: "private_dialog",
                column: "dialog_is_opened_person_id");

            migrationBuilder.CreateIndex(
                name: "IX_private_dialog_PrivateMessageId",
                table: "private_dialog",
                column: "PrivateMessageId");
        }
    }
}
