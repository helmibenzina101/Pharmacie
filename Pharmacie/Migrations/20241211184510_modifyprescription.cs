using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pharmacie.Migrations
{
    /// <inheritdoc />
    public partial class modifyprescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medications_Prescriptions_PrescriptionId",
                table: "Medications");

            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_Medecins_MedecinId",
                table: "Prescriptions");

            migrationBuilder.DropIndex(
                name: "IX_Medications_PrescriptionId",
                table: "Medications");

            migrationBuilder.DropColumn(
                name: "PrescriptionId",
                table: "Medications");

            migrationBuilder.AlterColumn<int>(
                name: "MedecinId",
                table: "Prescriptions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Medications",
                table: "Prescriptions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_Medecins_MedecinId",
                table: "Prescriptions",
                column: "MedecinId",
                principalTable: "Medecins",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_Medecins_MedecinId",
                table: "Prescriptions");

            migrationBuilder.DropColumn(
                name: "Medications",
                table: "Prescriptions");

            migrationBuilder.AlterColumn<int>(
                name: "MedecinId",
                table: "Prescriptions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PrescriptionId",
                table: "Medications",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Medications_PrescriptionId",
                table: "Medications",
                column: "PrescriptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medications_Prescriptions_PrescriptionId",
                table: "Medications",
                column: "PrescriptionId",
                principalTable: "Prescriptions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_Medecins_MedecinId",
                table: "Prescriptions",
                column: "MedecinId",
                principalTable: "Medecins",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
