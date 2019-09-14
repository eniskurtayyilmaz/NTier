using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTier.Entities.ViewModel.Operation
{
  public class OperationResponse<T> where T : class, new()
  {
    public OperationResponse()
    {
      Status = false;
      Message = "";
    }
    public bool Status { get; set; }
    public string Message { get; set; }
    public object Data { get; set; }
  }
}
