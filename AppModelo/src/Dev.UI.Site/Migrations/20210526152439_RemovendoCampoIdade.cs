﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace Dev.UI.Site.Migrations
{
    public partial class RemovendoCampoIdade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Idade",
                table: "Alunos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Idade",
                table: "Alunos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
