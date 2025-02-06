using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoftverskaRealizacijaBackend.Migrations
{
    /// <inheritdoc />
    public partial class m3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NodeConnections_Nodes_TopNodeId",
                table: "NodeConnections");

            migrationBuilder.DropForeignKey(
                name: "FK_Nodes_NodeConnections_NodeConnectionId",
                table: "Nodes");

            migrationBuilder.DropIndex(
                name: "IX_Nodes_NodeConnectionId",
                table: "Nodes");

            migrationBuilder.DropIndex(
                name: "IX_NodeConnections_TopNodeId",
                table: "NodeConnections");

            migrationBuilder.DropColumn(
                name: "CoordinateX",
                table: "Nodes");

            migrationBuilder.DropColumn(
                name: "CoordinateY",
                table: "Nodes");

            migrationBuilder.DropColumn(
                name: "NodeConnectionId",
                table: "Nodes");

            migrationBuilder.RenameColumn(
                name: "TopNodeId",
                table: "NodeConnections",
                newName: "StartPinId");

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Nodes",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "Nodes",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "EndPinId",
                table: "NodeConnections",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Nodes");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Nodes");

            migrationBuilder.DropColumn(
                name: "EndPinId",
                table: "NodeConnections");

            migrationBuilder.RenameColumn(
                name: "StartPinId",
                table: "NodeConnections",
                newName: "TopNodeId");

            migrationBuilder.AddColumn<int>(
                name: "CoordinateX",
                table: "Nodes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CoordinateY",
                table: "Nodes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NodeConnectionId",
                table: "Nodes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Nodes_NodeConnectionId",
                table: "Nodes",
                column: "NodeConnectionId");

            migrationBuilder.CreateIndex(
                name: "IX_NodeConnections_TopNodeId",
                table: "NodeConnections",
                column: "TopNodeId");

            migrationBuilder.AddForeignKey(
                name: "FK_NodeConnections_Nodes_TopNodeId",
                table: "NodeConnections",
                column: "TopNodeId",
                principalTable: "Nodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Nodes_NodeConnections_NodeConnectionId",
                table: "Nodes",
                column: "NodeConnectionId",
                principalTable: "NodeConnections",
                principalColumn: "Id");
        }
    }
}
