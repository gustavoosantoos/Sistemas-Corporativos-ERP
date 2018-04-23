using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebERP.Data.Migrations
{
    public partial class TelefoneFornecedor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TelefoneFornecedores_Fornecedores_FornecedorId",
                table: "TelefoneFornecedores");

            migrationBuilder.DropIndex(
                name: "IX_TelefoneFornecedores_FornecedorId",
                table: "TelefoneFornecedores");

            migrationBuilder.DropColumn(
                name: "FornecedorId",
                table: "TelefoneFornecedores");

            migrationBuilder.AddColumn<int>(
                name: "TelefoneId",
                table: "Fornecedores",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Fornecedores_TelefoneId",
                table: "Fornecedores",
                column: "TelefoneId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fornecedores_TelefoneFornecedores_TelefoneId",
                table: "Fornecedores",
                column: "TelefoneId",
                principalTable: "TelefoneFornecedores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fornecedores_TelefoneFornecedores_TelefoneId",
                table: "Fornecedores");

            migrationBuilder.DropIndex(
                name: "IX_Fornecedores_TelefoneId",
                table: "Fornecedores");

            migrationBuilder.DropColumn(
                name: "TelefoneId",
                table: "Fornecedores");

            migrationBuilder.AddColumn<int>(
                name: "FornecedorId",
                table: "TelefoneFornecedores",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TelefoneFornecedores_FornecedorId",
                table: "TelefoneFornecedores",
                column: "FornecedorId");

            migrationBuilder.AddForeignKey(
                name: "FK_TelefoneFornecedores_Fornecedores_FornecedorId",
                table: "TelefoneFornecedores",
                column: "FornecedorId",
                principalTable: "Fornecedores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
