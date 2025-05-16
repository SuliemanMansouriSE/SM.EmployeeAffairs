using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SM.EmployeeAffairs.Migrations
{
    /// <inheritdoc />
    public partial class fluentAPI : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Administrations_AdministrationId",
                table: "Employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employees",
                table: "Employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Administrations",
                table: "Administrations");

            migrationBuilder.RenameTable(
                name: "Employees",
                newName: "Employee");

            migrationBuilder.RenameTable(
                name: "Administrations",
                newName: "Administration");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_AdministrationId",
                table: "Employee",
                newName: "IX_Employee_AdministrationId");

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Employee",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "NameEng",
                table: "Employee",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "NameAr",
                table: "Employee",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "NID",
                table: "Employee",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Employee",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Administration",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Administration",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employee",
                table: "Employee",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Administration",
                table: "Administration",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_NameAr",
                table: "Employee",
                column: "NameAr");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_Phone_Email_NID",
                table: "Employee",
                columns: new[] { "Phone", "Email", "NID" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Administration_AdministrationId",
                table: "Employee",
                column: "AdministrationId",
                principalTable: "Administration",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Administration_AdministrationId",
                table: "Employee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employee",
                table: "Employee");

            migrationBuilder.DropIndex(
                name: "IX_Employee_NameAr",
                table: "Employee");

            migrationBuilder.DropIndex(
                name: "IX_Employee_Phone_Email_NID",
                table: "Employee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Administration",
                table: "Administration");

            migrationBuilder.RenameTable(
                name: "Employee",
                newName: "Employees");

            migrationBuilder.RenameTable(
                name: "Administration",
                newName: "Administrations");

            migrationBuilder.RenameIndex(
                name: "IX_Employee_AdministrationId",
                table: "Employees",
                newName: "IX_Employees_AdministrationId");

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "NameEng",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "NameAr",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "NID",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Administrations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Administrations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employees",
                table: "Employees",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Administrations",
                table: "Administrations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Administrations_AdministrationId",
                table: "Employees",
                column: "AdministrationId",
                principalTable: "Administrations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
