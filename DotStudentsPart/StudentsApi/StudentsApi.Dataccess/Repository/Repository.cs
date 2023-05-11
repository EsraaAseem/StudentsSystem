using Microsoft.EntityFrameworkCore;
using StudentsApi.Dataccess.Data;
using StudentsApi.Dataccess.Repository.IRepository;
using StudentsApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StudentsApi.Dataccess.Repository
{
    public class Repository<T> : IRepository<T> where T:class
    {
        ApplicatioDbContext _db;
        public DbSet<T> dbSet;
        public Repository(ApplicatioDbContext db) 
        {
            this._db = db;
            this.dbSet = _db.Set<T>();
        }


        public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperity = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includeProperity != null)
            {
                foreach (var properity in includeProperity.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(properity);
                }
            }
            return await query.ToListAsync();
        }

        public async Task<T> GetFirstOrDefualt(Expression<Func<T, bool>> filter, string? includeProperity = null, bool tracked= true)
        {
            IQueryable<T> query;
            if (tracked == true)
            {
                query = dbSet;
            }
            else
            {
                query = dbSet.AsNoTracking();
            }
            if (filter!=null)
            {
                query = query.Where(filter);
            }
            if (includeProperity != null)
            {
                foreach(var properity in includeProperity.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(properity);
                }
            }
            return query.FirstOrDefault();
        }

        public async Task<T> remove(T identity)
        {
            return null;
        }

        public void removerange(IEnumerable<T> identity)
        {
            _db.RemoveRange(identity);
        }

        public async Task<T> Add(T identity)
        {
            var student=await _db.AddAsync(identity);
            return student.Entity;
        }
    }
}
