using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Infrastructure.BaseRepositories
{
    public interface IBaseRepository<T> where T : class
    {
       public Task<T> Create(T entity);
       public Task<T> Update(int id,T entity);
       public Task<bool> Delete(Expression<Func<T, bool>>expression);
       public Task<T> GetByAny(Expression<Func<T,bool>>expression);
       public Task<IEnumerable<T>> GetAll();
    }
}
