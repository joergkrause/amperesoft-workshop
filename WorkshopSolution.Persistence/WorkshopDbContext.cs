using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopSolution.DomainModels;

namespace WorkshopSolution.Persistence;

public class WorkshopDbContext : DbContext
{

  public WorkshopDbContext(DbContextOptions options) : base(options)
  {

  }

  public DbSet<Rack> Racks { get; set; }  

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Rack>().ToCollection("Racks");
  }

}
