using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure_WebReklam.Context.Migrations
{
    /// <inheritdoc />
    public partial class UpdateVillage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "RequestForms");

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "Villages",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Villages_CityId",
                table: "Villages",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Villages_Cities_CityId",
                table: "Villages",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Villages_Cities_CityId",
                table: "Villages");

            migrationBuilder.DropIndex(
                name: "IX_Villages_CityId",
                table: "Villages");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Villages");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "RequestForms",
                type: "text",
                nullable: true);
        }
    }
}
