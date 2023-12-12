using Microsoft.Azure.Cosmos;
using System;
using WorkshopSolution.DomainModels;
using WorkshopSolution.Persistence;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WorkshopSolution.Repositories
{
  public class RackRepository
  {
    private List<Rack> _racks = new List<Rack>();
    private readonly IUserContext _userContext;
    private readonly WorkshopDbContext _workshopDbContext;

    public RackRepository(IUserContext userContext, WorkshopDbContext workshopDbContext)
    {      
      _userContext = userContext;
      _workshopDbContext = workshopDbContext;
      try
      {
        _racks = _workshopDbContext.Set<Rack>().ToList();
      }
      catch (CosmosException)
      {
        _workshopDbContext.Database.EnsureCreated();
      }
      if (!_racks.Any())
      {
        var demoRacks = new List<Rack>()
        {
          new() { Id = 1, Name = "Demo Rack 1", Height = 1, Width = 12 },
          new() { Id = 2, Name = "Demo Rack 2", Height = 2, Width = 12 },
          new() { Id = 3, Name = "Demo Rack 3", Height = 3, Width = 12 },
        };
        _workshopDbContext.AddRange(demoRacks);
        _workshopDbContext.SaveChanges();
      }
    }

    public IEnumerable<Rack> GetAllRacks()
    {
      if (_racks.IsValid())
      {
        return _racks;
      }
      else
      {
        throw new InvalidOperationException(); // TODO: Add exception handling
      }
    }

    public Rack? GetRack(int id)
    {
      var model = _racks.FirstOrDefault(r => r.Id == id);
      if (model.IsValid())
      {
        return model;
      }
      else
      {
        throw new InvalidOperationException(); // TODO: Add exception handling
      }

    }
  }
}
