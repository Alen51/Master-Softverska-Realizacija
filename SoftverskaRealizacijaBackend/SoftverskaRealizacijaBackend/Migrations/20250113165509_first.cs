using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoftverskaRealizacijaBackend.Migrations
{
    /// <inheritdoc />
    public partial class first : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clienti",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TipKorisnika = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clienti", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kvraovi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VremePrijave = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VremeOtkanjanja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    NodeId = table.Column<int>(type: "int", nullable: false),
                    StanjeKvara = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kvraovi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kvraovi_Clienti_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clienti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NodeConnections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TopNodeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NodeConnections", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Nodes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NodeConnectionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Nodes_NodeConnections_NodeConnectionId",
                        column: x => x.NodeConnectionId,
                        principalTable: "NodeConnections",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clienti_Email",
                table: "Clienti",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Kvraovi_ClientId",
                table: "Kvraovi",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Kvraovi_NodeId",
                table: "Kvraovi",
                column: "NodeId");

            migrationBuilder.CreateIndex(
                name: "IX_NodeConnections_TopNodeId",
                table: "NodeConnections",
                column: "TopNodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Nodes_NodeConnectionId",
                table: "Nodes",
                column: "NodeConnectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Kvraovi_Nodes_NodeId",
                table: "Kvraovi",
                column: "NodeId",
                principalTable: "Nodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NodeConnections_Nodes_TopNodeId",
                table: "NodeConnections",
                column: "TopNodeId",
                principalTable: "Nodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NodeConnections_Nodes_TopNodeId",
                table: "NodeConnections");

            migrationBuilder.DropTable(
                name: "Kvraovi");

            migrationBuilder.DropTable(
                name: "Clienti");

            migrationBuilder.DropTable(
                name: "Nodes");

            migrationBuilder.DropTable(
                name: "NodeConnections");
        }
    }
}
