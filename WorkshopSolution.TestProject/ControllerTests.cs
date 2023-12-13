using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WorkshopSolution.BackendServiceCtrl.Controllers;
using WorkshopSolution.BusinessLogic;
using WorkshopSolution.DataTransferObjects;

namespace WorkshopSolution.TestProject
{
  [TestClass]
  public class ControllerTests
  {

    [TestInitialize]
    public void Init() { }


    [TestMethod]
    public void RackController_Get_Ok() {       
      // Arrange
      var mockRackManager = new Mock<IRackManager>();

      var sut = new RackController(mockRackManager.Object);

      var mockRack = new RackDetailDto() { Id = 1, Name = "Demo Rack 1 CB", Height = 1, Width = 12 };
      mockRackManager.Setup(x => x.GetRack(It.IsAny<int>())).Returns(mockRack);

      // Act
      var result = sut.Get(1);
      // Assert
      Assert.IsNotNull(result);
      Assert.AreEqual(typeof(OkObjectResult), result.GetType());
      
    }

    [TestMethod]
    public void RackController_Get_NotFound()
    {
      // Arrange
      var mockRackManager = new Mock<IRackManager>();

      var sut = new RackController(mockRackManager.Object);

      var mockRack = new RackDetailDto() { Id = 1, Name = "Demo Rack 1 CB", Height = 1, Width = 12 };
      mockRackManager.Setup(x => x.GetRack(1)).Returns(mockRack);

      // Act
      var result = sut.Get(2);
      // Assert
      Assert.IsNotNull(result);
      Assert.AreEqual(typeof(NotFoundResult), result.GetType());

    }

    [TestMethod]
    public void RackController_Get_HasSwaggerAttributes()
    {
      // Arrange
      var mockRackManager = new Mock<IRackManager>();

      var sut = new RackController(mockRackManager.Object);

      // Act
      var attrs = sut.GetType().GetMethod("Get", [typeof(int)]).GetCustomAttributes<ProducesResponseTypeAttribute>();

      // Assert
      Assert.IsNotNull(attrs);
      Assert.AreEqual(2, attrs.Count());

    }

  }

}
