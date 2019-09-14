using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using NTier.Entities.Model;

namespace NTier.Entities.Validation
{
  public class ClientValidation : AbstractValidator<Client>
  {
    public ClientValidation()
    {
      RuleFor(x => x.ClientName).NotNull().NotEmpty().MinimumLength(1);
      RuleFor(x=> x.ClientTitle).NotNull().NotEmpty().MinimumLength(1);
    }
  }
}
