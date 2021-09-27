using System.Collections.Generic;

namespace Application.Common.Interfaces.Repositories
{
    public interface IRepository<T> 
    {
        IEnumerable<T> GetAll();
        T GetById(object id);
        int Insert(T entity);
        int Delete(T entity);
        int Update(T entity);
    }
}
