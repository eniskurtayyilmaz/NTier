using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NTier.Entities.Model;
using NTier.Entities.Validation;

namespace NTier.Tests.EntitiesTests
{
  [TestClass]
  public class ValidatorTests
  {
    [TestMethod]
    public void Can_Pass_Validation_For_Client()
    {
      //Arrange
      Client newClient = new Client()
      {
        ClientName = "Kurtay",
        ClientTitle = "CodeApp Co."
      };

      //Action
      var result = new ClientValidation().Validate(newClient);

      //Assert
      Assert.IsTrue(result.IsValid);
      Assert.IsTrue(result.Errors.Count <= 0);

    }

    [TestMethod]
    public void Can_Not_Pass_Validation_For_Client()
    {
      //Arrange
      Client newClient = new Client()
      {
        ClientName = "",
        ClientTitle = ""
      };

      //Action
      var result = new ClientValidation().Validate(newClient);

      //Assert
      Assert.IsFalse(result.IsValid);
      Assert.IsTrue(result.Errors.Count > 0);

      foreach (var error in result.Errors)
      {
        Debug.WriteLine(error.ErrorMessage);
      }
    }
  }
}
