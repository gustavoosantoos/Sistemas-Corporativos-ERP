using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebERP.Data.Migrations
{
    public partial class UpdateSolicitacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Solicitacoes_AspNetUsers_SolicitanteId1",
                table: "Solicitacoes");

            migrationBuilder.DropIndex(
                name: "IX_Solicitacoes_SolicitanteId1",
                table: "Solicitacoes");

            migrationBuilder.DropColumn(
                name: "SolicitanteId1",
                table: "Solicitacoes");

            migrationBuilder.AlterColumn<string>(
                name: "SolicitanteId",
                table: "Solicitacoes",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Solicitacoes_SolicitanteId",
                table: "Solicitacoes",
                column: "SolicitanteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Solicitacoes_AspNetUsers_SolicitanteId",
                table: "Solicitacoes",
                column: "SolicitanteId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Solicitacoes_AspNetUsers_SolicitanteId",
                table: "Solicitacoes");

            migrationBuilder.DropIndex(
                name: "IX_Solicitacoes_SolicitanteId",
                table: "Solicitacoes");

            migrationBuilder.AlterColumn<int>(
                name: "SolicitanteId",
                table: "Solicitacoes",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "SolicitanteId1",
                table: "Solicitacoes",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Solicitacoes_SolicitanteId1",
                table: "Solicitacoes",
                column: "SolicitanteId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Solicitacoes_AspNetUsers_SolicitanteId1",
                table: "Solicitacoes",
                column: "SolicitanteId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
