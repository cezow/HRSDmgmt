using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRSDmgmt.Migrations
{
    public partial class aktualizacja_db : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Offers_OfferId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Offers_Companies_CompanytId",
                table: "Offers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Offers",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Employees");

            migrationBuilder.RenameTable(
                name: "Offers",
                newName: "Offer");

            migrationBuilder.RenameIndex(
                name: "IX_Offers_CompanytId",
                table: "Offer",
                newName: "IX_Offer_CompanytId");

            migrationBuilder.AddColumn<string>(
                name: "Education",
                table: "Employees",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Employees",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Experience",
                table: "Employees",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "Employees",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Mobile",
                table: "Employees",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "Companies",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "NIP",
                table: "Companies",
                type: "bigint",
                maxLength: 10,
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Information",
                table: "AspNetUsers",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "AspNetUsers",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Offer",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Display",
                table: "Offer",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Vacancy",
                table: "Offer",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Offer",
                table: "Offer",
                column: "OfferId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Id",
                table: "Employees",
                column: "Id",
                unique: true,
                filter: "[Id] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_Id",
                table: "Companies",
                column: "Id",
                unique: true,
                filter: "[Id] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_AspNetUsers_Id",
                table: "Companies",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_AspNetUsers_Id",
                table: "Employees",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Offer_OfferId",
                table: "Employees",
                column: "OfferId",
                principalTable: "Offer",
                principalColumn: "OfferId");

            migrationBuilder.AddForeignKey(
                name: "FK_Offer_Companies_CompanytId",
                table: "Offer",
                column: "CompanytId",
                principalTable: "Companies",
                principalColumn: "CompanyId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_AspNetUsers_Id",
                table: "Companies");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_AspNetUsers_Id",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Offer_OfferId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Offer_Companies_CompanytId",
                table: "Offer");

            migrationBuilder.DropIndex(
                name: "IX_Employees_Id",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Companies_Id",
                table: "Companies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Offer",
                table: "Offer");

            migrationBuilder.DropColumn(
                name: "Education",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Experience",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Mobile",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "NIP",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Information",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Photo",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "Offer");

            migrationBuilder.DropColumn(
                name: "Display",
                table: "Offer");

            migrationBuilder.DropColumn(
                name: "Vacancy",
                table: "Offer");

            migrationBuilder.RenameTable(
                name: "Offer",
                newName: "Offers");

            migrationBuilder.RenameIndex(
                name: "IX_Offer_CompanytId",
                table: "Offers",
                newName: "IX_Offers_CompanytId");

            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "Employees",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Offers",
                table: "Offers",
                column: "OfferId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Offers_OfferId",
                table: "Employees",
                column: "OfferId",
                principalTable: "Offers",
                principalColumn: "OfferId");

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_Companies_CompanytId",
                table: "Offers",
                column: "CompanytId",
                principalTable: "Companies",
                principalColumn: "CompanyId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
