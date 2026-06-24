using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TraineeManagement.api.Migrations
{
    /// <inheritdoc />
    public partial class ProcessingJobCreate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Attempts",
                table: "ProcessingJobs",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Attempts",
                table: "ProcessingJobs",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
