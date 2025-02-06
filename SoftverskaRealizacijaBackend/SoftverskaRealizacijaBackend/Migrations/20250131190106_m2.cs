using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoftverskaRealizacijaBackend.Migrations
{
    /// <inheritdoc />
    public partial class m2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Clienti",
                newName: "fullName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "fullName",
                table: "Clienti",
                newName: "Name");
        }
    }
}
