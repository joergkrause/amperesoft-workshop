using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Security.Claims;
using WorkshopSolution.BusinessLogic;
using WorkshopSolution.BusinessLogic.Mappings;
using WorkshopSolution.DataTransferObjects;
using WorkshopSolution.DomainModels;
using WorkshopSolution.Persistence;
using WorkshopSolution.Repositories;

namespace WorkshopSolution.TestProject
{
  [TestClass]
  public class BusinessLayerTests
  {




    [TestMethod]
    public void GetRack_Success()
    {
      // Arrange
      var mockRackRepo = new Mock<IRackRepository>();
      var mockMapper = new Mock<IMapper>();
      var mockUserContext = new Mock<IUserContext>();
      var mockServiceProvider = new Mock<IServiceProvider>();

      mockServiceProvider.Setup(x => x.GetService(typeof(IRackRepository))).Returns(mockRackRepo.Object);
      mockServiceProvider.Setup(x => x.GetService(typeof(IMapper))).Returns(mockMapper.Object);
      mockServiceProvider.Setup(x => x.GetService(typeof(IUserContext))).Returns(mockUserContext.Object);

      var sut = new RackManager(mockServiceProvider.Object);

      var mockRack = new Rack() { Id = 1, Name = "Demo Rack 1 CB", Height = 1, Width = 12 };
      mockRackRepo.Setup(x => x.GetRack(It.IsAny<int>())).Returns(mockRack);

      mockMapper.Setup(x => x.Map<RackDetailDto>(It.IsAny<Rack>())).Returns(new RackDetailDto() { Id = 1, Name = "Demo Rack 1 CB", Height = 1, Width = 12 });
      mockUserContext.Setup(x => x.User).Returns(new ClaimsPrincipal(new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.Role, "Admin") })));
      // Act
      var result = sut.GetRack(1);
      // Assert
      Assert.IsNotNull(result);
      Assert.AreEqual(1, result.Id);
      Assert.AreEqual("Demo Rack 1 CB", result.Name);
      Assert.AreEqual(1, result.Height);
      Assert.AreEqual(12, result.Width);
    }

    [TestMethod]
    public void GetRack_WithMapper_Success()
    {
      // Arrange
      var mockRackRepo = new Mock<IRackRepository>();
      var mockUserContext = new Mock<IUserContext>();
      var mockServiceProvider = new Mock<IServiceProvider>();

      var mapper = new MapperConfiguration(cfg =>
      {
        cfg.AddProfile(new MappingProfile());
      }).CreateMapper();

      mockServiceProvider.Setup(x => x.GetService(typeof(IRackRepository))).Returns(mockRackRepo.Object);
      mockServiceProvider.Setup(x => x.GetService(typeof(IMapper))).Returns(mapper);
      mockServiceProvider.Setup(x => x.GetService(typeof(IUserContext))).Returns(mockUserContext.Object);

      var sut = new RackManager(mockServiceProvider.Object);

      var mockRack = new Rack() { Id = 1, Name = "Demo Rack 1 CB", Height = 1, Width = 12 };
      mockRackRepo.Setup(x => x.GetRack(It.IsAny<int>())).Returns(mockRack);

      mockUserContext.Setup(x => x.User).Returns(new ClaimsPrincipal(new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.Role, "Admin") })));
      // Act
      var result = sut.GetRack(1);
      // Assert
      Assert.IsNotNull(result);
      Assert.AreEqual(1, result.Id);
      Assert.AreEqual("Demo Rack 1 CB", result.Name);
      Assert.AreEqual(1, result.Height);
      Assert.AreEqual(12, result.Width);
    }

    [TestMethod]
    public void GetRack_WithMapper_ServiceProvider_Success()
    {
      // Arrange
      var mockRackRepo = new Mock<IRackRepository>();
      var mockUserContext = new Mock<IUserContext>();
      var serviceCollection = new ServiceCollection();

      var mapper = new MapperConfiguration(cfg =>
      {
        cfg.AddProfile(new MappingProfile());
      }).CreateMapper();

      serviceCollection.AddSingleton<IMapper>(mapper);
      serviceCollection.AddSingleton<IRackRepository>(mockRackRepo.Object);
      serviceCollection.AddSingleton<IUserContext>(mockUserContext.Object);

      var serviceProvider = serviceCollection.BuildServiceProvider();

      var sut = new RackManager(serviceProvider);

      var mockRack = new Rack() { Id = 1, Name = "Demo Rack 1 CB", Height = 1, Width = 12 };
      mockRackRepo.Setup(x => x.GetRack(It.IsAny<int>())).Returns(mockRack);

      mockUserContext.Setup(x => x.User).Returns(new ClaimsPrincipal(new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.Role, "Admin") })));
      // Act
      var result = sut.GetRack(1);
      // Assert
      Assert.IsNotNull(result);
      Assert.AreEqual(1, result.Id);
      Assert.AreEqual("Demo Rack 1 CB", result.Name);
      Assert.AreEqual(1, result.Height);
      Assert.AreEqual(12, result.Width);
    }

  }
}