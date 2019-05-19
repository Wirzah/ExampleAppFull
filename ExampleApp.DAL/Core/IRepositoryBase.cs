using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ExampleApp.DAL.Core
{
  public interface IRepositoryBase<TEntity>
    where TEntity : class, IEntity
  {
    TEntity GetById(object id, string includeProperties = "");
    IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
    void Insert(TEntity entity);
    void Update(TEntity entityToUpdate);
    void Delete(object id);
    void Delete(TEntity entityToDelete);

    Task<TEntity> GetByIdAsync(object id, string includedProperties = "");
    Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");

  }
}