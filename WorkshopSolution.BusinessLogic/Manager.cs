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
      RackRepo = serviceProvider.GetRequiredService<IRackRepository>();
      Mapper = serviceProvider.GetRequiredService<IMapper>();
      UserContext = serviceProvider.GetRequiredService<IUserContext>();
    }

    protected IMapper Mapper { get; }

    protected IRackRepository RackRepo { get; }

    protected IUserContext UserContext { get; }
  }
}