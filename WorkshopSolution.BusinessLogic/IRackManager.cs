using WorkshopSolution.DataTransferObjects;

namespace WorkshopSolution.BusinessLogic
{
  public interface IRackManager
  {
    RackDetailDto GetRack(int id);
    IEnumerable<RackListDto> GetRackByParams(int minheight, int maxheight, int minwidth, int maxwidth);
    IEnumerable<RackListDto> GetRacks();
  }
}