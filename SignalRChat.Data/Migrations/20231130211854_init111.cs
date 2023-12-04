using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SignalRChat.Data.Migrations
{
    /// <inheritdoc />
    public partial class init111 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_group_message_group_chat_room_GroupChatRoomId",
                table: "group_message");

            migrationBuilder.AlterColumn<int>(
                name: "GroupChatRoomId",
                table: "group_message",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_group_message_group_chat_room_GroupChatRoomId",
                table: "group_message",
                column: "GroupChatRoomId",
                principalTable: "group_chat_room",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_group_message_group_chat_room_GroupChatRoomId",
                table: "group_message");

            migrationBuilder.AlterColumn<int>(
                name: "GroupChatRoomId",
                table: "group_message",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_group_message_group_chat_room_GroupChatRoomId",
                table: "group_message",
                column: "GroupChatRoomId",
                principalTable: "group_chat_room",
                principalColumn: "Id");
        }
    }
}
