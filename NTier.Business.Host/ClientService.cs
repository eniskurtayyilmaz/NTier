using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using NTier.Business.Interface;
using NTier.Data.Interface.DataAccess;
using NTier.Entities.Model;
using NTier.Entities.ViewModel.Operation;

namespace NTier.Business.Host
{
  public class ClientService : IClientManager
  {


    public OperationResponse<Client> GetClientByFilter(Expression<Func<Client, bool>> filter = null, Func<IQueryable<Client>, IOrderedQueryable<Client>> orderBy = null, string includedProperties = "")
    {
      throw new NotImplementedException();
    }

    public OperationResponse<Client> GetClientById(int id)
    {
      throw new NotImplementedException();
    }

    public OperationResponse<Client> SaveClient(Client clientEntity)
    {
      throw new NotImplementedException();
    }

    public OperationResponse<Client> DeleteClientById(int id)
    {
      throw new NotImplementedException();
    }
  }
}
