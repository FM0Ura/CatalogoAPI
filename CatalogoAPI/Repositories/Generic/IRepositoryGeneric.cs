using System.Linq.Expressions;

namespace CatalogoAPI.Repositories.Generic;
public interface IRepositoryGeneric<T> where T : class
{
    IEnumerable<T> GetAll();
    T? GetOne(Expression<Func<T, bool>> predicate);
    T Add(T entity);
    T Update(T entity);
    T Delete(T entity);

}
