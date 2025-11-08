using CatalogoAPI.Repositories.Categorias;
using CatalogoAPI.Repositories.Produtos;

namespace CatalogoAPI.Repositories.Unity_of_Work;

public interface IUnitOfWork
{
    IProdutosRepository Produtos { get; }
    ICategoriasRepository Categorias { get; }
    void Commit();
    void Rollback();
}
