using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PasteBin.DAL.Migrations
{
    /// <inheritdoc />
    public partial class version_40 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Pasts",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "Views",
                table: "Pasts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Pasts_UserId",
                table: "Pasts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pasts_AspNetUsers_UserId",
                table: "Pasts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pasts_AspNetUsers_UserId",
                table: "Pasts");

            migrationBuilder.DropIndex(
                name: "IX_Pasts_UserId",
                table: "Pasts");

            migrationBuilder.DropColumn(
                name: "Views",
                table: "Pasts");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Pasts",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
