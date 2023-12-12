
using WorkshopSolution.DataTransferObjects;

namespace WorkshopSolution.BusinessLogic
{
  public class RackManager(IServiceProvider serviceProvider) : Manager(serviceProvider)
  {
    public RackDetailDto GetRack(int id)
    {
      var model = RackRepo.GetRack(id);
      if (UserContext.User.IsInRole("Admin"))
      {
        // hole mehr Daten
      }
      return Mapper.Map<RackDetailDto>(model);
    }

    public IEnumerable<RackListDto> GetRacks()
    {
      var models = RackRepo.GetAllRacks();
      return Mapper.Map<IEnumerable<RackListDto>>(models);
    }
  }
}
