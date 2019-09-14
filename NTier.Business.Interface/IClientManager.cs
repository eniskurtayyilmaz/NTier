using System;
using System.Linq;
using System.Linq.Expressions;
using NTier.Entities.Model;
using NTier.Entities.ViewModel.Operation;

namespace NTier.Business.Interface
{
  public interface IClientManager
  {
    OperationResponse<Client> GetClientByFilter(Expression<Func<Client, bool>> filter = null,
      Func<IQueryable<Client>, IOrderedQueryable<Client>> orderBy = null,
      string includedProperties = "");
    OperationResponse<Client> GetClientById(int id);
    OperationResponse<Client> SaveClient(Client clientEntity);
    OperationResponse<Client> DeleteClientById(int id);
  }
}