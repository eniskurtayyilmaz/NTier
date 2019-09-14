using System.Collections.Generic;
using NTier.Data.Interface.GenericRepository;
using NTier.Entities.Enum;
using NTier.Entities.Model;

namespace NTier.Data.Interface.DataAccess
{
  public interface IClientLineDal : IGenericRepository<ClientLine>
  {
    IEnumerable<ClientLine> GetClientLinesByClientLineType(ClientLineType clientLineType);
  }
}