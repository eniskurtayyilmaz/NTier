using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NTier.Business.Host;
using NTier.Business.Interface;
using NTier.Data.Repository.Host.EntityFramework;
using NTier.Data.Repository.UnitOfWork;
using NTier.Entities.Model;

namespace NTier.Presentation.Console
{
  class Program
  {
    static void Main(string[] args)
    {
      //ConnectionString
      string connectionString =
        "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=NTIER;Data Source=.";

      //EfNTierContext
      var efNTierContext = new EfNTierContext(connectionString);

      //unitOfWork
      var uow = new UnitOfWork(efNTierContext);

      //IClientManager
      IClientManager clientManager = new ClientService(uow);

      var responseClient = clientManager.GetClientById(1);

      System.Console.WriteLine(responseClient.Status);
      System.Console.WriteLine(responseClient.Message);
      System.Console.WriteLine(responseClient.Data);


      System.Console.WriteLine(((Client)responseClient.Data).ClientName);

      System.Console.ReadKey();


    }
  }
}
