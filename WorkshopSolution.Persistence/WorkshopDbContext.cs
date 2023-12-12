using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopSolution.DomainModels;

namespace WorkshopSolution.Persistence
{
  public class WorkshopDbContext : DbContext
  {
    public WorkshopDbContext(DbContextOptions options)
        : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
      modelBuilder.Entity<Rack>().ToCollection("RacksCollection");
    }
  }
}
