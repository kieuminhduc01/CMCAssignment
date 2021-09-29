using System.Collections.Generic;

namespace Application.Common.Interfaces.Repositories
{
    public interface IRepository<T> 
    {
        IEnumerable<T> GetAll();
        T GetById(object id);
        void Insert(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}
