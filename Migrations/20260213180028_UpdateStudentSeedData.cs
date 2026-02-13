using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SynapsePENS.Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateStudentSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "NRP", "Name" },
                values: new object[] { "3124600004", "Akari Kanzoo Triputra" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "NRP", "Name" },
                values: new object[] { "2110191001", "Mahasiswa PENS" });
        }
    }
}
