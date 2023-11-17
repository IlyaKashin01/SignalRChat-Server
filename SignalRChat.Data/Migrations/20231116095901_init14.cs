using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SignalRChat.Data.Migrations
{
    /// <inheritdoc />
    public partial class init14 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_group_member_person_PersonId",
                table: "group_member");

            migrationBuilder.RenameColumn(
                name: "PersonId",
                table: "group_member",
                newName: "member_id");

            migrationBuilder.RenameIndex(
                name: "IX_group_member_PersonId",
                table: "group_member",
                newName: "IX_group_member_member_id");

            migrationBuilder.AddForeignKey(
                name: "FK_group_member_person_member_id",
                table: "group_member",
                column: "member_id",
                principalTable: "person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_group_member_person_member_id",
                table: "group_member");

            migrationBuilder.RenameColumn(
                name: "member_id",
                table: "group_member",
                newName: "PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_group_member_member_id",
                table: "group_member",
                newName: "IX_group_member_PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_group_member_person_PersonId",
                table: "group_member",
                column: "PersonId",
                principalTable: "person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
