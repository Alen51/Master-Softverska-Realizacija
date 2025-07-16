using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoftverskaRealizacijaBackend.Migrations
{
    /// <inheritdoc />
    public partial class kvarovi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Kvraovi",
                table: "Kvraovi");

            migrationBuilder.RenameTable(
                name: "Kvraovi",
                newName: "Kvarovi");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Kvarovi",
                table: "Kvarovi",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Kvarovi",
                table: "Kvarovi");

            migrationBuilder.RenameTable(
                name: "Kvarovi",
                newName: "Kvraovi");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Kvraovi",
                table: "Kvraovi",
                column: "Id");
        }
    }
}
