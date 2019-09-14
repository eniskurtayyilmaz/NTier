using System;
using System.Linq;
using System.Linq.Expressions;
using NTier.Entities.Enum;
using NTier.Entities.Model;
using NTier.Entities.ViewModel.Operation;

namespace NTier.Business.Interface
{
  public interface IClientLineManager
  {
    OperationResponse<ClientLine> GetClientLineByFilter(Expression<Func<ClientLine, bool>> filter = null,
      Func<IQueryable<ClientLine>, IOrderedQueryable<ClientLine>> orderBy = null,
      string includedProperties = "");
    OperationResponse<ClientLine> GetClientLineByClientLineType(ClientLineType clientLineType);
    OperationResponse<ClientLine> GetClientLineById(int id);
    OperationResponse<ClientLine> SaveClientLine(ClientLine clientLineEntity);
    OperationResponse<ClientLine> DeleteClientLineById(int id);
  }
}