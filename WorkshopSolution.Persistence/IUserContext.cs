using System.Security.Claims;

namespace WorkshopSolution.Persistence
{
  public interface IUserContext
  {
    ClaimsPrincipal User { get; set; }
  }

  public class UserContext : IUserContext
  {
    public ClaimsPrincipal User { get; set; }
  }
}
