using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NTier.Data.Interface.UnitOfWork;
using NTier.Data.Repository.Host.EntityFramework;
using NTier.Data.Repository.UnitOfWork;
using NTier.Entities.Model;

namespace NTier.Tests.DataTests
{
  [TestClass]
  public class UnitOfWorkTests
  {
    [TestMethod]
    public void Client_Dal_Check_From_UnitOfWork_For_ClientId_2_Valid()
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

      moqDbSet.Setup(m => m.Find(It.IsAny<object[]>()))
        .Returns<object[]>(ids => clientList.FirstOrDefault(d => d.ClientId == (int) ids[0]));
      moqDbContext.Setup(x => x.Set<Client>()).Returns(moqDbSet.Object);

      IUnitOfWork unitOfWork = new UnitOfWork(moqDbContext.Object);


      //Action
      var result = unitOfWork.ClientRepository.GetById(2);

      //Assert
      Assert.IsNotNull(result);
      Assert.AreEqual(2, result.ClientId);
      Assert.AreEqual("ClientName2", result.ClientName);
      Assert.AreEqual("ClientTitle2", result.ClientTitle);
    }

    [TestMethod]
    public void Client_Dal_Check_From_UnitOfWork_For_ClientId_2_NonValid()
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

      moqDbSet.Setup(m => m.Find(It.IsAny<object[]>()))
        .Returns<object[]>(ids => clientList.FirstOrDefault(d => d.ClientId == (int)ids[0]));
      moqDbContext.Setup(x => x.Set<Client>()).Returns(moqDbSet.Object);

      IUnitOfWork unitOfWork = new UnitOfWork(moqDbContext.Object);


      //Action
      var result = unitOfWork.ClientRepository.GetById(1);

      //Assert
      Assert.IsNotNull(result);
      Assert.AreNotEqual(2, result.ClientId);
      Assert.AreNotEqual("ClientName2", result.ClientName);
      Assert.AreNotEqual("ClientTitle2", result.ClientTitle);
    }
  }
}
