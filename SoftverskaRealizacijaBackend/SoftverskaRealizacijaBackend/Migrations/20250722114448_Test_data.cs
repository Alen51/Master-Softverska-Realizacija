using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SoftverskaRealizacijaBackend.Migrations
{
    /// <inheritdoc />
    public partial class Test_data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Clienti",
                columns: new[] { "Id", "Email", "Password", "TipKorisnika", "fullName" },
                values: new object[,]
                {
                    { 1, "admin1@gmail.com", "$2a$11$1iBglCJqpnnDt/VZip0Ofuk.w9YvxARxk8IlERJFbXHCo6thSS6W6", 0, "Admin1" },
                    { 2, "gost1@gmail.com", "$2a$11$q3CatFh0dzpa.tA7oe1yZOYeNZoFPmi5znaB0/LL/MUM2wrBUm6IG", 1, "Gost1" },
                    { 3, "kupac1@gmail.com", "$2a$11$o3PauMbkEHGO8hvMSZea2etfAlgOYRlW99UE70QOZtvPbqc4pDnzy", 2, "Kupac1" },
                    { 4, "kupac2@gmail.com", "$2a$11$fMfgeJHBoZTyiW/UJmCUKO6xI.Yge8CQkMHMO1dfwybOKGZwGVCe6", 2, "Kupac2" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Clienti",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Clienti",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Clienti",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Clienti",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
