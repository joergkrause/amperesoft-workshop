using Microsoft.Extensions.Primitives;
using System.Linq.Expressions;
using System.Security.Claims;
using WorkshopSolution.DomainModels;

namespace WorkshopSolution.Repositories
{
  public interface IRackRepository
  {
    IEnumerable<Rack> GetAllRacks();
    Rack? GetRack(int id);
    IEnumerable<Rack> GetRackByName(string name);
    IEnumerable<Claim> GetUserClaims(StringValues userName);
    IEnumerable<Rack> QueryRackByName(Expression<Func<Rack, bool>> expression);
    IQueryable<Rack> QueryRackByName(string name);
  }
}