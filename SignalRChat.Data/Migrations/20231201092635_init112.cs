using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SignalRChat.Data.Migrations
{
    /// <inheritdoc />
    public partial class init112 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_group_message_group_chat_room_GroupChatRoomId",
                table: "group_message");

            migrationBuilder.DropIndex(
                name: "IX_group_message_GroupChatRoomId",
                table: "group_message");

            migrationBuilder.DropColumn(
                name: "GroupChatRoomId",
                table: "group_message");

            migrationBuilder.CreateIndex(
                name: "IX_group_message_group_id",
                table: "group_message",
                column: "group_id");

            migrationBuilder.AddForeignKey(
                name: "FK_group_message_group_chat_room_group_id",
                table: "group_message",
                column: "group_id",
                principalTable: "group_chat_room",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_group_message_group_chat_room_group_id",
                table: "group_message");

            migrationBuilder.DropIndex(
                name: "IX_group_message_group_id",
                table: "group_message");

            migrationBuilder.AddColumn<int>(
                name: "GroupChatRoomId",
                table: "group_message",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_group_message_GroupChatRoomId",
                table: "group_message",
                column: "GroupChatRoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_group_message_group_chat_room_GroupChatRoomId",
                table: "group_message",
                column: "GroupChatRoomId",
                principalTable: "group_chat_room",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
