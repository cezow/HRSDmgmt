using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRSDmgmt.Migrations
{
    public partial class manytomany_rel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Offer_OfferId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_OfferId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "OfferId",
                table: "Employees");

            migrationBuilder.CreateTable(
                name: "Candidate",
                columns: table => new
                {
                    CandidateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OfferId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidate", x => x.CandidateId);
                    table.ForeignKey(
                        name: "FK_Candidate_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Candidate_Offer_OfferId",
                        column: x => x.OfferId,
                        principalTable: "Offer",
                        principalColumn: "OfferId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Candidate_EmployeeId",
                table: "Candidate",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidate_OfferId",
                table: "Candidate",
                column: "OfferId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Candidate");

            migrationBuilder.AddColumn<int>(
                name: "OfferId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_OfferId",
                table: "Employees",
                column: "OfferId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Offer_OfferId",
                table: "Employees",
                column: "OfferId",
                principalTable: "Offer",
                principalColumn: "OfferId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
