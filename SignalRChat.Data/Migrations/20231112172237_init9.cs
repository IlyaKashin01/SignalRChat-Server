using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SignalRChat.Data.Migrations
{
    /// <inheritdoc />
    public partial class init9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "person_join_to_group");

            migrationBuilder.RenameColumn(
                name: "from_person_id",
                table: "group_message",
                newName: "sender_id");

            migrationBuilder.AddColumn<int>(
                name: "PersonId",
                table: "group_chat_room",
                type: "integer",
                nullable: false,
                defaultValue: 0);

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_group_chat_room_person_PersonId",
                table: "group_chat_room");

            migrationBuilder.DropIndex(
                name: "IX_group_chat_room_PersonId",
                table: "group_chat_room");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "group_chat_room");

            migrationBuilder.RenameColumn(
                name: "sender_id",
                table: "group_message",
                newName: "from_person_id");

            migrationBuilder.CreateTable(
                name: "person_join_to_group",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    group_id = table.Column<int>(type: "integer", nullable: false),
                    person_id = table.Column<int>(type: "integer", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    delete_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_person_join_to_group", x => x.Id);
                    table.ForeignKey(
                        name: "FK_person_join_to_group_group_chat_room_group_id",
                        column: x => x.group_id,
                        principalTable: "group_chat_room",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_person_join_to_group_person_person_id",
                        column: x => x.person_id,
                        principalTable: "person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_person_join_to_group_group_id",
                table: "person_join_to_group",
                column: "group_id");

            migrationBuilder.CreateIndex(
                name: "IX_person_join_to_group_person_id",
                table: "person_join_to_group",
                column: "person_id");
        }
    }
}
