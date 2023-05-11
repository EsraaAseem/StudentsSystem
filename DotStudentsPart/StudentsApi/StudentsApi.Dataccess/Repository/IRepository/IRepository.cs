using StudentsApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StudentsApi.Dataccess.Repository.IRepository
{
    public interface IRepository<T> where T:class
    {
       Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>>? filter = null,string?includeProperity=null);
       Task<T> GetFirstOrDefualt(Expression<Func<T, bool>>filter, string? includeProperity = null,bool tracked=true);
        Task<T> Add(T identity);
        Task<T> remove(T identity);
        void removerange(IEnumerable<T> identity);


    }
}
