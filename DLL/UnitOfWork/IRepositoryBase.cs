using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DLL.DbContext;
using Microsoft.EntityFrameworkCore;

namespace DLL.UnitOfWork
{
    public interface IRepositoryBase<T> where T:class
    {
        Task CreateAsync(T entry);
        void UpdateAsyc(T entry);
        void DeleteAsync(T entry);
        Task<T> GetAAsynce(Expression<Func<T, bool>> expression = null);
        Task<List<T>> GetListAsynce(Expression<Func<T, bool>> expression = null);
        IQueryable<T> QueryAll(Expression<Func<T, bool>> expression = null);


    }

    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly ApplicationDbContext _context;


        public RepositoryBase(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(T entry)
        {
           await _context.Set<T>().AddAsync(entry);
        }

        public void UpdateAsyc(T entry)
        {
            _context.Set<T>().Update(entry);
        }

        public void DeleteAsync(T entry)
        {
            _context.Set<T>().Remove(entry);
        }

        public async Task<T> GetAAsynce(Expression<Func<T, bool>> expression = null)
        {
           return  await _context.Set<T>().FirstOrDefaultAsync(expression);
        }

        public async Task<List<T>> GetListAsynce(Expression<Func<T, bool>> expression = null)
        {
            if (expression == null)
            {
                return await _context.Set<T>().AsNoTracking().ToListAsync();
            }
            return await _context.Set<T>().Where(expression).AsNoTracking().ToListAsync();
          
        }

        public  IQueryable<T> QueryAll(Expression<Func<T, bool>> expression = null)
        {
            if (expression == null)
            {
                return  _context.Set<T>().AsQueryable().AsNoTracking();
            }
            return  _context.Set<T>().Where(expression).AsQueryable().AsNoTracking();
        }
    }
}