﻿using System;
using WorkshopSolution.DomainModels;
using WorkshopSolution.Persistence;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WorkshopSolution.Repositories
{
  public class RackRepository
  {
    private List<Rack> _racks = new List<Rack>();

    public RackRepository(IUserContext userContext)
    {
      _racks = new List<Rack>
      {
        new Rack() { Id = 1, Name = "Demo Rack 1", Height = 1, Width = 12 },
        new Rack() { Id = 2, Name = "Demo Rack 2", Height = 2, Width = 12 },
        new Rack() { Id = 3, Name = "Demo Rack 3", Height = 3, Width = 12 },
      };
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