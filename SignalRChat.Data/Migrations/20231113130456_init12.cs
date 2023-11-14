using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SignalRChat.Data.Migrations
{
    /// <inheritdoc />
    public partial class init12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_group_chat_room_person_PersonId",
                table: "group_chat_room");

            migrationBuilder.DropForeignKey(
                name: "FK_person_group_member_GroupMemberId",
                table: "person");

            migrationBuilder.DropIndex(
                name: "IX_person_GroupMemberId",
                table: "person");

            migrationBuilder.DropIndex(
                name: "IX_group_chat_room_PersonId",
                table: "group_chat_room");

            migrationBuilder.DropColumn(
                name: "GroupMemberId",
                table: "person");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "group_chat_room");

            migrationBuilder.AddColumn<int>(
                name: "PersonId",
                table: "group_member",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_group_member_PersonId",
                table: "group_member",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_group_chat_room_creator_id",
                table: "group_chat_room",
                column: "creator_id");

            migrationBuilder.AddForeignKey(
                name: "FK_group_chat_room_person_creator_id",
                table: "group_chat_room",
                column: "creator_id",
                principalTable: "person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_group_member_person_PersonId",
                table: "group_member",
                column: "PersonId",
                principalTable: "person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_group_chat_room_person_creator_id",
                table: "group_chat_room");

            migrationBuilder.DropForeignKey(
                name: "FK_group_member_person_PersonId",
                table: "group_member");

            migrationBuilder.DropIndex(
                name: "IX_group_member_PersonId",
                table: "group_member");

            migrationBuilder.DropIndex(
                name: "IX_group_chat_room_creator_id",
                table: "group_chat_room");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "group_member");

            migrationBuilder.AddColumn<int>(
                name: "GroupMemberId",
                table: "person",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PersonId",
                table: "group_chat_room",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_person_GroupMemberId",
                table: "person",
                column: "GroupMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_group_chat_room_PersonId",
                table: "group_chat_room",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_group_chat_room_person_PersonId",
                table: "group_chat_room",
                column: "PersonId",
                principalTable: "person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_person_group_member_GroupMemberId",
                table: "person",
                column: "GroupMemberId",
                principalTable: "group_member",
                principalColumn: "Id");
        }
    }
}
