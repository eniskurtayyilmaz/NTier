using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NTier.Entities.Model;

namespace NTier.Data.Repository.Host.EntityFramework
{
  public class EfNTierContext : DbContext
  {
    public EfNTierContext()
    {

    }

    public EfNTierContext(string nameOrConnectionString) : base(nameOrConnectionString)
    {
      Database.SetInitializer<EfNTierContext>(null);
    }

    public virtual DbSet<Client> Clients { get; set; }
    public virtual DbSet<ClientLine> ClientLines { get; set; }
  }
}
