using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoftverskaRealizacijaBackend.Migrations
{
    /// <inheritdoc />
    public partial class kvar2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kvraovi_Clienti_ClientId",
                table: "Kvraovi");

            migrationBuilder.DropForeignKey(
                name: "FK_Kvraovi_Nodes_NodeId",
                table: "Kvraovi");

            migrationBuilder.DropIndex(
                name: "IX_Kvraovi_ClientId",
                table: "Kvraovi");

            migrationBuilder.DropIndex(
                name: "IX_Kvraovi_NodeId",
                table: "Kvraovi");

            migrationBuilder.RenameColumn(
                name: "NodeId",
                table: "Kvraovi",
                newName: "Node");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "Kvraovi",
                newName: "Client");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Node",
                table: "Kvraovi",
                newName: "NodeId");

            migrationBuilder.RenameColumn(
                name: "Client",
                table: "Kvraovi",
                newName: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Kvraovi_ClientId",
                table: "Kvraovi",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Kvraovi_NodeId",
                table: "Kvraovi",
                column: "NodeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Kvraovi_Clienti_ClientId",
                table: "Kvraovi",
                column: "ClientId",
                principalTable: "Clienti",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Kvraovi_Nodes_NodeId",
                table: "Kvraovi",
                column: "NodeId",
                principalTable: "Nodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
