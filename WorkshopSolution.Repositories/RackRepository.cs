using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Primitives;
using System;
using System.Linq.Expressions;
using System.Security.Claims;
using WorkshopSolution.DomainModels;
using WorkshopSolution.Persistence;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WorkshopSolution.Repositories
{
  public class RackRepository : IRackRepository
  {
    private readonly IUserContext _userContext;
    private readonly WorkshopDbContext _workshopDbContext;

    public RackRepository(IUserContext userContext, WorkshopDbContext workshopDbContext)
    {
      _workshopDbContext = workshopDbContext;
      _userContext = userContext;
      try
      {
        var racks = _workshopDbContext.Racks.ToList();
      }
      catch (CosmosException)
      {
        _workshopDbContext.Database.EnsureCreated();
        var racks = CreateDemoRacks();
        _workshopDbContext.Racks.AddRange(racks);
        _workshopDbContext.SaveChanges();
      }
    }

    private List<Rack> CreateDemoRacks()
    {
      return [
          new Rack() { Id = 1, Name = "Demo Rack 1 CB", Height = 1, Width = 12 },
        new Rack() { Id = 2, Name = "Demo Rack 2 CB", Height = 2, Width = 12 },
        new Rack() { Id = 3, Name = "Demo Rack 3 CB", Height = 3, Width = 12 },
      ];
    }

    // v1
    public IEnumerable<Rack> GetRackByName(string name)
    {
      var racks = _workshopDbContext.Racks.Where(r => r.Name == name).ToList();
      if (racks.IsValid())
      {
        return racks;
      }
      else
      {
        throw new InvalidOperationException(); // TODO: Add exception handling
      }
    }

    public IQueryable<Rack> QueryRackByName(string name)
    {
      var racks = _workshopDbContext.Racks.Where(r => r.Name == name).AsQueryable();
      return racks;
    }

    public IEnumerable<Rack> QueryRackByName(Expression<Func<Rack, bool>> expression)
    {
      var racks = _workshopDbContext.Racks.Where(expression).AsQueryable();

      return racks;
    }

    public IEnumerable<Rack> GetAllRacks()
    {
      var racks = _workshopDbContext.Racks.ToList();
      if (racks.IsValid())
      {
        return racks;
      }
      else
      {
        throw new InvalidOperationException(); // TODO: Add exception handling
      }
    }

    public Rack? GetRack(int id)
    {
      var model = _workshopDbContext.Racks.SingleOrDefault(r => r.Id == id);
      if (model.IsValid())
      {
        return model;
      }
      else
      {
        throw new InvalidOperationException(); // TODO: Add exception handling
      }

    }

    public IEnumerable<Claim> GetUserClaims(StringValues userName)
    {
      return Enumerable.Empty<Claim>();
    }
  }
}
