using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using NTier.Data.Interface.DataAccess;
using NTier.Data.Interface.UnitOfWork;
using NTier.Data.Repository.Host.EntityFramework;
using NTier.Data.Repository.Host.EntityFramework.DataAccess;

namespace NTier.Data.Repository.UnitOfWork
{
  public class UnitOfWork : IUnitOfWork
  {
    private readonly EfNTierContext _context;

    private IClientDal _clientDal;
    private IClientLineDal _clientLineDal;

    public UnitOfWork(EfNTierContext context)
    {
      _context = context;
    }

    public IClientDal ClientRepository
    {
      get
      {
        if (_clientDal == null)
        {
          _clientDal = new EfClientDal(_context);
        }

        return _clientDal;
      }
    }

    public IClientLineDal ClientLineDalRepository
    {
      get
      {
        if (_clientLineDal == null)
        {
          _clientLineDal = new EfClientLineDal(_context);
        }

        return _clientLineDal;
      }
    }


    public bool SaveAllChanges()
    {
      try
      {
        using (TransactionScope tScope = new TransactionScope())
        {
          this._context.SaveChanges();
          tScope.Complete();
          return true;
        }
      }
      catch (Exception e)
      {
        Console.WriteLine(e); //Logger Helpers tarafındaı
        throw;
      }
    }

    private bool disposed = false;

    protected virtual void Dispose(bool disposing)
    {
      if (!this.disposed)
      {
        if (disposing)
        {
          _context.Dispose();
        }
      }
      this.disposed = true;
    }

    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

  }
}
