using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using WorkshopSolution.Persistence;
using WorkshopSolution.Repositories;

namespace WorkshopSolution.BusinessLogic
{
  public abstract class Manager
  {
    public Manager(IServiceProvider serviceProvider)
    {
      RackRepo = serviceProvider.GetRequiredService<RackRepository>();
      Mapper = serviceProvider.GetRequiredService<IMapper>();
      UserContext = serviceProvider.GetRequiredService<IUserContext>();
    }

    protected IMapper Mapper { get; }

    protected RackRepository RackRepo { get; }

    protected IUserContext UserContext { get; }
  }
}