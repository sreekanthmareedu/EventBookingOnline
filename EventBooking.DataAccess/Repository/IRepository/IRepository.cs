using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEvents.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();

        Task<T> GetAsync(Expression<Func<T, bool>> filter = null);

        Task CreateAsync(T entity);



        Task RemoveAsync(T entity);

        

    }
}
