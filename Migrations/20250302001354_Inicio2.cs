using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiFuncional.Migrations
{
	/// <inheritdoc />
	public partial class Inicio2 : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "Clientes",
				columns: table => new
				{
					Cnpj = table.Column<string>(type: "nvarchar(450)", nullable: false),
					Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Fantasia = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Telefone = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Logradouro = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Bairro = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Municipio = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Uf = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Cep = table.Column<string>(type: "nvarchar(max)", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Clientes", x => x.Cnpj);
				});
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "Clientes");
		}
	}
}