using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NTier.Entities.Infrastructure;

namespace NTier.Entities.Model
{
  public class Client : BaseModel
  {
    public int ClientId { get; set; }
    public string ClientName { get; set; }
    public string ClientTitle { get; set; }

  }
}
