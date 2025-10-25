using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatalogoAPI.Migrations
{
    /// <inheritdoc />
    public partial class PopulaProdutos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("INSERT INTO Produtos (Nome, Descricao, Preco, ImagemUrl, Estoque, DataCadastro, CategoriaId) " +
                "VALUES ('Coca-Cola', 'Refrigerante de cola gelado', 5.45, 'coca_cola.png', 5, now(),(SELECT CategoriaId FROM Categorias WHERE Nome = 'Bebidas'));");
            mb.Sql("INSERT INTO Produtos (Nome, Descricao, Preco, ImagemUrl, Estoque, DataCadastro, CategoriaId) " +
                "VALUES ('Hambúrguer', 'Delicioso hambúrguer artesanal', 15.90, 'hamburguer.png', 20, now(), (SELECT CategoriaId FROM Categorias WHERE Nome = 'Lanches'));");
            mb.Sql("INSERT INTO Produtos (Nome, Descricao, Preco, ImagemUrl, Estoque, DataCadastro, CategoriaId) " +
                "VALUES ('Sorvete', 'Sorvete de baunilha cremoso', 7.25, 'sorvete.png', 30, now(), (SELECT CategoriaId FROM Categorias WHERE Nome = 'Sobremesas'));");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("DELETE FROM Produtos;");
        }
    }
}
