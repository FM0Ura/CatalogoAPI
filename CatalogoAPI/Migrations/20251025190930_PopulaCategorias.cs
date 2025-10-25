﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatalogoAPI.Migrations;

/// <inheritdoc />
public partial class PopulaCategorias : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder mb)
    {
        mb.Sql("INSERT INTO Categorias (Nome, ImagemUrl) VALUES ('Bebidas', 'bebidas.png');");
        mb.Sql("INSERT INTO Categorias (Nome, ImagemUrl) VALUES ('Lanches', 'lanches.png');");
        mb.Sql("INSERT INTO Categorias (Nome, ImagemUrl) VALUES ('Sobremesas', 'sobremesas.png');");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder mb)
    {
        mb.Sql("DELETE FROM Categorias;");
    }
}
