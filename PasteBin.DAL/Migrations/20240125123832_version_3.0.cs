using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PasteBin.DAL.Migrations
{
    /// <inheritdoc />
    public partial class version_30 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pasts_AspNetUsers_UserId",
                table: "Pasts");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Pasts",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

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

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Pasts",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Pasts_AspNetUsers_UserId",
                table: "Pasts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
