using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CaesarApi.Migrations
{
    /// <inheritdoc />
    public partial class RemoveMensajeOriginal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MensajeOriginal",
                table: "Mensajes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MensajeOriginal",
                table: "Mensajes",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
