using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SignalRChat.Data.Migrations
{
    /// <inheritdoc />
    public partial class init123 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "created_date",
                table: "personal_message");

            migrationBuilder.DropColumn(
                name: "created_date",
                table: "group_message");

            migrationBuilder.DropColumn(
                name: "created_date",
                table: "group_member");

            migrationBuilder.RenameColumn(
                name: "content",
                table: "personal_message",
                newName: "Content");

            migrationBuilder.RenameColumn(
                name: "updated_date",
                table: "personal_message",
                newName: "UpdatedDate");

            migrationBuilder.RenameColumn(
                name: "sent_at",
                table: "personal_message",
                newName: "SentAt");

            migrationBuilder.RenameColumn(
                name: "sender_id",
                table: "personal_message",
                newName: "SenderId");

            migrationBuilder.RenameColumn(
                name: "is_check",
                table: "personal_message",
                newName: "IsCheck");

            migrationBuilder.RenameColumn(
                name: "delete_date",
                table: "personal_message",
                newName: "DeleteDate");

            migrationBuilder.RenameColumn(
                name: "created_date",
                table: "person",
                newName: "registration_date");

            migrationBuilder.RenameColumn(
                name: "content",
                table: "group_message",
                newName: "Content");

            migrationBuilder.RenameColumn(
                name: "updated_date",
                table: "group_message",
                newName: "UpdatedDate");

            migrationBuilder.RenameColumn(
                name: "sent_at",
                table: "group_message",
                newName: "SentAt");

            migrationBuilder.RenameColumn(
                name: "sender_id",
                table: "group_message",
                newName: "SenderId");

            migrationBuilder.RenameColumn(
                name: "is_check",
                table: "group_message",
                newName: "IsCheck");

            migrationBuilder.RenameColumn(
                name: "delete_date",
                table: "group_message",
                newName: "DeleteDate");

            migrationBuilder.AlterColumn<DateTime>(
                name: "added_date",
                table: "group_member",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Content",
                table: "personal_message",
                newName: "content");

            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                table: "personal_message",
                newName: "updated_date");

            migrationBuilder.RenameColumn(
                name: "SentAt",
                table: "personal_message",
                newName: "sent_at");

            migrationBuilder.RenameColumn(
                name: "SenderId",
                table: "personal_message",
                newName: "sender_id");

            migrationBuilder.RenameColumn(
                name: "IsCheck",
                table: "personal_message",
                newName: "is_check");

            migrationBuilder.RenameColumn(
                name: "DeleteDate",
                table: "personal_message",
                newName: "delete_date");

            migrationBuilder.RenameColumn(
                name: "registration_date",
                table: "person",
                newName: "created_date");

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
                name: "SenderId",
                table: "group_message",
                newName: "sender_id");

            migrationBuilder.RenameColumn(
                name: "IsCheck",
                table: "group_message",
                newName: "is_check");

            migrationBuilder.RenameColumn(
                name: "DeleteDate",
                table: "group_message",
                newName: "delete_date");

            migrationBuilder.AddColumn<DateTime>(
                name: "created_date",
                table: "personal_message",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AddColumn<DateTime>(
                name: "created_date",
                table: "group_message",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<DateTime>(
                name: "added_date",
                table: "group_member",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AddColumn<DateTime>(
                name: "created_date",
                table: "group_member",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP");
        }
    }
}
