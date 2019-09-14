using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NTier.Business.Host;
using NTier.Business.Interface;
using NTier.Data.Interface.UnitOfWork;
using NTier.Data.Repository.Host.EntityFramework;
using NTier.Data.Repository.UnitOfWork;
using NTier.Entities.Model;

namespace NTier.Tests.BusinessTests
{
  [TestClass]
  public class ClientManagerTests
  {
    [TestMethod]
    public void Client_Class_Can_Not_Be_Null()
    {
      //Arrange
      var unitOfWork = new UnitOfWork(null);
      IClientManager clientManager = new ClientService(unitOfWork);
      Client clientForTest = null;


      //Action
      var result = clientManager.SaveClient(clientForTest);

      //Assert
      Assert.IsFalse(result.Status);
      Debug.WriteLine(result.Message);
    }

    [TestMethod]
    public void Client_Properties_Can_Not_Be_Null()
    {
      //Arrange
      var unitOfWork = new UnitOfWork(null);
      IClientManager clientManager = new ClientService(unitOfWork);
      Client clientForTest = new Client()
      {
        ClientId = 0,
        ClientName = "",
        ClientTitle = "",
      };


      //Action
      var result = clientManager.SaveClient(clientForTest);

      //Assert
      Assert.IsFalse(result.Status);
      Debug.WriteLine(result.Message);
    }


    [TestMethod]
    public void Client_Can_Add()
    {
      //Arrange
      List<Client> clientList = new List<Client>()
      {
        new Client() { ClientId = 1, ClientName = "ClientName1", ClientTitle = "ClientTitle1"},
        new Client() { ClientId = 2, ClientName = "ClientName2", ClientTitle = "ClientTitle2"},
        new Client() { ClientId = 3, ClientName = "ClientName3", ClientTitle = "ClientTitle3"},
      };

      var moqDbContext = new Mock<EfNTierContext>();
      var moqDbSet = new Mock<DbSet<Client>>();

      moqDbContext.Setup(x => x.Clients).Returns(() => moqDbSet.Object);

      var queryable = clientList.AsQueryable();
      moqDbSet.As<IQueryable<Client>>().Setup(m => m.Provider).Returns(queryable.Provider);
      moqDbSet.As<IQueryable<Client>>().Setup(m => m.Expression).Returns(queryable.Expression);
      moqDbSet.As<IQueryable<Client>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
      moqDbSet.As<IQueryable<Client>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator);



      moqDbSet.Setup(m => m.Add(It.IsAny<Client>()))
        .Returns(
          (Client client) =>
          {
            client.ClientId = clientList.Max(x => x.ClientId) + 1;
            client.CreatedDateTime = DateTime.Now;

            return client;
          });
      moqDbContext.Setup(x => x.Set<Client>()).Returns(moqDbSet.Object);

      IUnitOfWork unitOfWork = new UnitOfWork(moqDbContext.Object);
      IClientManager clientManager = new ClientService(unitOfWork);

      Client clientToAdd = new Client()
      {
        ClientId = 0,
        ClientName = "Kurtay",
        ClientTitle = "CodeApp Co.",
        CreatedDateTime = null,
      };

      //Action
      var result = clientManager.SaveClient(clientToAdd);

      //Assert
      Assert.IsTrue(result.Status);
      Assert.IsTrue(result.Message == "");
      Assert.IsTrue(((Client)result.Data).ClientId != 0);
      Assert.IsTrue(((Client)result.Data).ClientName == "Kurtay");
      Assert.IsTrue(((Client)result.Data).ClientTitle == "CodeApp Co.");
      Debug.WriteLine($"ClientId:{((Client)result.Data).ClientId}");

    }

  }
}
