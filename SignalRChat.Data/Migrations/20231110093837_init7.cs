using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SignalRChat.Data.Migrations
{
    /// <inheritdoc />
    public partial class init7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_personal_message_person_PersonId1",
                table: "personal_message");

            migrationBuilder.DropTable(
                name: "message_join_to_group");

            migrationBuilder.DropIndex(
                name: "IX_personal_message_PersonId1",
                table: "personal_message");

            migrationBuilder.DropColumn(
                name: "PersonId1",
                table: "personal_message");

            migrationBuilder.AddColumn<int>(
                name: "GroupMemberId",
                table: "person",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GroupChatRoomId",
                table: "group_message",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "group_member",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    group_id = table.Column<int>(type: "integer", nullable: false),
                    added_by_person = table.Column<int>(type: "integer", nullable: false),
                    added_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    delete_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_group_member", x => x.Id);
                    table.ForeignKey(
                        name: "FK_group_member_group_chat_room_group_id",
                        column: x => x.group_id,
                        principalTable: "group_chat_room",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_person_GroupMemberId",
                table: "person",
                column: "GroupMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_group_message_GroupChatRoomId",
                table: "group_message",
                column: "GroupChatRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_group_member_group_id",
                table: "group_member",
                column: "group_id");

            migrationBuilder.AddForeignKey(
                name: "FK_group_message_group_chat_room_GroupChatRoomId",
                table: "group_message",
                column: "GroupChatRoomId",
                principalTable: "group_chat_room",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_person_group_member_GroupMemberId",
                table: "person",
                column: "GroupMemberId",
                principalTable: "group_member",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_group_message_group_chat_room_GroupChatRoomId",
                table: "group_message");

            migrationBuilder.DropForeignKey(
                name: "FK_person_group_member_GroupMemberId",
                table: "person");

            migrationBuilder.DropTable(
                name: "group_member");

            migrationBuilder.DropIndex(
                name: "IX_person_GroupMemberId",
                table: "person");

            migrationBuilder.DropIndex(
                name: "IX_group_message_GroupChatRoomId",
                table: "group_message");

            migrationBuilder.DropColumn(
                name: "GroupMemberId",
                table: "person");

            migrationBuilder.DropColumn(
                name: "GroupChatRoomId",
                table: "group_message");

            migrationBuilder.AddColumn<int>(
                name: "PersonId1",
                table: "personal_message",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "message_join_to_group",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    group_id = table.Column<int>(type: "integer", nullable: false),
                    message_id = table.Column<int>(type: "integer", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    delete_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_message_join_to_group", x => x.Id);
                    table.ForeignKey(
                        name: "FK_message_join_to_group_group_chat_room_group_id",
                        column: x => x.group_id,
                        principalTable: "group_chat_room",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_message_join_to_group_group_message_message_id",
                        column: x => x.message_id,
                        principalTable: "group_message",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_personal_message_PersonId1",
                table: "personal_message",
                column: "PersonId1");

            migrationBuilder.CreateIndex(
                name: "IX_message_join_to_group_group_id",
                table: "message_join_to_group",
                column: "group_id");

            migrationBuilder.CreateIndex(
                name: "IX_message_join_to_group_message_id",
                table: "message_join_to_group",
                column: "message_id");

            migrationBuilder.AddForeignKey(
                name: "FK_personal_message_person_PersonId1",
                table: "personal_message",
                column: "PersonId1",
                principalTable: "person",
                principalColumn: "Id");
        }
    }
}
