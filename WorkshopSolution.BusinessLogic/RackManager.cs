
using WorkshopSolution.DataTransferObjects;

namespace WorkshopSolution.BusinessLogic
{
  public class RackManager(IServiceProvider serviceProvider) : Manager(serviceProvider), IRackManager
  {
    public RackDetailDto GetRack(int id)
    {
      var model = RackRepo.GetRack(id);
      if (UserContext.User.IsInRole("Admin"))
      {
        // hole mehr Daten
      }
      // Test case!
      // model.Name = $"R{model.Name}";
      return Mapper.Map<RackDetailDto>(model);
    }

    public IEnumerable<RackListDto> GetRacks()
    {
      var models = RackRepo.GetAllRacks();
      return Mapper.Map<IEnumerable<RackListDto>>(models);
    }

    public IEnumerable<RackListDto> GetRackByParams(int minheight, int maxheight, int minwidth, int maxwidth)
    {
      var models = RackRepo.QueryRackByName(r => r.Height >= minheight && r.Height <= maxheight && r.Width >= minwidth && r.Width <= maxwidth);
      return Mapper.Map<IEnumerable<RackListDto>>(models);
    }

  }
}
