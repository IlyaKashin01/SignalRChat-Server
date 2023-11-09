using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SignalRChat.Data.Migrations
{
    /// <inheritdoc />
    public partial class init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatMessages_Users_PersonId",
                table: "ChatMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_MessageJoinToGroup_GroupMessage_MessageId",
                table: "MessageJoinToGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_MessageJoinToGroup_Groups_GroupId",
                table: "MessageJoinToGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonJoinToGroups_Groups_GroupId",
                table: "PersonJoinToGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonJoinToGroups_Users_PersonId",
                table: "PersonJoinToGroups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonJoinToGroups",
                table: "PersonJoinToGroups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MessageJoinToGroup",
                table: "MessageJoinToGroup");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Groups",
                table: "Groups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChatMessages",
                table: "ChatMessages");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "person");

            migrationBuilder.RenameTable(
                name: "PersonJoinToGroups",
                newName: "person_join_to_group");

            migrationBuilder.RenameTable(
                name: "MessageJoinToGroup",
                newName: "message_join_to_group");

            migrationBuilder.RenameTable(
                name: "Groups",
                newName: "group_chat_room");

            migrationBuilder.RenameTable(
                name: "ChatMessages",
                newName: "private_message");

            migrationBuilder.RenameColumn(
                name: "Role",
                table: "person",
                newName: "role");

            migrationBuilder.RenameColumn(
                name: "Login",
                table: "person",
                newName: "login");

            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                table: "person",
                newName: "updated_date");

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "person",
                newName: "password_hash");

            migrationBuilder.RenameColumn(
                name: "DeleteDate",
                table: "person",
                newName: "delete_date");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "person",
                newName: "created_date");

            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                table: "person_join_to_group",
                newName: "updated_date");

            migrationBuilder.RenameColumn(
                name: "PersonId",
                table: "person_join_to_group",
                newName: "person_id");

            migrationBuilder.RenameColumn(
                name: "GroupId",
                table: "person_join_to_group",
                newName: "group_id");

            migrationBuilder.RenameColumn(
                name: "DeleteDate",
                table: "person_join_to_group",
                newName: "delete_date");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "person_join_to_group",
                newName: "created_date");

            migrationBuilder.RenameIndex(
                name: "IX_PersonJoinToGroups_PersonId",
                table: "person_join_to_group",
                newName: "IX_person_join_to_group_person_id");

            migrationBuilder.RenameIndex(
                name: "IX_PersonJoinToGroups_GroupId",
                table: "person_join_to_group",
                newName: "IX_person_join_to_group_group_id");

            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                table: "message_join_to_group",
                newName: "updated_date");

            migrationBuilder.RenameColumn(
                name: "MessageId",
                table: "message_join_to_group",
                newName: "message_id");

            migrationBuilder.RenameColumn(
                name: "GroupId",
                table: "message_join_to_group",
                newName: "group_id");

            migrationBuilder.RenameColumn(
                name: "DeleteDate",
                table: "message_join_to_group",
                newName: "delete_date");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "message_join_to_group",
                newName: "created_date");

            migrationBuilder.RenameIndex(
                name: "IX_MessageJoinToGroup_MessageId",
                table: "message_join_to_group",
                newName: "IX_message_join_to_group_message_id");

            migrationBuilder.RenameIndex(
                name: "IX_MessageJoinToGroup_GroupId",
                table: "message_join_to_group",
                newName: "IX_message_join_to_group_group_id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "group_chat_room",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                table: "group_chat_room",
                newName: "updated_date");

            migrationBuilder.RenameColumn(
                name: "DeleteDate",
                table: "group_chat_room",
                newName: "delete_date");

            migrationBuilder.RenameColumn(
                name: "CreatorId",
                table: "group_chat_room",
                newName: "creator_id");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "group_chat_room",
                newName: "created_date");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "private_message",
                newName: "content");

            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                table: "private_message",
                newName: "updated_date");

            migrationBuilder.RenameColumn(
                name: "ToUserId",
                table: "private_message",
                newName: "to_person_id");

            migrationBuilder.RenameColumn(
                name: "SentAt",
                table: "private_message",
                newName: "sent_at");

            migrationBuilder.RenameColumn(
                name: "FromUserId",
                table: "private_message",
                newName: "from_person_id");

            migrationBuilder.RenameColumn(
                name: "DeleteDate",
                table: "private_message",
                newName: "delete_date");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "private_message",
                newName: "created_date");

            migrationBuilder.RenameIndex(
                name: "IX_ChatMessages_PersonId",
                table: "private_message",
                newName: "IX_private_message_PersonId");

            migrationBuilder.AddColumn<bool>(
                name: "IsCheck",
                table: "GroupMessage",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_date",
                table: "person",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_date",
                table: "person",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_date",
                table: "person_join_to_group",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_date",
                table: "person_join_to_group",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_date",
                table: "message_join_to_group",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_date",
                table: "message_join_to_group",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_date",
                table: "group_chat_room",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_date",
                table: "group_chat_room",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_date",
                table: "private_message",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_date",
                table: "private_message",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<bool>(
                name: "is_check",
                table: "private_message",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_person",
                table: "person",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_person_join_to_group",
                table: "person_join_to_group",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_message_join_to_group",
                table: "message_join_to_group",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_group_chat_room",
                table: "group_chat_room",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_private_message",
                table: "private_message",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_message_join_to_group_GroupMessage_message_id",
                table: "message_join_to_group",
                column: "message_id",
                principalTable: "GroupMessage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_message_join_to_group_group_chat_room_group_id",
                table: "message_join_to_group",
                column: "group_id",
                principalTable: "group_chat_room",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_person_join_to_group_group_chat_room_group_id",
                table: "person_join_to_group",
                column: "group_id",
                principalTable: "group_chat_room",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_person_join_to_group_person_person_id",
                table: "person_join_to_group",
                column: "person_id",
                principalTable: "person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_private_message_person_PersonId",
                table: "private_message",
                column: "PersonId",
                principalTable: "person",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_message_join_to_group_GroupMessage_message_id",
                table: "message_join_to_group");

            migrationBuilder.DropForeignKey(
                name: "FK_message_join_to_group_group_chat_room_group_id",
                table: "message_join_to_group");

            migrationBuilder.DropForeignKey(
                name: "FK_person_join_to_group_group_chat_room_group_id",
                table: "person_join_to_group");

            migrationBuilder.DropForeignKey(
                name: "FK_person_join_to_group_person_person_id",
                table: "person_join_to_group");

            migrationBuilder.DropForeignKey(
                name: "FK_private_message_person_PersonId",
                table: "private_message");

            migrationBuilder.DropPrimaryKey(
                name: "PK_private_message",
                table: "private_message");

            migrationBuilder.DropPrimaryKey(
                name: "PK_person_join_to_group",
                table: "person_join_to_group");

            migrationBuilder.DropPrimaryKey(
                name: "PK_person",
                table: "person");

            migrationBuilder.DropPrimaryKey(
                name: "PK_message_join_to_group",
                table: "message_join_to_group");

            migrationBuilder.DropPrimaryKey(
                name: "PK_group_chat_room",
                table: "group_chat_room");

            migrationBuilder.DropColumn(
                name: "IsCheck",
                table: "GroupMessage");

            migrationBuilder.DropColumn(
                name: "is_check",
                table: "private_message");

            migrationBuilder.RenameTable(
                name: "private_message",
                newName: "ChatMessages");

            migrationBuilder.RenameTable(
                name: "person_join_to_group",
                newName: "PersonJoinToGroups");

            migrationBuilder.RenameTable(
                name: "person",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "message_join_to_group",
                newName: "MessageJoinToGroup");

            migrationBuilder.RenameTable(
                name: "group_chat_room",
                newName: "Groups");

            migrationBuilder.RenameColumn(
                name: "content",
                table: "ChatMessages",
                newName: "Content");

            migrationBuilder.RenameColumn(
                name: "updated_date",
                table: "ChatMessages",
                newName: "UpdatedDate");

            migrationBuilder.RenameColumn(
                name: "to_person_id",
                table: "ChatMessages",
                newName: "ToUserId");

            migrationBuilder.RenameColumn(
                name: "sent_at",
                table: "ChatMessages",
                newName: "SentAt");

            migrationBuilder.RenameColumn(
                name: "from_person_id",
                table: "ChatMessages",
                newName: "FromUserId");

            migrationBuilder.RenameColumn(
                name: "delete_date",
                table: "ChatMessages",
                newName: "DeleteDate");

            migrationBuilder.RenameColumn(
                name: "created_date",
                table: "ChatMessages",
                newName: "CreatedDate");

            migrationBuilder.RenameIndex(
                name: "IX_private_message_PersonId",
                table: "ChatMessages",
                newName: "IX_ChatMessages_PersonId");

            migrationBuilder.RenameColumn(
                name: "updated_date",
                table: "PersonJoinToGroups",
                newName: "UpdatedDate");

            migrationBuilder.RenameColumn(
                name: "person_id",
                table: "PersonJoinToGroups",
                newName: "PersonId");

            migrationBuilder.RenameColumn(
                name: "group_id",
                table: "PersonJoinToGroups",
                newName: "GroupId");

            migrationBuilder.RenameColumn(
                name: "delete_date",
                table: "PersonJoinToGroups",
                newName: "DeleteDate");

            migrationBuilder.RenameColumn(
                name: "created_date",
                table: "PersonJoinToGroups",
                newName: "CreatedDate");

            migrationBuilder.RenameIndex(
                name: "IX_person_join_to_group_person_id",
                table: "PersonJoinToGroups",
                newName: "IX_PersonJoinToGroups_PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_person_join_to_group_group_id",
                table: "PersonJoinToGroups",
                newName: "IX_PersonJoinToGroups_GroupId");

            migrationBuilder.RenameColumn(
                name: "role",
                table: "Users",
                newName: "Role");

            migrationBuilder.RenameColumn(
                name: "login",
                table: "Users",
                newName: "Login");

            migrationBuilder.RenameColumn(
                name: "updated_date",
                table: "Users",
                newName: "UpdatedDate");

            migrationBuilder.RenameColumn(
                name: "password_hash",
                table: "Users",
                newName: "PasswordHash");

            migrationBuilder.RenameColumn(
                name: "delete_date",
                table: "Users",
                newName: "DeleteDate");

            migrationBuilder.RenameColumn(
                name: "created_date",
                table: "Users",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "updated_date",
                table: "MessageJoinToGroup",
                newName: "UpdatedDate");

            migrationBuilder.RenameColumn(
                name: "message_id",
                table: "MessageJoinToGroup",
                newName: "MessageId");

            migrationBuilder.RenameColumn(
                name: "group_id",
                table: "MessageJoinToGroup",
                newName: "GroupId");

            migrationBuilder.RenameColumn(
                name: "delete_date",
                table: "MessageJoinToGroup",
                newName: "DeleteDate");

            migrationBuilder.RenameColumn(
                name: "created_date",
                table: "MessageJoinToGroup",
                newName: "CreatedDate");

            migrationBuilder.RenameIndex(
                name: "IX_message_join_to_group_message_id",
                table: "MessageJoinToGroup",
                newName: "IX_MessageJoinToGroup_MessageId");

            migrationBuilder.RenameIndex(
                name: "IX_message_join_to_group_group_id",
                table: "MessageJoinToGroup",
                newName: "IX_MessageJoinToGroup_GroupId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Groups",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "updated_date",
                table: "Groups",
                newName: "UpdatedDate");

            migrationBuilder.RenameColumn(
                name: "delete_date",
                table: "Groups",
                newName: "DeleteDate");

            migrationBuilder.RenameColumn(
                name: "creator_id",
                table: "Groups",
                newName: "CreatorId");

            migrationBuilder.RenameColumn(
                name: "created_date",
                table: "Groups",
                newName: "CreatedDate");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "ChatMessages",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "ChatMessages",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "PersonJoinToGroups",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "PersonJoinToGroups",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Users",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Users",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "MessageJoinToGroup",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "MessageJoinToGroup",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Groups",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Groups",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChatMessages",
                table: "ChatMessages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonJoinToGroups",
                table: "PersonJoinToGroups",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MessageJoinToGroup",
                table: "MessageJoinToGroup",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Groups",
                table: "Groups",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMessages_Users_PersonId",
                table: "ChatMessages",
                column: "PersonId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MessageJoinToGroup_GroupMessage_MessageId",
                table: "MessageJoinToGroup",
                column: "MessageId",
                principalTable: "GroupMessage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MessageJoinToGroup_Groups_GroupId",
                table: "MessageJoinToGroup",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonJoinToGroups_Groups_GroupId",
                table: "PersonJoinToGroups",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonJoinToGroups_Users_PersonId",
                table: "PersonJoinToGroups",
                column: "PersonId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
