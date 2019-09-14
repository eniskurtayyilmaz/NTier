using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTier.Entities.Infrastructure
{
    public class BaseModel
    {
      public Nullable<DateTime> CreatedDateTime { get; set; }
      public Nullable<DateTime> UpdatedDateTime { get; set; }
    }
}
