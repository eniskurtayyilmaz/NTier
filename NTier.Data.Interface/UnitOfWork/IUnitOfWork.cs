using System;
using NTier.Data.Interface.DataAccess;

namespace NTier.Data.Interface.UnitOfWork
{
  public interface IUnitOfWork : IDisposable
  {
    IClientDal ClientRepository { get; }
    IClientLineDal ClientLineDalRepository { get; }
    bool SaveAllChanges();
  }
}