using CatalogoAPI.Context;
using Microsoft.EntityFrameworkCore;

namespace CatalogoAPI.Repositories.Generic;

public class RepositoryGeneric<T> : IRepositoryGeneric<T> where T : class
{
    protected readonly CatalogoAPIContext _context;

    public RepositoryGeneric(CatalogoAPIContext context)
    {
        _context = context;
    }

    public IEnumerable<T> GetAll()
    {
        return _context.Set<T>().AsNoTracking().ToList();
    }
    public T? GetOne(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
    {
        return _context.Set<T>().AsNoTracking().FirstOrDefault(predicate);
    }
    public T Add(T entity)
    {
        _context.Set<T>().Add(entity);
        return entity;
    }
    public T Update(T entity)
    {
        _context.Set<T>().Update(entity);
        return entity;
    }

    public T Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
        return entity;
    }
}
