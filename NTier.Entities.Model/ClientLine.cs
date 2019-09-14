using NTier.Entities.Enum;
using NTier.Entities.Infrastructure;

namespace NTier.Entities.Model
{
  public class ClientLine : BaseModel
  {
    public int ClientLineId { get; set; }
    public int ClientId { get; set; }
    public ClientLineType ClientLineType { get; set; }
  }
}
