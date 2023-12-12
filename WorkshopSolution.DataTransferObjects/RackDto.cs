using System.ComponentModel.DataAnnotations;

namespace WorkshopSolution.DataTransferObjects
{
  public class RackListDto
  {
    public int Id { get; set; }

    public string Name { get; set; }
    
  }

  public class RackDetailDto
  {
    public int Id { get; set; }

    [StringLength(100)]
    public string Name { get; set; }

    [Range(1, 1000)]
    public int Height { get; set; }

    [Range(1, 1000)] 
    public int Width { get; set; }
  }
}
