using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RomiCrud.Api.Migrations
{
    /// <inheritdoc />
    public partial class TablasEnEspanol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persons_DocumentTypes_DocumentTypeId",
                table: "Persons");

            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Genders_GenderId",
                table: "Persons");

            migrationBuilder.DropForeignKey(
                name: "FK_Persons_PersonTypes_PersonTypeId",
                table: "Persons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonTypes",
                table: "PersonTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Persons",
                table: "Persons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Genders",
                table: "Genders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DocumentTypes",
                table: "DocumentTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationErrorLogs",
                table: "ApplicationErrorLogs");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "Usuarios");

            migrationBuilder.RenameTable(
                name: "PersonTypes",
                newName: "TiposPersona");

            migrationBuilder.RenameTable(
                name: "Persons",
                newName: "Personas");

            migrationBuilder.RenameTable(
                name: "Genders",
                newName: "Generos");

            migrationBuilder.RenameTable(
                name: "DocumentTypes",
                newName: "TiposDocumento");

            migrationBuilder.RenameTable(
                name: "ApplicationErrorLogs",
                newName: "ErroresAplicacion");

            migrationBuilder.RenameIndex(
                name: "IX_Users_Username",
                table: "Usuarios",
                newName: "IX_Usuarios_Username");

            migrationBuilder.RenameIndex(
                name: "IX_PersonTypes_Code",
                table: "TiposPersona",
                newName: "IX_TiposPersona_Code");

            migrationBuilder.RenameIndex(
                name: "IX_Persons_PersonTypeId",
                table: "Personas",
                newName: "IX_Personas_PersonTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Persons_GenderId",
                table: "Personas",
                newName: "IX_Personas_GenderId");

            migrationBuilder.RenameIndex(
                name: "IX_Persons_DocumentTypeId_DocumentNumber",
                table: "Personas",
                newName: "IX_Personas_DocumentTypeId_DocumentNumber");

            migrationBuilder.RenameIndex(
                name: "IX_Genders_Code",
                table: "Generos",
                newName: "IX_Generos_Code");

            migrationBuilder.RenameIndex(
                name: "IX_DocumentTypes_Code",
                table: "TiposDocumento",
                newName: "IX_TiposDocumento_Code");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TiposPersona",
                table: "TiposPersona",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Personas",
                table: "Personas",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Generos",
                table: "Generos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TiposDocumento",
                table: "TiposDocumento",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ErroresAplicacion",
                table: "ErroresAplicacion",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Personas_Generos_GenderId",
                table: "Personas",
                column: "GenderId",
                principalTable: "Generos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Personas_TiposDocumento_DocumentTypeId",
                table: "Personas",
                column: "DocumentTypeId",
                principalTable: "TiposDocumento",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Personas_TiposPersona_PersonTypeId",
                table: "Personas",
                column: "PersonTypeId",
                principalTable: "TiposPersona",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Personas_Generos_GenderId",
                table: "Personas");

            migrationBuilder.DropForeignKey(
                name: "FK_Personas_TiposDocumento_DocumentTypeId",
                table: "Personas");

            migrationBuilder.DropForeignKey(
                name: "FK_Personas_TiposPersona_PersonTypeId",
                table: "Personas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TiposPersona",
                table: "TiposPersona");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TiposDocumento",
                table: "TiposDocumento");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Personas",
                table: "Personas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Generos",
                table: "Generos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ErroresAplicacion",
                table: "ErroresAplicacion");

            migrationBuilder.RenameTable(
                name: "Usuarios",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "TiposPersona",
                newName: "PersonTypes");

            migrationBuilder.RenameTable(
                name: "TiposDocumento",
                newName: "DocumentTypes");

            migrationBuilder.RenameTable(
                name: "Personas",
                newName: "Persons");

            migrationBuilder.RenameTable(
                name: "Generos",
                newName: "Genders");

            migrationBuilder.RenameTable(
                name: "ErroresAplicacion",
                newName: "ApplicationErrorLogs");

            migrationBuilder.RenameIndex(
                name: "IX_Usuarios_Username",
                table: "Users",
                newName: "IX_Users_Username");

            migrationBuilder.RenameIndex(
                name: "IX_TiposPersona_Code",
                table: "PersonTypes",
                newName: "IX_PersonTypes_Code");

            migrationBuilder.RenameIndex(
                name: "IX_TiposDocumento_Code",
                table: "DocumentTypes",
                newName: "IX_DocumentTypes_Code");

            migrationBuilder.RenameIndex(
                name: "IX_Personas_PersonTypeId",
                table: "Persons",
                newName: "IX_Persons_PersonTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Personas_GenderId",
                table: "Persons",
                newName: "IX_Persons_GenderId");

            migrationBuilder.RenameIndex(
                name: "IX_Personas_DocumentTypeId_DocumentNumber",
                table: "Persons",
                newName: "IX_Persons_DocumentTypeId_DocumentNumber");

            migrationBuilder.RenameIndex(
                name: "IX_Generos_Code",
                table: "Genders",
                newName: "IX_Genders_Code");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonTypes",
                table: "PersonTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DocumentTypes",
                table: "DocumentTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Persons",
                table: "Persons",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Genders",
                table: "Genders",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationErrorLogs",
                table: "ApplicationErrorLogs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_DocumentTypes_DocumentTypeId",
                table: "Persons",
                column: "DocumentTypeId",
                principalTable: "DocumentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Genders_GenderId",
                table: "Persons",
                column: "GenderId",
                principalTable: "Genders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_PersonTypes_PersonTypeId",
                table: "Persons",
                column: "PersonTypeId",
                principalTable: "PersonTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
