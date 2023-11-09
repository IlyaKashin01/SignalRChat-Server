using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SignalRChat.Data.Migrations
{
    /// <inheritdoc />
    public partial class init4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_message_join_to_group_GroupMessages_message_id",
                table: "message_join_to_group");

            migrationBuilder.DropForeignKey(
                name: "FK_private_message_person_PersonId",
                table: "private_message");

            migrationBuilder.DropIndex(
                name: "IX_private_message_PersonId",
                table: "private_message");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupMessages",
                table: "GroupMessages");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "private_message");

            migrationBuilder.RenameTable(
                name: "GroupMessages",
                newName: "group_message");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "group_message",
                newName: "content");

            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                table: "group_message",
                newName: "updated_date");

            migrationBuilder.RenameColumn(
                name: "SentAt",
                table: "group_message",
                newName: "sent_at");

            migrationBuilder.RenameColumn(
                name: "IsCheck",
                table: "group_message",
                newName: "is_check");

            migrationBuilder.RenameColumn(
                name: "GroupId",
                table: "group_message",
                newName: "group_id");

            migrationBuilder.RenameColumn(
                name: "FromUserId",
                table: "group_message",
                newName: "from_person_id");

            migrationBuilder.RenameColumn(
                name: "DeleteDate",
                table: "group_message",
                newName: "delete_date");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "group_message",
                newName: "created_date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_date",
                table: "group_message",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_date",
                table: "group_message",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddPrimaryKey(
                name: "PK_group_message",
                table: "group_message",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "private_dialog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    dialog_is_opened_person_id = table.Column<int>(type: "integer", nullable: false),
                    PrivateMessageId = table.Column<int>(type: "integer", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    delete_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
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

            migrationBuilder.AddForeignKey(
                name: "FK_message_join_to_group_group_message_message_id",
                table: "message_join_to_group",
                column: "message_id",
                principalTable: "group_message",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_message_join_to_group_group_message_message_id",
                table: "message_join_to_group");

            migrationBuilder.DropTable(
                name: "private_dialog");

            migrationBuilder.DropPrimaryKey(
                name: "PK_group_message",
                table: "group_message");

            migrationBuilder.RenameTable(
                name: "group_message",
                newName: "GroupMessages");

            migrationBuilder.RenameColumn(
                name: "content",
                table: "GroupMessages",
                newName: "Content");

            migrationBuilder.RenameColumn(
                name: "updated_date",
                table: "GroupMessages",
                newName: "UpdatedDate");

            migrationBuilder.RenameColumn(
                name: "sent_at",
                table: "GroupMessages",
                newName: "SentAt");

            migrationBuilder.RenameColumn(
                name: "is_check",
                table: "GroupMessages",
                newName: "IsCheck");

            migrationBuilder.RenameColumn(
                name: "group_id",
                table: "GroupMessages",
                newName: "GroupId");

            migrationBuilder.RenameColumn(
                name: "from_person_id",
                table: "GroupMessages",
                newName: "FromUserId");

            migrationBuilder.RenameColumn(
                name: "delete_date",
                table: "GroupMessages",
                newName: "DeleteDate");

            migrationBuilder.RenameColumn(
                name: "created_date",
                table: "GroupMessages",
                newName: "CreatedDate");

            migrationBuilder.AddColumn<int>(
                name: "PersonId",
                table: "private_message",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "GroupMessages",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "GroupMessages",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupMessages",
                table: "GroupMessages",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_private_message_PersonId",
                table: "private_message",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_message_join_to_group_GroupMessages_message_id",
                table: "message_join_to_group",
                column: "message_id",
                principalTable: "GroupMessages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_private_message_person_PersonId",
                table: "private_message",
                column: "PersonId",
                principalTable: "person",
                principalColumn: "Id");
        }
    }
}
