using NTier.Data.Interface.DataAccess;
using NTier.Data.Repository.Infrastructure.EntityFramework;
using NTier.Entities.Model;

namespace NTier.Data.Repository.Host.EntityFramework.DataAccess
{
  public class EfClientDal : EfEntityRepositoryBase<Client>, IClientDal
  {
    public EfClientDal(EfNTierContext context) : base(context)
    {

    }
  }
}