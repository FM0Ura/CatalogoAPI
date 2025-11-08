using CatalogoAPI.Context;
using CatalogoAPI.Repositories.Categorias;
using CatalogoAPI.Repositories.Produtos;

namespace CatalogoAPI.Repositories.Unity_of_Work;

public class UnitOfWork : IUnitOfWork
{
    private IProdutosRepository? _produtoRepo;

    private ICategoriasRepository? _categoriaRepo;

    public CatalogoAPIContext _context;

    public UnitOfWork(CatalogoAPIContext context)
    {
        _context = context;
    }

    public IProdutosRepository Produtos
    {
        get
        {
            return _produtoRepo = _produtoRepo  ?? new ProdutosRepository(_context);
        }
    }
    public ICategoriasRepository Categorias
    {
        get
        {
            return _categoriaRepo = _categoriaRepo ?? new CategoriasRepository(_context);
        }
    }

    public void Commit()
    {
        _context.SaveChanges();
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public void Rollback()
    {
        throw new NotImplementedException();
    }
}
