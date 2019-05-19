using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ExampleApp.DAL.Core
{
  //https://docs.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application#implement-a-generic-repository-and-a-unit-of-work-class
  public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity>
    where TEntity : class, IEntity
  {
    private readonly ExampleAppDbContext _dbContext;
    private readonly DbSet<TEntity> _dbSet;

    public RepositoryBase(ExampleAppDbContext dbContext)
    {
      _dbContext = dbContext;
      _dbSet = _dbContext.Set<TEntity>();
    }

    public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
                                            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                            string includeProperties = "")
    {
      return GetAsync(filter, orderBy, includeProperties).Result;
    }

    public virtual TEntity GetById(object id, string includeProperties = "") => GetByIdAsync(id, includeProperties).Result;

    public virtual void Insert(TEntity entity) => _dbSet.Add(entity);

    public virtual void Delete(object id)
    {
      TEntity entityToDelete = _dbSet.Find(id);
      Delete(entityToDelete);
    }

    public virtual void Delete(TEntity entityToDelete)
    {
      if (_dbContext.Entry(entityToDelete).State == EntityState.Detached)
      {
        _dbSet.Attach(entityToDelete);
      }
      _dbSet.Remove(entityToDelete);
    }

    public virtual void Update(TEntity entityToUpdate)
    {
      _dbSet.Attach(entityToUpdate);
      _dbContext.Entry(entityToUpdate).State = EntityState.Modified;
    }

    public async Task<TEntity> GetByIdAsync(object id, string includeProperties = "")
    {
      IQueryable<TEntity> query = _dbSet;

      foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        query = query.Include(includeProperty);

      return await query.SingleOrDefaultAsync(s => s.Id.Equals(id));
    }

    public virtual async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null,
                                                              Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                              string includeProperties = "")
    {
      IQueryable<TEntity> query = _dbSet;

      if (filter != null)
        query = query.Where(filter);

      foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        query = query.Include(includeProperty);

      if (orderBy != null)
        return await orderBy(query).ToListAsync();
      else
        return await query.ToListAsync();
    }
  }
}
