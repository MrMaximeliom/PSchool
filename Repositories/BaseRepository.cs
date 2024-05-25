using Microsoft.EntityFrameworkCore;
using PSchool.Backend.Constants;
using PSchool.Backend.Interfaces;
using System;
using System.Linq.Expressions;

namespace PSchool.Backend.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected ApplicationDbContext _context;
        
        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<T> GetAll() 
        {
         return [.. _context.Set<T>()];
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public T? GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }
        
        public T? GetById(string id)
        {
            return _context.Set<T>().Find(id);
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T?> GetByIdAsync(string id)
        {
            
            return await _context.Set<T>().FindAsync(id);
        }

        public T? Find(Expression<Func<T,bool>> predicate, string[]? includes = null)
        {
            IQueryable<T> query = _context.Set<T>();

            if(includes is not null)
            {
                foreach(var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return query.SingleOrDefault(predicate);

        }

        public async Task<T?> FindAsync(Expression<Func<T,bool>> predicate, string[]? includes = null)
        {
            IQueryable<T> query = _context.Set<T>();

            if(includes is not null)
            {
                foreach(var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return await query.SingleOrDefaultAsync(predicate);
        }

        public async Task<IAsyncEnumerable<T>?> FindAll(Expression<Func<T,bool>> predicate, string[]? includes = null)
        {
            IQueryable<T> query = _context.Set<T>();

            if(includes is not null)
            {
                foreach(var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return (IAsyncEnumerable<T>?)await query.ToListAsync();
        }
        public async Task<IEnumerable<string>> GetProperty(Expression<Func<T,bool>> predicate, Expression<Func<T,string>> select ,bool isDistinct)
        {

         
            return isDistinct ? _context.Set<T>().Where(predicate).Select(select).Distinct() : _context.Set<T>().Where(predicate).Select(select);
            

    /*        if (includes is not null)
            {
                foreach (var include in includes)
                {
                    if(isDistinct)
                    query = query.Include(include).Distinct();
                    else query = query.Include(include);
                }
            }
            return [.. query];*/

        }

        public IEnumerable<T> FindAll(Expression<Func<T,bool>> predicate, int? skip , int? take, Expression<Func<T,object>>? orderBy = null , string orderByDirection = OrderBy.Ascending)
        {
            IQueryable<T> query = _context.Set<T>().Where(predicate);

            if(skip.HasValue)
            {
                query = query.Skip(skip.Value); 
            }
            if(take.HasValue)
            {
                query = query.Take(take.Value);
            }
            if(orderBy is not null)
            {
                if(orderByDirection.Equals(OrderBy.Ascending))
                {
                    query = query.OrderBy(orderBy);
                }
                else
                {
                    query = query.OrderByDescending(orderBy);
                }
            }
            return [.. query];
        }

        public async Task<IEnumerable<T?>> FindAllAsync(Expression<Func<T,bool>> predicate, string[]? includes = null)
        {
            IQueryable<T> query = _context.Set<T>().Where(predicate);

            if( includes is not null)
            {
                foreach(var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<T?>> FindAllAsync(Expression<Func<T,bool>> predicate, int? take, int? skip,
            Expression<Func<T,object>>? orderBy = null, string orderByDirection = OrderBy.Ascending )
        {
            IQueryable<T> query = _context.Set<T>().Where(predicate);

            if(take.HasValue)
            {
                query = query.Take(take.Value); 
            }
            if(skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }
            if(orderBy is not null)
            {
                if( orderByDirection.Equals(OrderBy.Ascending))
                {
                    query = query.OrderBy(orderBy);
                }
               else {
                
                query = query.OrderByDescending(orderBy);
                }
            }
            return await query.ToListAsync();   
        }

        public T Add(T entity)
        {
            _context.Set<T>().Add(entity);
            return entity;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return entity;

        }

        public IEnumerable<T> AddRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
            return entities;
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
            return entities;
        }

        public T Update(T entity)
        {
            _context.Update(entity);
            return entity;
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entites)
        {
            _context.Set<T>().RemoveRange(entites);
        }

        public void Attach(T entity)
        {
            _context.Set<T>().Attach(entity);   
        }

        public void AttachRange(IEnumerable<T> entites)
        {
            _context.Set<T>().AttachRange(entites);
        }

        public int Count()
        {
            return _context.Set<T>().Count(); 
        }

        public int Count(Expression<Func<T,bool>> predicate)
        {
            return _context.Set<T>().Count(predicate);
        }

        public async Task<int> CountAsync()
        {
            return await _context.Set<T>().CountAsync();
        }

        public async Task<int> CountAsync(Expression<Func<T,bool>> predicate)
        {
            return await _context.Set<T>().CountAsync(predicate);
        }

        public IEnumerable<T?> FindAll(Expression<Func<T,bool>> predicate,int skip,int take)
        {
            return [.. _context.Set<T>().Where(predicate).Skip(skip).Take(take)];    

        }

        public async Task<IEnumerable<T?>> FindAllAsync(Expression<Func<T,bool>> predicate,int skip,int take)
        {
            return await _context.Set<T>().Where(predicate).Skip(skip).Take(take).ToListAsync();
        }

        public IEnumerable<T?> FindAll(Expression<Func<T, bool>> predicate, string? includes = null)
        {
            IQueryable<T> query = _context.Set<T>().Where(predicate);

            if(includes is not null)
            {
                query = query.Include(includes);
            }
            return [.. query];
        }

        public async Task<IEnumerable<T>?> FindAllAndDive(string  predicate, string diveInclude)
        {
            // Not implemented yet
            var result =  _context.Set<T>().Include(predicate);
            return await result.Include(diveInclude).ToListAsync(); 

        }
    }

}
