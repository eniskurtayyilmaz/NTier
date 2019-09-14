using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using NTier.Data.Interface.GenericRepository;
using NTier.Data.Repository.Host.EntityFramework;

namespace NTier.Data.Repository.Infrastructure.EntityFramework
{
  public class EfEntityRepositoryBase<TEntity> : IGenericRepository<TEntity> where TEntity : class, new()
  {
    private readonly EfNTierContext _context;
    private readonly DbSet<TEntity> _dbSet;
    public EfEntityRepositoryBase(EfNTierContext context)
    {
      _context = context;
      _dbSet = _context.Set<TEntity>();
    }


    public void Delete(object id)
    {
      TEntity entityToDelete = _dbSet.Find(id);
      Delete(entityToDelete);
    }

    public void Delete(TEntity entityToDelete)
    {
      if (_context.Entry(entityToDelete).State == EntityState.Detached)
      {
        _dbSet.Attach(entityToDelete);
      }
      _dbSet.Remove(entityToDelete);
    }

    public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includedProperties = "")
    {
      IQueryable<TEntity> query = _dbSet;

      if (filter != null)
      {
        query = query.Where(filter);
      }

      foreach (var includedProperty in includedProperties.Split( new char[] {','}, StringSplitOptions.RemoveEmptyEntries ))
      {
        query = query.Include(includedProperty);
      }

      if (orderBy != null)
      {
        return orderBy(query).ToList();
      }
      else
      {
        return query.ToList();
      }
    }

    public TEntity GeyById(object id)
    {
      return _dbSet.Find(id);
    }

    public void Insert(TEntity entityToInsert)
    {
      _dbSet.Add(entityToInsert);
    }

    public void Update(TEntity entityToUpdate)
    {
      _dbSet.Attach(entityToUpdate);
      _context.Entry(entityToUpdate).State = EntityState.Modified;
    }
  }
}
