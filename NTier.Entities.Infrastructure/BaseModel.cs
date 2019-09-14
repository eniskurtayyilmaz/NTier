using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTier.Entities.Infrastructure
{
    public class BaseModel
    {
      public DateTime CreateDateTime { get; set; }
      public DateTime UpdatedDateTime { get; set; }
    }
}
