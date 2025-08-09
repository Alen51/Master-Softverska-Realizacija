using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoftverskaRealizacijaBackend.Migrations
{
    /// <inheritdoc />
    public partial class nodeConnUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasError",
                table: "NodeConnections",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Clienti",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$t6IoRjWr7HbmrdRyqFVKBeCf7aO4q7caZRIgI4mUT3Ny0MbbafXQ2");

            migrationBuilder.UpdateData(
                table: "Clienti",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$4NpYg0Wc.awYK1QM6knAo.HDVzMGUAMN2FAlu3jjA80MaM.9tvfNi");

            migrationBuilder.UpdateData(
                table: "Clienti",
                keyColumn: "Id",
                keyValue: 3,
                column: "Password",
                value: "$2a$11$050s/hUxcumSARcqKK2tfeKEx1rRjbOiFzy1tT.yDqjZDAjUa2JTu");

            migrationBuilder.UpdateData(
                table: "Clienti",
                keyColumn: "Id",
                keyValue: 4,
                column: "Password",
                value: "$2a$11$h6mAFAhsJTIlJX/f5ADpy.VKxIsGUBASAN18rh2I1tNejjrE69r0S");

            migrationBuilder.UpdateData(
                table: "NodeConnections",
                keyColumn: "Id",
                keyValue: 1,
                column: "HasError",
                value: false);

            migrationBuilder.InsertData(
                table: "NodeConnections",
                columns: new[] { "Id", "EndPinId", "HasError", "StartPinId" },
                values: new object[] { 5, 2, false, 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "NodeConnections",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DropColumn(
                name: "HasError",
                table: "NodeConnections");

            migrationBuilder.UpdateData(
                table: "Clienti",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$1iBglCJqpnnDt/VZip0Ofuk.w9YvxARxk8IlERJFbXHCo6thSS6W6");

            migrationBuilder.UpdateData(
                table: "Clienti",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$q3CatFh0dzpa.tA7oe1yZOYeNZoFPmi5znaB0/LL/MUM2wrBUm6IG");

            migrationBuilder.UpdateData(
                table: "Clienti",
                keyColumn: "Id",
                keyValue: 3,
                column: "Password",
                value: "$2a$11$o3PauMbkEHGO8hvMSZea2etfAlgOYRlW99UE70QOZtvPbqc4pDnzy");

            migrationBuilder.UpdateData(
                table: "Clienti",
                keyColumn: "Id",
                keyValue: 4,
                column: "Password",
                value: "$2a$11$fMfgeJHBoZTyiW/UJmCUKO6xI.Yge8CQkMHMO1dfwybOKGZwGVCe6");
        }
    }
}
